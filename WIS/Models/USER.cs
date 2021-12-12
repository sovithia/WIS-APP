using System;
using Xamarin.Forms;

namespace WIS.Models
{
    public class USER
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string usertype { get; set; }

        public string phone_1 { get; set; }
        public string identifier { get; set; }
        public string password { get; set; }

        public string email { get; set; }
        public string access_token { get; set; }
        public string imageurl;

        public StreamImageSource ProfilePicture{
            get
            {
                return new AvatarImageSource(firstname,Color.White,Color.Black,48);

            }
        }
    }
}
