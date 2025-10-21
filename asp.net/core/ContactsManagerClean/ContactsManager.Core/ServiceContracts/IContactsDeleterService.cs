namespace ServiceContracts
{
    /// <summary>
    /// Logic business for manipulating the Person Entity
    /// Design note: passing returning DTO objects, not Person 
    /// </summary>
    public interface IContactsDeleterService
    {
        Task<bool> DeleteContact(Guid? id);
    }
}
