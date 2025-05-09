using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace text_adventer_rouge_like.models
{
    public class OverWorld
    {
        public bool IsAlive { get; set; }
        public void StartGame()
        {
            Player player1 = new Player();
            player1.SetName();
            player1.ClassSelector();
            player1.SetHardMode();
            this.IsAlive = true;
            this.RunGame(player1);
        }

        //this is the main gameplay loop. it handles movement special commands, stuff like that.
        // basicly if it is somthing that happens in the game its probably here.

        public void RunGame(Player player)
        {
            Map map = new Map();
            map.GennerateSize();
            map.GennerateMap(player);
            Console.Clear();
            Console.WriteLine("press 'H' for help");
            while (IsAlive)
            {
                Console.WriteLine("What would you like to do? you are safe... as far as you know\n\n");
                Console.WriteLine("Map:");
                Console.WriteLine(map.MapString);
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                Random RandomDeath = new Random();

                if (keyInfo.Key == ConsoleKey.H)
                {
                    Console.Clear();
                    Console.WriteLine("I = Inventory \nTab = Player stats \n...");
                }
                if (keyInfo.Key == ConsoleKey.I)
                {
                    Console.Clear();
                    player.VeiwInventory();
                }
                if (keyInfo.Key == ConsoleKey.Tab)
                {
                    Console.Clear();
                    Console.WriteLine(player.ToString()); 
                }
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("are you sure you want to leave? you will lose all progress. y?");
                    ConsoleKeyInfo keyinfo = Console.ReadKey(intercept: true);
                    if ( keyinfo.Key == ConsoleKey.Y)
                    {
                        EndGame(false);
                    }
                }
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    Console.Clear();
                    if (player.HardMode)
                    {
                        if (RandomDeath.Next(1, 2000) == 7)
                        {
                            Console.WriteLine("today is your unlucky day, \n i'll see you next time");
                            player.HitPoints = 0;
                        }
                    }
                    player.MoveUp();
                    if (player.YPosition <= -map.Hight - 1)
                    {
                        Console.WriteLine("You can not move in that direction");
                        player.MoveDown();
                    }
                    map.GennerateMap(player);
                }
                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    Console.Clear();
                    if (player.HardMode)
                    {
                        if (RandomDeath.Next(1, 2000) == 7)
                        {
                            Console.WriteLine("today is your unlucky day, \n i'll see you next time");
                            player.HitPoints = 0;
                        }
                    }
                    player.MoveDown();
                    if (player.YPosition >= map.Hight + 1)
                    {
                        Console.WriteLine("You can not move in that direction");
                        player.MoveUp();
                    }
                    map.GennerateMap(player);
                }
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    Console.Clear();
                    if (player.HardMode)
                    {
                        if (RandomDeath.Next(1, 2000) == 7)
                        {
                            Console.WriteLine("today is your unlucky day, \n i'll see you next time");
                            player.HitPoints = 0;
                        }
                    }
                    player.MoveRight();
                    if (player.XPosition >= map.Width + 1)
                    {
                        Console.WriteLine("You can not move in that direction");
                        player.MoveLeft();
                    }
                    map.GennerateMap(player);
                }
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    Console.Clear();
                    if (player.HardMode)
                    {
                        if (RandomDeath.Next(1, 2000) == 7)
                        {
                            Console.WriteLine("today is your unlucky day, \n i'll see you next time");
                            player.HitPoints = 0;
                        }
                    }
                    player.MoveLeft();
                    if (player.XPosition <= -map.Width - 1)
                    {
                        Console.WriteLine("You can not move in that direction");
                        player.MoveRight();
                    }
                    map.GennerateMap(player);
                }



                if (player.HitPoints <= 0)
                {
                    this.EndGame(true);
                }
            }
        }

        //this method handles when you end the game... wow couldent tell right

        public void EndGame(bool Death)
        {
            IsAlive = false;
            Console.WriteLine("Thanks for playing!! :)");
            if (Death)
            {
                Console.WriteLine("This is the end of the line... sort of");
                Console.WriteLine("Would you like to play again? \n y/n");
                bool StayOrLeave = false;
                while (StayOrLeave == false)
                {
                    ConsoleKeyInfo keyinfo = Console.ReadKey(intercept: true);
                    if (keyinfo.Key == ConsoleKey.Y)
                    {
                        Console.Clear();
                        this.StartGame();
                        StayOrLeave = true;
                    }
                    else if (keyinfo.Key == ConsoleKey.N)
                    {
                        Console.WriteLine("See you next time!! :)");
                        StayOrLeave = true;
                    }
                    else
                    {
                        Console.WriteLine("I dont understand please try agin or close the game.");
                    }
                }
            }
        }


    }
}
