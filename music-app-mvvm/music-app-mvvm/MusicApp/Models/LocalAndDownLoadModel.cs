using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MusicApp.Common;
using MusicApp.Models;

namespace MusicApp.Models
{
    public class LocalAndDownLoadModel:NotifyBase
    {
        private List<SongModel> _localsonglist;
        public List<SongModel> localsonglist
        {
            get { 
                if(_localsonglist == null)  _localsonglist = new List<SongModel>();
                return _localsonglist; }
            set
            {
                _localsonglist = value;
                DoNotify();
            }
        }

        private Visibility _localmusic_Vis=Visibility.Visible;
        public Visibility localmusic_Vis
        {
            get
            {
                return _localmusic_Vis;
            }
            set { 
                _localmusic_Vis = value;
                DoNotify();
            }
        }

        private Visibility _downloaded_Vis;
        public Visibility downloaded_Vis
        {
            get
            {
                return _downloaded_Vis;
            }
            set
            {
                _downloaded_Vis = value;
                DoNotify();
            }
        }

 
        private int _selected_index;
        public int selected_index
        {
            get { return _selected_index; }
            set
            {
                _selected_index = value;
                DoNotify("selected_index");
            }
        }
    }
}
