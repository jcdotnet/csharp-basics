using ContactsManager.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Logic business for manipulating the Person Entity
    /// Design note: passing returning DTO objects, not Person 
    /// </summary>
    public interface IContactsGetterService
    {
        Task<List<PersonResponse>> GetContacts();

        Task<PersonResponse?> GetContact(Guid? id);

        Task<List<PersonResponse>> GetFilteredContacts(string searchBy, string? search);
    }
}
