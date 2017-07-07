using System;

namespace DotnetCoreServer.Models
{
    public class RankUser
    {
        public long UserID;
        public string FacebookID;
        public string FacebookName;
        public string FacebookPhotoURL;
        public int Point;
        public DateTime CreatedAt;
        public int Rank;

    }
}