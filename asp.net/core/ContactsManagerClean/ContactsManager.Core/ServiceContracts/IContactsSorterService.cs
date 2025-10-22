using ContactsManager.Core.DTO;
using ContactsManager.Enums;

namespace ServiceContracts
{
    /// <summary>
    /// Logic business for manipulating the Person Entity
    /// Design note: passing returning DTO objects, not Person 
    /// </summary>
    public interface IContactsSorterService
    {
        Task<List<PersonResponse>> GetSortedContacts(List<PersonResponse> contacts, 
            string sortBy, SortOrder sortOrder);
    }
}
