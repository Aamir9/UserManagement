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
    }

    public partial class RoleManager : IRoleManager, IDisposable
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private bool disposed;
        private bool disposedValue;
        private AutoSmartTechAPI.UserManager.UserManager _userManager;
      
        #endregion
        public RoleManager(AutoSmartTechAPI.UserManager.UserManager userManager)
        {
            _userManager = userManager;
            _unitOfWork = new UnitOfWork();
            
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
