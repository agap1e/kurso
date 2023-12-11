using Base;
namespace Kursov
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            BD bd = new BD();
            /*bool isEx = true;
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
            bd.Show(0);*/
            string title = "Человек-паук";
            int year = 2023;
            int circual = 10000;
            int price = 10;
            bd.Show(2);
            string str1 = Console.ReadLine();
            if (str1 == "новый")
            {
                str1 = Console.ReadLine();
                bd.Add(str1);
            }
            else
            {
                str1 = Console.ReadLine();
                bd.AddWriter()
            }
            bd.Add(title, year, circual, price);
        }
    }
}