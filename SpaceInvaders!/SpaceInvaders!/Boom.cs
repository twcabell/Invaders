using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders_
{
    class Boom
    {
        public Point Location;
        Image explode = Properties.Resources.boom;
        public int BoomLife = 30;
        public Boom(Point p)
        {
            Location = p;
        }

        public void DrawBoom(Graphics g, Point p)
        {
            g.DrawImage(explode, p.X-50,p.Y-30);//fine tuning for explosion
        }
    }
}
