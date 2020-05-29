using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Business.Managers
{
    /// <summary>
    ///
    /// </summary>
    internal class AuthCodeLogic : BaseLogic
    {
        /// <summary>
        /// 添加权限码
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public Base_AuthCode Add(Base_AuthCode authCode)
        {
            if (string.IsNullOrWhiteSpace(authCode.Code))
            { throw new AXWarringMesssageException("请输入权限码"); }

            var oldModel = GetDB().SingleOrDefault<Base_AuthCode>("WHERE Code = @0", authCode.Code);
            if (oldModel != null)
            { throw new AXWarringMesssageException($"已存在 {authCode.Code} 权限码"); }
            GetDB().Insert<Base_AuthCode>(authCode);
            return authCode;
        }

        /// <summary>
        /// 获取全部列表
        /// </summary>
        /// <returns></returns>
        public List<Base_AuthCode> GetList()
        {
            return GetDB().GetList<Base_AuthCode>();
        }

        /// <summary>
        /// 根据一组 id 获取逗号分隔组合的 code 串
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public String GetCodeStringByIds(String ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
            { return null; }
            var authCodes = GetDB().GetList<Base_AuthCode>("WHERE Id IN @ids", ids);
            return string.Join(",", authCodes.Select(p => p.Code).ToList());
        }

        /// <summary>
        /// 获取权限码字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Base_AuthCode> GetDictionary()
        {
            return GetList().ToDictionary(p => p.Id, p => p);
        }
    }
}