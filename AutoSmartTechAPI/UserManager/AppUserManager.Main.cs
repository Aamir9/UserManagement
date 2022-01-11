using AutoSmartTechAPI.Models;
using AutoSmartTechAPI.UserManager;
using DataAccessLayer.Services;
using Microsoft.AspNet.Identity;
using System;

namespace AutoSmartTechAPI
{

    public partial class AppUserManager : UserManager<ApplicationUser, Guid>
    {
        #region Private variables...
        private readonly  AppUserStoreManager _userStoreManager;
        #endregion
        public AppUserManager(AppUserStoreManager userStoreManager) :base(userStoreManager)
        {
            _userStoreManager = userStoreManager;

            AppUserSettings();

        }

       



    }
}
