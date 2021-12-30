using AutoSmartTechAPI.UserComm;
using DataAccessLayer.DataEntities;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.UserManager
{
    internal class UserStoreManager
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        #endregion

      public  UserStoreManager(IUnitOfWork unitOfWork)
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
        public async Task<User> FindByIdAsync(Guid Id)
        {
            ExceptionsAndLogging.NullExceptionsLogging(Id);
            try
            {
                return await  this._unitOfWork.UserRepository.FindByIdAsync(Id);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);
                return null;
            }

        }
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
        public void Update(User user)
        {
            ExceptionsAndLogging.NullExceptionsLogging(user.Id);
            try
            {
                _unitOfWork.UserRepository.Update(user);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

            }

        }
        public Task<int> SaveChangesAsync()
        {
            return _unitOfWork.SaveChangesAsync();
        }
        public void Insert(User entity)
        {
            ExceptionsAndLogging.NullExceptionsLogging(entity.Id);
            try
            {
                _unitOfWork.UserRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                ExceptionsAndLogging.CatchExceptionAndLogging(ex);

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
    }
}