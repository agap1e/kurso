using ComicsClass;
using GenreClass;
using WriterClass;
using PublisherClass;
using IdentityClass;
using System.Xml.Linq;

namespace Base
{
    class BD
    {
        List<Comics> BD_Comics = new List<Comics>();
        List<Genre> BD_Genre = new List<Genre>();
        List<Writer> BD_Writer = new List<Writer>();
        List<Publisher> BD_Publisher = new List<Publisher>();
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
            comics.Writer_id = FindWriterInfo(str).ID;
        }
        public void AddPubl(Comics comics, string str)
        {
            comics.Publ_id = FindPublisherInfo(str).ID;
        }
        public void AddGenre(Comics comics, string str)
        {
            comics.Genre_id = FindGenreInfo(str).ID;
        }
        
        public void RemoveComics(string name) //Удаление комикса
        {
            if (BD_Comics.Contains(FindComicsInfo(name)))
            {
                BD_Comics.Remove(FindComicsInfo(name));
            }
            else throw new Exception("Нельзя удалить комикс, т.к. его не существует");
        }
        public void RemoveGenre(string name) //Удаление жанра
        {
            if (CheckForRemove(name) && BD_Genre.Contains(FindGenreInfo(name)))
            {
                BD_Genre.Remove(FindGenreInfo(name));
            }
            else throw new Exception("Нельзя удалить жанр, т.к. он используется или не существует");
        }
        public void RemoveWriter(string name) //Удаление сценариста
        {
            if (CheckForRemove(name) && BD_Writer.Contains(FindWriterInfo(name)))
            {
                BD_Writer.Remove(FindWriterInfo(name));
            }
            else throw new Exception("Нельзя удалить сценариста, т.к. он используется или не существует");
        }
        public void RemovePublisher(string name) //Удаление издателя
        {
            if (CheckForRemove(name) && BD_Publisher.Contains(FindPublisherInfo(name)))
            {
                BD_Publisher.Remove(FindPublisherInfo(name));
            }
            else throw new Exception("Нельзя удалить издателя, т.к. он используется или не существует");
        }
        private bool CheckForRemove(string name)
        {
            foreach(Comics com in BD_Comics)
            {
                if (com.Genre_id == FindGenreInfo(name).ID || com.Writer_id == FindWriterInfo(name).ID || com.Publ_id == FindPublisherInfo(name).ID)
                {
                    return false;
                }
            }
            return true;
        }
        public void Clear() //Удаление всего бд
        { 
            BD_Comics.Clear();
            BD_Genre.Clear();
            BD_Writer.Clear();
            BD_Publisher.Clear();
        }
        public void Show(int n)
        {
            switch(n){
                case 1:
                    Console.WriteLine("Название комикса  Жанр  Год выхода  Тираж  Цена  Сценарист  Издатель\n");
                    foreach (Comics comics in BD_Comics) 
                    {
                        Console.WriteLine(comics.Name + "\t" + FindGenreInfo(comics.Genre_id).Name + "\t" + comics.Year + "\t" + comics.Circulation + "\t" + comics.Price + "\t" + FindWriterInfo(comics.Writer_id).Name + "\t" + FindPublisherInfo(comics.Publ_id).Name);
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
                    Console.WriteLine("Издатели  Страна\n");
                    foreach (Publisher publisher in BD_Publisher)
                    {
                        Console.WriteLine(publisher.Name + "\t" + publisher.Country);
                    }
                    break;
            }
        } //Вывод
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
        private bool isAdd(string sim)
        {
            Console.WriteLine($"Значение похоже на: {sim}, уверены, что написали правильно?(y/n)");
            if(Console.ReadLine() == "y")
            {
                return true;
            }
            return false;
        }
        private Writer FindWriterInfo(int writer_id) //Поиск сценарист по ID
        {
            foreach (Writer writer in BD_Writer)
            {
                if (writer.ID == writer_id)
                {
                    return writer;
                }
            }
            throw new Exception("Не удалось найти сценариста");
        }
        private Writer FindWriterInfo(string writer_name) //Поиск ID сценарист по фамилии
        {
            foreach (Writer writer in BD_Writer)
            {
                if (writer.Name == writer_name)
                {
                    return writer;
                }
            }
            throw new Exception("Не удалось найти сценариста");
        }
        private Genre FindGenreInfo(int genre_id) //Поиск жанра по ID
        {
            foreach (Genre genre in BD_Genre)
            {
                if (genre.ID == genre_id)
                {
                    return genre;
                }
            }
            throw new Exception("Не удалось найти жанр");
        }
        private Genre FindGenreInfo(string genre_name) //Поиск ID жанра по названию
        {
            foreach (Genre genre in BD_Genre)
            {
                if (genre.Name == genre_name)
                {
                    return genre;
                }
            }
            throw new Exception("Не удалось найти жанр");
        }
        private Publisher FindPublisherInfo(int publ_id) //Поиск издательства по ID
        {
            foreach (Publisher publ in BD_Publisher)
            {
                if (publ.ID == publ_id)
                {
                    return publ;
                }
            }
            throw new Exception("Не удалось найти издателя");
        }
        private Publisher FindPublisherInfo(string publ_name) //Поиск ID издательства по названию
        {
            foreach (Publisher publ in BD_Publisher)
            {
                if (publ.Name == publ_name)
                {
                    return publ;
                }
            }
            throw new Exception("Не удалось найти издателя");
        }
        private Comics FindComicsInfo(string comics_title) //Поиск ID издательства по названию
        {
            foreach (Comics comics in BD_Comics)
            {
                if (comics.Name == comics_title)
                {
                    return comics;
                }
            }
            throw new Exception("Не удалось найти комикс");
        }
    }
}