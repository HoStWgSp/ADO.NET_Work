
using System.Data;
using MySql.Data.MySqlClient;

namespace SkillFactory;

class Program
{
    static void Main(string[] args)
    {
        MainConnector mainConnector = new MainConnector();
        DataTable data = new DataTable();
        DbExecutor db = new DbExecutor(mainConnector);
        string tablename = "NetworkUser";

        Task<bool> result = mainConnector.ConnectAsync("127.0.0.1", "MyTest", "evgeny", "Vtcnj!21");
        
        if (result.Result) 
        {
            Console.WriteLine("Подключено успешно!"); 
            Console.WriteLine("Получаем данные таблицы " + tablename);
            data = db.SelectAll(tablename);
            Console.WriteLine("Отключаем БД!");
            //mainConnector.DisconnectAsync();
        }
        else Console.WriteLine("Ошибка подключения!");

        Console.WriteLine("Количество строк в " + data.TableName + ": " + data.Rows.Count);

        foreach(DataColumn column in data.Columns) Console.Write($"{column.ColumnName}\t");
        Console.WriteLine();


        foreach(DataRow row in data.Rows) 
        {
            var cells = row.ItemArray;
            foreach(var cell in cells) 
            {
                Console.Write($"{row[data.Columns[0].ColumnName]}\t");
            }
            Console.WriteLine();
        }
        
        MySqlDataReader reader = db.SelectAllCommandReader(tablename);

        var columnList = new List < string > ();

        for (int i = 0; i < reader.FieldCount; i++) 
        {
            var name = reader.GetName(i);
            columnList.Add(name);
        }

        for (int i = 0; i < columnList.Count; i++) 
        {
            Console.Write($"{columnList[i]}\t");
        }
        Console.WriteLine();

        while (reader.Read()) 
        {
            for (int i = 0; i < columnList.Count; i++) 
            {
                var value = reader[columnList[i]];
                Console.Write($"{value}\t");
            }

        Console.WriteLine();
        }
    }
}
