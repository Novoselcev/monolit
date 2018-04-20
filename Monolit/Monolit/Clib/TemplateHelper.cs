using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using Monolit.Clib;
using Monolit;
using Monolit.Models;

namespace Monolit.Clib
{
    public class TemplateHelper
    {
        private List<PriceProduct> pr;
        private List<Product> product = new List<Product>();
        Monolit.u7716449_monolitEntities db = new Monolit.u7716449_monolitEntities();


        //foolPrice - флаг на просмотр категорий которые входят в нашу категорию
        public string GetTablePrice(Int32 id, bool foolPrice)
        {
            string rezult = "";
            db = new Monolit.u7716449_monolitEntities();
            product = db.Products.Where(x => (x.Visible)).Select(o => o).ToList();
            ContextPage cp = db.ContextPage.FirstOrDefault(x => x.Id == id);
            if (cp != null)
            {
                CategoryProduct catolPage = cp.PriceContentCrosses.FirstOrDefault().CategoryProduct;
                pr = new List<PriceProduct>();
                pr = product.Where(x => x.CategoryId == catolPage.Id).OrderBy(x=>x.Position).Select(o => new PriceProduct {
                    Currency = o.Currency,
                    IE = o.IE,
                    Name = o.Name,
                    NameGroup = o.CategoryProduct.Name,
                    PositionGroupName = o.CategoryProduct.Position,
                    PositionName = o.Position,
                    Price = (decimal)o.Price
                }).ToList<PriceProduct>();
                if (foolPrice)
                {
                    //вытаскиваем продукты из всех подкатегорий через рекурсию
                    SearchProduct(catolPage.Id);
                }

                //Получаем полный список продуктов


            }

            rezult = GenerateTableProduct();

            return rezult;
        }
        /*Загрузка спеска полезных статей*/
        public string GetInfoTable()
        {
            string rezult = "";
            db = new Monolit.u7716449_monolitEntities();
            List<InfoArticle> spis = db.MenuSite.Where(x => x.Visible && x.LevelMenu==3 && x.Template=="Информация").Select(o => new InfoArticle
            {
                date = (DateTime)o.ContextPage.FirstOrDefault().Date,
                URL = o.URL,
                Url_Image = o.ContextPage.FirstOrDefault().UrlDescriptImg,
                Description = o.ContextPage.FirstOrDefault().SmallDescript.Length > 150 ? o.ContextPage.FirstOrDefault().SmallDescript.Substring(0, 150) : o.ContextPage.FirstOrDefault().SmallDescript,
                Header = o.ContextPage.FirstOrDefault().Header
            }).OrderBy(x=>x.date).ToList();
            //Генерация полного списка статей
            TagBuilder tag = new TagBuilder("table");
            tag.AddCssClass("tab_art");
            foreach (var st in spis)
            {
                tag.InnerHtml += "<tr><td rowspan=\"2\"  valign=\"top\"  style=\"padding: 1em 10px; \"><img src=" + st.Url_Image + " /></td>";
                tag.InnerHtml += "<td style =\" padding-top:1em;\"><span style=\"color:#D76366;margin: 0 10px 0 0;font-size:18px;\">" + st.date.ToShortDateString() + "</span><span style=\"color:#1f1e1e;font-size:18px;font-weight:600;\">"+st.Header+ "</span>  </td></tr> ";
                tag.InnerHtml += "<tr><td style=\"padding-bottom:1em; \">" + st.Description+ "...<a href=\"/Information/" + st.URL+"\">Подробнее</a></td></tr>";
                tag.InnerHtml += "<tr style =\" border-bottom:1px solid #d6d0d0;\"><td></td><td></td></tr>";
            }
            rezult = tag.ToString();
            return rezult;
        }

        /*
         <tr>
                    <td rowspan="2" valign="top" ; style="padding:3px 10px;"><img src="@item.Url_Image" /></td>
                    <td>
                        <span style="color:#D76366;margin: 0 10px 0 0;font-size:18px;">@item.date.ToShortDateString()</span>
                        <span style="color:#1f1e1e;font-size:18px;font-weight:600;">@item.Header</span>

                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom:1em;">
                        @item.Description...
                    @Html.RouteLink("Подробнее", new { controller = "Home", action = "InfoPage", id = item.URL })
                </td>
            </tr>
             */



        private void SearchProduct(int parrentId)
        {
            db = new Monolit.u7716449_monolitEntities();
            List<CategoryProduct> spis = db.CategoryProducts.Where(x => (x.Visible) && x.ParentId == parrentId).Select(o => o).ToList();
            foreach (var z in spis)
            {
              List<PriceProduct> prTemp = product.Where(x => x.CategoryId == z.Id).OrderBy(x => x.Position).Select(o => new PriceProduct
                {
                    Currency = o.Currency,
                    IE = o.IE,
                    Name = o.Name,
                    NameGroup = o.CategoryProduct.Name,
                    PositionGroupName = o.CategoryProduct.Position,
                    PositionName = o.Position,
                    Price = (decimal)o.Price
                }).ToList<PriceProduct>();
                pr = pr.Concat(prTemp).ToList();
                SearchProduct(z.Id);
            }
        }
        // генерация HTML таблицы продукции
        private string GenerateTableProduct()
        {
            var mod = pr.GroupBy(x => x.NameGroup);
            TagBuilder tag = new TagBuilder("table");
            tag.AddCssClass("tablePrice");
            tag.InnerHtml += "<tr><th>Вид работ</th><th>Единица измерения</th><th>Цена, руб.*</th></tr>";
            foreach (var item in mod)
            {
                tag.InnerHtml += "<tr><td class=\"HeadTd\" colspan = \"3\" style = \"text-align:center;background-color:#ee7f80;color:#fff;\" >" + item.Key + "</td></tr>";
                foreach (var t in item)
                {
                    tag.InnerHtml += "<tr><td>" + t.Name + "</td><td>" + t.IE + "</td><td>" + t.Price + "</td></tr>";
                }
            }
            return tag.ToString()+ "<p style=\"font-weight:600;margin-top:1em;\">Предусмотрена система скидок зависящая от объема работ</p>";
        }

    }
}