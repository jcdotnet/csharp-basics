using ContactsManager.DTO;
using ContactsManager.Helpers;
using ContactsManager.Domain.Entities;
using RepositoryContracts;

namespace Services
{
    public class ContactsAdderService : IContactsAdderService
    {
        private readonly IContactsRepository _repository;

        public ContactsAdderService(IContactsRepository repository)
        {
            _repository = repository;
        }

        public async Task<PersonResponse> AddContact(PersonAddRequest? personDto)
        {
            if (personDto != null)
            {
                ValidationHelper.ModelValidation(personDto);

                Person person = personDto.ToPerson();
                person.Id = Guid.NewGuid();

                await _repository.AddContact(person);

                return person.ToPersonResponse();
            }

            throw new ArgumentNullException(nameof(personDto));
        }
    }
}
