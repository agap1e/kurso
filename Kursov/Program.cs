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
            while(isWorking) {
                switch (n)
                {
                    case 0:
                        Console.WriteLine("1 - добавление, 2 - удаление, 3 - показ информации, 4 - выход");
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
                                case 1:
                                    bool isAddingComics = true;
                                    while (isAddingComics)
                                    {
                                        try
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
                                        catch(Exception ex)
                                        {
                                            Console.WriteLine(ex.Message+ " Для продолжения нажмите Enter");
                                            Console.ReadKey();
                                            break;
                                        }
                                    }
                                    Console.WriteLine("Хотите добавить что-то ещё? (Введите n для выхода)");
                                    if (Console.ReadLine().ToLower() == "n")
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
                                            Console.Clear();
                                            Console.WriteLine("Жанр успешно добавлен. Хотите добавить ещё?(Введите n для выхода)");
                                        }
                                        catch(Exception ex)
                                        {
                                            Console.WriteLine(ex.Message + " Для продолжения нажмите Enter");
                                            Console.ReadKey();
                                            break;
                                        }
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isAddingGenre = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите добавить что-то ещё? (Введите n для выхода)");
                                    if (Console.ReadLine().ToLower() == "n")
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
                                            Console.Clear();
                                            Console.WriteLine("Сценарист успешно добавлен. Хотите добавить ещё?(Введите n для выхода)");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message + " Для продолжения нажмите Enter");
                                            Console.ReadKey();
                                            break;
                                        }
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isAddingWriter = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите добавить что-то ещё? (Введите n для выхода)");
                                    if (Console.ReadLine().ToLower() == "n")
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
                                            Console.Clear();
                                            Console.WriteLine("Издатель успешно добавлен. Хотите добавить ещё?(Введите n для выхода)");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message + " Для продолжения нажмите Enter");
                                            Console.ReadKey();
                                            break;
                                        }
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isAddingPubl = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите добавить что-то ещё? (Введите n для выхода)");
                                    if (Console.ReadLine().ToLower() == "n")
                                    {
                                        isAdding = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 5:
                                    isAdding = false;
                                    break;
                                default:
                                    Console.WriteLine("Введено некорректное значение. Закончить добавление?(Введите y для выхода)");
                                    if (Console.ReadLine().ToLower() == "y")
                                    {
                                        isAdding = false;
                                    }
                                    break;
                            }
                        }
                        n = 0;
                        break;
                    case 2:
                        bool isRemoving = true;
                        while (isRemoving)
                        {
                            Console.WriteLine("Удалить: 1 - комикс, 2 - жанр, 3 - сценариста, 4 - издателя; 5 - возврат");
                            int i = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (i)
                            {
                                case 1: 
                                    bool isRemovingComics = true;
                                    while (isRemovingComics)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Введите название комикса");
                                            string title = Console.ReadLine();
                                            bd.RemoveComics(title);
                                            Console.Clear();
                                            Console.WriteLine("Комикс успешно удалён. Хотите удалить ещё?(Введите n для выхода)");
                                            if (Console.ReadLine().ToLower() == "n")
                                            {
                                                isRemovingComics = false;
                                            }
                                            Console.Clear();
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message + " Для продолжения нажмите Enter");
                                            Console.ReadKey();
                                            break;
                                        }
                                    }
                                    Console.WriteLine("Хотите удалить что-то ещё? (Введите n для выхода)");
                                    if (Console.ReadLine().ToLower() == "n")
                                    {
                                        isRemoving = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 2:
                                    bool isRemovingGenre = true;
                                    while (isRemovingGenre)
                                    {
                                        Console.WriteLine("Введите название жанра");
                                        try
                                        {
                                            bd.RemoveGenre(Console.ReadLine());
                                            Console.Clear();
                                            Console.WriteLine("Жанр успешно удалён. Хотите удалить ещё?(Введите n для выхода)");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message + " Для продолжения нажмите Enter");
                                            Console.ReadKey();
                                            break;
                                        }
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isRemovingGenre = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите удалить что-то ещё? (Введите n для выхода)");
                                    if (Console.ReadLine().ToLower() == "n")
                                    {
                                        isRemoving = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 3:
                                    bool isRemovingWriter = true;
                                    while (isRemovingWriter)
                                    {
                                        Console.WriteLine("Введите фамилия сценариста");
                                        try
                                        {
                                            bd.RemoveWriter(Console.ReadLine());
                                            Console.Clear();
                                            Console.WriteLine("Сценарист успешно удалён. Хотите удалить ещё?(Введите n для выхода)");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message + " Для продолжения нажмите Enter");
                                            Console.ReadKey();
                                            break;
                                        }
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isRemovingWriter = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите удалить что-то ещё? (Введите n для выхода)");
                                    if (Console.ReadLine().ToLower() == "n")
                                    {
                                        isRemoving = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 4:
                                    bool isRemovingPubl = true;
                                    while (isRemovingPubl)
                                    {
                                        Console.WriteLine("Введите название издательства");
                                        try
                                        {
                                            bd.RemovePublisher(Console.ReadLine());
                                            Console.Clear();
                                            Console.WriteLine("Издатель успешно удалён. Хотите удалить ещё?(Введите n для выхода)");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message + " Для продолжения нажмите Enter");
                                            Console.ReadKey();
                                            break;
                                        }
                                        if (Console.ReadLine().ToLower() == "n")
                                        {
                                            isRemovingPubl = false;
                                        }
                                        Console.Clear();
                                    }
                                    Console.WriteLine("Хотите удалить что-то ещё? (Введите n для выхода)");
                                    if (Console.ReadLine().ToLower() == "n")
                                    {
                                        isRemoving = false;
                                    }
                                    Console.Clear();
                                    break;
                                case 5:
                                    isRemoving = false;
                                    break;
                                default:
                                    Console.WriteLine("Введено некорректное значение. Закончить удаление?(Введите y для выхода)");
                                    if (Console.ReadLine().ToLower() == "y")
                                    {
                                        isAdding = false;
                                    }
                                    break;
                            }
                        }
                        n = 0;
                        break;
                    case 3:
                        Console.WriteLine("Показать: 1 - комиксы, 2 - жанры, 3 - сценаристов, 4 - издателей; 5 - возврат");
                        int show = int.Parse(Console.ReadLine());
                        Console.Clear();
                        bd.Show(show);
                        Console.Write("\nДля продолжения нажмите Enter.");
                        Console.ReadKey();
                        Console.Clear();
                        n = 0;
                        break;
                    case 4:
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("Введено некорректное значение. Закончить сеанс?(Введите y для выхода)");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            isWorking = false;
                        }
                        Console.Clear();
                        n = 0;
                        break;
                }
            }
        }
    }
}