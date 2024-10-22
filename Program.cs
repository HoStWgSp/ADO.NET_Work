

namespace SkillFactory;

class Program
{
        static Manager manager;
        static void Main(string[] args)
        {
            // Создаем контекст для добавления данных
        using (var db = new AppContext())
        {
            // Пересоздаем базу
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            // Заполняем данными
            Company company1 = new Company { Name = "SF" };
            Company company2 = new Company { Name = "VK" };
            Company company3 = new Company { Name = "FB" };
            db.Companies.AddRange(company1, company2, company3);

            UserData user1 = new UserData { Name = "Arthur", Role = "Admin", Company = company1 };
            UserData user2 = new UserData { Name = "Bob", Role = "Admin", Company = company2 };
            UserData user3 = new UserData { Name = "Clark", Role = "User", Company = company2 };
            UserData user4 = new UserData { Name = "Dan", Role = "User", Company = company3 };

            db.Users.AddRange(user1, user2, user3, user4);

            db.SaveChanges();
        }

        // Создаем контекст для выбора данных
        using (var db = new AppContext())
        {
            var usersQuery =
                from user in db.Users
                where user.CompanyId == 2
                select user;

            var users = usersQuery.ToList();

            foreach (var user in users)
            {
                // Вывод Id пользователей
                Console.WriteLine(user.Id);
            }
        }
    }





    static void ManagerWork()
    {
        manager = new Manager();

        manager.Connect("127.0.0.1", "MyTest", "evgeny", "Vtcnj!21");

        CommandsList();

        string command;
        do 
        {
            Console.WriteLine();
            CommandsList();
            command = Console.ReadLine();
            Console.WriteLine();
            switch (command) 
            {
                case
                nameof(Commands.add): 
                { 
                    Add(); 
                    break; 
                }
                case
                nameof(Commands.delete): 
                {
                    Delete();
                    break;
                }
                case
                nameof(Commands.update): 
                {
                    Update();
                    break;
                }
                case
                nameof(Commands.show): 
                {
                    manager.ShowData();
                    break;
                }
            }
        } 
        while (command != nameof(Commands.stop));
    }
    static void CommandsList()
    {
        Console.WriteLine("Список команд для работы консоли:");
        Console.WriteLine(Commands.stop + ": прекращение работы");
        Console.WriteLine(Commands.add + ": добавление данных");
        Console.WriteLine(Commands.delete + ": удаление данных");
        Console.WriteLine(Commands.update + ": обновление данных");
        Console.WriteLine(Commands.show + ": просмотр данных");
    }
    public enum Commands 
    {
        stop,
        add,
        delete,
        update,
        show
    }
    static void Add() 
    {
        Console.WriteLine("Введите логин для добавления:");

        var login = Console.ReadLine();

        Console.WriteLine("Введите имя для добавления:");
        var name = Console.ReadLine();

        manager.AddUser(name, login);

        manager.ShowData();
    }
    static void Update() 
    {
        Console.WriteLine("Введите логин для обновления:");

        var login = Console.ReadLine();

        Console.WriteLine("Введите имя для обновления:");
        var name = Console.ReadLine();

        var count = manager.UpdateUserByLogin(login, name);

        Console.WriteLine("Строк обновлено" + count);

        manager.ShowData();
    }
    static void Delete() 
    {
        Console.WriteLine("Введите логин для удаления:");

        var count = manager.DeleteUserByLogin(Console.ReadLine());

        Console.WriteLine("Количество удаленных строк " + count);

        manager.ShowData();
    }
}
