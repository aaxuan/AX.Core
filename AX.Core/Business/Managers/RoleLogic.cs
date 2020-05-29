using System;
using System.Collections.Generic;
using System.Text;

namespace AX.Core.Business.Managers
{
    class RoleLogic
    {
        //public Role Add(Role role)
        //{
        //    if (string.IsNullOrWhiteSpace(role.Name))
        //    { throw new AXWarringMesssageException("请输入角色名称"); }

        //    var db = Services.DBFactory.GetDB();
        //    if (db.SingleOrDefault<Role>("WHERE Name = @0", role.Name) != null)
        //    { throw new AXWarringMesssageException("已存在角色"); }

        //    role.Id = AX.Framework.ID.GuidManager.NewId();
        //    db.Insert(role);
        //    return role;
        //}

        //public Role Update(Role role)
        //{
        //    if (string.IsNullOrWhiteSpace(role.Id))
        //    { throw new AXWarringMesssageException("没有主键"); }
        //    if (string.IsNullOrWhiteSpace(role.Name))
        //    { throw new AXWarringMesssageException("请输入角色名称"); }
        //    var db = Services.DBFactory.GetDB();

        //    var oldModel = db.SingleOrDefaultById<Role>(role.Id);
        //    oldModel.Name = role.Name;
        //    oldModel.Description = role.Description;
        //    oldModel.AuthorizationCodeIds = role.AuthorizationCodeIds;

        //    db.Update<Role>(oldModel, new string[] { "Name", "Description", "AuthorizationCodeIds" });
        //    return oldModel;
        //}

        //public Role Get(string id)
        //{
        //    var db = Services.DBFactory.GetDB();
        //    var result = db.SingleOrDefaultById<Role>(id);
        //    if (result == null)
        //    { throw new AXWarringMesssageException("角色不存在"); }
        //    result.AuthorizationCodeIds = new AuthorizationCodeLogic().GetCodeStringByIds(result.AuthorizationCodeIds);
        //    return result;
        //}
    }
}
