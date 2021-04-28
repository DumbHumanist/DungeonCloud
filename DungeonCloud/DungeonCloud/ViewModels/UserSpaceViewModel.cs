using DungeonCloud.Infrastructure;
using DungeonCloud.Models;
using DungeonCloud.Models.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syroot.Windows.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace DungeonCloud.ViewModels
{
    class UserSpaceViewModel : Caliburn.Micro.PropertyChangedBase
    {

        private FileSystemInfoExt selectedItem;

        public FileSystemInfoExt SelectedItem 
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange();
            }
        }

       



        public UserSpaceViewModel()
        {
            
        }



        public void BackButtonClick()
        {
            try
            {
                UserDirectorySingletone.Instance.CurrentDirectory = UserDirectorySingletone.Instance.CurrentDirectory.GetParent();
                
            }
            catch
            {
                return;
            }
        }

        public void SelectedItemDoubleClick()
        {
            try
            {
                if (Path.GetExtension(SelectedItem.FSI.Path) == "")
                {
                    DungeonDirectoryInfo SubDir = UserDirectorySingletone.Instance.CurrentDirectory.GetChildByName(SelectedItem.FSI.Name);
                    UserDirectorySingletone.Instance.CurrentDirectory = SubDir;
                   
                }
                else if(Path.GetExtension(SelectedItem.FSI.Name) == ".png")
                {
                    DownloadButtonClick();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(@"C:\Documents and Settings\All Users\Documents\My Pictures\Sample Pictures\Water Lilies.jpg");
                    bitmap.EndInit();
                    ThemeSingleton.Instance.Image = bitmap;
                    ViewSingleton.Instance.CurrentView = ViewSingleton.Instance.imageView; 
                }

            }
            catch
            {
                return;
            }
        }

        public async void DownloadButtonClick()
        {
            if (SelectedItem != null)            
            {
                string downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
                byte[] fileBytes = await Task<byte[]>.Run(() =>
                SessionSingleton.Instance.NM.DownloadFile(UserDirectorySingletone.Instance.UD,
                SelectedItem.FSI.Path.Substring(SelectedItem.FSI.Path.IndexOf('\\')),
                Convert.ToInt32(SelectedItem.FSI.GetParent().ChildrenFiles.Where(a => a.Path == SelectedItem.FSI.Path).FirstOrDefault().FileSize)));

                await Task.Factory.StartNew(() => File.WriteAllBytes(downloadsPath + '\\' + SelectedItem.FSI.Name, fileBytes));
            }
        }

        public async void UploadButtonClick()
        {
            string downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
            string filePath;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
                else
                    return;
            }

            FileInfo fileInfoForHumans = new FileInfo(filePath);

            DungeonFileInfo fileInfo = new DungeonFileInfo()
            {
                Path = fileInfoForHumans.FullName,
                Name = fileInfoForHumans.Name,
                FileSize = fileInfoForHumans.Length
            };

            string pathFromRoot;

            try
            {
                pathFromRoot =
                    UserDirectorySingletone.Instance.CurrentDirectory.Path.Substring(
                        UserDirectorySingletone.Instance.CurrentDirectory.Path.IndexOf('\\'));
            }
            catch
            {
                pathFromRoot = "";
            }

            await Task.Factory.StartNew(() =>
            SessionSingleton.Instance.NM.UploadNewFile(UserDirectorySingletone.Instance.UD,
                fileInfo,
                pathFromRoot,
                filePath));
        }
    }
}
