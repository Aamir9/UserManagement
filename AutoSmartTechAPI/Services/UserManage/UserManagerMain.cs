using DataAccessLayer.DataEntities;
using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
   
   public interface IUserManage
    {
        Task<List<User>> FindAllAsync();
        Task<User> FindByIdAsync(Guid Id);
        User FindById(Guid Id);
        void Update(User user);
        Task<int> SaveChangesAsync();
        void Insert(User entity);
        int Save();
        void Delete(object Id);
        Task<User> FindByNameAsync(string userName);
        Task<bool> ResetPasswordAsync(Guid userId, String password);
        Task<bool> SetTempPasswordAsync(Guid userId, string password);
        Task<bool> UpdateTokenAsync(Guid userId, String resetPasswordToken);
        void DeleteUnSelectedObjects(User dbUser, IList<int> userEthnicityIds, IList<int> userRaceIdsk);
    }
   public partial class UserManage : IDisposable, IUserManage
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private bool disposed;

        #endregion
       public UserManage()
        {
            _unitOfWork = new UnitOfWork();
        }

        #region Implementing IDiosposable...
        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);

        }


        #endregion
    }
}
