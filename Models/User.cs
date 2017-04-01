using System;

namespace DotnetCoreServer.Models
{
    public class User
    {

        public long UserID { get; set; }
        public string FacebookID { get; set; }
        public string FacebookName { get; set; }
        public string FacebookPhotoURL { get; set; }
        public string FacebookAccessToken { get; set; }
        public int Point { get; set;}
        public string AccessToken { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}