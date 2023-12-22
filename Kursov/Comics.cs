using IdentityClass;
using System.Text.RegularExpressions;

namespace ComicsClass
{
    internal class Comics : Identity
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
                else if (int.TryParse(value, out var num) == true)
                {
                    throw new Exception("Название комикса некорректно введено");
                }
                else if (Regex.IsMatch(value, @"^[А-ЯЁа-яё][А-ЯЁа-яё]*(?:-[А-ЯЁа-яё][А-ЯЁа-яё]*)?$"))
                {
                    name = value;
                }
                else throw new Exception("Название комикса некорректно введено");
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
        public Comics(string title, int year, int circulation, int price)
        {
            ID = new_id++;
            Name = title;
            Year = year;
            Circulation = circulation;
            Price = price;
        }
        public int Writer_id { get; set; }
        public int Publ_id { get; set; }
        public int Genre_id { get; set; }
    }
}
