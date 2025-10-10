using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Logic business for manipulating the Person Entity
    /// Design note: passing returning DTO objects, not Person 
    /// </summary>
    public interface IContactsService
    {
        PersonResponse AddContact(PersonAddRequest? personDto);

        List<PersonResponse> GetContacts();

        PersonResponse? GetContact(Guid? id);

        List<PersonResponse> GetFilteredContacts(string searchBy, string? search);

        List<PersonResponse> GetSortedContacts(List<PersonResponse> contacts, string sortBy, SortOrder sortOrder);

        PersonResponse UpdateContact(PersonUpdateRequest? personDto);
    }
}
