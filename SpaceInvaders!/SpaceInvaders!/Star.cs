using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders_
{
    class Star
    {
        
            
        public Point MyStarLocation;
        public Brush MyStarColor;
        public int MyStarSize;
        public int MyStarMovementSpeed;
        Random random = new Random();
        public bool MyVisibility=true;
        //public int MyVisibilityTime;
        
        public Star(Point p, int C, int S,int speed)
        {
            MyStarLocation = p;
            MyStarColor = NumberToColor(C);
            MyStarSize = S;
            MyStarMovementSpeed = speed;
            //MyVisibilityTime = random.Next(50) + 50;
            
            
        }

        private Brush NumberToColor(int inputNumber)
        {
            Brush scrub= Brushes.White;
            if (inputNumber >= 0 && inputNumber <10)
                scrub = Brushes.Orange;
            else if (inputNumber >= 10 && inputNumber <15)
                scrub = Brushes.LightBlue;
            else if (inputNumber >= 15 && inputNumber <20)
                scrub = Brushes.Red;
            else if (inputNumber >= 20 && inputNumber <40)
                scrub = Brushes.Yellow;
            else if (inputNumber >= 40 && inputNumber <45)
                scrub = Brushes.Cyan;
            return scrub;
        }

        public void Draw(Graphics g, Star star)
        {
            g.FillRectangle(star.MyStarColor, new Rectangle(star.MyStarLocation.X, star.MyStarLocation.Y, star.MyStarSize, star.MyStarSize));
        }

       
       
    }
}
