using AX.Core.Business.DataModel;
using AX.Core.CommonModel.Exceptions;
using System;
using System.Collections.Generic;

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
            GetDB().Insert<Base_Menu>(menu);
            return menu;
        }

        ///// <summary>
        ///// 更新菜单项
        ///// </summary>
        ///// <param name="menu"></param>
        ///// <returns></returns>
        //public Base_Menu UpDate(Base_Menu menu)
        //{
        //    if (string.IsNullOrWhiteSpace(menu.Name))
        //    { throw new AXWarringMesssageException("请输入菜单名"); }
        //    var oldModel = GetDB().CheckById<Base_Menu>(menu.Id);

        //    GetDB().Update<Base_Menu>(menu,  "Name ,  ParentId,Url", "OnlyAdmin", "Order", "AuthorizationCodeIds" });
        //    return menu;
        //}

        //public List<Menu> GetList()
        //{
        //    var db = Services.DBFactory.GetDB();
        //    var result = db.GetList<Menu>();
        //    SetAuthorizationCodeStrs(result);
        //    return result;
        //}

        //public List<Menu> GetFirstMenu()
        //{
        //    var result = new List<Menu>();
        //    var db = Services.DBFactory.GetDB();
        //    var user = AuthLogic.GetCurrentUser();
        //    if (user.IsAdmin)
        //    {
        //        result = db.GetList<Menu>("WHERE ParentId = '0' || ParentId IS NULL || ParentId = ''");
        //    }
        //    else
        //    {
        //        result = db.GetList<Menu>("WHERE ParentId = '0' || ParentId IS NULL || ParentId = '' AND OnlyAdmin = @0", false);
        //    }
        //    SetAuthorizationCodeStrs(result);
        //    return result;
        //}

        //public List<Menu> GetMenuTree()
        //{
        //    var allMenu = GetList();
        //    var FirstMenus = allMenu.Where(p => string.IsNullOrWhiteSpace(p.ParentId) || p.ParentId == "0").OrderBy(p => p.Order).ToList();
        //    foreach (var item in FirstMenus)
        //    {
        //        SetChild(item, allMenu);
        //    }
        //    return FirstMenus;
        //}

        //public void Delete(String id)
        //{
        //    var db = Services.DBFactory.GetDB();
        //    db.Delete<Menu>(id);
        //}

        //private void SetChild(Menu menu, List<Menu> menus = null)
        //{
        //    menu.Child = menus.Where(p => p.ParentId == menu.Id).OrderBy(p => p.Order).ToList();
        //    foreach (var item in menu.Child)
        //    {
        //        if (menus.Count(p => p.ParentId == item.Id) > 0)
        //        { SetChild(item, menus); }
        //    }
        //    return;
        //}

        //private void SetAuthorizationCodeStrs(List<Menu> menus)
        //{
        //    foreach (var item in menus)
        //    {
        //        SetAuthorizationCodeStrs(item);
        //    }
        //}

        //private void SetAuthorizationCodeStrs(Menu menu)
        //{
        //    var authorizationCodeLogic = new AuthorizationCodeLogic();
        //    if (string.IsNullOrWhiteSpace(menu.AuthorizationCodeIds))
        //    { return; }
        //    menu.AuthorizationCodeStrs = authorizationCodeLogic.GetCodeStringByIds(menu.AuthorizationCodeIds);
        //}
    }
}