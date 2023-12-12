using System.Xml.Linq;
namespace Base
{
    class BD
    {
        List<Comics> BD_Comics = new List<Comics>();
        List<Genre> BD_Genre = new List<Genre>();
        List<Writer> BD_Writer = new List<Writer>();
        List<Publisher> BD_Publisher = new List<Publisher>();
        //СЦЕНАРИСТ МОЖЕТ БЫТЬ ОДИН И ТОТ ЖЕ
        public void Add(string title, int year, int circulation, int price, string genre, string writer, string publ, string country) //Добавляем в бд комикс
        {
            Comics comics = new Comics(title, year, circulation, price);
            try
            {
                AddWriter(comics, writer);
            }
            catch
            {
                Add(writer);
            }
            try
            {
                AddPubl(comics, publ);
            }
            catch
            {
                Add(publ, country);
            }
            try
            {
                AddGenre(comics, genre);
            }
            catch
            {
                Add(genre, true);
            }
            BD_Comics.Add(comics);
        }
        public void Add(string name, bool isGenre) //Добавляем в бд жанры
        {
            Genre genre = new Genre(name);
            int? sim = Check(genre, BD_Genre);
            if (sim is null) BD_Genre.Add(genre);
            else
            {
                if (isAdd(BD_Genre[(int)sim].Name) == true)
                {
                    BD_Genre.Add(genre);
                }
            }
        }
        public void Add(string name) //Добавляем в бд сценаристов
        {
            Writer writer = new Writer(name);
            int? sim = Check(writer, BD_Writer);
            if (sim is null) BD_Writer.Add(writer);
            else
            {
                if (isAdd(BD_Writer[(int)sim].Name) == true)
                {
                    BD_Writer.Add(writer);
                }
            }
        }
        public void Add(string name, string country) //Добавляем в бд издателей
        {
            Publisher publ = new Publisher(name, country);
            int? sim = Check(publ, BD_Publisher);
            if (sim is null) BD_Publisher.Add(publ);
            else
            {
                if (isAdd(BD_Publisher[(int)sim].Name) == true)
                {
                    BD_Publisher.Add(publ);
                }
            }
        }
        public void AddWriter(Comics comics, string str)
        {
            comics.Writer_id = FindWriterInfo(str);
        }
        public void AddPubl(Comics comics, string str)
        {
            comics.Publ_id = FindPublisherInfo(str);
        }
        public void AddGenre(Comics comics, string str)
        {
            comics.Genre_id = FindGenreInfo(str);
        }
        
        public void Remove() //Удаление элемента бд
        {

        } 
        public void Clear() //Удаление всего бд
        { 
            BD_Comics.Clear();
            BD_Genre.Clear();
            BD_Writer.Clear();
            BD_Publisher.Clear();
        }
        /*public return Search(int n, string str) //Поиск в бд
        {
            switch (n)
            {
                case 0:
                    foreach (Comics comics in BD_Comics)
                    {

                        Console.WriteLine(comics.Name + "\t" + "Жанр" + "\t" + comics.Year + "\t" + comics.Circulation + "\t" + "Сценарсит" + "\t" + "Издатель");
                    }
                    break;
                case 1:
                    Console.WriteLine("Жанры\n");
                    foreach (Genre genre in BD_Genre)
                    {
                        Console.WriteLine(genre.Name);
                    }
                    break;
                case 2:
                    foreach (Writer writer in BD_Writer)
                    {
                        if (writer.ID == writer_id)
                        {
                            return writer.Name;
                        }
                    }
                    return "Не удалось найти сценариста";
                case 3:
                    Console.WriteLine("Издатели\tСтрана\n");
                    foreach (Publisher publisher in BD_Publisher)
                    {
                        Console.WriteLine(publisher.Name + publisher.Country);
                    }
                    break;
            }
        }*/
        public void Show(int n)
        {
            switch(n){
                case 1:
                    Console.WriteLine("Название комикса  Жанр  Год выхода  Тираж  Цена  Сценарист  Издатель\n");
                    foreach (Comics comics in BD_Comics) 
                    {
                        Console.WriteLine(comics.Name + "\t" + FindGenreInfo(comics.Genre_id) + "\t" + comics.Year + "\t" + comics.Circulation + "\t" + comics.Price + "\t" + FindWriterInfo(comics.Writer_id) + "\t" + FindPublisherInfo(comics.Publ_id));
                    }
                    break;
                case 2:
                    Console.WriteLine("Жанры\n");
                    foreach (Genre genre in BD_Genre)
                    {
                        Console.WriteLine(genre.Name);
                    }
                    break;
                case 3:
                    Console.WriteLine("Сценарситы\n");
                    foreach (Writer writer in BD_Writer)
                    {
                        Console.WriteLine(writer.Name);
                    }
                    break;
                case 4:
                    Console.WriteLine("Издатели\tСтрана\n");
                    foreach (Publisher publisher in BD_Publisher)
                    {
                        Console.WriteLine(publisher.Name + publisher.Country);
                    }
                    break;
            }
        } //Вывод поправить
        private int? Check<T>(T what, List<T> list) where T : Identity  //коэффициент Танимото
        {
            double kmax = 0;
            int? index = null;
            char[] a = what.Name.ToLower().ToCharArray();
            foreach(T whatBD in list)
            {
                int c = 0;
                int lim;
                char[] b = whatBD.Name.ToLower().ToCharArray();
                if (a.Length > b.Length)
                {
                    lim = b.Length;
                }
                else lim = a.Length;
                for (int i = 0; i < lim; i++)
                {
                    if (b[i] == a[i])
                    {
                        c++;
                    }
                }
                double k = (double) c / (a.Length + b.Length - c);
                if (k > kmax && k > 0.5)
                {
                    kmax = k;
                    index = whatBD.ID;
                }
            }
            return index;
        }
        private bool isAdd(string sim) //Изменить!!!
        {
            Console.WriteLine($"Значение похоже на: {sim}, уверены, что написали правильно?(y/n)");
            if(Console.ReadLine() == "y")
            {
                return true;
            }
            return false;
        }
        private string FindWriterInfo(int writer_id) //Поиск сценарист по ID
        {
            foreach (Writer writer in BD_Writer)
            {
                if (writer.ID == writer_id)
                {
                    return writer.Name;
                }
            }
            throw new Exception("Не удалось найти сценариста");
        }
        private int FindWriterInfo(string writer_name) //Поиск ID сценарист по фамилии
        {
            foreach (Writer writer in BD_Writer)
            {
                if (writer.Name == writer_name)
                {
                    return writer.ID;
                }
            }
            throw new Exception("Не удалось найти сценариста");
        }
        private string FindGenreInfo(int genre_id) //Поиск жанра по ID
        {
            foreach (Genre genre in BD_Genre)
            {
                if (genre.ID == genre_id)
                {
                    return genre.Name;
                }
            }
            throw new Exception("Не удалось найти сценариста");
        }
        private int FindGenreInfo(string genre_name) //Поиск ID жанра по названию
        {
            foreach (Genre genre in BD_Genre)
            {
                if (genre.Name == genre_name)
                {
                    return genre.ID;
                }
            }
            throw new Exception("Не удалось найти сценариста");
        }
        private string FindPublisherInfo(int publ_id) //Поиск издательства по ID
        {
            foreach (Publisher publ in BD_Publisher)
            {
                if (publ.ID == publ_id)
                {
                    return publ.Name;
                }
            }
            throw new Exception("Не удалось найти сценариста");
        }
        private int FindPublisherInfo(string publ_name) //Поиск ID издательства по названию
        {
            foreach (Publisher publ in BD_Publisher)
            {
                if (publ.Name == publ_name)
                {
                    return publ.ID;
                }
            }
            throw new Exception("Не удалось найти сценариста");
        }
        //Издатель по стране??
        //Комикс по названию
        //Комикс по автору
        //Комикс по году
        //Комикс по цене
        //Комикс по издательству
        //Комикс по жанру
    }
    abstract class Identity
    {
        public abstract int ID { get; }
        public abstract string Name { get; set; }
    }
    class Comics : Identity
    {
        static int new_id = 0;
        int year;
        int circulation;
        int price;
        string name;
        public override int ID { get; }
        public override string Name 
        {
            get => name;
            set
            {
                value = value.Trim();
                if ((value is null) || (value.Length == 0))
                {
                    throw new Exception("Должно быть указано название комикса.");
                }
                else name = value;
            }
        }
        public int Year
        {
            get => year;
            set
            {
                if (1894 < value && value < 2025)
                {
                    year = value;
                }
                else throw new Exception("Год указан некорректно.");
            }
        }
        public int Circulation
        {
            get => circulation;
            set 
            { 
                if (value < 0)
                {
                    throw new Exception("Тираж указан некорректно");
                }
                else circulation = value;
                
            }
        }
        public int Price
        {
            get => price;
            set 
            { 
                if (value < 0)
                {
                    throw new Exception("Цена указана некорректно");
                }
                else price = value;
            }  
        }
        public Comics (string title, int year, int circulation, int price)
        {
            ID = new_id++;
            Name = title;
            Year = year;
            Circulation = circulation;
            Price = price;
        }
        public int Writer_id { get; set; } //Автосвойство потому что программа сама поставит (Покажется какие есть варианты и пользователь введёт или новое, или которое есть)
        public int Publ_id { get; set; } //Автосвойство потому что программа сама поставит (Покажется какие есть варианты и пользователь введёт или новое, или которое есть)
        public int Genre_id { get; set; } //Автосвойство потому что программа сама поставит (Покажется какие есть варианты и пользователь введёт или новое, или которое есть)

    }
    class Genre : Identity
    {
        static int new_id = 0;
        string name;
        public override int ID { get; }
        public override string Name
        {
            get => name;
            set
            {
                value = value.Trim();
                if ((value is null) || (value.Length == 0))
                {
                    throw new Exception("Должно быть указано название жанра.");
                }
                else name = value;
            }
        }
        public Genre(string name)
        {
            ID = new_id++;
            Name = name;
        }
    }
    class Writer : Identity
    {
        static int new_id = 0;
        string name;
        public override int ID { get; }
        public override string Name  //Исключения!
        {
            get => name;
            set
            {
                value = value.Trim();
                if ((value is null) || (value.Length == 0))
                {
                    throw new Exception("Должна быть указана фамилия автора.");
                }
                else name = value;
            }
        }
        public Writer(string name)
        {
            ID = new_id++;
            Name = name;
        }
    }
    class Publisher : Identity
    {
        static int new_id = 0;
        string name;
        string country;
        public override int ID { get; }
        public override string Name    // Исключения!
        {
            get => name;
            set
            {
                value = value.Trim();
                if ((value is null) || (value.Length == 0))
                {
                    throw new Exception("Должно быть указано название издателя.");
                }
                else name = value;
            }
        }
        public string Country   // Исключения!
        {
            get => country;
            set
            {
                value = value.Trim();
                if ((value is null) || (value.Length == 0))
                {
                    throw new Exception("Должно быть указано название страны.");
                }
                else country = value;
            }
        }
        public Publisher(string name, string country)
        {
            ID = new_id++;
            Name = name;
            Country = country;
        }
    }
}