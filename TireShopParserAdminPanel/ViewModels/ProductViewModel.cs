using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using TireShopParserAdminPanel.Models;

namespace TireShopParserAdminPanel.ViewModels
{
    public class ProductViewModel : NotifyPropertyChangedBase
    {
        public Product Product { get; set; }

        public uint? SiteId
        {
            get => Product.SiteId;
            set
            {
                Product.SiteId = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => Product.Title;
            set
            {
                Product.Title = value;
                OnPropertyChanged();
            }
        }
        public string Brand
        {
            get => Product.Specifications.FirstOrDefault(s => s.Title == Specification.Brand)?.Value ?? "No brand";
        }

        public decimal Price
        {
            get => Product.Price;
            set
            {
                Product.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string ImageSrc
        {
            get => Product.ImageSrc;
            set
            {
                Product.ImageSrc = value;
                OnPropertyChanged(nameof(ImageSrc));
            }
        }

        public string TireRadius
        {
            get
            {
                var pattern = @"\sZ{0,1}(R\d\d)\w{0,1}\s";
                var match = Regex.Match(Title, pattern);
                return match.Groups[1]?.Value ?? "R00";
            }
        }

        public ObservableCollection<Specification> Specifications
        {
            get {
                var list = new ObservableCollection<Specification>();
                Product.Specifications.ToList().ForEach(specification =>
                {
                    list.Add(specification);
                });
                return list;
            }
        }
    }
}
