using AX.Core.Business.DataModel;
using AX.Core.Business.DataModel.DTO;
using AX.Core.CommonModel.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Business.Managers
{
    /// <summary>
    ///
    /// </summary>
    public class MenuLogic : BaseLogic
    {
        /// <summary>
        /// 添加菜单项
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public Base_Menu Add(Base_Menu menu)
        {
            if (string.IsNullOrWhiteSpace(menu.Name))
            { throw new AXWarringMesssageException("请输入菜单名"); }
            if (DB.SingleOrDefault<Base_Menu>("Where name = @name", menu.Name) != null)
            { throw new AXWarringMesssageException("已存在相同菜单名"); }
            if (string.IsNullOrWhiteSpace(menu.AuthCodeId) == false)
            { menu.AuthCode = new AuthCodeLogic().GetCodeStringByIds(menu.AuthCodeId); }

            DB.Insert<Base_Menu>(menu);
            return menu;
        }

        /// <summary>
        /// 更新菜单项
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public Base_Menu UpDate(Base_Menu menu)
        {
            if (string.IsNullOrWhiteSpace(menu.Name))
            { throw new AXWarringMesssageException("请输入菜单名"); }
            if (DB.SingleOrDefault<Base_Menu>("Where name = @name", menu.Name) != null)
            { throw new AXWarringMesssageException("已存在相同菜单名"); }
            var oldModel = DB.CheckById<Base_Menu>(menu.Id);
            DB.Update<Base_Menu>(menu);
            return menu;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<Base_Menu> GetList()
        {
            var result = DB.GetList<Base_Menu>();
            return result;
        }

        /// <summary>
        /// 获取一级菜单
        /// </summary>
        /// <returns></returns>
        public List<Base_Menu> GetFirstMenuByCurrentUser()
        {
            var result = new List<Base_Menu>();
            if (CurrentUser.IsSuperAdmin)
            {
                result = DB.GetList<Base_Menu>("WHERE PId = '0' || PId IS NULL || PId = ''");
            }
            else
            {
                result = DB.GetList<Base_Menu>("WHERE PId = '0' || PId IS NULL || PId = '' AND OnlyAdmin = @0", false);
            }
            return result;
        }

        /// <summary>
        /// 获取当前用户可视菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuTreeNode> GetMenuTreeByCurrentUser()
        {
            var result = new List<MenuTreeNode>();
            var allMenu = GetList();
            var FirstMenus = GetFirstMenuByCurrentUser();
            foreach (var firstMenu in FirstMenus)
            {
                var resultItem = new MenuTreeNode();
                resultItem.Menu = firstMenu;
                if (CurrentUser.IsSuperAdmin)
                { resultItem.Child = allMenu.Where(p => p.PId == firstMenu.Id).OrderBy(p => p.Order).ToList(); }
                else
                { resultItem.Child = allMenu.Where(p => p.PId == firstMenu.Id && p.OnlyAdmin == false).OrderBy(p => p.Order).ToList(); }
                result.Add(resultItem);
            }
            return result;
        }
    }
}