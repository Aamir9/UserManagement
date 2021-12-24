using AutoSmartTechAPI.UserComm;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AutoSmartTechAPI.TokenManager
{
    public interface ITokenManger
    {
        Task<bool> UpdateTokenAsync(Guid userId, String resetPasswordToken);
    }
    public partial class TokenManger : ITokenManger, IDisposable
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private bool disposed;
        private bool disposedValue;
        private AutoSmartTechAPI.UserManager.UserManager _userManager;
        #endregion
        public TokenManger(AutoSmartTechAPI.UserManager.UserManager userManager)
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

        public async Task<bool> UpdateTokenAsync(Guid userId, string resetPasswordToken)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userId);
            ExceptionsAndLogging.NullExceptionsLogging(resetPasswordToken);
            try
            {
                var user = _userManager.FindById(userId);
                if (user == null)
                    throw new ArgumentException("User Password model does not correspond to a User entity.", "user");

                user.ResetPasswordToken = resetPasswordToken;
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
    

