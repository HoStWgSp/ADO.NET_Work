using MySql.Data.MySqlClient;
using System.Data;

namespace SkillFactory;

internal class MainConnector
{
    private MySqlConnection mySqlConnection;    

    public async Task < bool > ConnectAsync(string server, string dataBase, string userId, string password) 
    {
        try 
        {
            mySqlConnection = new MySqlConnection(ConnectionString.GetConnectionString(server, dataBase, userId, password));
            await mySqlConnection.OpenAsync();
            return true;
        } 
        catch {return false;}
    }
    public async void DisconnectAsync() 
    {
        if (mySqlConnection.State == ConnectionState.Open) await mySqlConnection.CloseAsync();
    }
    public MySqlConnection GetConnection()
    {
        if (mySqlConnection.State == ConnectionState.Open) return mySqlConnection;
        else throw new Exception("Подключение уже закрыто!");
    }
    
}