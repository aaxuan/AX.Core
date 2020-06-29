using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Business.Managers
{
    internal class AuthCodeLogic : BaseLogic
    {
        public Base_AuthCode Add(Base_AuthCode authCode)
        {
            if (string.IsNullOrWhiteSpace(authCode.Code))
            { throw new AXWarringMesssageException("请输入权限码"); }

            var oldModel = DB.SingleOrDefault<Base_AuthCode>("WHERE Code = @0", authCode.Code);
            if (oldModel != null)
            { throw new AXWarringMesssageException($"已存在 {authCode.Code} 权限码"); }
            DB.Insert<Base_AuthCode>(authCode);
            return authCode;
        }

        public List<Base_AuthCode> GetList()
        {
            return DB.GetList<Base_AuthCode>();
        }

        public String GetCodeStringByIds(String ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
            { return null; }
            var authCodes = DB.GetList<Base_AuthCode>("WHERE Id IN @ids", ids);
            return string.Join(",", authCodes.Select(p => p.Code).ToList());
        }

        public Dictionary<String, Base_AuthCode> GetDictionary()
        {
            return GetList().ToDictionary(p => p.Id, p => p);
        }
    }
}