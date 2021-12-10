using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {
        public async Task<List<UserRole>> FindAllUserRoleAsync()
        {
            return await this._unitOfWork.UserRoleRepository.FindAllAsync(true);
        }


        public List<UserRole> FindUserRolesByUserId(Guid Id)
        {
           return   this._unitOfWork.UserRoleRepository.GetMany(a => a.UserId == Id);
        }


        public void insertUserRole(UserRole entity)
        {

            _unitOfWork.UserRoleRepository.Insert(entity);
        }

        public void updateUserRole(UserRole entity)
        {

            _unitOfWork.UserRoleRepository.Update(entity);
        }

        public void deleteUserRole(object Id)
        {
            _unitOfWork.UserRoleRepository.Delete(Id);
        }

    }
}
