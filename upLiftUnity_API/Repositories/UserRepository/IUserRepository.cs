﻿using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task <User> GetUserById(int id);

        Task<IEnumerable<User>> GetUsersByRoleId(int roliId);
        Task<User> UpdateUser(User objUser);
        bool DeleteUser(int ID);

        string GetUserRole(int roleId);
        Task<bool> ChangePassword(int userId, string oldPassword, string newPassword);

    }
}
