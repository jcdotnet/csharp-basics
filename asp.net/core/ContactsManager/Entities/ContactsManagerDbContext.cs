using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Entities
{
    public class ContactsManagerDbContext : DbContext
    {

        public ContactsManagerDbContext(DbContextOptions options): base(options) { }     

        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // optional but recommended
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("People");

            // seeding the DB with initial data (countries)
            // https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
            //modelBuilder.Entity<Country>().HasData(
            //    new Country() { Id = Guid.Parse("6B820C12-FF44-4412-9E14-7941A869FB03"), Name = "USA" },
            //);
            string jsonCountries = File.ReadAllText("countries.json");
            List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(jsonCountries);
            foreach (var country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }

            // seeding the DB with initial data (people)
            // https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
            string jsonPeople = File.ReadAllText("people.json");
            List<Person>? people = JsonSerializer.Deserialize<List<Person>>(jsonPeople);
            foreach (var person in people)
            {
                modelBuilder.Entity<Person>().HasData(person);
            }
        }

        public List<Person> sp_GetPeople()
        {
            return People.FromSqlRaw("EXECUTE [dbo].[GetPeople]").ToList();
        }

        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@Id", person.Id),
                new SqlParameter("@Name", person.Name),
                new SqlParameter("@Email", (object)person.Email ?? DBNull.Value),
                new SqlParameter("@Address", (object)person.Address ?? DBNull.Value),
                new SqlParameter("@BirthDate", (object)person.BirthDate ?? DBNull.Value),
                new SqlParameter("@Gender", (object)person.Gender ?? DBNull.Value),
                new SqlParameter("@CountryId", (object)person.CountryId ?? DBNull.Value),
                new SqlParameter("@ReceiveNewsletters", person.ReceiveNewsletters),
            ]; 
            return Database.ExecuteSqlRaw(
                "EXECUTE [dbo].[InsertPerson] @Id, @Name, @Email, @Address, @BirthDate, @Gender, @CountryId, @ReceiveNewsletters", 
                parameters
            );
        }
    }
}
