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
        public bool insertRole(Role entity)
        {
            ExceptionsAndLogging.NullExceptionsLogging(entity);
            try
            {
                var data = _unitOfWork.RoleRepository.GetFirstOrDefault( x =>x.Name == entity.Name );
                if(data == null)
                {
                    _unitOfWork.RoleRepository.Insert(entity);
                    _unitOfWork.Save();
                    return true;
                }
                else
                {
                  return updateRole(entity);
                }
                
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return false;

            }

        }
        public bool updateRole(Role entity)
        {
            ExceptionsAndLogging.NullExceptionsLogging(entity);
            try
            {
                _unitOfWork.RoleRepository.Update(entity);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return false;

            }

        }
        public bool deleteRole(object Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                _unitOfWork.RoleRepository.Delete(Id);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return false ;

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

        public bool DeleteAllUserRoleByUserId(UserRole userRole)
        {
            try
            {
                var userRoleList = _unitOfWork.UserRoleRepository.FindAll(x => x.UserId == userRole.UserId);
                _unitOfWork.UserRoleRepository.DeleteObjects(userRoleList);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
            }
            return true;
        }

        public bool CreateOrUpdateUserRole(UserRole userRole)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userRole);
            try
            {

                var date = _unitOfWork.UserRoleRepository.GetFirstOrDefault(x => x.RoleId == userRole.RoleId && x.UserId == userRole.UserId);
                if (date != null)
                {
                    _unitOfWork.UserRoleRepository.Update(date);
                    _unitOfWork.Save();
                }
                else
                {
                    _unitOfWork.UserRoleRepository.Insert(userRole);
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


    }
}
