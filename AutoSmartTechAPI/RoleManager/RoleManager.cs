using AutoSmartTechAPI.UserComm;
using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;

namespace AutoSmartTechAPI.RoleManager
{
    public partial class RoleManager
    {
        public List<Role> FindAllRole()
        {
           return roleStoreManager.FindAllRole();
            
        }
        public List<Role> FindRoleById(int roleId)
        {
          return  roleStoreManager.FindRoleById(roleId);
           
        }
        public Role FindRoleByRoleName(string roleName)
        {
           return roleStoreManager.FindRoleByRoleName(roleName);
        }
        public void insertRole(Role entity)
        {
          roleStoreManager.insertRole(entity);
           
        }
        public void updateRole(Role entity)
        {
           roleStoreManager.updateRole(entity);
        }
        public void deleteRole(object Id)
        {
         roleStoreManager.deleteRole(Id);
            
        }
        public List<Role> GetAllRoles(Guid? userId)
        {
            return roleStoreManager.GetAllRoles(userId);
        }
       
        public List<RolePermission> FindRolePermissionsByRoleId(int Id)
        {
          return roleStoreManager.FindRolePermissionsByRoleId(Id);
        }

        public bool CreateOrUpdateUserRole(UserRole userRole)
        {
            return roleStoreManager.CreateOrUpdateUserRole(userRole);
        }
    }
}
