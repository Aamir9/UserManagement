using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoSmartTechAPI.Services.UserManager
{
    public partial class UserManage
    {

        public  List<Role> FindAllRole()
        {
            return  this._unitOfWork.RoleRepository.FindAll(true);
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
        public List<Role> GetAllRoles(Guid? userId)
        {
            List<Role> roles = new List<Role>();
            if (userId != null && userId.HasValue)
            {
                Guid id = NullableGuidAssigToGuid(userId);
                var UserRoles = FindUserRolesByUserId(id);
                roles = FindUserRolesToRoles(roles, UserRoles);
            }
            else
            {
                roles = FindAllRole();
            }
            return roles;
        }


    }
}
