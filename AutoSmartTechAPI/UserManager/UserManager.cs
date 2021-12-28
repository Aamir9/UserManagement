using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.UserManager
{
    public partial class UserManager
    {
        public async Task<List<User>> FindAllAsync()
        {
         return await userStoreManager.FindAllAsync();
        }
        public async Task<User> FindByIdAsync(Guid Id)
        {
         return   await userStoreManager.FindByIdAsync(Id);
        }
        public User FindById(Guid Id)
        {
           return userStoreManager.FindById(Id);
           
        }
        public void Update(User user)
        {
           userStoreManager.Update(user);

        }
        public async Task<int> SaveChangesAsync()
        {
            return  await _unitOfWork.SaveChangesAsync();
        }
        public void Insert(User entity)
        {
             userStoreManager.Insert(entity);
            
        }
        public int Save()
        {
            return userStoreManager.Save();
        }
        public void Delete(object Id)
        {
            userStoreManager.Delete(Id);
           
        }
        public async Task<User> FindByNameAsync(string userName)
        {
         return await userStoreManager.FindByNameAsync(userName);
           
        }
        public void DeleteUnSelectedObjects(User dbUser, IList<int> userEthnicityIds, IList<int> userRaceIdsk)
        {
            userStoreManager.DeleteUnSelectedObjects(dbUser, userEthnicityIds, userRaceIdsk);
        }
        public List<UserRole> FindUserRolesByUserId(Guid Id)
        {
          return userStoreManager.FindUserRolesByUserId(Id);

        }
        public string FindEmailById(Guid userId)
        {
            return userStoreManager.FindEmailById(userId); 
        }

    }
}
