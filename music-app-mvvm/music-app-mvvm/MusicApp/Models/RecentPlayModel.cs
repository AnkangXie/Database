using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class RecentPlayModel:NotifyBase
    {
        private List<Simple_SongModel> _PlayList;

        public List<Simple_SongModel> PlayList
        {
            get {
                if (_PlayList == null)
                {
                    _PlayList= new List<Simple_SongModel>();
                }
                return _PlayList;
            }
            set
            {
                _PlayList = value;
                DoNotify();
            }
        }

        private int _Count;

        public int Count
        {
            get { return _Count; }

            set
            {
                _Count = value;
                DoNotify();
            }
            
        }
    }

    

    public class Simple_SongModel:NotifyBase
    {
        private string _SongName;

        public string SongName
        {
            get { return _SongName; } 
            set
            {
                _SongName = value;
                DoNotify();
            }
        }

        private string _Singer;

        public string Singer
        {
            get { return _Singer; }
            set
            {
                _Singer = value;
                DoNotify();
            }
        }

        private string _PlayTime;

        public string PlayTime
        {
            get { return _PlayTime; }
            set
            {
                _PlayTime = value;
                DoNotify();
            }
        }
    }
}
