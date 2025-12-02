using Dapper;
using eCommerce.Application.RepositoryContracts;
using eCommerce.Domain.Entities;
using eCommerce.Infrastructure.Data;

namespace eCommerce.Infrastructure.Repositories
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly DapperDbContext _db;

        public UsersRepository(DapperDbContext db)
        {
            _db = db;
        }

        public async Task<ApplicationUser?> AddUser(ApplicationUser user)
        {
            user.UserId = Guid.NewGuid();
            
            string query = @"INSERT INTO public.""Users""
            (""UserId"", ""Email"", ""UserName"", ""Gender"", ""Password"")
            VALUES (@UserId, @Email, @UserName, @Gender, @Password)";
            
            int rowsAffected = await _db.DbConnection.ExecuteAsync(query, user);
            if (rowsAffected > 0)
            {
                return user;
            }
            return null;
        }

        public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
        {
            string query = @"SELECT * FROM public.""Users""
            WHERE @Email=""Email"" AND @Password=""Password""";

            var param = new { Email = email, Password = password };
            var user = await _db.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, param);
            return user;
        }

        public async Task<ApplicationUser?> GetUserByUserId(Guid? userId)
        {
            string query = @"SELECT * FROM public.""Users"" WHERE ""UserId""=@UserId";
            var param = new { UserId = userId };

            using var connection = _db.DbConnection;
            return await connection.QueryFirstOrDefaultAsync<ApplicationUser>(query, param);

        }
    }
}
