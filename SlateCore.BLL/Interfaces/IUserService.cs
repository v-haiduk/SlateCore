using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlateCore.BLL.DTO;

namespace SlateCore.BLL.Interfaces
{
    public interface IUserService : IService<UserAccountDTO>
    {
        Task<UserAccountDTO> GetByLogin(string email);
    }
}
