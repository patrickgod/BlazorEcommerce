using AutoMapper;

using BlazorEcommerce.Client.Shared;
using BlazorEcommerce.Server.Data;
using BlazorEcommerce.Server.Services.PersonService;
using BlazorEcommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Identity;
using System.Text;
using static BlazorEcommerce.Server.Services.FinanceService.FinanceService;


namespace BlazorEcommerce.Server.Services.FinanceService
{

    public static class FinanceOperation{
        public enum Operation
        {
            Add,
            Update
        }
    }

    public class FinanceService : IFinanceService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly Lazy<IPersonService> _personService;

        private readonly static int _defaultAddYearsForFinanceDueDate = 2;
        private readonly static int _defaultStartingDayForFinanceDueDate = 15;

 

        public FinanceService(DataContext context, IHttpContextAccessor httpContextAccessor,IMapper mapper, Lazy<IPersonService> personService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _personService = personService;

        }

         public async Task<ServiceResponse<FinanceDto>> CreateFinance(FinanceDto Finance)
        {
            //To do Add mapper
            _context.Finance.Add(_mapper.Map<Finance>(Finance));
            await _context.SaveChangesAsync();
            return new ServiceResponse<FinanceDto> { Data = Finance };

            //throw new NotImplementedException();

        }

        public async Task<ServiceResponse<FinanceDto>> UpdateFinance(FinanceDto Finance, int selectedMonth)
        {
            if (!Finance.Ispaid)
            {
                Finance.Paymentdate = null;
            }
            var dbProduct = await _context.Finance.AsNoTracking().FirstOrDefaultAsync(p => p.Financeid == Finance.Financeid);

            if (dbProduct == null)
            {
                return new ServiceResponse<FinanceDto>
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }

            var updatedEntity = _mapper.Map<Finance>(Finance);
//            updatedEntity.Class = null;

            try
            {  
                if(_context.Entry(updatedEntity).State == EntityState.Detached){
                    _context.Attach(updatedEntity);
                    _context.Entry(updatedEntity).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();

                //Update all due date of person
                //await _context.Finance.Where(p => p.Personid == Finance.Personid).ForEachAsync(t=>t.Duedateday = Finance.Duedateday);
                await AddFinanceDateForNewPerson(FinanceOperation.Operation.Update, Finance.Personid, Finance.Financedate.Month,Finance.Duedateday);
                //await _context.SaveChangesAsync();

            }
            catch (Exception exp)
            {

                throw new Exception(exp.Message);
            }
          
            return new ServiceResponse<FinanceDto> { Data = Finance };
        }

        public async Task DeleteByPersonId(Guid personId)
        {
            await _context.Finance.Where(s => s.Personid == personId).ForEachAsync(t => t.Deleted = true);
            await _context.SaveChangesAsync();
        }

        public async Task<ServiceResponse<bool>> DeleteFinance(Guid FinanceID)
        {
            var dbProduct = await _context.Finance.FindAsync(FinanceID);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }

            dbProduct.Deleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<FinanceDto>> GetFinanceAsync(int FinanceId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<FinanceDto>>> GetFinanceListAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResponse<List<FinanceDto>>> GetFinanceListAsync(int year,int month)
        {
            //Daterange;
            //https://blazor.syncfusion.com/documentation/datepicker/date-range;

            try
            {
                List<FinanceDto> preFinanceList = new List<FinanceDto>();
                var sb = new StringBuilder();

                //Get finance records
                var financeList = await _context.Finance.AsNoTracking().Where(s => !s.Deleted && s.Financedate.Year == year && s.Financedate.Month == month).ToListAsync();

                //get person ID's
                var personList = await _personService.Value.GetPersonListAsync();
                //var personIdList = await _personService.Value.GetPersonIdListAsync();

                //All person list
                foreach (var person in personList.Data)
                {
                    var checkUnpaymentDetails = await _context.Finance.AsNoTracking() 
                                                                        .Where(s => !s.Deleted
                                                                                   && s.Personid == person.Personid 
                                                                                   && s.Financedate.Year <= DateTime.Now.Year 
                                                                                   && ((s.Financedate.Month == DateTime.Now.Month && s.Duedateday < DateTime.Now.Day ) || (s.Financedate.Month < DateTime.Now.Month))
                                                                                   && s.Ispaid == false)
                                                                        .Select(t=>t.Financedate)
                                                                       .OrderByDescending(t => t).ToListAsync();

                    var hasDoublePaymentInMonth = financeList.Where(s => s.Personid == person.Personid
                                                                        && s.Duedateday2 != null).FirstOrDefault();
                    var hasFinancyEntity = financeList.Where(s => s.Personid == person.Personid).FirstOrDefault();



                    //List of previous unpayment details for this peerson
                   
                    foreach (var unpaymentDetail in checkUnpaymentDetails)
                    {
                        
                        var date = unpaymentDetail;
                        sb.Append("(");
                        sb.Append(date.Month.ToString()+".");
                        sb.Append(") ");
                    }

                    if (checkUnpaymentDetails.Any())
                    {
                        sb.Append("ay ödemesi yapılmamış!");
                    }

                    if (hasDoublePaymentInMonth != null)
                    {
                        if (checkUnpaymentDetails.Any())
                            sb.Append(Environment.NewLine);
                        sb.Append($" Bu ay 2 ödemesi bulunmaktadır => {hasDoublePaymentInMonth.Duedateday} ve {hasDoublePaymentInMonth.Duedateday2}");
                    }

                    if (hasFinancyEntity != null)
                    {
                        FinanceDto dto = _mapper.Map<FinanceDto>(hasFinancyEntity);
                        dto.FullName = person.FullName;
                        dto.Note = sb.ToString();
                        dto.NotPaidPreviousmonth = checkUnpaymentDetails.Any() ? true : false;
                        preFinanceList.Add(dto);
                    }

                    else
                    {

                        FinanceDto dto = new FinanceDto();
                        dto.Financeid = new Guid();
                        dto.Personid = person.Personid;
                        dto.FullName = person.FullName;
                        dto.Duedateday = null;
                        dto.Ispaid = false;
                        dto.Paymentdate = null;
                        dto.Note = null;
                        preFinanceList.Add(dto);
                    }
                    sb.Clear();
                }

                return new ServiceResponse<List<FinanceDto>> { Data = preFinanceList };

            }
            catch (Exception exp)
            {

                throw new Exception(exp.Message);
            }

          
        }

        public async Task<ServiceResponse<FinanceDto>> GetFinanceListByName(string searchText)
        {
            throw new NotImplementedException();
        }

        
        public async Task AddFinanceDateForNewPerson(FinanceOperation.Operation operation ,Guid personId,int? startMonth = null, int? startDay = null)
        {
            if (startMonth == null) {
                startMonth = DateTime.Now.Month;
            }

            if (startDay == null)
            {
                startDay = _defaultStartingDayForFinanceDueDate;
            }

            FinanceDto financeDto = new FinanceDto();
            Finance financeEntitity = null;

            var dueDateTime = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime lastDueDate = new DateTime();
            for (int y = DateTime.Now.Year; y <= DateTime.Now.Year + _defaultAddYearsForFinanceDueDate; y++)
            {
                int i = 1;
                if (y == DateTime.Now.Year)
                    dueDateTime = new DateTime(y, (y == DateTime.Now.Year ? startMonth.Value : 1), startDay.Value);
                else
                    dueDateTime = lastDueDate.AddDays(28);


                for (int m = (y == DateTime.Now.Year ? startMonth.Value : 1); m <= 12; m++)
                {
                    
                    //Skip first
                    if (i > 1)
                    {
                            dueDateTime = lastDueDate.AddDays(28);
                       

                    }

                    if(operation == FinanceOperation.Operation.Add)
                    {
                        financeDto.Personid = personId;
                        financeDto.Financedate = new DateTime(y, m, 1);
                        financeDto.Duedateday = null;
                        financeDto.Duedateday2 = null;
                        financeDto.Ispaid = ((m < DateTime.Now.Month && y < DateTime.Now.Year + 1) ? true : false);
                        _context.Finance.Add(_mapper.Map<Finance>(financeDto));
                        
                    }


                    if (operation == FinanceOperation.Operation.Update)
                    {
                        var dbFinance = await _context.Finance.FirstOrDefaultAsync(p => p.Personid == personId && p.Financedate.Year == y && p.Financedate.Month == m);
                        if (dbFinance != null)
                        {
                            dbFinance.Duedateday = dueDateTime.Day;
                            var compare = new DateTime(dbFinance.Financedate.Year, dbFinance.Financedate.Month, dbFinance.Duedateday.Value).AddDays(28);

                            var doublePaymentInMonth = compare.Month == dbFinance.Financedate.Month && compare.Year == dbFinance.Financedate.Year;
                            dbFinance.Duedateday2 = doublePaymentInMonth ? compare.Day : null;
                            lastDueDate = dbFinance.Duedateday2.HasValue ? new DateTime(dbFinance.Financedate.Year, dbFinance.Financedate.Month, dbFinance.Duedateday2.Value) : dueDateTime;
                        }
                    }
                    await _context.SaveChangesAsync();

                    i++;
                }
            }
        }


        public async Task FillData()
        {
            FinanceDto financeDto = new FinanceDto();
            var personList = await _personService.Value.GetPersonListAsync();

            foreach (var person in personList?.Data)
            {
                await AddFinanceDateForNewPerson(FinanceOperation.Operation.Add, person.Personid,1,1);
            }

            
            
        }


    }
}
