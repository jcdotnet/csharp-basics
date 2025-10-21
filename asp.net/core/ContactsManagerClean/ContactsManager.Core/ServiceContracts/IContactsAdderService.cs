namespace ContactsManager.DTO
{
    /// <summary>
    /// Logic business for manipulating the Person Entity
    /// Design note: passing returning DTO objects, not Person 
    /// </summary>
    public interface IContactsAdderService
    {
        Task<PersonResponse> AddContact(PersonAddRequest? personDto);
    }
}
