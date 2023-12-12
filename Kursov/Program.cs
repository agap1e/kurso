using Base;
namespace Kursov
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BD bd = new BD();
            int n = 0;
            bool isWorking = true;
            while(isWorking) { //Везде добавить некорректное значение + break + если введены одни и те же значения + если не то число введено
                switch (n)
                {
                    case 0:
                        Console.WriteLine("1 - добавление, 2 - удаление, 3 - показ информации, 4 - удаление всей базы данных, 5 - выход");
                        n = int.Parse(Console.ReadLine());
                        Console.Clear();
                        break;
                    case 1:
                        bool isAdding = true;
                        while (isAdding)
                        {
                            Console.WriteLine("Добавить: 1 - комикс, 2 - жанр, 3 - сценариста, 4 - издателя; 5 - возврат");
                            int i = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (i)
                            {
                                case 1: // try catch
                                    bool isAddingComics = true;
                                    while (isAddingComics)
                                    {
                                        Console.WriteLine("Введите название комикса, год выхода, тираж, цену, жанр, сценариста, издателя, страну издателя");//Переделать (убрать страну????)
                                        string title = Console.ReadLine();
                                        int year = int.Parse(Console.ReadLine());
                                        int circ = int.Parse(Console.ReadLine());
                                        int price = int.Parse(Console.ReadLine());
                                        string genre = Console.ReadLine();
                                        string writer = Console.ReadLine();
                                        string publ = Console.ReadLine();
                                        string country = Console.ReadLine();
                                        bd.Add(title, year, circ, price, genre, writer, publ, country);
                                        Console.Clear();
                                        Console.WriteLine("Комикс успешно добавлен. Хотите добавить ещё?(Введите n для выхода)");
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isAddingComics = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите добавить что-то ещё? (Введите y для добавления)");
                                    if (Console.ReadLine().ToLower() == "y")
                                    {
                                        isAdding = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 2:
                                    bool isAddingGenre = true;
                                    while (isAddingGenre)
                                    {
                                        Console.WriteLine("Введите название жанра");
                                        try
                                        {
                                            bd.Add(Console.ReadLine(), true);
                                        }
                                        catch(Exception ex)
                                        { 
                                            Console.WriteLine(ex.Message);
                                            break;
                                        }
                                        Console.Clear();
                                        Console.WriteLine("Жанр успешно добавлен. Хотите добавить ещё?(Введите n для выхода)");
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isAddingGenre = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите добавить что-то ещё? (Введите y для добавления)");
                                    if (Console.ReadLine().ToLower() == "y")
                                    {
                                        isAdding = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 3:
                                    bool isAddingWriter = true;
                                    while (isAddingWriter)
                                    {
                                        Console.WriteLine("Введите фамилия сценариста");
                                        try
                                        {
                                            bd.Add(Console.ReadLine());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            break;
                                        }
                                        Console.Clear();
                                        Console.WriteLine("Сценарист успешно добавлен. Хотите добавить ещё?(Введите n для выхода)");
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isAddingWriter = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите добавить что-то ещё? (Введите y для добавления)");
                                    if (Console.ReadLine().ToLower() == "y")
                                    {
                                        isAdding = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 4:
                                    bool isAddingPubl = true;
                                    while (isAddingPubl)
                                    {
                                        Console.WriteLine("Введите название издательства и страну");
                                        string publ = Console.ReadLine();
                                        string country = Console.ReadLine();
                                        try
                                        {
                                            bd.Add(publ, country);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                            break;
                                        }
                                        Console.Clear();
                                        Console.WriteLine("Издатель успешно добавлен. Хотите добавить ещё?(Введите n для выхода)");
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isAddingPubl = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите добавить что-то ещё? (Введите y для добавления)");
                                    if (Console.ReadLine().ToLower() == "y")
                                    {
                                        isAdding = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 5:
                                    isAdding = false;
                                    break;
                            }
                        }
                        n = 0;
                        break;
                    case 2:
                        break;
                    case 3:
                        Console.WriteLine("Показать: 1 - комиксы, 2 - жанры, 3 - сценаристов, 4 - издателей; 5 - возврат");
                        bd.Show(int.Parse(Console.ReadLine()));
                        n = 0;
                        break;
                    case 4:
                        bd.Clear();
                        break;
                    case 5:
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("Введено некорректное значение. Закончить сеанс?(Введите y для выхода)");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            isWorking = false;
                        }
                        break;
                }
            }
            
            /*bool isEx = true;
            string YN;
            while (isEx) 
            {
                try
                {
                    bd.AddGenre(Console.ReadLine()!);
                    isEx = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}, хотите написать ещё раз?(y/n)");
                    YN = Console.ReadLine();
                    if (YN == "n")
                    {
                        isEx = false; 
                    }
                }
            }
            bd.AddGenre(Console.ReadLine()!);
            bd.Show(0);
            string title = "Человек-паук";
            int year = 2023;
            int circual = 10000;
            int price = 10;
            string genre = "dsa";
            string writer = "dsadsa";
            string publ = "adas";
            string country = "dsadsa";
            bd.Add(title, year, circual, price, genre, writer, publ, country);
            title = "Бетмен";
            year = 2000;
            circual = 100;
            price = 1000;
            genre = "dsadsadas";
            writer = "ds";
            publ = "dsadsafdassfsafsa";
            country = "dsadsadsadsadsadsa";
            bd.Add(title, year, circual, price, genre, writer, publ, country);
            bd.Show(0);*/
        }
    }
}