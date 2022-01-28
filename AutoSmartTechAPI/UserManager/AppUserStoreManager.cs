using AutoSmartTechAPI.Models;
using AutoSmartTechAPI.UserComm;
using DataAccessLayer.DataEntities;
using DataAccessLayer.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.UserManager
{

    public class AppUserStoreManager  : IUserStore<ApplicationUser, Guid>,
         IUserPasswordStore<ApplicationUser, Guid>,
       IUserEmailStore<ApplicationUser, Guid>
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public AppUserStoreManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<User>> FindAllAsync()
        {
            try
            {
                return await this._unitOfWork.UserRepository.FindAllAsync(true);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                throw;
            }

        }
        //public async Task<User> FindByIdAsync(Guid Id)
        //{
        //    ExceptionsAndLogging.NullExceptionsLogging(Id);
        //    try
        //    {
        //        return await  this._unitOfWork.UserRepository.FindByIdAsync(Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionsAndLogging.CatchExceptionAndLogging(ex);
        //        return null;
        //    }

        //}
        public User FindById(Guid Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                return this._unitOfWork.UserRepository.FindById(Id);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }

        }
        public bool Update(User user)
        {
            ExceptionsAndLogging.NullExceptionsLogging(user.Id);
            try
            {

                _unitOfWork.UserRepository.Update(user);
                return true;


            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return false;

            }

        }
        public Task<int> SaveChangesAsync()
        {
            return _unitOfWork.SaveChangesAsync();
        }
        public int Insert(User entity)
        {
            ExceptionsAndLogging.NullExceptionsLogging(entity.Id);
            try
            {
                _unitOfWork.UserRepository.Insert(entity);

                return 1;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return 0;

            }

        }
        public int Save()
        {
            return _unitOfWork.Save();
        }
        public void Delete(object Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                _unitOfWork.UserRepository.Delete(Id);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

            }

        }
        public async Task<User> FindByNameAsync(string userName)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userName);
            try
            {
                return await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(p => p.EmailAddress.ToLower().Trim() == userName.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }

        }
        public void DeleteUnSelectedObjects(User dbUser, IList<int> userEthnicityIds, IList<int> userRaceIdsk)
        {
            ExceptionsAndLogging.NullExceptionsLogging(dbUser);
            ExceptionsAndLogging.NullExceptionsLogging(dbUser.Ethnicities);
            ExceptionsAndLogging.NullExceptionsLogging(dbUser.Races);
            ExceptionsAndLogging.NullExceptionsLogging(userEthnicityIds);
            ExceptionsAndLogging.NullExceptionsLogging(userRaceIdsk);

            try
            {
                var deletedEthnicities = new List<Ethnicity>();
                foreach (var deletedEthnicity in dbUser.Ethnicities.ToList())
                {
                    if (!userEthnicityIds.Contains(deletedEthnicity.Id))
                        deletedEthnicities.Add(deletedEthnicity);
                }

                var deletedRaces = new List<Race>();

                foreach (var deletedRace in dbUser.Races.ToList())
                {
                    if (!userRaceIdsk.Contains(deletedRace.Id))
                        deletedRaces.Add(deletedRace);
                }
                foreach (var deletedRace in deletedRaces)
                    dbUser.Races.Remove(deletedRace);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
            }
        }
        public List<UserRole> FindUserRolesByUserId(Guid Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                return this._unitOfWork.UserRoleRepository.GetMany(a => a.UserId == Id);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }

        }
        public string FindEmailById(Guid userId)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userId);
            try
            {
                return FindById(userId).EmailAddress;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }


        }

        public bool CreateOrUpdateUserRole(UserRole userRole)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userRole);
            try
            {
                var date = _unitOfWork.UserRoleRepository.GetFirstOrDefault(x => x.RoleId == userRole.RoleId && x.UserId == userRole.UserId);
                if (date != null)
                {

                    _unitOfWork.UserRoleRepository.Update(date);

                }
                else
                {
                    _unitOfWork.UserRoleRepository.Insert(userRole);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return false;

            }

        }

        public async Task CreateAsync(ApplicationUser appUser)
        {
            User user = appUser.MapSameProperties<User>();
            _unitOfWork.UserRepository.Insert(user);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationUser appUser)
        {
            User user = appUser.MapSameProperties<User>();
            Update(user);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteAsync(ApplicationUser appUser)
        {
            User user = appUser.MapSameProperties<User>();
            await Task.Run(() => Delete(user));
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult<string>(user.Password);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.Password));
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            user.UserName = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.EmailAddress == email);
            ApplicationUser applicationUser = null;
            if ( user!= null)
            {
                //var data = user.MapSameProperties<ApplicationUser>();
                //applicationUser = ApplicationUser.MapUserToApplicationUser(user);
                 applicationUser = user.MapSameProperties<ApplicationUser>();

            }
            return applicationUser;
        }


        async Task<ApplicationUser> IUserStore<ApplicationUser, Guid>.FindByIdAsync(Guid userId)
        {
            ExceptionsAndLogging.NullExceptionsLogging(userId);
            try
            {
                var data = await this._unitOfWork.UserRepository.FindByIdAsync(userId);
                return data.MapSameProperties<ApplicationUser>();

            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }

        }



        async Task<ApplicationUser> IUserStore<ApplicationUser, Guid>.FindByNameAsync(string userName)
        {
            var user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(x => x.EmailAddress == userName);
            ApplicationUser applicationUser = null;
            if (user != null)
            {
                //  applicationUser = ApplicationUser.MapUserToApplicationUser(user);
                applicationUser = user.MapSameProperties<ApplicationUser>();

            }
            return applicationUser;
        }


    }
}