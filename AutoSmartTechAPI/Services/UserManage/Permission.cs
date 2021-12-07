using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {
        public  List<Permission> FindAllPermissionById(int Id)
        {
            return  this._unitOfWork.PermissionRepository.GetMany(a =>a.Id == Id);
        }
        public void insertPermission(Permission entity)
        {
            _unitOfWork.PermissionRepository.Insert(entity);
        }

        public void updatePermission(Permission entity)
        {

            _unitOfWork.PermissionRepository.Update(entity);
        }

        public void deletePermission(object Id)
        {
            _unitOfWork.PermissionRepository.Delete(Id);
        }
    }
}
