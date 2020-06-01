using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;

namespace AX.Core.Business.Managers
{
    internal class RoleLogic : BaseLogic
    {
        public Base_Role Add(Base_Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            { throw new AXWarringMesssageException("请输入角色名称"); }

            if (string.IsNullOrWhiteSpace(role.AuthCodeIds))
            { throw new AXWarringMesssageException("角色必须包含权限码"); }

            if (DB.SingleOrDefault<Base_Role>("WHERE Name = @Name", role.Name) != null)
            { throw new AXWarringMesssageException("已存在该角色名称"); }

            DB.Insert(role);
            return role;
        }

        public Base_Role Update(Base_Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            { throw new AXWarringMesssageException("请输入角色名称"); }
            DB.CheckById<Base_Role>(role.Id);
            role.AuthCodeStrs = new AuthCodeLogic().GetCodeStringByIds(role.AuthCodeIds);
            DB.Update<Base_Role>(role);
            return role;
        }

        public Base_Role Get(string id)
        {
            var result = DB.CheckById<Base_Role>(id);
            return result;
        }
    }
}