using AutoSmartTechAPI.UserComm;
using AutoSmartTechAPI.UserManager;
using DataAccessLayer.Services;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace AutoSmartTechAPI.PasswordManager
{
    public class PasswordStoreManager
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserStoreManager userStoreManager; 
        #endregion
        public PasswordStoreManager(IUnitOfWork unitOfWork)
        {
            userStoreManager = new UserStoreManager(unitOfWork);
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ResetPasswordAsync(Guid userId, string password)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userId);
            ExceptionsAndLogging.NullExceptionsLogging(password);

            try
            {
                var user = userStoreManager.FindById(userId);
                user.Password = password;
                user.UpdatedOn = DateTime.UtcNow;
                user.ResetPasswordToken = null;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    userStoreManager.Update(user);
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
                var user = userStoreManager.FindById(userId);

                user.TempPassword = password;
                user.UpdatedOn = DateTime.UtcNow;

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    userStoreManager.Update(user);
                    await userStoreManager.SaveChangesAsync();
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
