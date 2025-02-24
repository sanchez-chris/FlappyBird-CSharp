class Program
{
    static void Main()
    {
        var FlappyBird = new GameEngine.Game();
        Console.CursorVisible = false;


        while (true)
        {
            Console.Clear();
            Console.SetCursorPosition(10, 5);
            Console.WriteLine(" -------------------------- ");
            Console.SetCursorPosition(10, 6);
            Console.WriteLine(" |     Flappy Bird        | ");
            Console.SetCursorPosition(10, 7);
            Console.WriteLine(" --------------------------");
            Console.SetCursorPosition(10, 9);
            Console.WriteLine("1. Start Game");
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("2. Instructions");
            Console.SetCursorPosition(10, 11);
            Console.WriteLine("3. Quit");
            Console.SetCursorPosition(10, 13);
            Console.Write("Select option: ");

            char op = Console.ReadKey().KeyChar;

            if (op == '1')
            {
                FlappyBird.StartGame();
            }
            else if (op == '2')
            {
                FlappyBird.Instructions();
            }
            else if (op == '3')
            {
                Environment.Exit(0);
            }
        }
    }
}
