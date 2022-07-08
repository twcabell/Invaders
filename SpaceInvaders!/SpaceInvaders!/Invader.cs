using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders_
{
    class Invader
    {
        private const int HorizontalInterval = 10;
        private const int VerticalInterval = 20;
        public int Row;
        public int Column;
        public bool IsOnBottom=true;
        //public int bugType;

        private Bitmap image = Properties.Resources.satellite1;
        public int CellNumber;

        public Point Location= new Point(0,0);

        public ShipType InvaderType;

        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location.X,Location.Y ,40,40);
            }
        }

        public int PointValue=10;

        public Invader(int InvaderRow, int InvaderColumn)
        {
            Row = InvaderRow;
            Column = InvaderColumn;
            //bugType = InvaderRow;
            InvaderType = assignInvader(InvaderRow);
            Location.Y +=Row*(25+VerticalInterval);
            Location.X += Column*(50+HorizontalInterval)-50;
        }

        
        public enum ShipType
        {
            Watchit=0, Saucer=1, Satellite=2, Spaceship=3, Star=4,
        }

        public void Move(bool ChangeDirection)
        {
            if (ChangeDirection)
                Location.X += 1;
            //needs to move the ship in the specified direction
            else
                Location.X -= 1;
        }

       

        public void Draw(Graphics g, Rectangle Rectangle, ShipType BugType)
        {
            g.DrawImage(InvaderImage(BugType,CellNumber), Area);//Draws Invader
        }

        private Bitmap InvaderImage(ShipType invadertype, int animationCell)
        {
            //returns the right bitmap for the convenience cell
            if (invadertype==ShipType.Saucer)
           {
                switch (animationCell)
                        {
                            case 0:
                                return Properties.Resources.flyingsaucer1;
                            case 1:
                                return Properties.Resources.flyingsaucer2;
                            case 2:
                                return Properties.Resources.flyingsaucer3;
                            default:
                                return Properties.Resources.flyingsaucer4;
                        }
                    }
                else if (invadertype==ShipType.Spaceship)
                    {
                        switch (animationCell)
                        {
                            case 0:
                                return Properties.Resources.spaceship1;
                            case 1:
                                return Properties.Resources.spaceship2;
                            case 2:
                                return Properties.Resources.spaceship3;
                            default:
                                return Properties.Resources.spaceship4;
                        }
                    }
                else if(invadertype== ShipType.Star)
                    {
                        switch (animationCell)
                        {
                            case 0:
                                return Properties.Resources.star1;
                            case 1:
                                return Properties.Resources.star2;
                            case 2:
                                return Properties.Resources.star3;
                            default:
                                return Properties.Resources.star4;
                        }
                    }
                else if(invadertype== ShipType.Watchit)
                    {
                        switch (animationCell)
                        {
                            case 0:
                                return Properties.Resources.watchit1;
                            case 1:
                                return Properties.Resources.watchit2;
                            case 2:
                                return Properties.Resources.watchit3;
                            default:
                                return Properties.Resources.watchit4;
                        }
                    }
            else
                    {
                        switch (animationCell)
                        {
                            case 0:
                                return Properties.Resources.satellite1;
                            case 1:
                                return Properties.Resources.satellite2;
                            case 2:
                                return Properties.Resources.satellite3;
                            default:
                                return Properties.Resources.satellite4;
                        }
                    }
        }
        private ShipType assignInvader(int invadertype)
        {
            switch (invadertype)
            {
                case 1:
                    {
                        PointValue = 50;
                        return ShipType.Star;
                        
                    }
                case 2:
                    {
                        PointValue = 40;
                        return ShipType.Satellite;
                        
                    }
                case 3:
                    {
                        PointValue = 30;
                        return ShipType.Saucer;
                        
                    }
                case 4:
                    {
                        PointValue = 20;
                        return ShipType.Watchit;
                        
                    }
                default:
                    {
                        PointValue = 10;
                        return ShipType.Spaceship; 
                    }
            
            }
        }

        }

}
