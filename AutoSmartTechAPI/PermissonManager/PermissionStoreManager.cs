using AutoSmartTechAPI.RoleManager;
using AutoSmartTechAPI.UserManager;
using DataAccessLayer.DataEntities;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;

namespace AutoSmartTechAPI.PermissonManager
{
    public class PermissionStoreManager
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleStoreManager roleStoreManager;
        private readonly UserStoreManager userStoreManager;
        #endregion
        public PermissionStoreManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            roleStoreManager = new RoleStoreManager(unitOfWork);
            userStoreManager = new UserStoreManager(unitOfWork);

        }

        public List<Permission> FindAllPermissionById(int Id)
        {
            return this._unitOfWork.PermissionRepository.GetMany(a => a.Id == Id);
        }
        public void insertPermission(Permission entity)
        {
            _unitOfWork.PermissionRepository.Insert(entity);
        }

        public void updatePermission(Permission entity)
        {
            _unitOfWork.PermissionRepository.Update(entity);
        }

        public void deletePermission(object Id)
        {
            _unitOfWork.PermissionRepository.Delete(Id);
        }

        public List<Permission> FindAllPermission()
        {
            return _unitOfWork.PermissionRepository.FindAll(true);
        }
        public List<Permission> GetAllPermissions(Guid? userId)
        {
            List<Permission> permissions = new List<Permission>();
            if (userId != null && userId.HasValue)
            {
                Guid id = AutoSmartTechAPI.UserComm.UserComm.NullableGuidAssigToGuid(userId);
                var roles = userStoreManager.FindUserRolesByUserId(id);
                if (roles != null)
                {
                    FindRolesToRolesPermissions(permissions, roles);
                }
            }
            else
            {
                permissions = FindAllPermission();
            }
            return permissions;
        }

        public List<Permission> GetAllPermissionsByRoleId(int roleId)
        {
            List<Permission> permissions = new List<Permission>();
            if (roleId > 0)
            {
                var roles = roleStoreManager.FindRolePermissionsByRoleId(roleId);
                if (roles != null)
                {
                    var rolePermissions = roleStoreManager.FindRolePermissionsByRoleId(roleId);
                    FindPermissionsListFromRolesPermissions(permissions, rolePermissions);
                }
            }

            return permissions;
        
        }

        private void FindPermissionsListFromRolesPermissions(List<Permission> permissions, List<RolePermission> rolePermissions)
        {
            foreach (var rolePermsm in rolePermissions)
            {
                var permissionList = FindAllPermissionById(rolePermsm.PermissionId);
                permissions.AddRange(permissionList);
            }
        }

        private void FindRolesToRolesPermissions(List<Permission> permissions, List<UserRole> roles)
        {
            foreach (var role in roles)
            {
                var rolePermissions = roleStoreManager.FindRolePermissionsByRoleId(role.Id);
                FindPermissionsListFromRolesPermissions(permissions, rolePermissions);
            }
        }

    }
}
