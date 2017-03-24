using MySql.Data.MySqlClient;
using System;

namespace DotnetCoreServer.Models
{
    public interface IUserDao{
        User FindUserByFUID(string FacebookUserID);
        User GetUser(Int64 UserID);
        User InsertUser(User user);
    }

    public class UserDao : IUserDao
    {
        public IDB db {get;}

        public UserDao(IDB db){
            this.db = db;
        }

        public User FindUserByFUID(string FacebookUserID){
            User user = new User();
            using (MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    "SELECT UserID, FacebookUserID, FacebookName, FacebookPhotoURL, Point, CreatedAt, AccessToken FROM tb_user WHERE FacebookUserID = '{0}'",
                     FacebookUserID);

                Console.WriteLine(query);

                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.UserID = reader.GetInt64(0);
                        user.FacebookUserID = reader.GetString(1);
                        user.FacebookName = reader.GetString(2);
                        user.FacebookPhotoURL = reader.GetString(3);
                        user.Point = reader.GetInt32(4);
                        user.CreatedAt = reader.GetDateTime(5);
                        user.AccessToken = reader.GetString(6);
                        return user;
                    }
                }
            }
            return null;
        }
        
        public User GetUser(Int64 UserID){
            User user = new User();
            using (MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    "SELECT UserID, FacebookUserID, FacebookName, FacebookPhotoURL, Point, AccessToken  FROM tb_user WHERE UserID = {0}",
                     UserID);

                Console.WriteLine(query);

                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.UserID = reader.GetInt64(0);
                        user.FacebookUserID = reader.GetString(1);
                        user.FacebookName = reader.GetString(2);
                        user.FacebookPhotoURL = reader.GetString(3);
                        user.Point = reader.GetInt32(4);
                        user.AccessToken = reader.GetString(5);
                    }
                }
            }
            return user;
        }

        public User InsertUser(User user){
            
            using (MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    "INSERT INTO tb_user (FacebookUserID, FacebookName, FacebookPhotoURL, Point, AccessToken, CreatedAt) VALUES ('{0}','{1}','{2}',{3}, '{4}', now())",
                     user.FacebookUserID, user.FacebookName, user.FacebookPhotoURL, 0, user.AccessToken);

                Console.WriteLine(query);

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            return user;
        }

    }
}