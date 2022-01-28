using AutoSmartTechAPI.RoleManager;
using AutoSmartTechAPI.UserComm;
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
        private readonly AppUserStoreManager userStoreManager;
        #endregion
        public PermissionStoreManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            roleStoreManager = new RoleStoreManager(unitOfWork);
            userStoreManager = new AppUserStoreManager(unitOfWork);

        }

        public List<Permission> FindAllPermissionById(int Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                return this._unitOfWork.PermissionRepository.GetMany(a => a.Id == Id);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;

            }
        }
        public void insertPermission(Permission entity)
        {
            ExceptionsAndLogging.NullExceptionsLogging(entity);
            try
            {
                _unitOfWork.PermissionRepository.Insert(entity);
                ;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

            }
        }

        public void updatePermission(Permission entity)
        {
            ExceptionsAndLogging.NullExceptionsLogging(entity);
            try
            {
                _unitOfWork.PermissionRepository.Update(entity);
                ;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

            }
        }

        public void deletePermission(object Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                _unitOfWork.PermissionRepository.Delete(Id);
                ;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

            }
           
        }

        public List<Permission> FindAllPermission()
        {

            try
            {
                return _unitOfWork.PermissionRepository.FindAll(true);
                ;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;

            }
           
        }
        public List<Permission> GetAllAdminPermissions()
        {

            try
            {
                return _unitOfWork.PermissionRepository.FindAll(x => x.Name.StartsWith("Admin"));
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;

            }

        }
        public List<Permission> GetAllClientPermissions()
        {

            try
            {
                return _unitOfWork.PermissionRepository.FindAll(x => x.Name.StartsWith("Client"));
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;

            }

        }
        public List<Permission> GetAllPermissions(Guid? userId)
        {
            
            List<Permission> permissions = new List<Permission>();
            try
            {
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
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;

            }
            return permissions;
        }

        public List<Permission> GetAllPermissionsByRoleId(int roleId)
        {
            ExceptionsAndLogging.NullExceptionsLogging(roleId);
            List<Permission> permissions = new List<Permission>();
            try
            {
                if (roleId > 0)
                {
                    var roles = roleStoreManager.FindRolePermissionsByRoleId(roleId);
                    if (roles != null)
                    {
                        var rolePermissions = roleStoreManager.FindRolePermissionsByRoleId(roleId);
                        FindPermissionsListFromRolesPermissions(permissions, rolePermissions);
                    }
                }

            }
            catch (Exception ex)
            {

                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
            }
            return permissions;
        
        }
        public bool  DeleteAllRolePermissonByRoleId(RolePermission rolePermission)
        {
            try
            {
             var rolePermissons =    _unitOfWork.RolePermissionRepository.FindAll( x => x.RoleId == rolePermission.RoleId );
             _unitOfWork.RolePermissionRepository.DeleteObjects( rolePermissons );
                _unitOfWork.Save();
            }
             catch (Exception ex)
            {

                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
            }
            return true;
        }

        public bool CreateOrUpdateRolePermisson(RolePermission rolePermission)
        {
            ExceptionsAndLogging.NullExceptionsLogging(rolePermission);
            try
            {
                              
                var date = _unitOfWork.RolePermissionRepository.GetFirstOrDefault(x => x.RoleId == rolePermission.RoleId && x.PermissionId == rolePermission.PermissionId);
                if (date != null)
                {
                    _unitOfWork.RolePermissionRepository.Update(date);
                    _unitOfWork.Save();
                }
                else
                {
                    _unitOfWork.RolePermissionRepository.Insert(rolePermission);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return false;
                
            }
         
        }

        private void FindPermissionsListFromRolesPermissions(List<Permission> permissions, List<RolePermission> rolePermissions)
        {
            ExceptionsAndLogging.NullExceptionsLogging(permissions);
            ExceptionsAndLogging.NullExceptionsLogging(rolePermissions);
            try
            {
                foreach (var rolePermsm in rolePermissions)
                {
                    var permissionList = FindAllPermissionById(rolePermsm.PermissionId);
                    permissions.AddRange(permissionList);
                }
            }
            catch (Exception ex)
            {

                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
            }
            
        }

        private void FindRolesToRolesPermissions(List<Permission> permissions, List<UserRole> userRoles)
        {
            ExceptionsAndLogging.NullExceptionsLogging(permissions);
            ExceptionsAndLogging.NullExceptionsLogging(userRoles);
            try
            {
                foreach (var userRole in userRoles)
                {
                    var rolePermissions = roleStoreManager.FindRolePermissionsByRoleId(userRole.RoleId);
                    FindPermissionsListFromRolesPermissions(permissions, rolePermissions);
                }
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
            }
          
        }


    }
}
