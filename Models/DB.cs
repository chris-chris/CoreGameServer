using System;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
public interface IDB
{
        MySqlConnection GetConnection();
}

public class DB : IDB
{
    public string ConnectionString;

    public DB(IConfiguration configuration){
        this.ConnectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public MySqlConnection GetConnection()
    {
        MySqlConnection connection = new MySqlConnection
        {
            ConnectionString = this.ConnectionString
        };
        connection.Open();
        return connection;
    }

}