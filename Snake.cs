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



        List<Pixel> teljePositie = new List<Pixel>();


        teljePositie.Add(hoofd);


        DateTime tijd = DateTime.Now;

        Obstakel obstakel = new Obstakel();

        obstakel.karacter = "*";

        obstakel.schermKleur = ConsoleColor.Cyan;

        obstakel.xPos = randomnummer.Next(1, screenwidth - 1);

        obstakel.yPos = randomnummer.Next(1, screenheight - 1);

        bool inExit = false;

        while (true)

        {

            Console.Clear();

            //Draw Obstacle

            Console.ForegroundColor = obstakel.schermKleur;

            Console.SetCursorPosition(obstakel.xPos, obstakel.yPos);

            Console.Write(obstakel.karacter);

            //Draw Snake

            foreach (Pixel segment in teljePositie)
            {
                Console.SetCursorPosition(segment.xPos, segment.yPos);
                Console.ForegroundColor = segment.schermKleur;
                Console.Write("■");
            }



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

            if (inExit)

            {
                Console.Write("Press ESC again to exit the game");
            }

            for (int i = 0; i < telje.Count(); i++)

            {

                Console.SetCursorPosition(telje[i], telje[i + 1]);

                Console.Write("■");

            }



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

                case ConsoleKey.Escape:

                    if (inExit)

                    {
                        gameOver(score, screenwidth, screenheight);

                        return;
                    }

                    else

                    {
                        inExit = true;

                        continue;
                    }

            }

            inExit = false;

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

                Pixel lastSegment;

                if (score == 1)

                {
                    lastSegment = teljePositie[teljePositie.Count - 1];
                    teljePositie.Add(new Pixel { xPos = lastSegment.xPos, yPos = lastSegment.yPos, schermKleur = lastSegment.schermKleur });
                }

                teljePositie[0].xPos = hoofd.xPos;
                teljePositie[0].yPos = hoofd.yPos;

                lastSegment = teljePositie[teljePositie.Count - 1];
                teljePositie.Add(new Pixel { xPos = lastSegment.xPos, yPos = lastSegment.yPos, schermKleur = lastSegment.schermKleur });


            }


            for (int i = teljePositie.Count - 1; i > 0; i--)
            {
                teljePositie[i].xPos = teljePositie[i - 1].xPos;
                teljePositie[i].yPos = teljePositie[i - 1].yPos;
            }

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

            Pixel head = new Pixel() { xPos = hoofd.xPos, yPos = hoofd.yPos };

            foreach (Pixel pixel in teljePositie.Skip(3))

            {
              
                if (head.xPos == pixel.xPos && head.yPos == pixel.yPos)

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




