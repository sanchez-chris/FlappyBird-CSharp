class Program
{
    const int ScreenWidth = 80;
    const int ScreenHeight = 20;
    const int WinWidth = 70;
    static int birdPos = ScreenHeight / 2;
    static int score = 0;
    static int[] pipePos = new int[20]; // Program.pipePos[x] se puede acceder sin hacer un objeto programa
    static int[] gapPos = new int[2];
    static int[] pipeFlag = new int[2];

    static void Main()
    {
        Console.CursorVisible = false;
        Random random = new Random();
        Console.SetWindowSize(ScreenWidth, ScreenHeight);
        Console.SetBufferSize(ScreenWidth, ScreenHeight);

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
                StartGame();
            }
            else if (op == '2')
            {
                Instructions();
            }
            else if (op == '3')
            {
                Environment.Exit(0);
            }
        }
    }

    static void Instructions()
    {
        Console.Clear();
        Console.WriteLine("Instructions");
        Console.WriteLine("----------------");
        Console.WriteLine("Press spacebar to make the bird fly");
        Console.WriteLine("Press any key to go back to menu");
        Console.ReadKey();
    }

    static void StartGame()
    {
        birdPos = ScreenHeight / 2;
        score = 0;
        pipeFlag[0] = 1;
        pipeFlag[1] = 0;
        pipePos[0] = pipePos[1] = 4;

        Console.Clear();
        DrawBorder();
        GeneratePipe(0);
        UpdateScore();

        Console.SetCursorPosition(10, 5);
        Console.Write("Press any key to start");
        Console.ReadKey();
        Console.SetCursorPosition(10, 5);
        Console.Write("                      ");

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar && birdPos > 1)
                {
                    birdPos -= 3;
                }
                if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            DrawBird();
            DrawPipe(0);
            DrawPipe(1);

            if (CheckCollision())
            {
                GameOver();
                return;
            }

            Thread.Sleep(100);
            EraseBird();
            ErasePipe(0);
            ErasePipe(1);
            birdPos++;

            if (birdPos >= ScreenHeight - 1)
            {
                GameOver();
                return;
            }

            if (pipeFlag[0] == 1)
            {
                pipePos[0] += 2;
            }

            if (pipeFlag[1] == 1)
            {
                pipePos[1] += 2;
            }

            if (pipePos[0] >= 40)
            {
                pipeFlag[1] = 1;
                pipePos[1] = 4;
                GeneratePipe(1);
            }

            if (pipePos[0] > 68)
            {
                score++;
                UpdateScore();
                pipeFlag[1] = 0;
                pipePos[0] = pipePos[1];
                gapPos[0] = gapPos[1];
            }
        }
    }

    static void DrawBorder()
    {
        for (int i = 0; i < ScreenHeight; i++)
        {
            Console.SetCursorPosition(WinWidth, i);
            Console.Write("|");
        }
        for (int j = 0; j < WinWidth; j++)
        {
            Console.SetCursorPosition(j, 19);
            Console.Write("_");
        }
    }

    static void GeneratePipe(int index)
    {
        Random random = new Random();

        gapPos[index] = 3 + random.Next(14);
    }
    static void DrawPipe(int index)
    {
        if (pipeFlag[index] == 1)
        {
            for (int i = 0; i < ScreenHeight-2; i++)
            {
                if (i < gapPos[index] || i > gapPos[index] + 6)
                {
                    Console.SetCursorPosition(WinWidth - pipePos[index], i);
                    Console.Write("***");
                }
            }
        }
    }

    static void ErasePipe(int index)
    {
        if (pipeFlag[index] == 1)
        {
            for (int i = 0; i < ScreenHeight; i++)
            {
                if (i < gapPos[index] || i > gapPos[index] + 6)
                {
                    Console.SetCursorPosition(WinWidth - pipePos[index], i);
                    Console.Write("   ");
                }
            }
        }
    }



    static void DrawBird()
    {
        Console.SetCursorPosition(2, birdPos);
        Console.Write("//--o\n");
        Console.WriteLine(" // |__ >");
    }

    static void EraseBird()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.SetCursorPosition(2, birdPos);
                Console.Write("                                \n                        ");
            }
        }
    }

    static bool CheckCollision()
    {
        if (pipePos[0] >= WinWidth - 4 && pipePos[0] <= WinWidth)
        {
            if (birdPos < gapPos[0] || birdPos > gapPos[0] + 6)
            {
                return false;
            }
        }
        return false;
    }

    static void GameOver()
    {
        Console.Clear();
        Console.WriteLine("Game Over!");
        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }

    static void UpdateScore()
    {
        Console.SetCursorPosition(WinWidth + 5, 5);
        Console.Write("Score: " + score);
    }



}
