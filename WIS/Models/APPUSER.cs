using System;
using Xamarin.Forms;

namespace WIS.Models
{
    public class APPUSER
    {
        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string user_type { get; set; }

        public string username { get; set; }
        public string identifier { get; set; }
        public string password { get; set; }

        public string email { get; set; }
        public string access_token { get; set; }
        public string imageurl;
        public bool reset_requested { get; set; }

        public string remember_me_token { get; set; }

        public StreamImageSource ProfilePicture{
            get
            {
                return new AvatarImageSource(firstname,Color.White,Color.Black,48);
            }
        }
    }
}
