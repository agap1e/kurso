using System.Xml.Linq;

namespace Kursov //название поменять
{
    class BD    //В отдельный неймспейс
    {
        List<Comics> BD_Comics = new List<Comics>();
        List<Genre> BD_Genre = new List<Genre>();
        List<Writer> BD_Writer = new List<Writer>();
        List<Publisher> BD_Publisher = new List<Publisher>();
        public void BD_AddComics(int id, string title, int year, int circulation) //Добавляем в бд комикс
        {
            Comics comics = new Comics(id, title, year, circulation);
            BD_Comics.Add(comics);
        }
        public void BD_AddGenre(int id, string name) //Добавляем в бд жанры
        {
            Genre genre = new Genre(id, name);
            BD_Genre.Add(genre);
        }
        public void BD_AddWriter(int id, string name) //Добавляем в бд сценаристов
        {
            Writer writer = new Writer(id, name);
            BD_Writer.Add(writer);
        }
        public void BD_AddPublisher(int id, string name, string country) //Добавляем в бд издателей
        {
            Publisher publ = new Publisher(id, name, country);
            BD_Publisher.Add(publ);
        }
        public void BD_Remove() { } //Удаление элемента бд
        public void BD_Clear() { } //Удаление всего бд
        public void BD_Search() //Поиск в бд
        { 

        } 
        public void BD_Show() 
        {
            foreach(Comics comics in BD_Comics)
            {
                Console.WriteLine($"{comics.Comics_id}, {comics.Comics_title}, ЖАНР, {comics.Comics_year}, {comics.Comics_circulation}, {BD_FindWriter(comics.Writer_id)}, Издатель ");
            }
        }
        private string BD_FindWriter(int writer_id) //Поиск сценарист по ID
        {
            foreach(Writer writer in BD_Writer)
            {
                if(writer.Writer_id == writer_id)
                {
                    return writer.Writer_name;
                }
            }
            return "Не удалось найти сценариста";
        }
    }
    class Comics    //В отдельный неймспейс
    {
        int comics_id;
        string comics_title;
        int comics_year;
        int comics_circulation;
        public int Comics_id // надо поставить константу!!!
        {
            get { return comics_id; }
            set { comics_id = value; }  
        }
        public string Comics_title //Исключения
        {
            get { return comics_title; }
            set { comics_title = value; }
        }
        public int Comics_year  //Исключения
        {
            get { return comics_year; }
            set { comics_year = value; }
        }
        public int Comics_circulation   //Исключения
        {
            get { return comics_circulation; }
            set { comics_circulation = value; }
        }
        public Comics(int id, string title, int year, int circulation)
        {
            Comics_id = id;
            Comics_title = title;
            Comics_year = year;
            Comics_circulation = circulation;
        }
        public int Writer_id { get; set; } //Автосвойство потому что программа сама поставит
        public int Publ_id { get; set; } //Автосвойство потому что программа сама поставит
        public int Genre_id { get; set; } //Автосвойство потому что программа сама поставит

    }
    class Genre //В отдельный неймспейс
    {
        int genre_id;
        string genre_name;
        public int Genre_id // надо поставить константу!!!
        {
            get { return genre_id; }
            set { genre_id = value; }
        }
        public string Genre_name    // Исключения!
        {
            get { return genre_name; }
            set { genre_name = value; }
        }
        public Genre(int id, string name)
        {
            Genre_id = id;
            Genre_name = name;
        }
    }
    class Writer    //В отдельный неймспейс
    {
        int writer_id;
        string writer_name;
        public int Writer_id //надо поставить константу!!!
        {
            get { return writer_id; }
            set { writer_id = value; }
        }
        public string Writer_name  //Исключения!
        {
            get { return writer_name; }
            set { writer_name = value; }
        }
        public Writer(int id, string name)
        {
            Writer_id = id;
            Writer_name = name;
        }
    }
    class Publisher //В отдельный неймспейс
    {
        int publisher_id;
        string publisher_name;
        string publisher_country;
        public int Publ_id // надо поставить константу!!!
        {
            get { return publisher_id; }
            set { publisher_id = value; }
        }
        public string Publ_name    // Исключения!
        {
            get { return publisher_name; }
            set { publisher_name = value; }
        }
        public string Publ_country   // Исключения!
        {
            get { return publisher_country; }
            set { publisher_country = value; }
        }
        public Publisher(int id, string name, string country)
        {
            Publ_id = id;
            Publ_name = name;
            Publ_country = country;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            BD bd = new BD();
            int comics_id;
            string comics_title;
            int comics_year;
            int comics_circulation;
            Console.WriteLine("Чтобы добавить новый комикс введите: ID, Название, год выпуска, тираж");
            comics_id = int.Parse(Console.ReadLine());
            comics_title = Console.ReadLine();
            comics_year = int.Parse(Console.ReadLine());
            comics_circulation = int.Parse(Console.ReadLine());
            bd.BD_AddComics(comics_id, comics_title, comics_year, comics_circulation);
            Console.WriteLine("Чтобы добавить новый комикс введите: ID, Название, год выпуска, тираж");
            comics_id = int.Parse(Console.ReadLine());
            comics_title = Console.ReadLine();
            comics_year = int.Parse(Console.ReadLine());
            comics_circulation = int.Parse(Console.ReadLine());
            bd.BD_AddComics(comics_id, comics_title, comics_year, comics_circulation);
            bd.BD_Show();


        }
    }
}