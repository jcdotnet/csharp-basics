using ContactsManager.Core.Entities;
using System.Linq.Expressions;

namespace RepositoryContracts
{
    public interface IContactsRepository
    {
        Task<Person> AddContact(Person person);
        
        Task<List<Person>> GetContacts();

        Task<Person?> GetContact(Guid id);

        /// <summary>
        /// Retrieves all person objects based on the given expresion
        /// </summary>
        /// <param name="predicate">LINQ expression to check to</param>
        /// <returns>all matching peson with the given expression</returns>
        Task<List<Person>> GetFilteredContacts(Expression<Func<Person, bool>> predicate);

        Task<Person> UpdateContact(Person person);

        Task<bool> DeleteContact(Guid id);
    }
}
