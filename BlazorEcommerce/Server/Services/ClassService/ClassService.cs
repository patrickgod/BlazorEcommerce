using AutoMapper;
using BlazorEcommerce.Server.Services.ClassService;
using BlazorEcommerce.Shared.Models;


namespace BlazorEcommerce.Server.Services.ProductService
{
    public class ClassService : IClassService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ClassService(DataContext context, IHttpContextAccessor httpContextAccessor,IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;

        }

         public async Task<ServiceResponse<ClassDto>> CreateClass(ClassDto Class)
        {
            //To do Add mapper
            _context.Class.Add(_mapper.Map<Class>(Class));
            await _context.SaveChangesAsync();
            return new ServiceResponse<ClassDto> { Data = Class };

            //throw new NotImplementedException();

        }

        public async Task<ServiceResponse<ClassDto>> UpdateClass(ClassDto dto)
        {
            var dbProduct = await _context.Class.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Classid == dto.Classid);

            if (dbProduct == null)
            {
                return new ServiceResponse<ClassDto>
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }

            var updatedEntity = _mapper.Map<Class>(dto);

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
          
            return new ServiceResponse<ClassDto> { Data = dto };
        }

        public async Task<ServiceResponse<bool>> DeleteClass(Guid classId)
        {
            var entity = await _context.Class.FindAsync(classId);
            if (entity == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }

            //Search that class fk in person
            var relatedPersonList = await _context.Person.Where(s => s.Classid == classId).ToListAsync();
            if (relatedPersonList.Any()) { 
                foreach (var relatedPerson in relatedPersonList)
                {
                    relatedPerson.Classid = null;
                }
            }

            _context.Class.Remove(entity);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<ClassDto>> GetClassAsync(int ClassId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ClassDto>>> GetClassListAsync()
        {
            var classList = await _context.Class.ToListAsync();
            var result = classList.Select(s => _mapper.Map<ClassDto>(s)).ToList();
             return new ServiceResponse<List<ClassDto>> { Data = result };
            
        }

        public async Task<ServiceResponse<List<IdValuePair>>> GetClassIdValuePair()
        {
            var result = await _context.Class.Select(s=> new IdValuePair() { Id = s.Classid , Value = s.Classname}).ToListAsync();
            return new ServiceResponse<List<IdValuePair>> { Data = result };

        }

        public async Task<ServiceResponse<ClassDto>> GetClassListByName(string searchText)
        {
            throw new NotImplementedException();
        }

    

    }
}
