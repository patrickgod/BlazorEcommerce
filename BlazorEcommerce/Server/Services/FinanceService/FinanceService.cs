using AutoMapper;

using BlazorEcommerce.Client.Shared;
using BlazorEcommerce.Server.Data;
using BlazorEcommerce.Server.Services.PersonService;
using BlazorEcommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Text;


namespace BlazorEcommerce.Server.Services.FinanceService
{
    public class FinanceService : IFinanceService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly Lazy<IPersonService> _personService;
        


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
            var dbProduct = await _context.Finance.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Financeid == Finance.Financeid);

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
                
                    }
            catch (Exception exp)
            {

                throw new Exception(exp.Message);
            }
          
            return new ServiceResponse<FinanceDto> { Data = Finance };
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

        public async Task<ServiceResponse<List<FinanceDto>>> GetFinanceListAsync(int day,int month)
        {
            //Daterange;
            //https://blazor.syncfusion.com/documentation/datepicker/date-range;

            try
            {
                List<FinanceDto> preFinanceList = new List<FinanceDto>();
                var sb = new StringBuilder();

                //Get finance records
                var financeList = await _context.Finance.Where(s => !s.Deleted  && s.Financedate.Month == month).ToListAsync();

                //get person ID's
                var personList = await _personService.Value.GetPersonListAsync();
                //var personIdList = new List<Guid>(); 

                //All person list
                foreach (var person in personList.Data)
                {
                    
                    var checkUnpaymentDetails = await _context.Finance .Where(s => !s.Deleted
                                                                                   && s.Personid == person.Personid 
                                                                                   && (s.Financedate.Year <= DateTime.Now.Year && s.Financedate.Month <= DateTime.Now.Month && s.Duedateday < DateTime.Now.Day)
                                                                                   && s.Ispaid == false)
                                                                       .OrderByDescending(t => t.Financedate).ToListAsync();
                    var hasFinancyEntity = financeList.Where(s => s.Personid == person.Personid).FirstOrDefault();



                    //List of previous unpayment details for this peerson
                   
                    foreach (var unpaymentDetail in checkUnpaymentDetails)
                    {
                        
                        var date = unpaymentDetail.Financedate;
                        sb.Append("(");
                        sb.Append(date.Month.ToString()+".");
                        sb.Append(") ");
                    }

                    if (checkUnpaymentDetails.Any())
                    {
                        sb.Append("ay ödemesi yapılmamış");
                    }

                    if (hasFinancyEntity != null)
                    {
                        FinanceDto dto = _mapper.Map<FinanceDto>(hasFinancyEntity);
                        dto.FullName = person.FullName;
                        dto.Note = sb.ToString();
                        dto.NotPaidPreviousmonth = sb.Length > 0 ? true : false;
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

        
        public async Task AddFinanceDateForNewPerson(Guid personId)
        {
            FinanceDto financeDto = new FinanceDto();
            for (int y = DateTime.Now.Year; y <= DateTime.Now.Year + 5; y++)
            {
                for (int m = 1; m <= 12; m++)
                {
                    financeDto.Personid = personId;
                    financeDto.Financedate = new DateTime(y, m, 1);
                    financeDto.Duedateday = 15;
                    financeDto.Ispaid = ((m < DateTime.Now.Month && y < DateTime.Now.Year + 1) ? true : false);
                    _context.Finance.Add(_mapper.Map<Finance>(financeDto));
                    await _context.SaveChangesAsync();
                }
            }
        }


        public async Task FillData()
        {
            FinanceDto financeDto = new FinanceDto();
            var personList = await _personService.Value.GetPersonListAsync();

            foreach (var person in personList.Data.Take(1))
            {
                for (int y = DateTime.Now.Year; y <= DateTime.Now.Year + 5; y++)
                {
                    for (int m = 1; m <= 12; m++)
                    {
                        financeDto.Personid = person.Personid;
                        financeDto.Financedate = new DateTime(y, m, 1);
                        financeDto.Duedateday = 15;
                        financeDto.Ispaid = ((m < DateTime.Now.Month && y < DateTime.Now.Year + 1) ? true : false);
                        _context.Finance.Add(_mapper.Map<Finance>(financeDto));
                        await _context.SaveChangesAsync();
                    }
                }
            }

            
            
        }


    }
}
