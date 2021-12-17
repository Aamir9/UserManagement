using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {
        //public async Task<List<RolePermission>> FindAllRolePermissionAsync()
        //{
        //    return await this._unitOfWork.RolePermissionRepository.FindAllAsync();
        //}

        public List<RolePermission> FindRolePermissionsByRoleId(int Id)
        {
          return  _unitOfWork.RolePermissionRepository.GetMany(a => a.RoleId == Id);
        }

        public void insertRolePermission(RolePermission entity)
        {
            _unitOfWork.RolePermissionRepository.Insert(entity);
        }

        public void updateRolePermission(RolePermission entity)
        {

            _unitOfWork.RolePermissionRepository.Update(entity);
        }

        public void deleteRolePermission(object Id)
        {
            _unitOfWork.RolePermissionRepository.Delete(Id);
        }

        private void FindRolesToRolesPermissions(List<Permission> permissions, List<UserRole> roles)
        {
            foreach (var role in roles)
            {
                var rolePermissions = FindRolePermissionsByRoleId(role.Id);
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
