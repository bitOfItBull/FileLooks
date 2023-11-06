using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FileLooks.Converter
{
    public sealed class UriToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            BitmapImage image = new BitmapImage();

            image.BeginInit();
            image.UriSource = new System.Uri((string)value);
            image.DecodePixelWidth = 10;
            image.CreateOptions = BitmapCreateOptions.DelayCreation;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();

            //Uri uri = (Uri)value;
            //BitmapImage bmp = new BitmapImage();
            //bmp.DecodePixelHeight = 50; // 确定解码高度，宽度不同时设置
            //bmp.DecodePixelWidth = 50;
            //bmp.BeginInit();
            //// 延迟，必要时创建
            //bmp.CreateOptions = BitmapCreateOptions.DelayCreation;
            //bmp.CacheOption = BitmapCacheOption.OnLoad;
            //bmp.UriSource = uri;
            //bmp.EndInit(); //结束初始化
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
