using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace Services
{
    public class ContactsSorterService : IContactsSorterService
    {
        private readonly IContactsRepository _repository;

        public ContactsSorterService(IContactsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PersonResponse>> GetSortedContacts(List<PersonResponse> contacts, string sortBy, SortOrder sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return contacts;

            List<PersonResponse> getSorted = (sortBy, sortOrder) switch
            {
                (nameof(PersonResponse.Name), SortOrder.Ascending) => contacts.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Name), SortOrder.Descending) => contacts.OrderByDescending(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrder.Ascending) => contacts.OrderBy(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrder.Descending) => contacts.OrderByDescending(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrder.Ascending) => contacts.OrderBy(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrder.Descending) => contacts.OrderByDescending(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.BirthDate), SortOrder.Ascending) => contacts.OrderBy(p => p.BirthDate).ToList(),

                (nameof(PersonResponse.BirthDate), SortOrder.Descending) => contacts.OrderByDescending(p => p.BirthDate).ToList(),

                (nameof(PersonResponse.Age), SortOrder.Ascending) => contacts.OrderBy(p => p.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrder.Descending) => contacts.OrderByDescending(p => p.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrder.Ascending) => contacts.OrderBy(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrder.Descending) => contacts.OrderByDescending(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrder.Ascending) => contacts.OrderBy(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrder.Descending) => contacts.OrderByDescending(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                _ => contacts
            };

            return getSorted;
        }
    }
}