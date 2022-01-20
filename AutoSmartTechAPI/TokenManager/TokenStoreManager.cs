using AutoSmartTechAPI.UserComm;
using AutoSmartTechAPI.UserManager;
using DataAccessLayer.Services;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace AutoSmartTechAPI.TokenManager
{
    public class TokenStoreManager
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppUserStoreManager userStoreManager;
        #endregion
        public TokenStoreManager(IUnitOfWork unitOfWork)
        {
            userStoreManager = new AppUserStoreManager(unitOfWork);
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> UpdateTokenAsync(Guid userId, string resetPasswordToken)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userId);
            ExceptionsAndLogging.NullExceptionsLogging(resetPasswordToken);
            try
            {
                var user = userStoreManager.FindById(userId);
                if (user == null)
                    throw new ArgumentException("User Password model does not correspond to a User entity.", "user");

                user.ResetPasswordToken = resetPasswordToken;
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