
using System.Data;
using MySql.Data.MySqlClient;


namespace SkillFactory;

internal class DbExecutor
{
    private MainConnector mainConnector;
    public DbExecutor(MainConnector mainConnector)
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
    public int DeleteByColumn(string table, string column, string value)
    {
        MySqlCommand mySqlCommand = new MySqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = "delete from " + table + " where " + column + " = '" + value + "';",
            Connection = mainConnector.GetConnection(),
        };
        return mySqlCommand.ExecuteNonQuery();
    }
    public int ExecProcedureAdding(string name, string login) 
    {
        MySqlCommand mySqlCommand = new MySqlCommand 
        {
            CommandType = CommandType.StoredProcedure,
            CommandText = "AddingUserProc",
            Connection = mainConnector.GetConnection(),
        };

        mySqlCommand.Parameters.Add(new MySqlParameter("UserName", name));
        mySqlCommand.Parameters.Add(new MySqlParameter("Login", login));


        int r = mySqlCommand.ExecuteNonQuery();
        return r;
    }
    public int UpdateByColumn(string table, string columntocheck,
    string valuecheck, string columntoupdate, string valueupdate)
    {
        MySqlCommand mySqlCommand = new MySqlCommand()
        {
            CommandType = CommandType.Text,
            CommandText = @$"update {table} set {columntoupdate} = '{valueupdate}'  
                            where {columntocheck} = '{valuecheck}';",
            Connection = mainConnector.GetConnection(),
        };
        return mySqlCommand.ExecuteNonQuery();
    }
    



} 
