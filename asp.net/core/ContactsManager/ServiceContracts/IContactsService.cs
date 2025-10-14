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
        Task<PersonResponse> AddContact(PersonAddRequest? personDto);

        Task<List<PersonResponse>> GetContacts();

        Task<PersonResponse?> GetContact(Guid? id);

        Task<List<PersonResponse>> GetFilteredContacts(string searchBy, string? search);

        Task<List<PersonResponse>> GetSortedContacts(List<PersonResponse> contacts, string sortBy, SortOrder sortOrder);

        Task<PersonResponse> UpdateContact(PersonUpdateRequest? personDto);

        Task<bool> DeleteContact(Guid? id);
    }
}
