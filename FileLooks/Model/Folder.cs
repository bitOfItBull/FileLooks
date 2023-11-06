
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FileLooks.Model
{
    public class Folder : NotifyBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public ObservableCollection<Folder> SubFolders { get; set; }
        public ObservableCollection<FileItem> SubFiles { get; set; }

        public CommandBase Cmd_Item_Click { get; set; }
        
    }
}
