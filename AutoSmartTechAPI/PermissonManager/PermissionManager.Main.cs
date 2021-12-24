using DataAccessLayer.DataEntities;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;

namespace AutoSmartTechAPI.PermissonManager
{
    public interface IPermissionManager
    {
        List<Permission> GetAllPermissions(Guid? userId);
        List<Permission> FindAllPermissionById(int Id);
        List<Permission> GetAllPermissionsByRoleId(int roleId);
        List<Permission> FindAllPermission();
    }
    public partial class PermissionManager : IPermissionManager, IDisposable
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private bool disposed;
        private bool disposedValue;
        private AutoSmartTechAPI.UserManager.UserManager _userManager;
        private AutoSmartTechAPI.RoleManager.RoleManager _roleManager;
        #endregion
        public PermissionManager(AutoSmartTechAPI.UserManager.UserManager userManager, AutoSmartTechAPI.RoleManager.RoleManager roleManager)
        {
            _userManager = userManager;
            _unitOfWork = new UnitOfWork();
            _roleManager = roleManager;
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
