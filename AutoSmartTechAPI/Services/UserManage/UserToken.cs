using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {
        public async Task<bool> UpdateTokenAsync(Guid userId, string resetPasswordToken)
        {

            var user = FindById(userId);
            if (user == null)
                throw new ArgumentException("User Password model does not correspond to a User entity.", "user");

            user.ResetPasswordToken = resetPasswordToken;
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
