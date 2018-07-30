using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System.Collections.Generic;

namespace Memeni.Services.Interfaces
{
    public interface IAdminUserService
    {
        List<Users> GetAll();
        Users GetById(int id);
        void Update(UsersUpdateRequest model);
        void ConfirmEmail(int id);
        void LockUser(UsersUpdateRequest model);
        void AdminAccess(UsersUpdateRequest model);
        UsersGrid GetGrid(UsersGridRequest model);
    }
}