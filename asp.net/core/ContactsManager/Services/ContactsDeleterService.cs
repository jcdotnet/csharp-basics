using Entities;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class ContactsDeleterService : IContactsDeleterService
    {
        private readonly IContactsRepository _repository;

        public ContactsDeleterService(IContactsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteContact(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            Person? person = await _repository.GetContact(id.Value);
            if (person == null) { return false; }

            await _repository.DeleteContact(id.Value);

            return true;
        }
    }
}