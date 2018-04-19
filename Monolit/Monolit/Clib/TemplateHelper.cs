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