using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;

namespace AutoSmartTechAPI.PermissonManager
{
    public partial class PermissionManager
    {
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
                var roles = _userManager.FindUserRolesByUserId(id);
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
                var roles = _roleManager.FindRolePermissionsByRoleId(roleId);
                if (roles != null)
                {
                    var rolePermissions = _roleManager.FindRolePermissionsByRoleId(roleId);
                    FindPermissionsListFromRolesPermissions(permissions, rolePermissions);
                }
            }

            return permissions;
        }

        private void FindRolesToRolesPermissions(List<Permission> permissions, List<UserRole> roles)
        {
            foreach (var role in roles)
            {
                var rolePermissions = _roleManager.FindRolePermissionsByRoleId(role.Id);
                FindPermissionsListFromRolesPermissions(permissions, rolePermissions);
            }
        }

        private void FindPermissionsListFromRolesPermissions(List<Permission> permissions, List<RolePermission> rolePermissions)
        {
            foreach (var rolePermsm in rolePermissions)
            {
                var permissionList = FindAllPermissionById(rolePermsm.PermissionId);
                permissions.AddRange(permissionList);
            }
        }
    }
}
