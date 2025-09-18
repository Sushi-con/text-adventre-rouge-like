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
            this.ShopPositions.Add(new Position { YPosition = random.Next(-this.Hight, this.Hight), XPosition = random.Next(-this.Width, this.Width) });
            this.ShopPositions.Add(new Position { YPosition = random.Next(-this.Hight, this.Hight), XPosition = random.Next(-this.Width, this.Width) });
        }

        //this will actualy draw out the map row by row.
        //it will store the map as a string in the MapString var.
        //it will only gennerate a new string when the player moves.

        public string GennerateMap(Player player)
        {

            SetPosition(player);
            string MapMark = "[ ]";
            string Explored = "[X]";
            string Shop = "[S]";
            string PlayerMarker = "[O]";
            string map = "";

            this.Positions.Add( new Position { XPosition = this.XPosition, YPosition = this.YPosition } );
            


            // loop thorugh the y axies
            for (int i = -this.Hight; i < this.Hight + 1; i++)
            {
                //checks if thire are explored tiles on this 
                if (this.ShopPositions.
                    Where(p => CheckYPosition(p, i)).
                    Any() &&
                    this.YPosition == i && 
                    this.Positions.
                    Where(p => CheckYPosition(p, i)).
                    Any())
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        // checks if the player is on the x axies and prioritizes the player marker over the explored marker
                        if (j == this.XPosition && 
                            this.Positions.
                            Where(p => CheckPosition(p, i, j)).
                            Any() &&
                            this.ShopPositions.
                            Where(p => CheckPosition(p, i, j)).
                            Any())
                            { map += PlayerMarker; }
                        // checks if the player is on the x axies
                        else if (j == this.XPosition) { map += PlayerMarker; }
                        else if (this.ShopPositions.
                            Where(p => CheckPosition(p, i, j)).
                            Any() &&
                            this.Positions.
                            Where(p => CheckPosition(p, i, j)).
                            Any())
                            { map += Shop; }
                        else if (this.ShopPositions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) 
                            { map += Shop; }
                        // checks if the tile is explored 
                        else if (this.Positions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) 
                            { map += Explored; }

                        else { map += MapMark; }
                    }
                }
                else if (this.ShopPositions.
                    Where(p => CheckYPosition(p, i)).
                    Any() && 
                    this.YPosition == i)
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        // checks if the player is on the x axies and prioritizes the player marker over the explored marker
                        if (j == this.XPosition &&
                            this.ShopPositions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) { map += PlayerMarker; }
                        // checks if the player is on the x axies
                        else if (j == this.XPosition) { map += PlayerMarker; }
                        // checks if the tile is explored 
                        else if (this.ShopPositions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) { map += Shop; }

                        else { map += MapMark; }
                    }
                }
                else if (this.Positions.
                    Where(p => CheckYPosition(p, i)).
                    Any() && this.YPosition == i)
                {
                    // loops thorugh the x axies
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        // checks if the player is on the x axies and prioritizes the player marker over the explored marker
                        if (j == this.XPosition &&
                            this.Positions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) { map += PlayerMarker; }
                        // checks if the player is on the x axies
                        else if (j == this.XPosition) { map += PlayerMarker; }
                        // checks if the tile is explored 
                        else if (this.Positions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) { map += Explored; }

                        else { map += MapMark; }
                    }
                }

                else if (this.ShopPositions.
                    Where(p => CheckYPosition(p, i)).
                    Any() && 
                    this.Positions.
                    Where(p => CheckYPosition(p, i)).
                    Any())
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        // checks if the player is on the x axies and prioritizes the player marker over the explored marker
                        if (this.ShopPositions.
                            Where(p => CheckPosition(p, i, j)).
                            Any() &&
                            this.Positions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) { map += Shop; }
                        // checks if the player is on the x axies
                        else if (this.ShopPositions.Where(p => CheckPosition(p, i, j)).Any()) { map += Shop; }
                        // checks if the tile is explored 
                        else if (this.Positions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) { map += Explored; }

                        else { map += MapMark; }
                    }
                }

                else if (this.ShopPositions.Where(position => CheckYPosition(position, i)).Any())
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        if (this.ShopPositions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) { map += Shop; }

                        else { map += MapMark; }
                    }
                }

                else if (this.Positions.Where(p => CheckYPosition(p, i)).Any())
                {
                    for (int j = -this.Width; j < this.Width + 1; j++)
                    {
                        if (this.Positions.
                            Where(p => CheckPosition(p, i, j)).
                            Any()) { map += Explored; }

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
