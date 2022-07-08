using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders_
{
    class PlayerShip
    {
        public Point Location = new Point();// PlayerShip coordinate
       //public bool Alive;
        public Rectangle Area;
        public Rectangle PlayerHitBox;
        public Bitmap MyPlayerShipImage = Properties.Resources.playershipTop;//Playership Default Picture

        private int ShipYLocation=550;
        private int ShipXLocation=489;
        private int ShipHeight=100;
        private int ShipWidth = 75;

        public int ShotOffSetX=36;
        public int ShotOffsetY=46;
        
        //public int StartingPosition = 489;

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }
        public PlayerShip()
        {
            
            Location.X = ShipXLocation;
            Location.Y = ShipYLocation;
            //Area.Location = Location;
            Area.X = ShipXLocation;
            Area.Y = ShipYLocation;
            Area.Height = ShipHeight;
            Area.Width = ShipWidth;

            PlayerHitBox.Width = 75;
            PlayerHitBox.Height = 75;
            PlayerHitBox.X = ShipXLocation;
            PlayerHitBox.Y = ShipYLocation+25;
            //Alive = true;
            //MyPlayerShip = pb;
            
        }

        public void Draw(Graphics g, Rectangle Rectangle)
        {
            g.DrawImage(MyPlayerShipImage, Area);//Draws Image
            //g.FillRectangle(Brushes.Green, Area);
            //g.FillRectangle(Brushes.Red, PlayerHitBox);//Draws HitBox
            
            //g.FillRectangle(Brushes.Yellow, new Rectangle(Area.X+ShotOffSetX,Area.Y-1000,2,1000));//aimer helper
        }

        public void StartingPosition()
        {
            ShipXLocation = 489;
        }

        public void Move(Direction direction)// cant get this working so i had to make 2 new ones, see MoveLeft() and MoveRight()
        {
            if (direction == Direction.Left)
            {
             
                Area.X -= 10;
            }

            else if (direction == Direction.Right)
            {
                
                Area.X += 10;
            }
        }

        public void MoveLeft()
        {
            if (Area.X > 1)
            {
                Area.X -= 5;
                Location.X -= 5;
                PlayerHitBox.X -= 5;
            }
        }

        public void MoveRight()
        {
            if (Area.X < 920)
            {
                Area.X += 5;
                Location.X += 5;
                PlayerHitBox.X += 5;
            }
        }
    }
}
