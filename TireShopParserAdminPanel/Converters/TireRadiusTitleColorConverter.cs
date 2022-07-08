using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TireShopParserAdminPanel.Converters
{
    public class TireRadiusTitleColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string title = ((string)value).ToUpperInvariant();

            if (title.Contains("R13"))
            {
                return Brushes.Red;
            }
            if (title.Contains("R14"))
            {
                return Brushes.Green;
            }
            if (title.Contains("R15"))
            {
                return Brushes.Yellow;
            }
            if (title.Contains("R16"))
            {
                return Brushes.Blue;
            }
            if (title.Contains("R17"))
            {
                return Brushes.Gray;
            }
            if (title.Contains("R18"))
            {
                return Brushes.Violet;
            }
            if (title.Contains("R19"))
            {
                return Brushes.Gold;
            }
            if (title.Contains("R20"))
            {
                return Brushes.Silver;
            }

            return Brushes.Black;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
