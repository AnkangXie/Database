using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::MusicApp.Common;
using global::MusicApp.Models.Vo;
using MusicApp.Common;
using MusicApp.Models.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



    namespace MusicApp.Models
    {
        class VideoPageModel : NotifyBase
        {
            private List<Video> _videoList;
            public List<Video> VideoList
            {
                get { 
                if (_videoList == null) _videoList = new List<Video>();
                return _videoList;
            }
                set
                {
                    _videoList = value;
                    DoNotify();
                }
            }
        }
        public class Video : NotifyBase
    {

            private int _Id;
            public int Id {
            get
            {
                return  this._Id;
            }
            set
            {
                _Id = value;
                DoNotify();
            }
            }

            private string _Cover;
            public string Cover
        {
            get
            {
                return _Cover;
            }
            set
            {
                _Cover = value;
                DoNotify();
            }
            
             }

            private string _Name;
            public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                _Name= value;
                DoNotify();
            }
        
        }
            public int PlayCount { get; set; }
            public string BriefDesc { get; set; }
            public string Desc { get; set; }

            private string _ArtistName;
            public string ArtistName
        {
            get
            {
                return  this._ArtistName;
            }
            set
            {
                _ArtistName= value;
                DoNotify();
            }
        }
            public int ArtistId { get; set; }
            public int Duration { get; set; }
            public int Mark { get; set; }
            public bool Subed { get; set; }
            public List<Artist> Artists { get; set; }

            public Video(string json)
            {
                var jsonDocument = JsonDocument.Parse(json);
                var root = jsonDocument.RootElement;

                Id = root.GetProperty("id").GetInt32();
                Cover = root.GetProperty("cover").GetString();
                Name = root.GetProperty("name").GetString();
                PlayCount = root.GetProperty("playCount").GetInt32();
                BriefDesc = root.GetProperty("briefDesc").GetString();
                Desc = root.GetProperty("desc").GetString();
                ArtistName = root.GetProperty("artistName").GetString();
                ArtistId = root.GetProperty("artistId").GetInt32();
                Duration = root.GetProperty("duration").GetInt32();
                Mark = root.GetProperty("mark").GetInt32();
                //Subed = root.GetProperty("subed").GetBoolean();

                Artists = new List<Artist>();

                var artistsArray = root.GetProperty("artists");
                foreach (var artistElement in artistsArray.EnumerateArray())
                {
                    var artist = new Artist
                    {
                        id = artistElement.GetProperty("id").GetInt32(),
                        name = artistElement.GetProperty("name").GetString(),
                        //alias = new List<string>(artistElement.GetProperty("alias").EnumerateArray().Select(a => a.GetString()))
                        //transNames = artistElement.GetProperty("transNames").EnumerateArray().Select(t => t.GetString()).ToList()
                    };

                    Artists.Add(artist);
                }
            }

        }
    }

