using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;

namespace DotnetCoreServer.Models
{
    public interface IUserDao{
        User FindUserByFUID(string FacebookID);
        User GetUser(Int64 UserID);
        User InsertUser(User user);
        bool UpdateUser(User user);
    }

    public class UserDao : IUserDao
    {
        public IDB db {get;}

        public UserDao(IDB db){
            this.db = db;
        }

        public User FindUserByFUID(string FacebookID){
            User user = new User();
            using(MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    "SELECT user_id, facebook_id, facebook_name, facebook_photo_url, point, created_at, access_token FROM unity.tb_user WHERE facebook_id = '{0}'",
                     FacebookID);

                Console.WriteLine(query);

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.UserID = reader.GetInt64(0);
                            user.FacebookID = reader.GetString(1);
                            user.FacebookName = reader.GetString(2);
                            user.FacebookPhotoURL = reader.GetString(3);
                            user.Point = reader.GetInt32(4);
                            user.CreatedAt = reader.GetDateTime(5);
                            user.AccessToken = reader.GetString(6);
                            return user;
                        }
                    }
                }
                conn.Close();
            }
            return null;
        }
        
        public User GetUser(Int64 UserID){
            User user = new User();
            using(MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    @"
                    SELECT 
                        user_id, facebook_id, facebook_name, 
                        facebook_photo_url, point, created_at, 
                        access_token, diamond, health, defense, damage,
                        speed, health_level, defense_level, 
                        damage_level, speed_level,
                        level, experience
                    FROM tb_user 
                    WHERE user_id = {0}",
                     UserID);

                Console.WriteLine(query);

                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.UserID = reader.GetInt64(0);
                            user.FacebookID = reader.GetString(1);
                            user.FacebookName = reader.GetString(2);
                            user.FacebookPhotoURL = reader.GetString(3);
                            user.Point = reader.GetInt32(4);
                            user.CreatedAt = reader.GetDateTime(5);
                            user.AccessToken = reader.GetString(6);

                            user.Diamond = reader.GetInt32(7);
                            user.Health = reader.GetInt32(8);
                            user.Defense = reader.GetInt32(9);
                            user.Damage = reader.GetInt32(10);
                            user.Speed = reader.GetInt32(11);
                            user.HealthLevel = reader.GetInt32(12);
                            user.DefenseLevel = reader.GetInt32(13);
                            user.DamageLevel = reader.GetInt32(14);
                            user.SpeedLevel = reader.GetInt32(15);
                            user.Level = reader.GetInt32(16);
                            user.Experience = reader.GetInt32(17);
                            
                        }
                    }
                }
                
                conn.Close();
            }
            return user;
        }

        public User InsertUser(User user){
            
            string query = String.Format(
                "INSERT INTO unity.tb_user (facebook_id, facebook_name, facebook_photo_url, point, access_token, created_at) VALUES ('{0}','{1}','{2}',{3}, '{4}', now())",
                    user.FacebookID, user.FacebookName, user.FacebookPhotoURL, 0, user.AccessToken);

            Console.WriteLine(query);

            using(MySqlConnection conn = db.GetConnection())
            using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
            {

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                conn.Close();
            }

        
            return user;
        }

        public bool UpdateUser(User user){
            using(MySqlConnection conn = db.GetConnection())
            {
                string query = String.Format(
                    @"
                    UPDATE tb_user SET 
                        health = {0}, defense = {1}, damage = {2}, speed = {3},
                        health_level = {4}, defense_level = {5}, damage_level = {6}, speed_level = {7},
                        diamond = {8}, point = {9}
                    WHERE user_id = {10}
                    ",
                    user.Health, user.Defense, user.Damage, user.Speed,
                    user.HealthLevel, user.DefenseLevel, user.DamageLevel, user.SpeedLevel,
                    user.Diamond, user.Point, user.UserID
                     );

                Console.WriteLine(query);
                
                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    
                }

                conn.Close();
            }
            return true;
        }



    }
}