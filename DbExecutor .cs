
using System.Data;
using MySql.Data.MySqlClient;


namespace SkillFactory;

internal class DbExecutor()
{
    private MainConnector mainConnector;
    public DbExecutor(MainConnector mainConnector):this()
    {
        this.mainConnector = mainConnector;
    }

    public DataTable SelectAll(string table)
    {
        string selectcommandtext = "select * from " + table;
        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(selectcommandtext, 
        mainConnector.GetConnection());
        DataSet dataSet = new DataSet();
        mySqlDataAdapter.Fill(dataSet);
        return dataSet.Tables[0];
    }
    public MySqlDataReader SelectAllCommandReader(string table) 
    {
        MySqlCommand command = new MySqlCommand
        {
            CommandType = CommandType.Text,
            CommandText = "select * from " + table,
            Connection = mainConnector.GetConnection(),
        };

        MySqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows) 
        {
            return reader;
        }

        return null;
    }
} 