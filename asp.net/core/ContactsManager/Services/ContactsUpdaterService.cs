using Entities;
using Exceptions;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class ContactsUpdaterService : IContactsUpdaterService
    {
        private readonly IContactsRepository _repository;

        public ContactsUpdaterService(IContactsRepository repository)
        {
            _repository = repository;
        }

        public async Task<PersonResponse> UpdateContact(PersonUpdateRequest? personDto)
        {
            if (personDto == null)
                throw new ArgumentNullException(nameof(Person));

            ValidationHelper.ModelValidation(personDto);

            Person? person = await _repository.GetContact(personDto.Id);
            if (person == null)
            {
                //throw new ArgumentException("Person does not exist");
                throw new InvalidPersonIdException("Person does not exist");
            }
            person.Name = personDto.Name;
            person.Email = personDto.Email;
            person.BirthDate = personDto.BirthDate;
            person.Gender = personDto.Gender.ToString();
            person.CountryId = personDto.CountryId;
            person.Address = personDto.Address;
            person.ReceiveNewsletters = personDto.ReceiveNewsletters;

            await _repository.UpdateContact(person);

            return person.ToPersonResponse();
        }
    }
}