using DataAccessLayer.DataEntities;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;

namespace AutoSmartTechAPI.RoleManager
{
    public interface IRoleManager
    {
        List<Role> GetAllRoles(Guid? userId);
        List<Role> FindRoleById(int roleId);
        Role FindRoleByRoleName(string roleName);
        List<Role> FindAllRole();
        List<RolePermission> FindRolePermissionsByRoleId(int Id);
        bool DeleteAllUserRoleByUserId(UserRole userRole);
        bool insertRole(Role entity);
        bool updateRole(Role entity);
        bool deleteRole(object Id);
    }

    public partial class RoleManager : IRoleManager, IDisposable
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private bool disposed;
        private readonly RoleStoreManager roleStoreManager;

        #endregion
        public RoleManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            roleStoreManager = new RoleStoreManager(unitOfWork);


        }

        #region Implementing IDiosposable...
        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }
        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        #endregion
     
    }
}
