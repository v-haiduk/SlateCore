using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlateCore.BLL.Interfaces;
using SlateCore.BLL.DTO;
using SlateCore.BLL.Infrastructure;
using SlateCore.DAL.Interfaces;
using SlateCore.DAL.Entities;
using SlateCore.Security;
using AutoMapper;

namespace SlateCore.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork uow { get; set; }

        public UserService(IUnitOfWork db)
        {
            uow = db;
        }

        public async Task<IEnumerable<UserAccountDTO>> GetAll()
        {
            Mapper.Initialize(configutation => configutation.CreateMap<UserAccount, UserAccountDTO>());
            var listOfUsers = await uow.UserRepository.GetAll();

            return Mapper.Map<IEnumerable<UserAccount>, IEnumerable<UserAccountDTO>>(listOfUsers);
        }

        public async Task<UserAccountDTO> GetById(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("The user's ID is not set ", "");
            }

            var foundedAccount = await uow.UserRepository.GetByID(id.Value);
            if (foundedAccount == null)
            {
                throw new ValidationException("Sorry, this user is not found ", "");
            }
            Mapper.Initialize(configutation => configutation.CreateMap<UserAccount, UserAccountDTO>());

            return Mapper.Map<UserAccount, UserAccountDTO>(foundedAccount);
        }

        public async Task Update(UserAccountDTO item)
        {
            if (item.PasswordHash != null)
            {
                var dynamicSalt = Security.PasswordHash.CreateDynamicSalt();
                item.PasswordHash = CreatePasswordHash(item.PasswordHash, dynamicSalt);
            }

            Mapper.Initialize(config => config.CreateMap<UserAccountDTO, UserAccount>());
            var updatedAccount = Mapper.Map<UserAccountDTO, UserAccount>(item);
            await uow.UserRepository.Update(updatedAccount);
            uow.Commit();
        }

        public async Task Create(UserAccountDTO item)
        {
            var dynamicSalt = Security.PasswordHash.CreateDynamicSalt();
            item.PasswordHash = CreatePasswordHash(item.PasswordHash, dynamicSalt);

            Mapper.Initialize(config => config.CreateMap<UserAccountDTO, UserAccount>());
            var newAccount = Mapper.Map<UserAccountDTO, UserAccount>(item);
            await uow.UserRepository.Create(newAccount);
            uow.Commit();
        }

        public async Task Delete(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("The user's ID is not set ", "");
            }
            await uow.UserRepository.Delete(id.Value);
            uow.Commit();
        }

        public void Dispose()
        {
            uow.Dispose();
        }

        private string CreatePasswordHash(string password, string dynamicSalt)
        {
            string globalSalt = null;              //add reading from file
            var passwordHash = Security.PasswordHash.CreatePasswordHash(password, dynamicSalt, globalSalt);

            return passwordHash;
        }

        public async Task<UserAccountDTO> GetByLogin(string email)
        {
            var foundedUser = (await uow.UserRepository.GetByPredicate(user => user.Email == email)).FirstOrDefault();
            if (foundedUser == null)
            {
                throw new ValidationException("Sorry, this user is not found ", "");
            }

            Mapper.Initialize(config => config.CreateMap<UserAccount, UserAccountDTO>());

            return Mapper.Map<UserAccount, UserAccountDTO>(foundedUser);
        }
    }
}
