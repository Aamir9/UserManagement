using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {
        public async Task<List<User>> FindAllAsync()
        {
            return await this._unitOfWork.UserRepository.FindAllAsync(true);
        }
        public Task<User> FindByIdAsync(Guid Id)
        {
            return this._unitOfWork.UserRepository.FindByIdAsync(Id);
        }

        public User FindById(Guid Id)
        {
            return this._unitOfWork.UserRepository.FindById(Id);
        }
        public void Update(User user)
        {
            _unitOfWork.UserRepository.Update(user);
        }

        public Task<int> SaveChangesAsync()
        {
           return _unitOfWork.SaveChangesAsync();
        }

        public void Insert(User entity)
        {
            _unitOfWork.UserRepository.Insert(entity);
        }

        public int Save()
        {
           return _unitOfWork.Save();
        }

        public void Delete(object Id)
        {
            _unitOfWork.UserRepository.Delete(Id);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(p => p.EmailAddress.ToLower().Trim() == userName.Trim().ToLower());
        }

        public void DeleteUnSelectedObjects(User dbUser, IList<int> userEthnicityIds, IList<int> userRaceIdsk)
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

        private static Guid NullableGuidAssigToGuid(Guid? userId)
        {
            return userId.HasValue ? userId.Value : Guid.Empty;
        }

        private List<Role> FindUserRolesToRoles(List<Role> roles, List<UserRole> UserRoles)
        {
            foreach (var userRole in UserRoles)
            {
                roles = FindRoleById(userRole.Id);
            }
            return roles;
        }
    }
}
