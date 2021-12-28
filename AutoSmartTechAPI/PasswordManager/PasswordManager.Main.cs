using AutoSmartTechAPI.UserComm;
using DataAccessLayer.Services;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace AutoSmartTechAPI.PasswordManager
{
    public interface IPasswordManager
    {
        Task<bool> ResetPasswordAsync(Guid userId, String password);
        Task<bool> SetTempPasswordAsync(Guid userId, string password);
    }
    public class PasswordManager : IPasswordManager , IDisposable
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordStoreManager passwordStoreManager;
        private bool disposed;
        #endregion
        public PasswordManager(IUnitOfWork unitOfWork)
        {   
            _unitOfWork = unitOfWork;
            passwordStoreManager = new PasswordStoreManager(unitOfWork);
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

        public async Task<bool> ResetPasswordAsync(Guid userId, string password)
        {
           return await passwordStoreManager.ResetPasswordAsync(userId, password);
        }
        public async Task<bool> SetTempPasswordAsync(Guid userId, string password)
        {

            return await passwordStoreManager.SetTempPasswordAsync(userId, password);
        }
    }


}
