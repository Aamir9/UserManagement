﻿using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManage
{
    public interface IUserManage
    {
        Task<List<User>> FindAllAsync();
        Task<User> FindByIdAsync(Guid Id);
        User FindById(Guid Id);
        void Update(User user);
        Task<int> SaveChangesAsync();
        void Insert(User entity);
        int Save();
        void Delete(object Id);
        Task<User> FindByNameAsync(string userName);
        Task<bool> ResetPasswordAsync(Guid userId, String password);
        Task<bool> SetTempPasswordAsync(Guid userId, string password);
        Task<bool> UpdateTokenAsync(Guid userId, String resetPasswordToken);
        void DeleteUnSelectedObjects(User dbUser, IList<int> userEthnicityIds, IList<int> userRaceIdsk);
        List<Permission> GetAllPermissionsByRolesAndUserId(Guid userId);
        string FindEmailById(Guid userId);
        List<Permission> FindAllPermissionById(int Id);
        List<RolePermission> FindRolePermissionsByRoleId(int Id);
        List<UserRole> FindUserRoleById(Guid Id);

        //Task<List<Role>> FindAllRoleAsync();
        //Task<Role> FindRoleById(Guid Id);
        //void insertRole(Role entity);
        //void updateRole(Role entity);
        //void deleteRole(object Id);
        //void insertPermission(Permission entity);
        //void updatePermission(Permission entity);
        //void deletePermission(object Id);
        //Task<List<RolePermission>> FindAllRolePermissionAsync();
        //void insertRolePermission(RolePermission entity);
        //void updateRolePermission(RolePermission entity);
        //void deleteRolePermission(object Id);
        //Task<List<UserRole>> FindAllUserRoleAsync();
        //void insertUserRole(UserRole entity);
        //void updateUserRole(UserRole entity);
        //void deleteUserRole(object Id);


    }
}