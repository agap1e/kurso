using IdentityClass;

namespace WriterClass
{
    internal class Writer : Identity
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
                else if (int.TryParse(value, out var num) == true)
                {
                    throw new Exception("Фамилия сценариста некорректно введена");
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
}
