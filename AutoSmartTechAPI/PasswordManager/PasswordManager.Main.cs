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
        private bool disposed;
        private bool disposedValue;
        private AutoSmartTechAPI.UserManager.UserManager _userManager;
        #endregion
        public PasswordManager( AutoSmartTechAPI.UserManager.UserManager userManager)
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

        public async Task<bool> ResetPasswordAsync(Guid userId, string password)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userId);
            ExceptionsAndLogging.NullExceptionsLogging(password);

            try
            {
                var user = _userManager.FindById(userId);
                user.Password = password;
                user.UpdatedOn = DateTime.UtcNow;
                user.ResetPasswordToken = null;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _userManager.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                }
                return true;

            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return false;
            }
        }
        public async Task<bool> SetTempPasswordAsync(Guid userId, string password)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userId);
            ExceptionsAndLogging.NullExceptionsLogging(password);
            try
            {
                var user = _userManager.FindById(userId);

                user.TempPassword = password;
                user.UpdatedOn = DateTime.UtcNow;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _userManager.Update(user);
                    await _userManager.SaveChangesAsync();
                    scope.Complete();
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
