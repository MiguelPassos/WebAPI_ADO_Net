using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_ADO_Net.Models;

namespace WebAPI_ADO_Net.Repositories.Interfaces
{
    public interface IUserService
    {
        Task<bool> InsertUserAsync(User user);
        
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserAsync(long idUser);

        Task<User> GetUserAsync(string name, string email);

        Task<User> UpdateUserAsync(long idUser, User user);

        Task<bool> DeleteUserAsync(long idUser);
    }
}
