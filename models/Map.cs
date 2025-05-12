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
        private  List<Position> Positions { get; set; } = new List<Position>();
        public  string MapString { get; set; }

        //this method is so the map can keep track of the players position.

        public void SetPosition(Player player)
        {
            XPosition = player.XPosition;
            YPosition = player.YPosition;
        }

        //this will randomly gennerat the map size

        public void GennerateSize()
        {
            Random MapSize = new Random();
            this.Hight = MapSize.Next(4, 9);
            this.Width = MapSize.Next(4, 9);
        }

        //this will actualy draw out the map row by row.
        //it will store the map as a string in the MapString var.
        //it will only gennerate a new string when the player moves.

        public string GennerateMap(Player player)
        {

            SetPosition(player);
            string MapMark = "[ ]";
            string Explored = "[X]";
            string PlayerMarker = "[O]";
            string map = "";

            this.Positions.Add( new Position { XPosition = this.XPosition, YPosition = this.YPosition } );
            


            
            for (int i = -this.Hight; i < this.Hight + 1; i++)
            {
                if (this.Positions.Where(Position => Position.YPosition == i).Any() && this.YPosition == i)
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        if (j == this.XPosition && this.Positions.Where(Position => Position.XPosition == j).Any() && this.Positions.Where(Position => Position.YPosition == i).Any()) { map += PlayerMarker; }

                        else if (j == this.XPosition) { map += PlayerMarker; }

                        else if (this.Positions.Where(Position => Position.XPosition == j && Position.YPosition == i).Any()) { map += Explored; }

                        else { map += MapMark; }
                    }
                }

                else if (this.Positions.Where(Position => Position.YPosition == i).Any())
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        if (this.Positions.Where(Position => Position.XPosition == j && Position.YPosition == i).Any() && this.Positions.Where(Position => Position.YPosition == i).Any()) { map += Explored; }

                        else { map += MapMark; }
                    }
                }

                else if (i == this.YPosition && player.YPosition > -this.Hight && player.YPosition < this.Hight)
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {

                        if (j == this.XPosition && player.XPosition > -this.Width) { map += PlayerMarker; }

                        else { map += MapMark; }
                    }
                }

                else
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        map += MapMark;
                    }
                }

                map += "\n";
            }

            this.MapString = map;

            return map;
        }
    }
}
