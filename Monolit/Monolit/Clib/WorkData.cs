using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monolit;

namespace Monolit.Clib
{
    public class WorkData
    {
        public string Title = "";
        public string KeyWords = "";
        public string Description = "";
        public string Layout = "";
        Monolit.u7716449_monolitEntities db = new Monolit.u7716449_monolitEntities();
        public List<MenuSite> GetTopMenu(string name_menu)
        {
             db = new Monolit.u7716449_monolitEntities();
            List<MenuSite> menu = new List<MenuSite>();
            var m = db.MenuSite.FirstOrDefault(x => x.Name == name_menu);
            menu = db.MenuSite.Where(x => x.ParentMenu == m.Id && x.Visible).OrderBy(x => x.Position).ToList();
            return menu;
        }

        public String GetContent(string url)
        {
            string result = "";
             db = new Monolit.u7716449_monolitEntities();
            var content = db.MenuSite.FirstOrDefault(x => x.URL == url);
            if (content != null)
            {
                content = SearchId(content);
                if (content.ContextPage.Count() > 0)
                {
                result = content.ContextPage.First().Context;
                Title = content.ContextPage.First().Title;
                KeyWords = content.ContextPage.First().Keywords;
                Description = content.ContextPage.First().Descript;
                Layout = content.Template1.Route;
                }
                /* if (content.Header == "Цена")
                 {
                     TemplateProduct tp = new TemplateProduct();
                     string price = tp.GetPrice();
                     result = result.Replace("@ShablonPrice", price);
                 };*/
            }
            return result;
        }

        private MenuSite SearchId(MenuSite c)
        {
            
            MenuSite context = new MenuSite();
            if (db.MenuSite.Count(x => x.ParentMenu == c.Id && x.Visible) > 0)
            {
                MenuSite temp = db.MenuSite.First(x => x.ParentMenu == c.Id && x.Visible);
                context = SearchId(temp);

            }
            else {
                context = c;
            }

            return context;
        }
    }
}