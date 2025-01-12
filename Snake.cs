using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program

{

    static void Main()

    {

        Console.WindowHeight = 16;

        Console.WindowWidth = 32;

        int screenwidth = Console.WindowWidth;

        int screenheight = Console.WindowHeight;

        Random randomnummer = new Random();

        Pixel hoofd = new Pixel();

        hoofd.xPos = screenwidth / 2;

        hoofd.yPos = screenheight / 2;

        hoofd.schermKleur = ConsoleColor.Red;

        string movement = "RIGHT";

        List<int> telje = new List<int>();

        int score = 0;



        List<int> teljePositie = new List<int>();



        teljePositie.Add(hoofd.xPos);

        teljePositie.Add(hoofd.yPos);



        DateTime tijd = DateTime.Now;

        Obstakel obstakel = new Obstakel();

        obstakel.karacter = "*";

        obstakel.schermKleur = ConsoleColor.Cyan;

        obstakel.xPos = randomnummer.Next(1, screenwidth - 1);

        obstakel.yPos = randomnummer.Next(1, screenheight - 1);

        while (true)

        {

            Console.Clear();

            //Draw Obstacle

            Console.ForegroundColor = obstakel.schermKleur;

            Console.SetCursorPosition(obstakel.xPos, obstakel.yPos);

            Console.Write(obstakel.karacter);



            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");



            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < screenwidth; i++)

            {

                Console.SetCursorPosition(i, 0);

                Console.Write("■");

            }

            for (int i = 0; i < screenwidth; i++)

            {

                Console.SetCursorPosition(i, screenheight - 1);

                Console.Write("■");

            }

            for (int i = 0; i < screenheight; i++)

            {

                Console.SetCursorPosition(0, i);

                Console.Write("■");

            }

            for (int i = 0; i < screenheight; i++)

            {

                Console.SetCursorPosition(screenwidth - 1, i);

                Console.Write("■");

            }

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("Score: " + score);

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("H");

            for (int i = 0; i < telje.Count(); i++)

            {

                Console.SetCursorPosition(telje[i], telje[i + 1]);

                Console.Write("■");

            }

            //Draw Snake

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");

            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);

            Console.Write("■");



            ConsoleKeyInfo info = Console.ReadKey();

            //Game Logic

            switch (info.Key)

            {

                case ConsoleKey.UpArrow:

                    movement = "UP";

                    break;

                case ConsoleKey.DownArrow:

                    movement = "DOWN";

                    break;

                case ConsoleKey.LeftArrow:

                    movement = "LEFT";

                    break;

                case ConsoleKey.RightArrow:

                    movement = "RIGHT";

                    break;

            }

            if (movement == "UP")

                hoofd.yPos--;

            if (movement == "DOWN")

                hoofd.yPos++;

            if (movement == "LEFT")

                hoofd.xPos--;

            if (movement == "RIGHT")

                hoofd.xPos++;

            //Hindernis treffen

            if (hoofd.xPos == obstakel.xPos && hoofd.yPos == obstakel.yPos)

            {

                score++;

                obstakel.xPos = randomnummer.Next(1, screenwidth - 1);

                obstakel.yPos = randomnummer.Next(1, screenheight - 1);

            }

            teljePositie.Insert(0, hoofd.xPos);

            teljePositie.Insert(1, hoofd.yPos);

            teljePositie.RemoveAt(teljePositie.Count - 1);

            teljePositie.RemoveAt(teljePositie.Count - 1);

            //Kollision mit Wände oder mit sich selbst

            if (hoofd.xPos == 0 || hoofd.xPos == screenwidth - 1 || hoofd.yPos == 0 || hoofd.yPos == screenheight - 1)

            {

                gameOver(score, screenwidth, screenheight);

            }

            for (int i = 0; i < telje.Count(); i += 2)

            {

                if (hoofd.xPos == telje[i] && hoofd.yPos == telje[i + 1])

                {

                    gameOver(score, screenwidth, screenheight);

                }

            }

            Thread.Sleep(50);

        }

    }

    private static void gameOver(int score, int screenwidth, int screenheight)

    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red;

        Console.SetCursorPosition(screenwidth / 5, screenheight / 2);

        Console.WriteLine("Game Over");

        Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);

        Console.WriteLine("Dein Score ist: " + score);

        Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);

        Environment.Exit(0);
    }

}




