using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;
using AX.Core.Extension;
using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Business.Managers
{
    internal class UserRoleMapLogic : BaseLogic
    {
        public Base_UserRoleMap Add(Base_UserRoleMap userRoleMap)
        {
            if (string.IsNullOrWhiteSpace(userRoleMap.RoleId))
            { throw new AXWarringMesssageException("角色不能为空"); }
            if (string.IsNullOrWhiteSpace(userRoleMap.UserId))
            { throw new AXWarringMesssageException("用户不能为空"); }

            var oldModel = DB.SingleOrDefault<Base_UserRoleMap>("WHERE UserId = @0 AND RoleId = @1", userRoleMap.UserId, userRoleMap.RoleId);
            if (oldModel != null)
            { throw new AXWarringMesssageException("用户已经绑定该角色"); }

            DB.Insert<Base_UserRoleMap>(userRoleMap);
            return userRoleMap;
        }

        public void Delete(string id)
        {
            DB.Delete<Base_UserRoleMap>(id);
        }

        public List<Base_Role> GetCurrentUserRole()
        {
            var userRoleMaps = DB.GetList<Base_UserRoleMap>("WHERE UserId = @0", CurrentUser.Id);
            var roleIds = userRoleMaps.Select(p => p.RoleId).ToList();
            var roles = DB.GetList<Base_Role>("WHERE Id IN (@0)", string.Join(",", roleIds));
            return roles;
        }

        public HashSet<string> GetCurrentAllRoleAuthCode()
        {
            var roles = GetCurrentUserRole();
            var allAuthCodeHashSet = new HashSet<string>();

            foreach (var item in roles)
            {
                var codeHasSet = item.AuthCodeStrs.Split(',').ToHashSet<string>();
                foreach (var authCode in codeHasSet)
                {
                    if (allAuthCodeHashSet.Contains(authCode) == false)
                    { allAuthCodeHashSet.Add(authCode); }
                }
            }
            return allAuthCodeHashSet;
        }
    }
}