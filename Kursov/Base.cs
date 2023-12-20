using ComicsClass;
using GenreClass;
using WriterClass;
using PublisherClass;
using IdentityClass;
using Spire.Xls;
using System.Data.SqlTypes;

namespace Base
{
    class BD
    {
        List<Comics> BD_Comics = new List<Comics>();
        List<Genre> BD_Genre = new List<Genre>();
        List<Writer> BD_Writer = new List<Writer>();
        List<Publisher> BD_Publisher = new List<Publisher>();
        Workbook workbook = new Workbook();
        public BD()
        {
            if (File.Exists("Base.xlsx"))
            {
                workbook.LoadFromFile("Base.xlsx");

                Worksheet worksheet = workbook.Worksheets[1];
                int rowsCount = worksheet.Rows.Count();
                for (int i = 2; i <= rowsCount; i++)
                {
                    Add(worksheet.Range[i, 2].Value, true);
                }

                worksheet = workbook.Worksheets[2];
                rowsCount = worksheet.Rows.Count();
                for (int i = 2; i <= rowsCount; i++)
                {
                    Add(worksheet.Range[i, 2].Value);
                }

                worksheet = workbook.Worksheets[3];
                rowsCount = worksheet.Rows.Count();
                for (int i = 2; i <= rowsCount; i++)
                {
                    Add(worksheet.Range[i, 2].Value, worksheet.Range[i, 3].Value);
                }

                worksheet = workbook.Worksheets[0];
                rowsCount = worksheet.Rows.Count();
                for (int i = 2; i <= rowsCount; i++)
                {
                    Add(worksheet.Range[i, 2].Value, int.Parse(worksheet.Range[i, 3].Value), int.Parse(worksheet.Range[i, 5].Value), int.Parse(worksheet.Range[i, 6].Value), FindGenreInfo(int.Parse(worksheet.Range[i, 4].Value)).Name, FindWriterInfo(int.Parse(worksheet.Range[i, 7].Value)).Name, FindPublisherInfo(int.Parse(worksheet.Range[i, 8].Value)).Name, FindPublisherInfo(int.Parse(worksheet.Range[i, 8].Value)).Country);
                }
            }
            else
            {
                workbook.CreateEmptySheet();
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.Range[1, 1].Value = "ID комикса";
                worksheet.Range[1, 2].Value = "Название комикса";
                worksheet.Range[1, 3].Value = "Год выхода";
                worksheet.Range[1, 4].Value = "ID Жанра";
                worksheet.Range[1, 5].Value = "Тираж";
                worksheet.Range[1, 6].Value = "Цена";
                worksheet.Range[1, 7].Value = "ID Сценариста";
                worksheet.Range[1, 8].Value = "ID Издателя";
                worksheet.AllocatedRange.AutoFitColumns();
                worksheet = workbook.Worksheets[1];
                worksheet.Range[1, 1].Value = "ID жанра";
                worksheet.Range[1, 2].Value = "Название жанра";
                worksheet.AllocatedRange.AutoFitColumns();
                worksheet = workbook.Worksheets[2];
                worksheet.Range[1, 1].Value = "ID сценариста";
                worksheet.Range[1, 2].Value = "Фамилия сценариста";
                worksheet.AllocatedRange.AutoFitColumns();
                worksheet = workbook.Worksheets[3];
                worksheet.Range[1, 1].Value = "ID издателя";
                worksheet.Range[1, 2].Value = "Название издателя";
                worksheet.Range[1, 3].Value = "Страна";
                worksheet.AllocatedRange.AutoFitColumns();
            }
        }
        public void Add(string title, int year, int circulation, int price, string genre, string writer, string publ, string country) //Добавляем в бд комикс
        {
            Comics comics = new Comics(title, year, circulation, price);
            try
            {
                AddWriter(comics, writer);
            }
            catch 
            {
                Add(writer, comics);
            }
            try
            {
                AddPubl(comics, publ);
            }
            catch 
            {
                Add(publ, country, comics);
            }
            try
            {
                AddGenre(comics, genre);
            }
            catch
            {
                Add(genre, true, comics);
            }
            BD_Comics.Add(comics);
            Save(comics);
        }
        public void Add(string name, bool isGenre) //Добавляем в бд жанры
        {
            Genre genre = new Genre(name);
            int? sim = Check(genre, BD_Genre);
            if (sim is null)
            {
                BD_Genre.Add(genre);
                Save(genre);
            }
            else
            {
                if (isAdd(BD_Genre[(int)sim].Name))
                {
                    BD_Genre.Add(genre);
                    Save(genre);
                }
            }
        }
        public void Add(string name) //Добавляем в бд сценаристов
        {
            Writer writer = new Writer(name);
            int? sim = Check(writer, BD_Writer);
            if (sim is null)
            {
                BD_Writer.Add(writer);
                Save(writer);
            }
            else
            {
                if (isAdd(BD_Writer[(int)sim].Name))
                {
                    BD_Writer.Add(writer);
                    Save(writer);
                }
            }
        }
        public void Add(string name, string country) //Добавляем в бд издателей
        {
            Publisher publ = new Publisher(name, country);
            int? sim = Check(publ, BD_Publisher);
            if (sim is null)
            {
                BD_Publisher.Add(publ);
                Save(publ);
            }
            else
            {
                if (isAdd(BD_Publisher[(int)sim].Name))
                {
                    BD_Publisher.Add(publ);
                    Save(publ);
                }
            }
        }
        
        public void RemoveComics(string name) //Удаление комикса
        {
            if (BD_Comics.Contains(FindComicsInfo(name)))
            {
                Delete(0, FindComicsInfo(name).ID);
                BD_Comics.Remove(FindComicsInfo(name));
            }
            else throw new Exception("Нельзя удалить комикс, т.к. его не существует");
        }
        public void RemoveGenre(string name) //Удаление жанра
        {
            if (CheckForRemove(name) && BD_Genre.Contains(FindGenreInfo(name)))
            {
                Delete(1, FindGenreInfo(name).ID);
                BD_Genre.Remove(FindGenreInfo(name));
            }
            else throw new Exception("Нельзя удалить жанр, т.к. он используется или не существует");
        }
        public void RemoveWriter(string name) //Удаление сценариста
        {
            if (CheckForRemove(name) && BD_Writer.Contains(FindWriterInfo(name)))
            {
                Delete(2, FindWriterInfo(name).ID);
                BD_Writer.Remove(FindWriterInfo(name));
            }
            else throw new Exception("Нельзя удалить сценариста, т.к. он используется или не существует");
        }
        public void RemovePublisher(string name) //Удаление издателя
        {
            if (CheckForRemove(name) && BD_Publisher.Contains(FindPublisherInfo(name)))
            {
                Delete(3, FindPublisherInfo(name).ID);
                BD_Publisher.Remove(FindPublisherInfo(name));
            }
            else throw new Exception("Нельзя удалить издателя, т.к. он используется или не существует");
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
        private bool CheckForRemove(string name) //Проверка на возможность удаления элемента
        {
            foreach (Comics com in BD_Comics)
            {
                if (com.Genre_id == FindGenreInfo(name).ID) return false;
                if (com.Writer_id == FindWriterInfo(name).ID) return false;
                if (com.Publ_id == FindPublisherInfo(name).ID) return false;
            }
            return true;
        }
        private int? Check<T>(T what, List<T> list) where T : Identity  //Проверка на правильность написания (коэффициент Танимото)
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
        private bool isAdd(string sim) //Запрос на сохранение элемента
        {
            Console.WriteLine($"Значение похоже на: {sim}, уверены, что написали правильно?(y/n)");
            if(Console.ReadLine() == "y")
            {
                return true;
            }
            return false;
        }
        public void Search(int i, string str)
        {
            switch(i)
            {
                case 1:
                    foreach(Comics comics in BD_Comics)
                    {
                        if (comics.Name == str) Show(comics);
                    }
                    break;
                case 2:
                    Genre genre = FindGenreInfo(str);
                    foreach(Comics comics in BD_Comics)
                    {
                        if (comics.Genre_id == genre.ID)
                        {
                            Show(comics);
                        }
                    }
                    break;
                case 3:
                    Writer writer = FindWriterInfo(str);
                    foreach (Comics comics in BD_Comics)
                    {
                        if (comics.Writer_id == writer.ID)
                        {
                            Show(comics);
                        }
                    }
                    break;
                case 4:
                    Publisher publ = FindPublisherInfo(str);
                    foreach (Comics comics in BD_Comics)
                    {
                        if (comics.Publ_id == publ.ID)
                        {
                            Show(comics);
                        }
                    }
                    break;
            }
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
        private void AddWriter(Comics comics, string str) //Добавление в комикс сценариста
        {
            comics.Writer_id = FindWriterInfo(str).ID;    
        }
        private void AddPubl(Comics comics, string str) //Добавление в комикс издателя
        {
            comics.Publ_id = FindPublisherInfo(str).ID;
        }
        private void AddGenre(Comics comics, string str) //Добавление в комикс жанра
        {
            comics.Genre_id = FindGenreInfo(str).ID;
        }
        private void Save(Comics comic)
        {
            Worksheet worksheet = workbook.Worksheets[0];
            int g = comic.ID + 2;
            worksheet.Range[g, 1].Value = comic.ID.ToString();
            worksheet.Range[g, 2].Value = comic.Name;
            worksheet.Range[g, 3].Value = comic.Year.ToString();
            worksheet.Range[g, 4].Value = comic.Genre_id.ToString();
            worksheet.Range[g, 5].Value = comic.Circulation.ToString();
            worksheet.Range[g, 6].Value = comic.Price.ToString();
            worksheet.Range[g, 7].Value = comic.Writer_id.ToString();
            worksheet.Range[g, 8].Value = comic.Publ_id.ToString();
            workbook.SaveToFile("Base.xlsx", ExcelVersion.Version2016);
        }
        private void Save(Genre genre)
        {
            Worksheet worksheet = workbook.Worksheets[1];
            int g = genre.ID + 2;
            worksheet.Range[g, 1].Value = genre.ID.ToString();
            worksheet.Range[g, 2].Value = genre.Name;
            workbook.SaveToFile("Base.xlsx", ExcelVersion.Version2016);
        }
        private void Save(Writer writer)
        {
            Worksheet worksheet = workbook.Worksheets[2];
            int g = writer.ID + 2;
            worksheet.Range[g, 1].Value = writer.ID.ToString();
            worksheet.Range[g, 2].Value = writer.Name;
            workbook.SaveToFile("Base.xlsx", ExcelVersion.Version2016);
        }
        private void Save(Publisher publ)
        {
            Worksheet worksheet = workbook.Worksheets[3];
            int g = publ.ID + 2;
            worksheet.Range[g, 1].Value = publ.ID.ToString();
            worksheet.Range[g, 2].Value = publ.Name;
            worksheet.Range[g, 3].Value = publ.Country;
            workbook.SaveToFile("Base.xlsx", ExcelVersion.Version2016);
        }
        private void Delete(int i, int b)
        {
            Worksheet worksheet = workbook.Worksheets[i];
            for (int g = 2; g <= worksheet.Rows.Count(); g++)
            {
                if (int.Parse(worksheet.Range[g,1].Value) == b)
                {
                    worksheet.DeleteRow(g);
                    workbook.SaveToFile("Base.xlsx", ExcelVersion.Version2016);
                }
            }
            
        }
        private void Add(string name, bool isGenre, Comics comics) //Добавляем в бд жанры
        {
            Genre genre = new Genre(name);
            int? sim = Check(genre, BD_Genre);
            if (sim is null)
            {
                BD_Genre.Add(genre);
                comics.Genre_id = genre.ID;
                Save(genre);
            }
            else
            {
                if (isAdd(BD_Genre[(int)sim].Name))
                {
                    BD_Genre.Add(genre);
                    comics.Genre_id = genre.ID;
                    Save(genre);
                }
                else
                {
                    comics.Genre_id = (int)sim;
                }
            }
        }
        private void Add(string name, Comics comics) //Добавляем в бд сценаристов
        {
            Writer writer = new Writer(name);
            int? sim = Check(writer, BD_Writer);
            if (sim is null)
            {
                BD_Writer.Add(writer);
                comics.Writer_id = writer.ID;
                Save(writer);
            }
            else if (isAdd(BD_Writer[(int)sim].Name))
            {
                BD_Writer.Add(writer);
                comics.Writer_id = writer.ID;
                Save(writer);
            }
            else
            {
                comics.Writer_id = (int)sim;
            }
        }
        private void Add(string name, string country, Comics comics) //Добавляем в бд издателей
        {
            Publisher publ = new Publisher(name, country);
            int? sim = Check(publ, BD_Publisher);
            if (sim is null)
            {
                BD_Publisher.Add(publ);
                comics.Publ_id = publ.ID;
                Save(publ);
            }
            else
            {
                if (isAdd(BD_Publisher[(int)sim].Name))
                {
                    BD_Publisher.Add(publ);
                    comics.Publ_id = publ.ID;
                    Save(publ);
                }
                else
                {
                    comics.Publ_id = (int)sim;
                }
            }
        }
        private void Show(Comics comics)
        {
            Console.WriteLine(comics.Name + "\t" + FindGenreInfo(comics.Genre_id).Name + "\t" + comics.Year + "\t" + comics.Circulation + "\t" + comics.Price + "\t" + FindWriterInfo(comics.Writer_id).Name + "\t" + FindPublisherInfo(comics.Publ_id).Name);
        }
    }
}