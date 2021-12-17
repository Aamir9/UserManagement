using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {
        public  List<Permission> FindAllPermissionById(int Id)
        {
            return  this._unitOfWork.PermissionRepository.GetMany(a =>a.Id == Id);
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
            return  _unitOfWork.PermissionRepository.FindAll(true);
        }
        public List<Permission> GetAllPermissions(Guid? userId)
        {
            List<Permission> permissions = new List<Permission>();
            if (userId != null && userId.HasValue)
            {
                Guid id = NullableGuidAssigToGuid(userId);
                var roles = FindUserRolesByUserId(id);
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
            if ( roleId > 0)
            {
                var roles = FindRolePermissionsByRoleId(roleId);
                if (roles != null)
                {
                    var rolePermissions = FindRolePermissionsByRoleId(roleId);
                    FindPermissionsListFromRolesPermissions(permissions, rolePermissions);
                }
            }
 
            return permissions;
        }
    }
}
