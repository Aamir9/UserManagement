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
        public bool insertRole(Role entity)
        {
          return roleStoreManager.insertRole(entity);
           
        }
        public bool updateRole(Role entity)
        {
          return roleStoreManager.updateRole(entity);
        }
        public bool deleteRole(object Id)
        {
         return roleStoreManager.deleteRole(Id);
            
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

        public bool DeleteAllUserRoleByUserId(UserRole userRole)
        {
            return roleStoreManager.DeleteAllUserRoleByUserId(userRole);
        }
    }
}
