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
        public List<Position> ExploredTiles { get; set; } = new List<Position>();
        public List<Position> MazeTiles { get; set; } = new List<Position>();
        public List<Position> ShopTile { get; set; } = new List<Position>();
        public string MapString { get; set; }
        public bool Maze { get; set; } = false;

        //this method is so the map can keep track of the players position.

        public void SetPosition(Player player)
        {
            XPosition = player.XPosition;
            YPosition = player.YPosition;
        }

        public string ChechPlayerPosition(Player player)
        {
            foreach (var tile in this.ShopTile)
            {
                if (player.XPosition == tile.XPosition && player.YPosition == tile.YPosition)
                { return "shop"; }
            }
            foreach (var tile in this.ExploredTiles)
            {
                if(player.XPosition == tile.XPosition && player.YPosition == tile.YPosition) 
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

        public void GenerateShops()
        {
            Random random = new Random();
            while (this.ShopTile.Any(p => p.XPosition == 0 && p.YPosition == 0) || this.ShopTile.Count < 1)
            {
                this.ShopTile.Add(new Position { YPosition = random.Next(-this.Hight, this.Hight), XPosition = random.Next(-this.Width, this.Width) });
            }
        }

        //this will actualy draw out the map row by row.
        //it will store the map as a string in the MapString var.
        //it will only gennerate a new string when the player moves.

        public string DrawMap()
        {
            string map = "";
            for(int y =  -this.Hight; y <= this.Hight; y++)
            {
                for(int x = -this.Width; x <= this.Width; x++)
                {
                    map += this.CheckPosition(x, y);
                }
                map += "\n";
            }
            return map;
        }


        public string GennerateMap(Player player)
        {
            SetPosition(player);
            this.ExploredTiles.Add( new Position { XPosition = this.XPosition, YPosition = this.YPosition } );

            if (!Maze)
            {
                return this.MapString = DrawMap();
            }
            else
            {
                return this.MapString = this.ProcedurelMapGenneration(player);
            }
        }

        public string ProcedurelMapGenneration(Player player)
        {
            string map = "";
            int tileAmount = (this.Hight * this.Width * 2);
            this.MazeTiles.Add( new Position { XPosition = this.XPosition, YPosition = this.YPosition } );

            if (this.MapString == null)
            {
                
                while (tileAmount != 0)
                {
                    Random random = new Random();
                    int rvar = random.Next(1, 6);
                    if (rvar == 1 && this.MazeTiles.Any(p => p.XPosition != this.XPosition + 1 && p.YPosition != this.YPosition))
                    {
                        this.MazeTiles.Add( new Position {XPosition = this.XPosition + 1, YPosition = this.YPosition } );
                        this.XPosition++;
                        tileAmount--;
                    }
                    else if (rvar == 2 && this.MazeTiles.Any(p => p.XPosition == this.XPosition && p.YPosition != this.YPosition + 1))
                    {
                        this.MazeTiles.Add( new Position {XPosition = this.XPosition, YPosition = this.YPosition + 1 } );
                        this.YPosition++;
                        tileAmount--;
                    }
                    else if (rvar == 3 && this.MazeTiles.Any(p => p.XPosition != this.XPosition - 1 && p.YPosition == this.YPosition))
                    {
                        this.MazeTiles.Add( new Position {XPosition = this.XPosition - 1, YPosition = this.YPosition } );
                        this.XPosition--;
                        tileAmount--;
                    }
                    else if (rvar == 4 && this.MazeTiles.Any(p => p.XPosition == this.XPosition && p.YPosition != this.YPosition - 1))
                    {
                        this.MazeTiles.Add( new Position {XPosition = this.XPosition, YPosition = this.YPosition - 1 } );
                        this.YPosition--;
                        tileAmount--;
                    }
                    if(this.XPosition <= -this.Width || this.YPosition <= -this.Hight || this.XPosition >= this.Width || this.YPosition >= this.Hight)
                    {
                        SetPosition(player);
                    }
                    
                }
                SetPosition(player);
            }

            map = DrawMap();
            return map;
        }

        public string CheckPosition(int xpos, int ypos)
        {
            bool shop = this.ShopTile.Any(p => p.XPosition == xpos && p.YPosition == ypos);
            bool explored = this.ExploredTiles.Any(p => p.XPosition == xpos && p.YPosition == ypos);
            bool player = this.XPosition == xpos && this.YPosition == ypos;
            bool tile = true;
            if(this.Maze)
            {
                tile = this.MazeTiles.Any(p => p.XPosition == xpos && p.YPosition == ypos);
            }

            if (player) { return "[o]"; }
            else if (shop) { return "[s]"; }
            else if (explored) { return "[x]"; }
            else if (tile) { return "[ ]"; }
            return "   ";
        }
    }
}
