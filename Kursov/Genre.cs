using IdentityClass;
using System.Text.RegularExpressions;

namespace GenreClass
{
    internal class Genre : Identity
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
                else if (int.TryParse(value, out var num) == true)
                {
                    throw new Exception("Название жанра некорректно введено");
                }
                else if (Regex.IsMatch(value, @"^[А-ЯЁа-яё][а-яё]*(?:-[А-ЯЁа-яё][а-яё]*)?$"))
                {
                    name = value;
                }
                else throw new Exception("Название жанра некорректно введено");
            }
        }
        public Genre(string name)
        {
            ID = new_id++;
            Name = name;
        }
    }
}
