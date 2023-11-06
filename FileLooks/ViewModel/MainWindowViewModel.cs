using CommonLib.Common;
using FileLooks.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileLooks.ViewModel
{
    public class MainWindowViewModel : NotifyBase
    {


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


        public ObservableCollection<Folder> Folders { get; set; }



        public CommandBase Cmd_SelectPath { get; set; }


        public MainWindowViewModel()
        {

            Cmd_SelectPath = new CommandBase(Click_SelectPath);

            Folders = new ObservableCollection<Folder>();
        }

        private void Click_SelectPath(object o)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {

                this.Folders.Clear();

                string selectedPath = folderBrowserDialog.SelectedPath;
                SelectPath = selectedPath;

                Folder folder = new Folder();
                folder.Path = selectedPath;
                folder.Name = Path.GetFileName(selectedPath);
                folder.SubFolders = new ObservableCollection<Folder> { };
                Folders.Add(folder);

                LoadDirectories(SelectPath, folder);
            }
        }


        public void LoadDirectories(string path, Folder parent)
        {

            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                Folder folder = new Folder();
                folder.Path = dir;
                folder.Name = Path.GetFileName(dir);
                folder.SubFolders = new ObservableCollection<Folder> { };
                folder.SubFiles = new ObservableCollection<FileItem> { };
                LoadDirectories(dir, folder);


                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    FileItem fileItem = new FileItem();
                    fileItem.Name = Path.GetFileName(file);
                    fileItem.Path = file;
                    folder.SubFiles.Add(fileItem);
                }

                parent.SubFolders.Add(folder);
            }

            

        }


    }
}
