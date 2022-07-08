using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireShopParserAdminPanel.Models
{
    public class SiteParser
    {
        /// <summary>
        /// Get page node by URL
        /// </summary>
        /// <param name="url">Page Url</param>
        /// <returns></returns>
        private HtmlNode GetPageNode(string url)
        {
            var web = new HtmlWeb();
            var document = web.Load(url);
            return document.DocumentNode;
        }

        private Specification GetSpecificationFromLiNode(HtmlNode liNode)
        {
            var name = liNode.QuerySelector("span").InnerText.Trim();
            var value = liNode.QuerySelector("strong").InnerText.Trim();

            switch (name)
            {
                case "Бренд":
                    return new Specification { Title = Specification.Brand, Value = value };
                case "Тип авто":
                    return new Specification { Title = Specification.CarType, Value = value };
                case "Сезон":
                    return new Specification { Title = Specification.Season, Value = value };
                case "Год выпуска":
                case "Рік випуску":
                    return new Specification { Title = Specification.Year, Value = value };
                //TODO Another specification type handle
                default:
                    return new Specification { Title = name, Value = value };
            }
        }

        public Product ParseProductPage(string url)
        {
            var page = GetPageNode(url);

            var product = new Product();

            product.SiteId =
                Convert.ToUInt32(page.QuerySelector("[data-product-id]")?.Attributes["data-product-id"]?.Value ?? "0");
            product.Price =
                Convert.ToDecimal(page.QuerySelector("[data-product-price]").Attributes["data-product-price"]?.Value
                    .Replace(".", ",") ?? "0");
            product.Quantity =
                Convert.ToInt32(page.QuerySelector("#jshq").InnerText);
            product.Description = page.QuerySelector("#tab1").InnerHtml;

            var specificationNodes = page.QuerySelectorAll("#tab2 li");

            foreach (HtmlNode liNode in specificationNodes)
            {
                product.Specifications.Add(GetSpecificationFromLiNode(liNode));
            }

            return product;
        }

        public List<Product> ParseCatalogPage(string url)
        {
            var page = GetPageNode(url);

            var products = new List<Product>();

            var dataNodes = page.QuerySelectorAll("div#main-catalog-section div.col-lg-4");

            foreach (HtmlNode node in dataNodes)
            {
                var product = new Product();
                product.SiteId = Convert.ToUInt32(node.Attributes["id-product"]?.Value ?? string.Empty);
                product.Title = node.QuerySelector("div.wr-name-pr a").InnerText.Trim();
                product.Price =
                    Convert.ToDecimal(node.QuerySelector("div.price").Attributes["data-price"]?.Value ?? "0");
                product.Url = node.QuerySelector("div.wr-name-pr a").Attributes["href"]?.Value ?? string.Empty;
                product.ImageSrc = node.QuerySelector("div.wr-img a img").Attributes["data-src"]?.Value ?? string.Empty;

                var dataList = node.QuerySelectorAll("div.wr-params-pr ul li");

                foreach (HtmlNode liNode in dataList)
                {
                    //TODO USE GetSpecificationFromLiNode() method
                    var name = liNode.QuerySelector("span").InnerText.Trim();
                    switch (name)
                    {
                        case "Бренд":
                            var brand = liNode.QuerySelector("strong").InnerText.Trim();
                            product.Specifications.Add(new Specification
                            { Title = Specification.Brand, Value = brand });
                            break;
                        case "Тип авто":
                            var carType = liNode.QuerySelector("strong").InnerText.Trim();
                            product.Specifications.Add(new Specification
                            { Title = Specification.CarType, Value = carType });
                            break;
                        case "Сезон":
                            var season = liNode.QuerySelector("strong").InnerText.Trim();
                            product.Specifications.Add(new Specification
                            { Title = Specification.Season, Value = season });
                            break;
                        case "Год выпуска":
                        case "Рік випуску":
                            var year = liNode.QuerySelector("strong").InnerText.Trim();
                            product.Specifications.Add(new Specification
                            { Title = Specification.Year, Value = year });
                            break;
                    }
                }
                products.Add(product);
            }

            return products;
        }
    }
}
