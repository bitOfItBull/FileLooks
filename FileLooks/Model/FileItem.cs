using CommonLib.Common;
using FileLooks.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileLooks.Model
{
    public class FileItem : NotifyBase
    {

        public string Name { get; set; }

        // public string Path { get; set; }



        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

    }
}
