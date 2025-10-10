using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ContactsService : IContactsService
    {
        private readonly List<Person> _contacts;
        private readonly ICountriesService _countriesService;

        public ContactsService()
        {
            _contacts = new List<Person>();
            _countriesService = new CountriesService();
        }

        public PersonResponse AddContact(PersonAddRequest? personDto)
        {
            if (personDto == null)
            {
                throw new ArgumentNullException(nameof(personDto));
            }

            /*
            if (string.IsNullOrEmpty(personDto.Name))
            {
                throw new ArgumentException(nameof(personDto.Name));
            }*/
            ValidationHelper.ModelValidation(personDto);

            Person person = personDto.ToPerson();
            person.Id = Guid.NewGuid();

            _contacts.Add(person);

            return ConvertToPersonResponseDto(person);
        }

        public PersonResponse? GetContact(Guid? id)
        {
            if (id == null) return null;

            Person? person = _contacts.FirstOrDefault(p => p.Id == id);

            return person?.ToPersonResponse();
        }

        public List<PersonResponse> GetContacts()
        {
            return _contacts.Select(c => c.ToPersonResponse()).ToList();
        }

        private PersonResponse ConvertToPersonResponseDto(Person person)
        {
            PersonResponse personResponseDto = person.ToPersonResponse();
            personResponseDto.Country = _countriesService.GetCountry(person.CountryId)?.Name;
            return personResponseDto;
        }
    }
}
