using System;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {
        public string FindEmailById(Guid userId)
        {
            return FindById(userId).EmailAddress;

        }
    }
}
