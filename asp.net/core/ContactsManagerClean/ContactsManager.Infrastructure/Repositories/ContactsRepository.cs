using ContactsManager.Core.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System.Linq.Expressions;

namespace Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly ApplicationDbContext _db;

        public ContactsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Person> AddContact(Person person)
        {
            // stored procedure 
            //_db.sp_InsertPerson(person);

            _db.People.Add(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeleteContact(Guid id)
        {
            _db.People.RemoveRange(_db.People.Where(p => p.Id == id));
            await _db.SaveChangesAsync();   // int rowsDeleted
            return true;                    // rowsDeleted > 0
        }

        public async Task<Person?> GetContact(Guid id)
        {
            return await _db.People.Include("Country").FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Person>> GetContacts()
        {
            // stored procedure
            // Include: the data returned from the stored procedure is mapped
            // to entities and not directly loaded into navigation properties
            // return _db.sp_GetPeople();

            return await _db.People.Include("Country").ToListAsync();
        }

        public async Task<List<Person>> GetFilteredContacts(Expression<Func<Person, bool>> predicate)
        {
            return await _db.People.Include("Country").Where(predicate).ToListAsync();
        }

        public async Task<Person> UpdateContact(Person person)
        {
            Person? matchingPerson = await _db.People.FirstOrDefaultAsync(p => p.Id == person.Id);

            if (matchingPerson == null)
                return person;

            matchingPerson.Name = person.Name;
            matchingPerson.Email = person.Email;
            matchingPerson.Address = person.Address;
            matchingPerson.BirthDate = person.BirthDate;
            matchingPerson.Gender = person.Gender;
            matchingPerson.Id = person.Id;
            matchingPerson.ReceiveNewsletters = person.ReceiveNewsletters;

            await _db.SaveChangesAsync();

            return matchingPerson;
        }
    }
}
