using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders_
{
    class Shot
    {
        public Point Location;
        private bool isFriendly;
        public int ShotLife=110;
        
        private Bitmap playerShot = Properties.Resources.playershotsmall1;
        private Bitmap invaderShot=Properties.Resources.playershotsmall2;
       
        public Shot(Point P,bool FoF)
        {
            Location = P;
            if (FoF)//fine tuning for shot location from player **may need additional location tweaking due to bitmap**
            {
                Location.X += 36;
                Location.Y -= 5;
            }
            else//fine tuning for shot location from invaders
            {
                Location.X += 23;
                Location.Y += 46;
            }
            isFriendly = FoF;
            
        }

        public void ShotMove()
        {
            if (isFriendly)
                Location.Y -= 5;
            else
                Location.Y += 5;
        }

        public void DrawShots(Graphics g, Shot Laser)
        {
            if (isFriendly)
            {
                g.DrawImage(playerShot, Laser.Location.X-34,Laser.Location.Y);
                //g.FillRectangle(Brushes.Green, new Rectangle(Laser.Location.X, Laser.Location.Y, 4, 12));
            }
            else
               // g.FillRectangle(Brushes.Blue, new Rectangle(Laser.Location.X, Laser.Location.Y, 4, 12));
                g.DrawImage(invaderShot, Laser.Location.X - 34, Laser.Location.Y);
        }

        
    }
}
