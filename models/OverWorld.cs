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
        public List<Enemies> Enemies { get; set; } = new List<Enemies>();
        public void StartGame(List<Enemies> enemies)
        {
            Player player1 = new Player();
            this.Enemies = enemies;
            player1.SetName();
            player1.ClassSelector();
            player1.SetHardMode();
            this.IsAlive = true;
            this.RunGame(player1);
        }

        //this is the main gameplay loop. it handles movement special commands, stuff like that.
        // basicly if it is something that happens in the game its probably here.

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
                Random RandomIncounter = new Random();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.WriteLine("are you sure you want to leave? you will lose all progress. y?");
                        ConsoleKeyInfo keyinfo = Console.ReadKey(intercept: true);
                        if (keyinfo.Key == ConsoleKey.Y)
                        {
                            EndGame(false);
                        }
                        break;
                    case ConsoleKey.H:
                        Console.Clear();
                        Console.WriteLine("I = Inventory \nTab = Player stats \nArrow Keys = player movement \n...");
                        break;
                    case ConsoleKey.I:
                        Console.Clear();
                        player.VeiwInventory();
                        break;
                    case ConsoleKey.Tab:
                        Console.Clear();
                        Console.WriteLine(player.ToString());
                        break;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (player.HardMode)
                        {
                            if (RandomDeath.Next(1, 2000) == 7)
                            {
                                Console.WriteLine("today is your unlucky day, \n see you next time");
                                player.HitPoints = 0;
                            }
                        }
                        player.MoveUp();
                        this.Combat(player, RandomIncounter);
                        if (player.YPosition <= -map.Hight - 1)
                        {
                            Console.WriteLine("You can not move in that direction");
                            player.MoveDown();
                        }
                        map.GennerateMap(player);
                        break;
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if (player.HardMode)
                        {
                            if (RandomDeath.Next(1, 2000) == 7)
                            {
                                Console.WriteLine("today is your unlucky day, \n see you next time");
                                player.HitPoints = 0;
                            }
                        }
                        player.MoveDown();
                        this.Combat(player, RandomIncounter);
                        if (player.YPosition >= map.Hight + 1)
                        {
                            Console.WriteLine("You can not move in that direction");
                            player.MoveUp();
                        }
                        map.GennerateMap(player);
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        if (player.HardMode)
                        {
                            if (RandomDeath.Next(1, 2000) == 7)
                            {
                                Console.WriteLine("today is your unlucky day, \n see you next time");
                                player.HitPoints = 0;
                            }
                        }
                        player.MoveRight();
                        this.Combat(player, RandomIncounter);
                        if (player.XPosition >= map.Width + 1)
                        {
                            Console.WriteLine("You can not move in that direction");
                            player.MoveLeft();
                        }
                        map.GennerateMap(player);
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        if (player.HardMode)
                        {
                            if (RandomDeath.Next(1, 2000) == 7)
                            {
                                Console.WriteLine("today is your unlucky day, \n see you next time");
                                player.HitPoints = 0;
                            }
                        }
                        player.MoveLeft();
                        this.Combat(player, RandomIncounter);
                        if (player.XPosition <= -map.Width - 1)
                        {
                            Console.WriteLine("You can not move in that direction");
                            player.MoveRight();
                        }
                        map.GennerateMap(player);
                        break;
                }



                if (player.HitPoints <= 0)
                {
                    this.EndGame(true);
                }
            }
        }

        public void Combat(Player player, Random Random)
        {
            if (Random.Next(1, 10) == 7)
            {
                Console.WriteLine("You have encountered a wild enemy!!");
                Random random = new Random();
                Enemies enemie = new Enemies();
                foreach (Enemies enemy in this.Enemies)
                {
                    if (random.Next(1, 3) == enemy.Id)
                    {
                        enemie = enemy;
                    }
                }
                bool InCombat = true;
                while (InCombat)
                {
                    Console.WriteLine($"you are in combat with {enemie.Name}!");
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.E:
                            Console.Clear();
                            Console.WriteLine("You have attacked the enemy!");
                            Console.WriteLine($"You have done {player.Damage} damage to the {enemie.Name}");
                            enemie.HP -= player.Damage;
                            break;
                        case ConsoleKey.Tab:
                            Console.Clear();
                            Console.WriteLine(player.ToString());
                            break;
                        case ConsoleKey.I:
                            Console.Clear();
                            player.VeiwInventory();
                            break;
                        case ConsoleKey.H:
                            Console.Clear();
                            Console.WriteLine("I = Inventory \nTab = Player stats \n...");
                            break;
                    }
                    Console.WriteLine(enemie.HP);
                    if (enemie.HP <= 0)
                    {
                        Console.WriteLine($"yay you did it!! The {enemie.Name} has fallen!");
                        InCombat = false;
                    }
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
                        this.StartGame(this.Enemies);
                        StayOrLeave = true;
                    }
                    else if (keyinfo.Key == ConsoleKey.N)
                    {
                        Console.WriteLine("See you next time!! :)");
                        StayOrLeave = true;
                    }
                    else
                    {
                        Console.WriteLine("I don't understand please try again or close the game.");
                    }
                }
            }
        }


    }
}
