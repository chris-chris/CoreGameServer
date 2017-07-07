using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DotnetCoreServer.Models
{
    public interface IRankDao{
        List<RankUser> TotalRank(int Start, int Count);
        List<RankUser> FriendRank(List<string> FacebookIDList);
    }

    public class RankDao : IRankDao
    {
        public IDB db {get;}

        public RankDao(IDB db){
            this.db = db;
        }

        public List<RankUser> TotalRank(int Start, int Count){
            
            List<RankUser> list = new List<RankUser>();
            using(MySqlConnection conn = db.GetConnection())
            {   

                string query = String.Format(
                    @"
                    SELECT 
                    user_id, facebook_id, facebook_name, 
                    facebook_photo_url, point, created_at
                    FROM tb_user
                    ORDER BY point desc
                    LIMIT {0}, {1}", Start, Count );

                Console.WriteLine(query);
                int rank = 0;
                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rank++;
                            RankUser user = new RankUser();
                            user.UserID = reader.GetInt64(0);
                            user.FacebookID = reader.GetString(1);
                            user.FacebookName = reader.GetString(2);
                            user.FacebookPhotoURL = reader.GetString(3);
                            user.Point = reader.GetInt32(4);
                            user.CreatedAt = reader.GetDateTime(5);
                            user.Rank = rank;
                            list.Add(user);
                        }
                    }
                }

                conn.Close();
                
            }
            
            return list;

        }
        
        public List<RankUser> FriendRank(List<string> FacebookIDList){

            for(int i = 0; i < FacebookIDList.Count; i++){
                FacebookIDList[i] = string.Format("'{0}'", FacebookIDList[i]);
            }
            
            string StrFacebookIDList = string.Join(",", FacebookIDList);

            List<RankUser> list = new List<RankUser>();
            using(MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    @"
                    SELECT 
                        user_id, facebook_id, facebook_name, 
                        facebook_photo_url, point, created_at
                    FROM tb_user
                    WHERE facebook_id IN ( {0} )",
                     StrFacebookIDList);

                Console.WriteLine(query);
                int rank = 0;

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rank++;
                            RankUser user = new RankUser();
                            user.UserID = reader.GetInt64(0);
                            user.FacebookID = reader.GetString(1);
                            user.FacebookName = reader.GetString(2);
                            user.FacebookPhotoURL = reader.GetString(3);
                            user.Point = reader.GetInt32(4);
                            user.CreatedAt = reader.GetDateTime(5);
                            user.Rank = rank;
                            list.Add(user);
                        }
                    }
                }
                
                conn.Close();
            }
            return list;
        }



    }
}