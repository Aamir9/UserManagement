using DataAccessLayer.Services;
using System;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.TokenManager
{
    public interface ITokenManager
    {
        Task<bool> UpdateTokenAsync(Guid userId, String resetPasswordToken);
    }
    public partial class TokenManager : ITokenManager, IDisposable
    {
        #region Private variables...
        private readonly IUnitOfWork _unitOfWork;
        private bool disposed;
        private readonly TokenStoreManager tokenStoreManager;
        #endregion
        public TokenManager(IUnitOfWork unitOfWork)
        {
            tokenStoreManager = new TokenStoreManager(unitOfWork);
            _unitOfWork = unitOfWork;
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

        public async Task<bool> UpdateTokenAsync(Guid userId, string resetPasswordToken)
        {
            return await tokenStoreManager.UpdateTokenAsync(userId, resetPasswordToken);
        }
    }
}
    

