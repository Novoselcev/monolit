using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Monolit.Clib;
using Monolit.Models;

namespace Monolit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TopMenu()
        {
            WorkData wd = new WorkData();
            List<Monolit.MenuSite> art = wd.GetTopMenu("Главное меню");
            return View(art);
        }

        public ActionResult TopMenuContext(string id = "")
        {
            WorkData wd = new WorkData();
            string zapros = wd.GetContent(id);
            ViewBag.Context = MvcHtmlString.Create(zapros);
            ViewBag.Title = wd.Title;
            ViewBag.KeyWord = wd.KeyWords;
            ViewBag.Description = wd.Description;
            ViewBag.UrlA = id;
            ViewBag.Layout = wd.Layout;
            if (zapros != "")
                return View();
            else return HttpNotFound();

        }

        public ActionResult MenuProduct(int subcategory = 0)
        {
            WorkData wd = new WorkData();
            List<Monolit.MenuSite> prodmenu = wd.GetTopMenu("Услуги");
            return View(prodmenu);
        }


        public ActionResult MenuInfo (int subcategory = 0)
        {
            WorkData wd = new WorkData();
            List<Monolit.MenuSite> prodmenu = wd.GetTopMenu("Полезная информация");
            return View(prodmenu);
        }

        public ActionResult ProdContext(string id = "")
        {
            WorkData wd = new WorkData();
            string zapros = wd.GetContent(id);
            ViewBag.Context = MvcHtmlString.Create(zapros);
            ViewBag.Title = wd.Title;
            ViewBag.KeyWord = wd.KeyWords;
            ViewBag.Description = wd.Description;
            ViewBag.UrlA = id;
            ViewBag.Layout = wd.Layout;
            if (zapros != "")
                return View();
            else return HttpNotFound();

        }

        public ActionResult InfoPage(string id = "")
        {
            WorkData wd = new WorkData();
            string zapros = wd.GetContent(id);
            ViewBag.Context = MvcHtmlString.Create(zapros);
            ViewBag.Title = wd.Title;
            ViewBag.KeyWord = wd.KeyWords;
            ViewBag.Description = wd.Description;
            ViewBag.UrlA = id;
            ViewBag.Layout = wd.Layout;
            if (zapros != "")
                return View();
            else return HttpNotFound();

        }

        public ActionResult InfoContext(Int32 id =0)
        {
            WorkData wd = new WorkData();
            List<InfoArticle> spis = wd.Get_spis_article_info(id);
            ViewBag.Title = "Раздел про " + wd.Description.ToLower();
            ViewBag.KeyWord = "Раздел про " + wd.Description.ToLower();
            ViewBag.Description = "Раздел про " + wd.Description.ToLower();
            return View(spis);
        }

        public ActionResult MessageSent()
        {
            Messager ct = new Messager();
            return View(ct);

        }

        [HttpPost]
        public JsonResult ASMessage(string UserName, string Message, string Phone, string EmailAdrress)
        {
            CheckField _ch = new CheckField();
            string MesEmail = "", MesPhone = "";
            bool check = true;
            if (!_ch.CheckEmail(EmailAdrress))
            {
                MesEmail = "Некорректный email";
                check = false;
            }
            if (!_ch.CheckPhone(Phone))
            {
                MesPhone = "Некорректный номер телефона";
                check = false;
            }
            if (check)
            {

                Messager ct = new Messager();
                ct.Date = DateTime.Now;
                ct.Id = 1;
                ct.UserName = UserName;
                ct.Message = Message;
                ct.Phone = Phone;
                ct.EmailAdrress = EmailAdrress;
                EmailMes em = new EmailMes();
                em.SentEmail(ct);
            }
            return Json(new { isAdded = check, isEmail = MesEmail, isPhone = MesPhone });
        }

        [HttpPost]
        public JsonResult MessIndex(string UserName, string Phone, string EmailAdrress)
        {
            CheckField _ch = new CheckField();
            string MesEmail = "", MesPhone = "";
            bool check = true;
            if (!_ch.CheckEmail(EmailAdrress))
            {
                MesEmail = "Некорректный email";
                check = false;
            }
            if (!_ch.CheckPhone(Phone))
            {
                MesPhone = "Некорректный номер телефона";
                check = false;
            }
            if (check)
            {
                Messager ct = new Messager();
                ct.Date = DateTime.Now;
                ct.Id = 1;
                ct.UserName = UserName;
                ct.Message = "Сообщение отсутсвует";
                ct.Phone = Phone;
                ct.EmailAdrress = EmailAdrress;
                EmailMes em = new EmailMes();
                em.SentEmail(ct);
            }
            return Json(new { isAdded = check, isEmail = MesEmail, isPhone = MesPhone });
        }



    }
}