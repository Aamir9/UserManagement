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
  

        public  List<Role> FindRoleById(int roleId)
        {
            return  this._unitOfWork.RoleRepository.GetMany( a=> a.Id  == roleId);
        }

        public Role FindRoleByRoleName(string roleName)
        {
            return  this._unitOfWork.RoleRepository.GetFirstOrDefault(a => a.Name == roleName);
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
