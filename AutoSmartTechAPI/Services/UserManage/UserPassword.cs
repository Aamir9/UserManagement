using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {

        public async Task<bool> ResetPasswordAsync(Guid userId, string password)
        {
            var user = FindById(userId);

            user.Password = password;
            user.UpdatedOn = DateTime.UtcNow;
            user.ResetPasswordToken = null;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                 Update(user);
                await _unitOfWork.SaveChangesAsync();
                scope.Complete();

            }
            return true;
        }

        public async Task<bool> SetTempPasswordAsync(Guid userId, string password)
        {

            var user = FindById(userId);

            user.TempPassword = password;
            user.UpdatedOn = DateTime.UtcNow;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Update(user);
                await SaveChangesAsync();
                scope.Complete();
            }

            return true;
        }


      
    }
}
