using IdentityClass;

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
                else name = value;
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
