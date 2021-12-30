using DataAccessLayer.DataEntities;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.PermissonManager
{
    public interface IPermissionManager
    {
        List<Permission> GetAllPermissions(Guid? userId);
        List<Permission> FindAllPermissionById(int Id);
        List<Permission> GetAllPermissionsByRoleId(int roleId);
        List<Permission> FindAllPermission();
        bool CreateOrUpdateRolePermisson(RolePermission rolePermission);
        bool DeleteAllRolePermissonByRoleId(RolePermission rolePermission);
    }
    public partial class PermissionManager : IPermissionManager, IDisposable
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private readonly PermissionStoreManager permissionStoreManager;
        private bool disposed;

        #endregion
        public PermissionManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            permissionStoreManager = new PermissionStoreManager(unitOfWork);

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
