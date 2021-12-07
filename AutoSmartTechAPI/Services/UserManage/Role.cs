using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {

        public async Task<List<Role>> FindAllRoleAsync()
        {
            return await this._unitOfWork.RoleRepository.FindAllAsync( a => a.IsActive == true);
        }
  

        public async Task<Role> FindRoleById(Guid Id)
        {
            return await this._unitOfWork.RoleRepository.GetFirstOrDefaultAsync(a => a.IsActive == true );
        }


        public void insertRole(Role entity){

              _unitOfWork.RoleRepository.Insert(entity);
         }

        public void updateRole(Role entity) {

            _unitOfWork.RoleRepository.Update(entity);
        }

        public void deleteRole(object Id)
        {
            _unitOfWork.RoleRepository.Delete(Id);
        }



    }
}
