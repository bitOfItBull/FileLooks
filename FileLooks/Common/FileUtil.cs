using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FileLooks.Common
{
    public class FileUtil
    {

        public static bool IsImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            if (extension != null)
            {
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; 

                if (Array.IndexOf(imageExtensions, extension.ToLower()) != -1)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
