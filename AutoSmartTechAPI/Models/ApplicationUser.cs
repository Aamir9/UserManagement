using DataAccessLayer.DataEntities;
using Microsoft.AspNet.Identity;
using System;
namespace AutoSmartTechAPI.Models
{
    public class ApplicationUser : User,IUser<Guid>
    {
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public ApplicationUser()
        {
            UserName = EmailAddress;
        }


        public static ApplicationUser MapUserToApplicationUser(User user)
        {
            try
            {

                ApplicationUser applicationUser = new ApplicationUser();

                 applicationUser.Id = user.Id;
                 applicationUser.BusinessName = user.BusinessName;
                 applicationUser.EmailAddress = user.EmailAddress;
                 applicationUser.UserName = user.EmailAddress;
                 applicationUser.FirstName = user.FirstName;
                 applicationUser.LastName = user.LastName;
                 applicationUser.Password = user.Password;
                 applicationUser.UserType = user.UserType;
                 applicationUser.UserTypeId = user.UserTypeId;
                 
                
                return applicationUser;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
