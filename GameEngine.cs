using System.Collections;
namespace GameEngine
{
    public class Game
    {
        const int screenHeight = 20;
        const int screenWidth = 70;
        static int birdPos = screenHeight / 2; // static = you can access it without having to do an object of the class
        static int score = 0;
        static List<int> pipePos = new List<int>();
        //static int[] pipePos = new int[20];
        static List<int> gapPos = new List<int>();
        //static int[] gapPos = new int[2];
        static List<int> pipeFlag = new List<int>();
       // static int[] pipeFlag = new int[2];
        public void Instructions()
        {
            Console.Clear();
            Console.WriteLine("Instructions");
            Console.WriteLine("----------------");
            Console.WriteLine("Press spacebar to make the bird fly");
            Console.WriteLine("Press any key to go back to menu");
            Console.ReadKey();
        }

        public void StartGame()
        { 
            score = 0;
            pipeFlag.Insert(0, 1); // insert in position 0 the value 1
            pipeFlag.Insert(1, 0);
            pipePos.Insert(0, 4);
            pipePos.Insert(1, 4);

            //pipePos[0] = pipePos[1] = 4;

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

                if (birdPos >= screenHeight - 1)
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
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(screenWidth, i);
                Console.Write("|");
            }
            for (int j = 0; j < screenWidth; j++)
            {
                Console.SetCursorPosition(j, 19);
                Console.Write("_");
            }
        }

        static void GeneratePipe(int index)
        {
            Random random = new Random();
            gapPos.Insert(index, 3 + random.Next(14));
            //gapPos[index] = 3 + random.Next(14);
        }
        static void DrawPipe(int index)
        {
            if (pipeFlag[index] == 1)
            {
                for (int i = 0; i < screenHeight - 1; i++)
                {
                    if (i < gapPos[index] || i > gapPos[index] + 6)
                    {
                        Console.SetCursorPosition(screenWidth - pipePos[index], i);
                        Console.Write("***");
                    }
                }
            }
        }

        static void ErasePipe(int index)
        {
            if (pipeFlag[index] == 1)
            {
                for (int i = 0; i < screenHeight - 1; i++)
                {
                    if (i < gapPos[index] || i > gapPos[index] + 6)
                    {
                        Console.SetCursorPosition(screenWidth - pipePos[index], i);
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
            if (pipePos[0] >= screenWidth - 4 && pipePos[0] <= screenWidth)
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
            Console.SetCursorPosition(screenWidth + 5, 5);
            Console.Write("Score: " + score);
        }
    }
}