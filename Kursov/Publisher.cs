using IdentityClass;
using System.Text.RegularExpressions;

namespace PublisherClass
{
    internal class Publisher : Identity
    {
        static int new_id = 0;
        string name;
        string country;
        public override int ID { get; }
        public override string Name
        {
            get => name;
            set
            {
                value = value.Trim();
                if ((value is null) || (value.Length == 0))
                {
                    throw new Exception("Должно быть указано название издателя.");
                }
                else if (int.TryParse(value, out var num) == true)
                {
                    throw new Exception("Название издателя некорректно введено");
                }
                else if (Regex.IsMatch(value, @"^[А-ЯЁа-яё][А-ЯЁа-яё]*(?:-[А-ЯЁа-яё][А-ЯЁа-яё]*)?$"))
                {
                    name = value;
                }
                else throw new Exception("Название издателя некорректно введено");
            }
        }
        public string Country
        {
            get => country;
            set
            {
                value = value.Trim();
                if ((value is null) || (value.Length == 0))
                {
                    throw new Exception("Должно быть указано название страны.");
                }
                else if (int.TryParse(value, out var num) == true)
                {
                    throw new Exception("Название страны некорректно введено");
                }
                else if (Regex.IsMatch(value, @"^[А-ЯЁа-яё][а-яё]*(?:-[А-ЯЁа-яё][а-яё]*)?$"))
                {
                    country = value;
                }
                else throw new Exception("Название страны некорректно введено");
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
