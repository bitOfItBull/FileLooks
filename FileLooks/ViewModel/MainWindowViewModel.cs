using CommonLib.Common;
using FileLooks.Common;
using FileLooks.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Data;

namespace FileLooks.ViewModel
{
    public class MainWindowViewModel : NotifyBase
    {


        /// <summary>
        /// select Navigation path
        /// </summary>
        private string select_path;
        public string SelectPath
        {
            get { return select_path; }
            set
            {
                select_path = value;
                DoNotifyPropertyChanged();
            }
        }


        /// <summary>
        /// select info path
        /// </summary>
        private string info_root_path;

        public string InfoRootPath
        {
            get { return info_root_path; }
            set
            {
                info_root_path = value;
                DoNotifyPropertyChanged();
            }
        }




        private bool isExpandedNavigation;

        public bool IsExpandedNavigation
        {
            get { return isExpandedNavigation; }
            set
            {
                isExpandedNavigation = value;
                DoNotifyPropertyChanged();
            }
        }



        public ObservableCollection<Folder> NavigationFolders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Folder> InfoFolders { get; set; }



        public IEventAction[] NaviagtionTreeViewActionsCommand { get; set; }



        public CommandBase Cmd_SelectPath { get; set; }
        public CommandBase Cmd_ClickItem { get; set; }


        public MainWindowViewModel()
        {

            Cmd_SelectPath = new CommandBase(Click_SelectPath);
            Cmd_ClickItem = new CommandBase(Click_Item);

            NavigationFolders = new ObservableCollection<Folder>();
            InfoFolders = new ObservableCollection<Folder>();
        }


        private void Click_Item(object o)
        {
            InfoFolders.Clear();
            Folder folder = (Folder)o;
            InfoRootPath = folder.Path;

            LoadDirFiles(folder);
        }


        /// <summary>
        /// 选择其实文件目录
        /// </summary>
        /// <param name="o"></param>
        private void Click_SelectPath(object o)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {

                this.NavigationFolders.Clear();

                SelectPath = folderBrowserDialog.SelectedPath;

                Folder root = new Folder();
                root.Path = SelectPath;
                root.Name = System.IO.Path.GetFileName(SelectPath);

                LoadDirsAndFiles(root);

                NavigationFolders.Add(root);
            }
        }


        /// <summary>
        /// 加载文件夹内文件及子文件夹
        /// </summary>
        /// <param name="parent"></param>
        private async void LoadDirsAndFiles(Folder parent)
        {
            parent.Cmd_Item_Click = this.Cmd_ClickItem;
            parent.SubFiles = new ObservableCollection<FileItem>();
            parent.SubFolders = new ObservableCollection<Folder>();
            try
            {
                //遍历子文件
                string[] files = Directory.GetFiles(parent.Path);
                foreach (var f in files)
                {
                    //只看图片
                    if (!FileUtil.IsImageFile(f))
                    {
                        continue;
                    }
                    FileItem fileItem = new FileItem();
                    fileItem.Name = System.IO.Path.GetFileName(f);
                    fileItem.Path = f;
                    parent.SubFiles.Add(fileItem);
                }

                //遍历子文件夹
                string[] dirs = Directory.GetDirectories(parent.Path);
                foreach (var d in dirs)
                {
                    Folder folder = new Folder();
                    folder.Path = d;
                    folder.Name = System.IO.Path.GetFileName(d);
                    parent.SubFolders.Add(folder);
                    await Task.Run(() =>
                    {
                        LoadDirsAndFiles(folder);
                    });
                }
            }
            catch { }
        }


        /// <summary>
        /// 所有文件夹单独只显示文件
        /// </summary>
        /// <param name="folder"></param>
        private  void LoadDirFiles(Folder folder)
        {

            //foreach (var f in folder.SubFiles)
            //{

            //    BitmapImage image = new BitmapImage();

            //    image.BeginInit();
            //    image.UriSource = new System.Uri(f.Path);
            //    image.DecodePixelWidth = 50;
            //    image.EndInit();
            //    image.Freeze();
            //    f.ImageSource = image;
            //}

            InfoFolders.Add(folder);

            foreach (var item in folder.SubFolders)
            {
                LoadDirFiles(item);
            }

        }


    }
}
