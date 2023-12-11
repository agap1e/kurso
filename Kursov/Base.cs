namespace Base
{
    class BD
    {
        List<Comics> BD_Comics = new List<Comics>();
        List<Genre> BD_Genre = new List<Genre>();
        List<Writer> BD_Writer = new List<Writer>();
        List<Publisher> BD_Publisher = new List<Publisher>();
        public void AddComics(string title, int year, int circulation) //Добавляем в бд комикс
        {
            Comics comics = new Comics(title, year, circulation);
            BD_Comics.Add(comics);
        }
        public void AddGenre(string name) //Добавляем в бд жанры
        {
            Genre genre = new Genre(name);
            int? sim = Check(genre, BD_Genre);
            if (sim is null) BD_Genre.Add(genre);
            else
            {
                if(isAdd(BD_Genre[(int)sim].Name) == true)
                {
                    BD_Genre.Add(genre);
                }
            }

        }
        public void AddWriter(string name) //Добавляем в бд сценаристов
        {
            Writer writer = new Writer(name);
            BD_Writer.Add(writer);
        }
        public void AddPublisher(string name, string country) //Добавляем в бд издателей
        {
            Publisher publ = new Publisher(name, country);
            BD_Publisher.Add(publ);
        }
        public void Remove() { } //Удаление элемента бд
        public void Clear() { } //Удаление всего бд
        public void Search() //Поиск в бд
        {

        }
        public void Show(int n)
        {
            switch(n){
                case 0:
                    Console.WriteLine("Жанр");
                    foreach (Genre genre in BD_Genre) 
                    {
                        Console.WriteLine(genre.Name);
                    }
                    break;
            }
           /*foreach (Comics comics in BD_Comics)
            {
                Console.WriteLine($"{comics.ID}, {comics.Title}, ЖАНР, {comics.Year}, {comics.Circulation}, {FindWriter(comics.Writer_id)}, Издатель ");
            }*/
        }
        private string FindWriter(int writer_id) //Поиск сценарист по ID
        {
            foreach (Writer writer in BD_Writer)
            {
                if (writer.ID == writer_id)
                {
                    return writer.Name;
                }
            }
            return "Не удалось найти сценариста";
        }
        private int? Check<T>(T what, List<T> list) where T : Identity//коэффициент Танимото
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
            Console.WriteLine(index);
            return index;
        }
        private bool isAdd(string sim) //Исключения!!!
        {
            Console.WriteLine($"Значение похоже на: {sim}, уверены, что написали правильно?(y/n)");
            if(Console.ReadLine() == "y")
            {
                return true;
            }
            return false;
        }
    }
    abstract class Identity
    {
        public abstract int ID { get; }
        public abstract string Name { get; set; }
    }
    class Comics : Identity
    {
        static int new_id = -1;
        int year;
        int circulation;
        public override int ID { get; }
        public override string Name { get; set; }//Исключения
        public int Year  //Исключения
        {
            get { return year; }
            set { year = value; }
        }
        public int Circulation   //Исключения
        {
            get { return circulation; }
            set { circulation = value; }
        }
        public Comics (string title, int year, int circulation)
        {
            ID = new_id++;
            Name = title;
            Year = year;
            Circulation = circulation;
        }
        public int Writer_id { get; set; } //Автосвойство потому что программа сама поставит
        public int Publ_id { get; set; } //Автосвойство потому что программа сама поставит
        public int Genre_id { get; set; } //Автосвойство потому что программа сама поставит

    }
    class Genre : Identity
    {
        static int new_id = 0;
        string name;
        public override int ID { get; }
        public override string Name    // Исключения!
        {
            get => name;
            set
            {
                value = value.Trim();
                if ((value is null) || (value.Length == 0))
                {
                    throw new Exception("Должно быть указано название жанра.");
                }
                else
                {
                    name = value;
                }
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
        static int new_id = -1;
        string name;
        public override int ID { get; }
        public override string Name  //Исключения!
        {
            get { return name; }
            set { name = value; }
        }
        public Writer(string name)
        {
            ID = new_id++;
            Name = name;
        }
    }
    class Publisher : Identity
    {
        static int new_id = -1;
        string name;
        string country;
        public override int ID { get; }
        public override string Name    // Исключения!
        {
            get { return name; }
            set { name = value; }
        }
        public string Country   // Исключения!
        {
            get { return country; }
            set { country = value; }
        }
        public Publisher(string name, string country)
        {
            ID = new_id++;
            Name = name;
            Country = country;
        }
    }
}
