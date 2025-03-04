namespace RedDog
{
    internal class Program
    {
        public static void ShowTitle() 
        {
            Console.Write("=== ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("RED DOG");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" ===");
            Console.WriteLine("(c) Nathan Gill");
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            ShowTitle();
        }
    }
}
