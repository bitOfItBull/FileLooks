using FileLooks.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FileLooks.Converter
{
    public class Path2Bitmap : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            string path = value as string;

            if (!FileUtil.IsImageFile(path))
            {

                path = "pack://application:,,,/Icons/file.png";

            }
            

            BitmapImage retImg = new BitmapImage();
            retImg.BeginInit();
            retImg.UriSource = new Uri(path,UriKind.Absolute);
            retImg.DecodePixelWidth = 100;
            retImg.EndInit();
            retImg.Freeze();

            return retImg;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return Binding.DoNothing;
        }
    }
}
