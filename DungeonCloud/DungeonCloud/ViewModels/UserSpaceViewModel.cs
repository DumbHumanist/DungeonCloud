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

        public async void SelectedItemDoubleClick()
        {
            string downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
            downloadsPath += ("\\" + SelectedItem.FSI.Name);

            string picExetensions = ".png .jpg .jpeg .bmp .gif";

            try
            {
                if (Path.GetExtension(SelectedItem.FSI.Path) == "")
                {
                    DungeonDirectoryInfo SubDir = UserDirectorySingletone.Instance.CurrentDirectory.GetChildByName(SelectedItem.FSI.Name);
                    UserDirectorySingletone.Instance.CurrentDirectory = SubDir;
                   
                }
                else if(picExetensions.Contains(Path.GetExtension(SelectedItem.FSI.Name)))
                {
                    int fileSize = Convert.ToInt32(SelectedItem.FSI.GetParent().ChildrenFiles.Where(a => a.Name == SelectedItem.FSI.Name).FirstOrDefault().FileSize);

                    byte[] fileBytes = await Task<byte[]>.Run(() =>
                    SessionSingleton.Instance.NM.DownloadFile(UserDirectorySingletone.Instance.UD,
                    SelectedItem.FSI.Path.Substring(SelectedItem.FSI.Path.IndexOf('\\')),
                    fileSize));

                    await Task.Factory.StartNew(() => File.WriteAllBytes(downloadsPath, fileBytes));


                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(downloadsPath);
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
                int fileSize = Convert.ToInt32(SelectedItem.FSI.GetParent().ChildrenFiles.Where(a => a.Name == SelectedItem.FSI.Name).FirstOrDefault().FileSize);
                string downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
                byte[] fileBytes = await Task<byte[]>.Run(() =>
                SessionSingleton.Instance.NM.DownloadFile(UserDirectorySingletone.Instance.UD,
                SelectedItem.FSI.Path.Substring(SelectedItem.FSI.Path.IndexOf('\\')),
                fileSize));

                await Task.Factory.StartNew(() => File.WriteAllBytes(downloadsPath + '\\' + SelectedItem.FSI.Name, fileBytes));
            }
        }

        public async void UploadButtonClick()
        {
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
                    UserDirectorySingletone.Instance.CurrentDirectory.Path;
            }
            catch
            {
                pathFromRoot = "";
            }

            UserDirectorySingletone.Instance.UD = await Task.Factory.StartNew(() =>
            SessionSingleton.Instance.NM.UploadNewFile(UserDirectorySingletone.Instance.UD,
                fileInfo,
                pathFromRoot,
                filePath));
        }


        public void DeleteButtonClick()
        {
            try
            {
                if (Path.GetExtension(SelectedItem.FSI.Path) == "")
                    UserDirectorySingletone.Instance.UD = SessionSingleton.Instance.NM.DeleteFolder(UserDirectorySingletone.Instance.UD,
                        SelectedItem.FSI.Path.Substring(SelectedItem.FSI.Path.IndexOf('\\')));
                else
                    UserDirectorySingletone.Instance.UD = SessionSingleton.Instance.NM.RemoveFile(UserDirectorySingletone.Instance.UD,
                        SelectedItem.FSI.Path.Substring(SelectedItem.FSI.Path.IndexOf('\\')));
            }
            catch { }
        }

        public string NewFolderName { get; set; }

        public void CreateFolderButtonClick()
        {
            try
            {
                UserDirectorySingletone.Instance.UD = SessionSingleton.Instance.NM.CreateNewFolder(UserDirectorySingletone.Instance.UD,
                    UserDirectorySingletone.Instance.CurrentDirectory.Path + '\\' + NewFolderName);
            }
            catch
            {

            }
        }
    }
}
