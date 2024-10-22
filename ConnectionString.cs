
using MySql.Data.MySqlClient;

namespace SkillFactory;

internal class ConnectionString
{

    private static MySqlConnectionStringBuilder mySqlConnectionStringBuilder;

    public static string GetConnectionString(string server, string dataBase, string userId, string password)
    {
        mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder(){
            Server = server,
            Database = dataBase,
            UserID = userId,
            Password = password
        };
        return mySqlConnectionStringBuilder.ConnectionString;
    }  
}