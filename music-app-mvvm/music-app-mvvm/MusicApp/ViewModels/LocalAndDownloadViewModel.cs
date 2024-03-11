using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicApp.Models;
using MusicApp.Common;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MusicApp.ViewModels
{
    public class LocalAndDownloadViewModel
    {
        public static LocalAndDownloadViewModel This;
        public LocalAndDownLoadModel localAndDownLoadModel { get; set; }

        
        public CommandBase SelectMusicCommand { get; set; }

        public CommandBase SelectFolderCommand { get; set; }

        public CommandBase SwitchToLocalCommand { get; set; }

        public CommandBase SwitchToDownloadCommand { get; set; }

        public CommandBase CleanListCommand { get; set; }
        public LocalAndDownloadViewModel()
        {
            This = this;
            localAndDownLoadModel = new LocalAndDownLoadModel();

            SelectMusicCommand = new CommandBase();
            SelectMusicCommand.DoExecute = new Action<object>((o) => SelectMusic());
            SelectMusicCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            SelectFolderCommand = new CommandBase();
            SelectFolderCommand.DoExecute = new Action<object>((o) => SelectFolder());
            SelectFolderCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            SwitchToLocalCommand = new CommandBase();
            SwitchToLocalCommand.DoExecute=new Action<object>((o) => SwitchToLocal());
            SwitchToLocalCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            SwitchToDownloadCommand=new CommandBase();
            SwitchToDownloadCommand.DoExecute=new Action<object>((o) => SwitchToDownload());
            SwitchToDownloadCommand.DoCanExecute=new Func<object, bool>((o) => { return true; });

            CleanListCommand= new CommandBase();
            CleanListCommand.DoExecute= new Action<object>((o) => Clean());
            CleanListCommand.DoCanExecute= new Func<object, bool>((o) => { return true; });
        }

        public void SelectMusic()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框的标题
            openFileDialog.Title = "选择MP3文件";

            // 设置对话框的筛选器，限定用户只能选择MP3文件
            openFileDialog.Filter = "MP3文件|*.mp3|所有文件|*.*";

            // 显示对话框，并获取用户的操作结果
            System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();

            // 根据用户的操作结果进行处理
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // 用户点击了确定按钮
                string selectedMp3Path = openFileDialog.FileName;
                new Thread(() =>
                {
                    List<string> list = new List<string>
                {
                    selectedMp3Path
                 };
                    localAndDownLoadModel.localsonglist=  SongPlayListViewModel.This.PlayLocalAudio(list);
                }).Start();
            }
            else
            {
                // 用户点击了取消按钮或关闭了对话框
                // System.Windows.MessageBox.Show("选择MP3文件操作被取消", "选择MP3文件取消", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SelectFolder()
        {

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // 设置对话框的标题
            folderBrowserDialog.Description = "选择文件夹";

            // 显示对话框，并获取用户的操作结果
            DialogResult result = folderBrowserDialog.ShowDialog();

            // 根据用户的操作结果进行处理
            if (result == System.Windows.Forms.DialogResult.OK)
            {
/*                new Thread(() =>
                {*/
                    // 用户点击了确定按钮
                    string selectedFolder = folderBrowserDialog.SelectedPath;

                    // 扫描文件夹下的MP3文件
                    List<string> mp3Files = ScanMp3Files(selectedFolder);

                    localAndDownLoadModel.localsonglist =  SongPlayListViewModel.This.PlayLocalAudio(mp3Files);
           //     }).Start();
            }
            else
            {
                // 用户点击了取消按钮或关闭了对话框

                //System.Windows.MessageBox.Show("选择文件夹操作被取消", "选择文件夹取消", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void SwitchToLocal()
        {
            localAndDownLoadModel.localmusic_Vis = System.Windows.Visibility.Visible;
            localAndDownLoadModel.downloaded_Vis = System.Windows.Visibility.Hidden;
        }

        public void SwitchToDownload()
        {
            localAndDownLoadModel.localmusic_Vis = System.Windows.Visibility.Hidden;
            localAndDownLoadModel.downloaded_Vis = System.Windows.Visibility.Visible;
        }

        public void Clean()
        {
            localAndDownLoadModel.localsonglist = new List<SongModel>();
        }
        private List<string> ScanMp3Files(string folderPath)
        {
            try
            {
                // 使用 Directory.GetFiles 获取文件夹下的所有文件
                string[] allFiles = Directory.GetFiles(folderPath);

                // 使用 LINQ 过滤出MP3文件
                List<string> mp3Files = allFiles
                    .Where(file => file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                return mp3Files;
            }
            catch (Exception ex)
            {
                // 处理异常，可以记录日志或者根据实际情况进行其他处理
                Console.WriteLine($"扫描MP3文件时发生异常: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
