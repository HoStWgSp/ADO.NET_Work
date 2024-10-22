
using System.Data;

namespace SkillFactory;

public class Manager
{

    private MainConnector mainConnector = new MainConnector();
    private DbExecutor dbExecutor;
    private Table userTable;
   

    public Manager()
    {
        mainConnector = new MainConnector();

        userTable = new Table();
        userTable.Name = "NetworkUser";
        userTable.ImportantField = "Login";
        userTable.Fields.Add("Id");
        userTable.Fields.Add("Login");
        userTable.Fields.Add("UserName");
    }
    


    /// <summary>
    /// Подключается к SQL и инициализирует класс DbExecutor.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="dataBase"></param>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    public void Connect(string server, string dataBase, string userId, string password)
    {
        Task<bool> result = mainConnector.ConnectAsync(server, dataBase, userId, password);

        if (result.Result)
        {
            Console.WriteLine("Подключено успешно!");
            dbExecutor = new DbExecutor(mainConnector);
        } 
        else Console.WriteLine("Ошибка подключения!");
    }

    /// <summary>
    /// Отключаестя от SQL
    /// </summary>
    public void Disconnect() 
    {
        Console.WriteLine("Отключаем БД!");
        mainConnector.DisconnectAsync();
    }

    /// <summary>
    /// Показывает данные из таблицы
    /// </summary>
    public void ShowData()
    {
        string tableName = "NetworkUser";

        Console.WriteLine("Получаем данные таблицы " + tableName);

        DataTable data = dbExecutor.SelectAll(tableName);

        Console.WriteLine("Количество строк в " + tableName + ": " + data.Rows.Count);
        Console.WriteLine();

        foreach(DataColumn column in data.Columns) { Console.Write($"{column.ColumnName}\t"); }

        Console.WriteLine();

        foreach(DataRow row in data.Rows) 
        {
            var cells = row.ItemArray;
            foreach(var cell in cells) 
            {
                Console.Write($"{cell}\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Удаляет строку у которой в колонке ImportantField значение value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int DeleteUserByLogin(string value) 
    {
        return dbExecutor.DeleteByColumn(userTable.Name, userTable.ImportantField, value);
    }

    /// <summary>
    /// Добавляет строку в таблицу
    /// </summary>
    /// <param name="name"></param>
    /// <param name="login"></param>
    public void AddUser(string name, string login) 
    {
        dbExecutor.ExecProcedureAdding(name, login);
    }

    public int UpdateUserByLogin(string value, string newvalue) 
    {
        return dbExecutor.UpdateByColumn(userTable.Name, userTable.ImportantField, value, userTable.Fields[2], newvalue);
    }

}