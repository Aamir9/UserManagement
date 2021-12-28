using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;

namespace AutoSmartTechAPI.PermissonManager
{
    public partial class PermissionManager
    {
        public List<Permission> FindAllPermissionById(int Id)
        {
           return permissionStoreManager.FindAllPermissionById(Id);
        }
        public void insertPermission(Permission entity)
        {
            permissionStoreManager.insertPermission(entity);
        }

        public void updatePermission(Permission entity)
        {
           permissionStoreManager.deletePermission(entity);
        }

        public void deletePermission(object Id)
        {
            permissionStoreManager.deletePermission(Id);
        }

        public List<Permission> FindAllPermission()
        {
           return permissionStoreManager.FindAllPermission();
        }
        public List<Permission> GetAllPermissions(Guid? userId)
        {
          return permissionStoreManager.GetAllPermissions(userId);
        }

        public List<Permission> GetAllPermissionsByRoleId(int roleId)
        {
         return permissionStoreManager.GetAllPermissionsByRoleId(roleId);
        }

    }
}
