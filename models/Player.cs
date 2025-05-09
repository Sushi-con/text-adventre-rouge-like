using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace text_adventer_rouge_like.models
{
    public class Player
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int levle { get; set; } = 1;
        private double Points { get; set; } = 0;
        public double PointCap { get; set; } = 25;
        public int Damage { get; set; }
        private int DefDamage { get; set; }
        public int Armor { get; set; }
        private int DefArmor { get; set; }
        public int HitPoints { get; set; }
        private int DefHitPoints { get; set; }
        private List<Item> Items { get; set; } = new List<Item>();
        public int XPosition { get; set; } = 0;
        public int YPosition { get; set; } = 0;
        public bool HardMode { get; set; }

        // Methods===============================================

        // set the players name to whatever the player enters.

        public string SetName()
        {
            Console.Clear();
            Console.WriteLine("What is your Name?");
            string? NameVar = Console.ReadLine();
            Name = NameVar;
            return this.Name;
        }

        public override string ToString()
        {
            return $"Name: {this.Name}\n Class: {this.Class}\n Dammage: {this.Damage}\n Armor: {this.Armor}\n HP: {this.HitPoints}\n Levle: {this.levle}\n Points: {this.Points}/{this.PointCap}";
        }

        //this is to set the temp stats equal to the base stats
        // mostly used when aplying item/stat effects

        private void SetBaseStats()
        {
            this.Damage = this.DefDamage;
            this.Armor = this.DefArmor;
            this.HitPoints = this.DefHitPoints;
        }

        // this method is made for the perpose of having multiple different roles the charicter can play.
        // there are four classes that you can choose from when the games starts after you enter your name.

        public void ClassSelector()
        {
            Console.Clear();
            var Class = false;
            while (!Class)
            {
                Console.WriteLine("What is your class? \n 1. Knight \n 2. Tank \n 3. Healer \n 4. Mage");
                ConsoleKeyInfo keyinfo = Console.ReadKey(intercept: true);
                if (keyinfo.Key == ConsoleKey.D1)
                {
                    this.Class = "Knight";
                    this.DefDamage = 25;
                    this.DefArmor = 25;
                    this.DefHitPoints = 150;
                    this.SetBaseStats();
                    Class = true;
                }
                else if (keyinfo.Key == ConsoleKey.D2)
                {
                    this.Class = "Tank";
                    this.DefDamage = 18;
                    this.DefArmor = 50;
                    this.DefHitPoints = 200;
                    this.SetBaseStats();
                    Class = true;
                }
                else if (keyinfo.Key == ConsoleKey.D3)
                {
                    this.Class = "Healer";
                    this.DefDamage = 15;
                    this.DefArmor = 20;
                    this.DefHitPoints = 100;
                    this.SetBaseStats();
                    Class = true;
                }
                else if (keyinfo.Key == ConsoleKey.D4)
                {
                    this.Class = "Mage";
                    this.DefDamage = 30;
                    this.DefArmor = 10;
                    this.DefHitPoints = 85;
                    this.SetBaseStats();
                    Class = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. try again");
                }
            }
        }

        //this method will ask the player if they would like to play on hard mode and change wthere random death can happen.

        public void SetHardMode()
        {
            Console.Clear();
            Console.WriteLine("would you like to play on hard mode? its not so bad... y/n");
            bool hard = false;
            while (!hard)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey(intercept: true);
                if (keyinfo.Key == ConsoleKey.Y)
                {
                    this.HardMode = true;
                    hard = true;
                }
                else if (keyinfo.Key == ConsoleKey.N)
                {
                    this.HardMode = false;
                    hard = true;
                }
                else
                {
                    Console.WriteLine("please try again");
                }
            }
        }

        // obveiosly a method to add items to the charicters inventory.
        // it also calculates the levle of the player

        public void AddItem(Item item)
        {
            this.Items.Add(item);
            this.StatEffect();
            this.Points += item.PointBonus;
            if (this.Points >= this.PointCap)
            {
                this.Points -= this.PointCap;
                this.levle++;
                this.PointCap += this.PointCap * 1.5;
            }
        }

        public void AddItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                this.AddItem(item);
            }
        }

        public void MoveUp()
        {
            this.YPosition--;
        }
        public void MoveDown()
        {
            this.YPosition++;
        }
        public void MoveRight()
        {
            this.XPosition++;
        }
        public void MoveLeft()
        {
            this.XPosition--;
        }

        // shows all the items that the player has in there inventory.

        public void VeiwInventory()
        {
            foreach (Item item in this.Items)
            {
                Console.Clear();
                Console.WriteLine(item.ToString());
            }
        }

        // this method will take all the items in the players inventory and cyles through there stat effects list and adds effects based on the name of the stat effects.

        private Item? StatEffect()
        {
            this.SetBaseStats();
            foreach(Item item in Items)
            {
                foreach (string stat in item.StatEffects)
                {
                    if (stat == "Damage up")
                    {
                        this.Damage += 5;
                    }
                    else if (stat =="Minor Damage up")
                    {
                        this.Damage += 2;
                    }
                    else if (stat == "Major Damage up")
                    {
                        this.Damage += 8;
                    }
                    else if (stat == "Armor up")
                    {
                        this.Armor += 3;
                    }
                    else if (stat == "Minor Armor up")
                    {
                        this.Armor += 1;
                    }
                    else if (stat == "Major Armor up")
                    {
                        this.Armor += 5;
                    }
                    else if (stat == "HP up")
                    {
                        this.HitPoints += 5;
                    }
                    else if (stat == "Minor HP up")
                    {
                        this.HitPoints += 2;
                    }
                    else if (stat == "Major HP up")
                    {
                        this.HitPoints += 8;
                    }
                    else if (stat == "Damage down")
                    {
                        this.Damage -= 5;
                    }
                    else if (stat == "Minor Damage down")
                    {
                        this.Damage -= 2;
                    }
                    else if (stat == "Major Damage down")
                    {
                        this.Damage -= 8;
                    }
                    else if (stat == "Armor down")
                    {
                        this.Armor -= 5;
                    }
                    else if (stat == "Minor Armor down")
                    {
                        this.Armor -= 2;
                    }
                    else if (stat == "Major Armor down")
                    {
                        this.Armor -= 8;
                    }
                    else if (stat == "HP down")
                    {
                        this.HitPoints -= 5;
                    }
                    else if (stat == "Minor HP down")
                    {
                        this.HitPoints -= 2;
                    }
                    else if (stat == "Major HP down")
                    {
                        this.HitPoints -= 8;
                    }
                }
            }
            return null;
        }

        //public void 

    }
}
