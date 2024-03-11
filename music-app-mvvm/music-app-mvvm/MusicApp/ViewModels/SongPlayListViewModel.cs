using MusicApp.Common;
using MusicApp.Control;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Media3D;
using NAudio.Wave;
using TagLib;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Windows.Navigation;

namespace MusicApp.ViewModels
{
    public class SongPlayListViewModel
    {
        public static SongPlayListViewModel This { get; set; }
        public SongPlayListModel Model { get; set; }

        //播放歌曲
        public CommandBase PlaySongClickCommand { get; set; }

        //点击删除一个
        public CommandBase DeleteSongClickCommand { get; set; }

        //清空列表
        public CommandBase ClosePlayListCommand { get; set; }

        public CommandBase UpdatePlayListCommand { get;set; }
        public SongPlayListViewModel()
        {
            This = this;
            Model = new SongPlayListModel();

            //播放歌曲
            PlaySongClickCommand = new CommandBase();
            PlaySongClickCommand.DoExecute = new Action<object>((o) => 
            {
                var item = Model.SongLists[(int)o];
                if (item == null) return;
                NextSongPlay(item.SongId, false, PlayModel.SimpleLoop);
            });
            PlaySongClickCommand.DoCanExecute = new Func<object, bool>((o) => {return true; });

            //点击删除一个
            DeleteSongClickCommand = new CommandBase();
            DeleteSongClickCommand.DoExecute = new Action<object>((o) =>
            {
                var list = new List<SongModel>(Model.SongLists);
                list.RemoveAt((int)o);
                Model.SongLists = list;
            });
            DeleteSongClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //清空列表
            ClosePlayListCommand = new CommandBase();
            ClosePlayListCommand.DoExecute = new Action<object>((o) =>
            {
                Model.SongLists = new List<SongModel>();
                Model.SongPlayCount = "总" + Model.SongLists.Count + "首";
                PlayerViewModel.This.StopPlay();
            });
            ClosePlayListCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            UpdatePlayListCommand = new CommandBase();
            UpdatePlayListCommand.DoExecute = new Action<object>((o) =>
            {
                Model.SongLists = Model.SongLists;
                Model.SongPlayCount= "总" + Model.SongLists.Count + "首";
            });
            UpdatePlayListCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


            //点击主窗体关闭正在播放列表
            MainWindowViewModel.GetInstance().BaseBorderMouseDownDelegate += new Action<object>((o) =>
            {
                Model.PlayListVisibility = Visibility.Collapsed;
            });

            //默认是关闭状态
            Model.PlayListVisibility = Visibility.Collapsed;
 
            
            //初始化数据
            Model.SongLists = InitJsonData.jsonDataModel.SongPlayList;

            //总条数
            Model.SongPlayCount = "总" + Model.SongLists.Count + "首";

            //播放器事件
            PlayerViewModel.This.PlayDelegate += new Action<SongModel>((o) =>
            {
                SetLisBoxColor(o);
            });
        }

        /// <summary>
        /// 播放下一首
        /// </summary>
        /// <param name="songId">当前播放完的歌曲id</param>
        /// <param name="isLast">是否是上一首</param>
        /// <param name="type"> ListLoop=列表循环 ,SimpleLoop=单曲循环 ,RandomPlay=随机循环 ,OrderPlay=顺序循环</param>
        public void NextSongPlay(string songId, bool isLast = false, PlayModel type = PlayModel.ListLoop)
        {
            //待播放列表长度
            int count = Model.SongLists.Count;
            if (count == 0) return;
            if (songId == null)
            {
                PlayerViewModel.This.InitPlay(Model.SongLists[0]);
                return;
            };
            //当前播放完的下标
            int index = Model.SongLists.FindIndex(t => t.SongId.Equals(songId));

            if (isLast)
            {
                if (index - 1 < 0)
                    PlayerViewModel.This.InitPlay(Model.SongLists[count - 1]);
                else
                    PlayerViewModel.This.InitPlay(Model.SongLists[index - 1]);
                return;
            }
            switch (type)
            {
                case PlayModel.ListLoop:
                    if (count > index + 1)
                        PlayerViewModel.This.InitPlay(Model.SongLists[index + 1]);
                    else
                        PlayerViewModel.This.InitPlay(Model.SongLists[0]);
                    break;
                case PlayModel.SimpleLoop:
                    PlayerViewModel.This.InitPlay(Model.SongLists[index]);
                    break;

                case PlayModel.RandomPlay:
                    int random = new Random().Next(0, count);
                    PlayerViewModel.This.InitPlay(Model.SongLists[random]);
                    break;
                case PlayModel.OrderPlay:
                    if (count > index + 1)
                        PlayerViewModel.This.InitPlay(Model.SongLists[index + 1]);
                    break;

            }
        }

        /// <summary>
        /// 更改列表状态
        /// </summary>
        /// <param name="model"></param>
        public void SetLisBoxColor(SongModel model)
        {
            //待播放歌曲列表
            List<SongModel> list = Model.SongLists;
            list.ForEach(item =>
            {
                if (item.IsSelected == true)
                {
                    item.IsSelected = false;
                }
            });
            SongModel songModel = list.Find(t => t.SongId.Equals(model.SongId));
            if (songModel == null) return;
            songModel.IsSelected = true;

        }

        /// <summary>
        /// 添加待播放歌曲播放列表
        /// </summary>
        /// <param name="idList">歌曲id</param>
        public void GetSongPlayList(List<string> idList)
        {
            new Thread(() =>
            {
                //格式化id
                StringBuilder builder = new StringBuilder();
                idList.ForEach(item =>
                {
                    builder.Append(item + ",");
                });
                string str = builder.ToString();
                string ids = str.Substring(0, str.Length - 1);
                //获取歌曲详细,歌曲头像\名称\作者
                string songDetailRes = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/song/detail?ids=" + ids);
                if (songDetailRes == null)
                {
                    return;
                }
                SongDetailResultModel detailModel = JsonConvert.DeserializeObject<SongDetailResultModel>(songDetailRes);

                //待播放歌曲列表
                List<SongModel> songPlayList = Model.SongLists;
                //临时变量
                List<SongModel> list = new List<SongModel>();
                for (int i = 0; i < idList.Count; i++)
                {
                    if (songPlayList == null) songPlayList = new List<SongModel>();
                    //是否已经存在播放列表
                    SongModel model = songPlayList.Find(t => t.SongId.Equals(idList[i]));
                    var detail = detailModel.songs.Find(t => t.id.ToString().Equals(idList[i]));
                    if (model != null)
                    {
                        list.Add(model);
                    }
                    else
                    {
                        SongModel newModel = new SongModel();
                        newModel.SongId = idList[i];
                        newModel.PicUrl = detail.al.picUrl;
                        newModel.SongName = detail.name;
                        newModel.Author = detail.ar[0].name;
                        newModel.SongTime = detail.dt;
                        newModel.FormatSongTime = StringUtil.FormatTimeoutToString(newModel.SongTime);
                        list.Add(newModel);
                    }

                }
                //之前播放列表添加到新列表中
                songPlayList.ForEach(item =>
                {
                    if (idList.Find(t => t.Equals(item.SongId)) == null)
                    {
                        list.Add(item);
                    }
                });

                //重新赋值
                Model.SongLists = list;
                Model.SongPlayCount = "总" + Model.SongLists.Count + "首";

                //保存待播放歌曲列表
                InitJsonData.jsonDataModel.SongPlayList = Model.SongLists;

                //开始播放
                PlayerViewModel.This.InitPlay(list[0]);

            }).Start();
        }

        //public void PlayLocalAudio(List<string> filePathList)
        //{
        //    new Thread(() =>
        //    {
        //        if (filePathList == null || filePathList.Count == 0)
        //        {
        //            Console.WriteLine("No audio files provided for playback.");
        //            return;
        //        }

        //        // 初始化播放列表
        //        List<SongModel> songPlayList = Model.SongLists;
        //        List<SongModel> list = new List<SongModel>();

        //        foreach (var filePath in filePathList)
        //        {
        //            try
        //            {
        //                // 获取本地音频文件信息
        //                var audioInfo = GetAudioFileInfo(filePath);

        //                if (audioInfo == null)
        //                {
        //                    Console.WriteLine($"Error getting audio info for file {filePath}");
        //                    continue;
        //                }

        //                // 是否已经存在在播放列表中
        //                SongModel model = songPlayList.Find(t => t.SongId.Equals(audioInfo.SongId));

        //                if (model != null)
        //                {
        //                    list.Add(model);
        //                }
        //                else
        //                {
        //                    // 创建新的 SongModel 对象
        //                    SongModel newModel = new SongModel
        //                    {
        //                        SongId = audioInfo.SongId,
        //                        PicUrl = audioInfo.PicUrl,
        //                        SongName = audioInfo.SongName,
        //                        Author = audioInfo.Author,
        //                        SongTime = audioInfo.SongTime,
        //                        FormatSongTime = audioInfo.FormatSongTime
        //                    };

        //                    list.Add(newModel);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error processing audio file {filePath}: {ex.Message}");
        //            }
        //        }

        //        // 将之前播放列表添加到新列表中
        //        songPlayList?.ForEach(item =>
        //        {
        //            if (filePathList.Find(t => t.Equals(item.SongId)) == null)
        //            {
        //                list.Add(item);
        //            }
        //        });

        //        // 更新播放列表和计数
        //        Model.SongLists = list;
        //        Model.SongPlayCount = $"Total {Model.SongLists.Count} songs";

        //        // 保存待播放歌曲列表
        //        InitJsonData.jsonDataModel.SongPlayList = Model.SongLists;

        //        // 开始播放
        //        PlayerViewModel.This.InitPlay(list[0]);

        //    }).Start();
        //}

        //private AudioFileInfo GetAudioFileInfo(string filePath)
        //{
        //    // 在实际应用中，你可能需要根据具体的音频文件格式（如MP3、WAV等）选择不同的获取信息的方法
        //    // 这里简单地返回一个包含文件名和时长的对象
        //    return new AudioFileInfo
        //    {
        //        SongId = Guid.NewGuid().ToString(), // 可以使用文件名等唯一标识
        //        PicUrl = "default_pic_url", // 替换为实际的图片URL
        //        SongName = System.IO.Path.GetFileNameWithoutExtension(filePath),
        //        Author = "Unknown", // 替换为实际的作者信息
        //        SongTime = GetAudioFileDuration(filePath),
        //        FormatSongTime = StringUtil.FormatTimeoutToString(GetAudioFileDuration(filePath)),
        //        FilePath = filePath
        //    };
        //}

        //private int GetAudioFileDuration(string filePath)
        //{
        //    try
        //    {
        //        using (var audioFile = new AudioFileReader(filePath))
        //        {
        //            // 获取音频文件的总时长（毫秒）
        //            int durationInMilliseconds = (int)audioFile.TotalTime.TotalMilliseconds;
        //            return durationInMilliseconds;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error getting audio file duration for file {filePath}: {ex.Message}");
        //        return 0; // 或者返回一个默认值，表示无法获取时长
        //    }
        //}
        public List<SongModel> PlayLocalAudio(List<string> filePathList)
        {
            List<SongModel> list = new List<SongModel>();
                if (filePathList == null || filePathList.Count == 0)
                {
                    return null;
                }

                // 初始化播放列表
                List<SongModel> songPlayList = Model.SongLists;

                foreach (var filePath in filePathList)
                {
                    try
                    {
                        // 获取本地音频文件信息
                        var audioInfo = GetAudioFileInfo(filePath);

                        if (audioInfo == null)
                        {
                            Console.WriteLine($"Error getting audio info for file {filePath}");
                            continue;
                        }

                        // 是否已经存在在播放列表中
                        SongModel model = songPlayList.Find(t => t.SongId.Equals(audioInfo.SongId));

                        if (model != null)
                        {
                            list.Add(model);
                        }
                        else
                        {
                        // 创建新的 SongModel 对象
                        SongModel newModel = new SongModel
                        {
                                SongId = audioInfo.SongId,
                                PicUrl = audioInfo.PicUrl,
                                SongName = audioInfo.SongName,
                                Author = audioInfo.Author,
                                Album= audioInfo.Album,
                                SongTime = audioInfo.SongTime,
                                FormatSongTime = audioInfo.FormatSongTime,
                                Lyric = "[00:00.00]本地音乐,无歌词",
                                LocalSongUrl = filePath
                            };
                            list.Add(newModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing audio file {filePath}: {ex.Message}");
                    }
                }

                // 将之前播放列表添加到新列表中
                songPlayList?.ForEach(item =>
                {
                    if (filePathList.Find(t => t.Equals(item.SongId)) == null)
                    {
                        list.Add(item);
                    }
                });
                // 更新播放列表和计数
                Model.SongLists = list;
                Model.SongPlayCount = $"总 {Model.SongLists.Count} 首";

                // 保存待播放歌曲列表
                InitJsonData.jsonDataModel.SongPlayList = Model.SongLists;

                // 开始播放
                PlayerViewModel.This.InitPlay(list[0]);

            
            return list;
        }

        /*private AudioFileInfo GetAudioFileInfo(string filePath)
        {
            // 在实际应用中，你可能需要根据具体的音频文件格式（如MP3、WAV等）选择不同的获取信息的方法
            // 这里简单地返回一个包含文件名和时长的对象
            return new AudioFileInfo
            {
                SongId = Guid.NewGuid().ToString(), // 可以使用文件名等唯一标识
                PicUrl = "default_pic_url", // 替换为实际的图片URL
                SongName = System.IO.Path.GetFileNameWithoutExtension(filePath),
                Author = "Unknown", // 替换为实际的作者信息
                SongTime = GetAudioFileDuration(filePath),
                FormatSongTime = StringUtil.FormatTimeoutToString(GetAudioFileDuration(filePath)),
                FilePath = filePath
            };
        }*/

        private AudioFileInfo GetAudioFileInfo(string filePath)
        {
            try
            {
                // 使用 TagLib# 获取MP3文件的元数据
                var file = TagLib.File.Create(filePath);

                // 获取文件名
                string fileName = Path.GetFileName(filePath);

                // 使用文件中的信息填充
                string songId = Guid.NewGuid().ToString();
                string songName = file.Tag.Title ?? Path.GetFileNameWithoutExtension(fileName);
                string author = file.Tag.FirstPerformer ?? "Unknown";
                string albumname=file.Tag.Album ?? "Unknown Album";
                string picUrl = "default_pic_url"; // 替换为实际的图片URL
                int songDuration = GetAudioFileDuration(filePath);
                string formattedDuration = StringUtil.FormatTimeoutToString(songDuration);

                return new AudioFileInfo
                {
                    SongId = songId,
                    PicUrl = picUrl,
                    SongName = songName,
                    Author = author,
                    Album=albumname,
                    SongTime = songDuration,
                    FormatSongTime = formattedDuration,
                    FilePath = filePath
                };
            }
            catch (Exception ex)
            {
                // 处理异常，可以记录日志或者根据实际情况进行其他处理
                Console.WriteLine($"获取音频文件信息时发生异常: {ex.Message}");
                return null;
            }
        }
        private int GetAudioFileDuration(string filePath)
        {
            try
            {
                using (var audioFile = new AudioFileReader(filePath))
                {
                    // 获取音频文件的总时长（毫秒）
                    int durationInMilliseconds = (int)audioFile.TotalTime.TotalMilliseconds;
                    return durationInMilliseconds;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting audio file duration for file {filePath}: {ex.Message}");
                return 0; // 或者返回一个默认值，表示无法获取时长
            }
        }
    }
}

