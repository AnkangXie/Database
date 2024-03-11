using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicApp.Models
{
    public class LoginModel : NotifyBase
    {
        private static long _userID;
        public static long userID
        {
            get { return _userID; }
            set
            {
                _userID = value;
            }
        }
        private string _username;
        public string username
        {
            get { return _username; }
            set
            {
                _username = value;
                DoNotify("username");
            }
        }
        private string _useremail;
        public string useremail
        {
            get { return _useremail; }
            set
            {
                _useremail = value;
                DoNotify("useremail");
            }
        }
        private SecureString _userpassword;
        public SecureString userpassword
        {
            get { return _userpassword; }
            set
            {
                _userpassword = value;
                DoNotify("userpassword");
            }
        }
        private SecureString _userrptpassword;

        public SecureString userrptpassword
        {
            get { return _userrptpassword; }
            set
            {
                _userrptpassword = value;
                DoNotify("userrptpassword");
            }
        }
        private Visibility _rpt_visibility = Visibility.Hidden;
        public Visibility rpt_visibility
        {
            get { return _rpt_visibility; }
            set
            {
                _rpt_visibility = value;
                DoNotify("rpt_visibility");
            }
        }

        private bool _canAdmit = false;

        public bool canAdmit
        {
            get { return _canAdmit; }
            set
            {
                _canAdmit = value;
                DoNotify("canAdmit");
            }
        }

        private Login_info _info;
        public Login_info info
        {
            get { return _info; }
            set
            {
                _info = value;
                DoNotify();
            }
        }

        private string _interact_info;

        public string interact_info
        {
            get { return _interact_info; }
            set
            {
                _interact_info = value;
                DoNotify("interact_info");
            }
        }
        public enum Login_info
        {
            Content_Pas_Uneql_Rpt,
            Content_Username_Exited,
            Content_Email_Exited,
            Content_Can_Not_Find_Target,
            Content_Password_Wrong,
            Content_Successfully_Login,
            Content_Successfully_Register,
            Content_Already_Registered,
            Content_DB_Failure,
        }
    }
}
