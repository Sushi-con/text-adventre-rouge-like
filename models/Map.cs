using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_adventer_rouge_like.models
{
    public class Map
    {
        public int Hight { get; set; }
        public int Width { get; set; }
        public int XPosition { get; set; } = 0;
        public int YPosition { get; set; } = 0;
        public int XExitPosition { get; set; }
        public int YExitPosition { get; set; }
        public  List<Position> Positions { get; set; } = new List<Position>();
        public List<Position> ShopPositions { get; set; } = new List<Position>();
        public  string MapString { get; set; }

        //this method is so the map can keep track of the players position.

        public void SetPosition(Player player)
        {
            XPosition = player.XPosition;
            YPosition = player.YPosition;
        }

        public string ChechPlayerPosition(Player player)
        {
            foreach (var position in this.ShopPositions)
            {
                if (player.XPosition == position.XPosition && player.YPosition == position.YPosition)
                { return "shop"; }
            }
            foreach (var position in this.Positions)
            {
                if(player.XPosition == position.XPosition && player.YPosition == position.YPosition) 
                { return "exp"; }
            }
            return "";
        }

        //this will randomly gennerat the map size

        public void GennerateSize()
        {
            Random MapSize = new Random();
            this.Hight = MapSize.Next(4, 9);
            this.Width = MapSize.Next(4, 9);
            this.GenerateShops();
        }

        public bool CheckYPosition(Position position, int Y)
        {
            if (position.YPosition == Y) { return true; }
            else
            {
                return false;
            }
        }

        public bool CheckPosition(Position position, int Y, int X)
        {
            if (position.XPosition == X && position.YPosition == Y) { return true; }
            else { return false; }
        }

        public void GenerateShops()
        {
            Random random = new Random();
            this.ShopPositions.Add(new Position { YPosition = random.Next(-this.Hight, this.Hight), XPosition = random.Next(-this.Width, this.Width) });
        }

        //this will actualy draw out the map row by row.
        //it will store the map as a string in the MapString var.
        //it will only gennerate a new string when the player moves.


        public string GennerateMap(Player player)
        {

            SetPosition(player);
            string map = "";
            this.Positions.Add( new Position { XPosition = this.XPosition, YPosition = this.YPosition } );            

            // loop thorugh the y axies
            for(int y =  -Hight; y <= Hight; y++)
            {
                // loop through the x axies
                for(int x = -Width; x <= Width; x++)
                {
                    map += checkposition(x, y);
                }
                map += "\n";
            }
        }

        public string checkposition(xpos, ypos)
        {
            bool shop = shoppositions.xPosition == xpos && shoppositions.yPosition == ypos;
            bool explored = positions.xPosition == xpos && positions.yPosition == ypos;
            bool player = player.xPosition == xpos && player.yPosition == ypos;

            if (shop) { return "[s]"; }
            if (explored) { return "[x]"; }
            if (player) { return "[o]"; }
            return "[ ]";
        }
    }
}
