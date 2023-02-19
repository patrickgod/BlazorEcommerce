using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Services.PersonService
{
    public interface IPersonService
    {
         Task<ServiceResponse<List<PersonDto>>> GetPersonListAsync();
         Task<ServiceResponse<PersonDto>> GetPersonAsync(int personId);
         Task<ServiceResponse<PersonDto>> GetPersonListByName(string searchText);
         Task<ServiceResponse<PersonDto>> CreatePerson(PersonDto PersonDto);
         Task<ServiceResponse<PersonDto>> UpdatePerson(PersonDto PersonDto);
         Task<ServiceResponse<bool>> DeletePerson(Guid PersonDtoId);

    }
}
