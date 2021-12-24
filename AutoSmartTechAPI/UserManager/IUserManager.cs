using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.UserManager
{
    public interface IUserManager
    {
        Task<List<User>> FindAllAsync();
        Task<User> FindByIdAsync(Guid Id);
        Task<User> FindByNameAsync(string userName);
        List<UserRole> FindUserRolesByUserId(Guid Id);
        User FindById(Guid Id);
        void Update(User user);
        Task<int> SaveChangesAsync();
        void Insert(User entity);
        int Save();
        void Delete(object Id);
        void DeleteUnSelectedObjects(User dbUser, IList<int> userEthnicityIds, IList<int> userRaceIdsk);
        string FindEmailById(Guid userId);

    }
}
