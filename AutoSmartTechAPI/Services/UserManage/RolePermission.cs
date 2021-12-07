using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {
        //public async Task<List<RolePermission>> FindAllRolePermissionAsync()
        //{
        //    return await this._unitOfWork.RolePermissionRepository.FindAllAsync();
        //}

        public List<RolePermission> FindRolePermissionsByRoleId(int Id)
        {
          return  _unitOfWork.RolePermissionRepository.GetMany(a => a.RoleId == Id);
        }

        public void insertRolePermission(RolePermission entity)
        {
            _unitOfWork.RolePermissionRepository.Insert(entity);
        }

        public void updateRolePermission(RolePermission entity)
        {

            _unitOfWork.RolePermissionRepository.Update(entity);
        }

        public void deleteRolePermission(object Id)
        {
            _unitOfWork.RolePermissionRepository.Delete(Id);
        }
    }
}
