using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Monolit;
using Monolit.Models;

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
                if (content.ContextPage.Count()==0 )  content = SearchId(content);

                if (content.ContextPage.Count() > 0)
                {
                result = content.ContextPage.First().Context;
                    if (result.LastIndexOf("@ShablonPrice") != -1)
                    {
                        TemplateHelper _tp = new TemplateHelper();
                        string shablon= _tp.GetTablePrice(content.ContextPage.First().Id, true);
                        result = result.Replace("@ShablonPrice", shablon);
                    }
                    else if (result.LastIndexOf("@ShablonInfo") != -1)
                    {
                        TemplateHelper _tp = new TemplateHelper();
                        string shablon = _tp.GetInfoTable();
                        result = result.Replace("@ShablonInfo", shablon);
                    }

                    Title = content.ContextPage.First().Title;
                KeyWords = content.ContextPage.First().Keywords;
                Description = content.ContextPage.First().Descript;
                Layout = content.Template1.Route;
                }

            }
            return result;
        }

        //получения списка статей
        public List<InfoArticle> Get_spis_article_info(Int32 id)
        {
            db = new Monolit.u7716449_monolitEntities();
            var page = db.MenuSite.FirstOrDefault(x => x.Id == id);
                if (page!=null)  Description = page.Name;
            List<InfoArticle> spis = db.MenuSite.Where(x => x.Visible && x.ParentMenu == id).Select(o => new InfoArticle
            {
                date = (DateTime)o.ContextPage.FirstOrDefault().Date,
                URL = o.URL,
                Url_Image = o.ContextPage.FirstOrDefault().UrlDescriptImg,
                Description = o.ContextPage.FirstOrDefault().SmallDescript.Length > 150 ? o.ContextPage.FirstOrDefault().SmallDescript.Substring(0, 150) : o.ContextPage.FirstOrDefault().SmallDescript,
                Header = o.ContextPage.FirstOrDefault().Header
            }).ToList();
            return spis;
        }


        private MenuSite SearchId(MenuSite c)
        {
            
            MenuSite context = new MenuSite();
            if (db.MenuSite.Count(x => x.ParentMenu == c.Id && x.Visible) > 0)
            {
                MenuSite temp = db.MenuSite.First(x => x.ParentMenu == c.Id && x.Visible);
                if (temp.ContextPage.Count() == 0)
                    context = SearchId(temp);
                else {
                    context = temp;
                }
            }
            else {
                context = c;
            }

            return context;
        }
    }
}