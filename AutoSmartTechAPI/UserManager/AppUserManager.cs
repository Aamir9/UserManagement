using AutoSmartTechAPI.Models;
using DataAccessLayer.DataEntities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI
{
    public partial class AppUserManager 
    {
        public void DataProtectorTokenProviderFromApp(IDataProtectionProvider dataProtectionProvider)
        {
            this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, Guid>(dataProtectionProvider.Create("ResetPassword"));
        }

        private void AppUserSettings()
        {
            this.UserLockoutEnabledByDefault = false;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(10);
            this.MaxFailedAccessAttemptsBeforeLockout = 10;

            this.UserValidator = new UserValidator<ApplicationUser, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false,

            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,

            };
        }
        public async Task<List<User>> FindAllAsync()
        {
            return await _userStoreManager.FindAllAsync();
          
        }

        //public async Task<User> FindByIdAsync(Guid Id)
        //  {
        //      return await base.FindByIdAsync(Id);
        ////  return   await _userStoreManager.FindByIdAsync(Id);
        //  }

        public User FindById(Guid Id)
        {
            return _userStoreManager.FindById(Id);

        }
        public void Update(User user)
        {
            _userStoreManager.Update(user);

        }
        public async Task<int> SaveChangesAsync()
        {
            return  await _userStoreManager.SaveChangesAsync();
        }
        public void Insert(User entity)
        {
             _userStoreManager.Insert(entity);
            
        }
        public int Save()
        {
            return _userStoreManager.Save();
        }
        public void Delete(object Id)
        {
            _userStoreManager.Delete(Id);
           
        }
        //public async Task<User> FindByNameAsync(string userName)
        //{
          
        // return await _userStoreManager.FindByNameAsync(userName);
           
        //}

        public void DeleteUnSelectedObjects(User dbUser, IList<int> userEthnicityIds, IList<int> userRaceIdsk)
        {
            _userStoreManager.DeleteUnSelectedObjects(dbUser, userEthnicityIds, userRaceIdsk);
        }
        public List<UserRole> FindUserRolesByUserId(Guid Id)
        {
          return _userStoreManager.FindUserRolesByUserId(Id);

        }
        public string FindEmailById(Guid userId)
        {
            return _userStoreManager.FindEmailById(userId);
        }
        public bool CreateOrUpdateUserRole(UserRole userRole)
        {
            return _userStoreManager.CreateOrUpdateUserRole(userRole);
        }

    }
}
