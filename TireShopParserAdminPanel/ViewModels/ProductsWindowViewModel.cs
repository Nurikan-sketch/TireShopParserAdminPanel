using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TireShopParserAdminPanel.Models;

namespace TireShopParserAdminPanel.ViewModels
{
    public class ProductsWindowViewModel : NotifyPropertyChangedBase
    {
        public List<Product> AllProducts = new List<Product>();

        public ObservableCollection<ProductViewModel> Products => FilteredProducts();
        

        private ProductViewModel _selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private string _findTitle;
        public string FindTitle
        {
            get { return _findTitle; }
            set
            {
                _findTitle = value;
                OnPropertyChanged(nameof(FindTitle));
                OnPropertyChanged(nameof(Products));
            }
        }

        private string _selectedBrand;
        public string SelectedBrand
        {
            get { return _selectedBrand; }
            set
            {
                _selectedBrand = value;
                OnPropertyChanged(nameof(Products));
                OnPropertyChanged(nameof(SelectedBrand));
            }
        }

        private ObservableCollection<ProductViewModel> FilteredProducts()
        {
            var filteredProducts = new ObservableCollection<ProductViewModel>();

            var products = AllProducts.AsEnumerable();

            if (!String.IsNullOrEmpty(FindTitle))
            {
                products = products.Where(p => p.Title.ToUpperInvariant().Contains(FindTitle.ToUpperInvariant()));
            }

            if (SelectedBrand != String.Empty)
            {
                products = products.Where(p =>
                {
                    var spec = p.Specifications.FirstOrDefault(s => s.Title == Specification.Brand);
                    return spec.Value == SelectedBrand;

                });
            }

            products.Select(p => new ProductViewModel { Product = p }).ToList().ForEach(wp => filteredProducts.Add(wp));
            return filteredProducts;
        }

        public ObservableCollection<string> Brands
        {
            get
            {
                var list = new ObservableCollection<string>() { String.Empty };
                AllProducts
                    .Select(p =>
                    {
                        var spec = p.Specifications.FirstOrDefault(s => s.Title == Specification.Brand);
                        return spec.Value;
                    })
                    .Distinct()
                    .OrderBy(b => b)
                    .ToList()
                    .ForEach(b => list.Add(b));
                return list;
            }
        }


        public async static Task<List<Product>> ParseSiteCatalogPageAsync(int page)
        {
            var parser = new SiteParser();
            var products = new List<Product>();
            await Task.Run(() =>
            {
                products.AddRange(parser.ParseCatalogPage($"https://tireshop.ua/ua/shini?page={page}"));
            });
            return products;
        }

        static List<Product> ParseSite()
        {
            var parser = new SiteParser();

            var products = new List<Product>();
            for (int i = 1; i <= 1; i++)
            {
                Console.WriteLine(i);
                products.AddRange(parser.ParseCatalogPage($"https://tireshop.ua/ua/shini?page={i}"));
            }

            return products;
        }

        public ProductsWindowViewModel()
        {

            //var parser = new SiteParser();
            //var products = ParseSite();
            //AllProducts = products;
            //AllProducts = XmlSerializerHelper.Deserialize<List<Product>>("products.xml");
            //AllProducts.ForEach(p => Products.Add(new ProductViewModel { Product = p }));
            //XmlSerializerHelper.Serialize("products.xml", products);
            SelectedBrand = String.Empty;
        }

        public ICommand TestCommand => new RelayCommand(
            //Делегат - Дія яка яка буде виконуватися коли натиснемо на кнопку
            x =>
            {
                MessageBox.Show(SelectedProduct.Title);
            },
            // Делегат - Функція, яка визначає чи можна цю дію зробити 
            x =>
            {
                return SelectedProduct != null;
            });

        private bool _isLoaded = false;


        public ICommand ParseCommand => new RelayCommand(
            //Делегат - Дія яка яка буде виконуватися коли натиснемо на кнопку
            x =>
            {
                _isLoaded = true;
                Task.Run(async () =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var products = await ParseSiteCatalogPageAsync(i);
                        AllProducts.AddRange(products);
                        OnPropertyChanged(nameof(Brands));
                        OnPropertyChanged(nameof(SelectedBrand));
                        OnPropertyChanged(nameof(Products));
                    }
                });
            },

            // Делегат - Функція, яка визначає чи можна цю дію зробити 
            x =>
            {
                return !_isLoaded;
            });

    }
}
