using Base;
namespace Kursov
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            BD bd = new BD();
            bool isEx = true;
            string YN;
            while (isEx) 
            {
                try
                {
                    bd.AddGenre(Console.ReadLine()!);
                    isEx = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}, хотите написать ещё раз?(y/n)");
                    YN = Console.ReadLine();
                    if (YN == "n")
                    {
                        isEx = false; 
                    }
                }
            }
            bd.AddGenre(Console.ReadLine()!);
            bd.Show(0);
        }
    }
}