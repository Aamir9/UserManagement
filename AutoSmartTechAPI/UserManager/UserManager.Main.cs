using DataAccessLayer.Services;
using System;

namespace AutoSmartTechAPI.UserManager
{

    public partial class UserManager : IUserManager, IDisposable
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private bool disposed;
        #endregion
        public UserManager()
        {
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
