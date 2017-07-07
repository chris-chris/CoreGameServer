using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DotnetCoreServer.Models
{
    public interface IUpgradeDao{
        List<UpgradeData> GetUpgradeInfo();
        UpgradeData GetUpgradeInfo(string UpgradeType, int UpgradeLevel);
        
    }

    public class UpgradeDao : IUpgradeDao
    {
        public IDB db {get;}

        public UpgradeDao(IDB db){
            this.db = db;
        }

        public List<UpgradeData> GetUpgradeInfo(){
            
            List<UpgradeData> list = new List<UpgradeData>();
            using(MySqlConnection conn = db.GetConnection())
            {   

                string query = String.Format(
                    "SELECT upgrade_type, upgrade_level, upgrade_amount, upgrade_cost FROM tb_upgrade_info");

                Console.WriteLine(query);
                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UpgradeData data = new UpgradeData();
                            data.UpgradeType = reader.GetString(0);
                            data.UpgradeLevel = reader.GetInt32(1);
                            data.UpgradeAmount = reader.GetInt32(2);
                            data.UpgradeCost = reader.GetInt32(3);
                            list.Add(data);
                        }
                    }
                }

                conn.Close();
                
            }
            
            return list;
        }
        
        public UpgradeData GetUpgradeInfo(string UpgradeType, int UpgradeLevel){
            
            UpgradeData data = new UpgradeData();
            using(MySqlConnection conn = db.GetConnection())
            {   
                string query = String.Format(
                    @"
                    SELECT 
                        upgrade_type, upgrade_level, 
                        upgrade_amount, upgrade_cost 
                    FROM tb_upgrade_info
                    WHERE upgrade_type = '{0}' AND upgrade_level = {1}
                    ", UpgradeType, UpgradeLevel);

                Console.WriteLine(query);
                using(MySqlCommand cmd = (MySqlCommand)conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (MySqlDataReader reader = (MySqlDataReader)cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.UpgradeType = reader.GetString(0);
                            data.UpgradeLevel = reader.GetInt32(1);
                            data.UpgradeAmount = reader.GetInt32(2);
                            data.UpgradeCost = reader.GetInt32(3);
                            return data;
                        }
                    }
                }

                conn.Close();

            }
            
            return null;
        }

    }
}