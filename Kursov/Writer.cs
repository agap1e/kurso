using IdentityClass;
using System.Text.RegularExpressions;

namespace WriterClass
{
    internal class Writer : Identity
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
                    throw new Exception("Должна быть указана фамилия автора.");
                }
                else if (int.TryParse(value, out var num) == true)
                {
                    throw new Exception("Фамилия сценариста некорректно введена");
                }
                else if (Regex.IsMatch(value, @"^[А-ЯЁа-яё][А-ЯЁа-яё]*(?:-[А-ЯЁа-яё][А-ЯЁа-яё]*)?$"))
                {
                    name = value;
                }
                else throw new Exception("Фамилия сценариста некорректно введена");
            }
        }
        public Writer(string name)
        {
            ID = new_id++;
            Name = name;
        }
    }
}
