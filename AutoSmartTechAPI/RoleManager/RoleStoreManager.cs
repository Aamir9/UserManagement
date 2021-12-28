using AutoSmartTechAPI.UserComm;
using DataAccessLayer.DataEntities;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;

namespace AutoSmartTechAPI.RoleManager
{


    internal class RoleStoreManager
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private AutoSmartTechAPI.UserManager.UserStoreManager _userStoreManager;

        #endregion

        public RoleStoreManager(IUnitOfWork unitOfWork)
        {
            _userStoreManager = new AutoSmartTechAPI.UserManager.UserStoreManager(unitOfWork);
            _unitOfWork = unitOfWork;
        }
        public List<Role> FindUserRolesToRoles(List<Role> roles, List<UserRole> UserRoles)
        {
            foreach (var userRole in UserRoles)
            {
                roles = FindRoleById(userRole.Id);
            }
            return roles;
        }
        public List<Role> FindRoleById(int roleId)
        {
            ExceptionsAndLogging.NullExceptionsLogging(roleId);
            try
            {
                return this._unitOfWork.RoleRepository.GetMany(a => a.Id == roleId);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }
        }

        public List<Role> GetAllRoles(Guid? userId)
        {
            List<Role> roles = new List<Role>();
            if (userId != null && userId.HasValue)
            {
                Guid id = AutoSmartTechAPI.UserComm.UserComm.NullableGuidAssigToGuid(userId);
                var UserRoles = _userStoreManager.FindUserRolesByUserId(id);
                roles = FindUserRolesToRoles(roles, UserRoles);
            }
            else
            {
                roles = FindAllRole();
            }
            return roles;
        }

        public List<Role> FindAllRole()
        {
            try
            {
                return this._unitOfWork.RoleRepository.FindAll(true);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }
        }

        public Role FindRoleByRoleName(string roleName)
        {
            ExceptionsAndLogging.NullExceptionsLogging(roleName);
            try
            {
                return this._unitOfWork.RoleRepository.GetFirstOrDefault(a => a.Name == roleName);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }
        }
        public void insertRole(Role entity)
        {
            ExceptionsAndLogging.NullExceptionsLogging(entity);
            try
            {
                _unitOfWork.RoleRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

            }

        }
        public void updateRole(Role entity)
        {
            ExceptionsAndLogging.NullExceptionsLogging(entity);
            try
            {
                _unitOfWork.RoleRepository.Update(entity);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

            }

        }
        public void deleteRole(object Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                _unitOfWork.RoleRepository.Delete(Id);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

            }

        }

        public List<RolePermission> FindRolePermissionsByRoleId(int Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                return _unitOfWork.RolePermissionRepository.GetMany(a => a.RoleId == Id);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }

        }
    }
}
