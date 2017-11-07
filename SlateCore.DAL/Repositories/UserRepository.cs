using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlateCore.DAL.Entities;
using SlateCore.DAL.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq.Expressions;

namespace SlateCore.DAL.Repositories
{
    class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction) {  }

        public async Task<IEnumerable<UserAccount>> GetAll()
        {
            var query = "SELECT * FROM UserAccounts";

            return await DB.QueryAsync<UserAccount>(query);
        }

        public async Task<UserAccount> GetByID(int id)
        {
            var query = "SELECT * FROM UserAccounts WHERE Id=@id";

            return (await DB.QueryAsync<UserAccount>(query, new { id })).FirstOrDefault();
        }

        public async Task Create(UserAccount item)
        {
            var query = "INSERT INTO UserAccounts (Email, PasswordHash, Salt) VALUES (@Email, @PasswordHash, @Salt)";
            await DB.QueryAsync<UserAccount>(query, item);
        }

        public async Task Delete(int id)                                    //fix
        {
            var query = "DELETE FROM UserAccounts WHERE Id=@id";
            DB.Execute(query, new { id });
        }

        public async Task Update(UserAccount item)                                //fix
        {
            var query = "UPDATE UserAccounts SET Email=@Email, PasswordHash=@PasswordHash WHERE Id=@Id";
            DB.Execute(query, item);
        }

        public Task<IEnumerable<UserAccount>> GetByPredicate(Expression<Func<UserAccount, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
