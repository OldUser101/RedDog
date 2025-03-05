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
            Hand h = new Hand();
            while (true) 
            {
                h.LoadHand();
                Console.WriteLine(h.ToString());
                Console.ReadLine();
                h.Reveal();
                Console.WriteLine(h.ToString());
                Console.ReadLine();
            }
        }
    }
}
