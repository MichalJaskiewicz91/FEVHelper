using FEVHelper.Directories;
using FEVHelper.Directories.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FEVHelper
{        /// <summary>
         /// Convert a full path to a specific image type of a drive, folder or file
         /// </summary>
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            // By default, we presume an image
            var image = "Images/file.png";

            // If the name is blank, we presume it's a drive as we cannot have a blank file or folder name

            switch ((DirectoryItemType)value)
            {
                case DirectoryItemType.Drive:
                    image = "Images/drive.png";
                    break;
                case DirectoryItemType.Folder:
                    image = "Images/folder-closed.png";
                    break;
            }

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
