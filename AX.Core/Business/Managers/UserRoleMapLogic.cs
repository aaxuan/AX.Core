using System;
using System.Collections.Generic;
using System.Text;

namespace AX.Core.Business.Managers
{
    class UserRoleMapLogic
    {
        //public UserRoleMap Add(UserRoleMap userRoleMap)
        //{
        //    if (string.IsNullOrWhiteSpace(userRoleMap.RoleId))
        //    { throw new AX.Framework.Exception.AXWarringMesssageException("请选择要绑定的角色"); }
        //    if (string.IsNullOrWhiteSpace(userRoleMap.UserId))
        //    { throw new AX.Framework.Exception.AXWarringMesssageException("请选择要绑定的用户"); }

        //    var db = Services.DBFactory.GetDB();
        //    var oldModel = db.SingleOrDefault<UserRoleMap>("WHERE UserId = @0 AND RoleId = @1", userRoleMap.UserId, userRoleMap.RoleId);
        //    if (oldModel != null)
        //    { throw new AX.Framework.Exception.AXWarringMesssageException("用户已经绑定该角色"); }

        //    db.Insert<UserRoleMap>(userRoleMap);
        //    return userRoleMap;
        //}

        //public void Delete(string id)
        //{
        //    var db = Services.DBFactory.GetDB();
        //    db.Delete<UserRoleMap>(id);
        //}

        //public List<Role> GetRoleByUserId(string userid)
        //{
        //    var db = Services.DBFactory.GetDB();
        //    var userRoleMaps = db.GetList<UserRoleMap>("WHERE UserId = @0", userid);
        //    var roleIds = userRoleMaps.Select(p => p.RoleId).Distinct().ToArray();
        //    var roles = db.GetList<Role>("WHERE Id IN (@0)", string.Join(",", roleIds));
        //    return roles;
        //}

        //public HashSet<string> GetAllRoleAuthCodeHashSetBuUserId(string userid)
        //{
        //    var roles = GetRoleByUserId(userid);
        //    var allAuthCodeHashSet = new HashSet<string>();
        //    var AuthCodeLogic = new AuthorizationCodeLogic();
        //    foreach (var item in roles)
        //    {
        //        var codeHasSet = AuthCodeLogic.GetHashSetByIds(item.AuthorizationCodeIds);
        //        foreach (var authCode in codeHasSet)
        //        {
        //            if (allAuthCodeHashSet.Contains(authCode) == false)
        //            { allAuthCodeHashSet.Add(authCode); }
        //        }
        //    }
        //    return allAuthCodeHashSet;
        //}

        //public string GetAllRoleAuthCodeByUserId(string userid)
        //{
        //    return string.Join(",", GetAllRoleAuthCodeHashSetBuUserId(userid));
        //}
    }
}
