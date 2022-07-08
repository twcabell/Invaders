using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders_
{
    class Game
    {
        public int score = 0;
        public int livesLeft = 3;
        public int wave = 1;
        public int HighScore = 0;

        private int gameHeight;
        private int gameWidth;
        private int numberOfStars;
        public bool MovementDirection;
        
        private Random random = new Random();

        public List<Invader> Invaders=new List<Invader>();//list of invaders
        public List<Star> StarField = new List<Star>();//list of stars
        public List<Boom> BoomField = new List<Boom>();//list of explosions

        public PlayerShip MyPlayerShip;
        public List<Shot> ShotsFired = new List<Shot>();//for player shots
        public List<Shot> InvaderShots = new List<Shot>();//for invader shots
       
        public Game(int StarNumber, int ScreenHeight, int ScreenWidth)
        {
            gameHeight = ScreenHeight;
            gameWidth = ScreenWidth;
            numberOfStars = StarNumber;
            MyPlayerShip = new PlayerShip();
            CreateStar(StarNumber, true);
            InitializeInvader();
        }
        public void Go()//meat and potatoes of the game
        {
            CheckForInvaderCollision(Invaders, ShotsFired);
            CheckForPlayerCollision(MyPlayerShip, InvaderShots);
           
            foreach (Invader invader in Invaders)
            {
                if (invader.Location.X > gameWidth - 55)
                {
                    MovementDirection = false;
                    advanceRow();
                }
                else if (invader.Location.X < 0)
                {
                    MovementDirection = true;
                    advanceRow();
                }
                invader.Move(MovementDirection);
                if (invader.IsOnBottom && random.Next(6)==1&& InvaderShots.Count()<wave)//must be on the bottom and need to change the random number to shot life
                    ReturnFire(invader, false);
            }
        }

        private void changeDirection()//when invaders reach the edge, this changes their direction
        {
            foreach (Invader invader in Invaders)
            {
                MovementDirection = true;
            }
        }

        private void advanceRow()//moves all invaders down when they reach the edge
        {
            foreach (Invader invader in Invaders)
            {
                invader.Location.Y += 10;
            }
        }
        
        public bool GameOver()//Ends the game when lives are depleted
        {
            if (livesLeft>0)
                return true;

            else
                return false;
        }
        
        public void CreateStar(int starNumber, bool initial)// code for creating stars
        {
            for (int i = 0; i < starNumber; i++)//creates starNumber amount of stars
            {
                int starSpeed =1+random.Next(5);
                Point StarLocation = new Point();
                if (initial)
                {
                    StarLocation.Y = random.Next(gameHeight);
                }
                else
                {
                    StarLocation.Y = random.Next(50);
                }
                StarLocation.X = random.Next(gameWidth);
                int Size = random.Next(3);
                int StarColorNumber = random.Next(100);

                StarField.Add(new Star(StarLocation, StarColorNumber, Size, starSpeed));
            }
        }
        
        public void DrawStars(Graphics g, Star star)//code for drawing the stars
        {
            g.FillRectangle(star.MyStarColor, new Rectangle(star.MyStarLocation.X, star.MyStarLocation.Y, star.MyStarSize, star.MyStarSize));
        }

        private void CheckForPlayerCollision(PlayerShip Me,List<Shot> InvaderShots)// check for when player is hit by shots
        {
            Shot shottemp = null;
            foreach (Shot shot in InvaderShots)
            {
                if(Me.PlayerHitBox.Contains(shot.Location))
                {
                    shottemp = shot;
                        livesLeft--;
                        BoomField.Add(new Boom(Me.Location));
                        break;
                }
            }
            if (shottemp != null)
            {
                PlayerDeath();
            }
        }

        private void CheckForInvaderCollision(List <Invader> Alien, List<Shot> PlayerShot ) //check for when invaders are hit by shots
        {
            Invader temp = null;
            Shot shottemp = null;
            foreach (Shot shot in PlayerShot)
            {
                foreach (Invader invader in Alien)
                {
                    if (invader.Area.Contains(shot.Location))
                    {
                        temp = invader;
                        shottemp = shot;
                        score += invader.PointValue;
                        BoomField.Add(new Boom(invader.Location));
                        break;
                    }
                }
            }
            if (temp != null)
            {
                Invaders.Remove(temp);
                ShotsFired.Remove(shottemp);
            }
        }

        public void Fire(bool FriendOrFoe, Point WhoFired) //When the Player fires
        {
            ShotsFired.Add(new Shot(WhoFired, FriendOrFoe));
        }
        
        private void ReturnFire(Invader invader, bool FriendOrFoe) //When the invaders fire back
        {
            InvaderShots.Add(new Shot(invader.Location, FriendOrFoe));
        }

        public void InitializeInvader()//creation of invaders
        {
            for (int j = 1; j < 6; j++) //creates #j Rows of invaders
            {
                for (int i = 1; i < 7; i++)// creates #i  Columns of invaders
                {
                    Invaders.Add(new Invader(j,i));
                }
            }
        }

        public void PlayerDeath()//what happens when player dies
        {
            InvaderShots.Clear();
            ShotsFired.Clear();
        }

        private void IsBottomInvader(List<Invader> Invaders)// not functional...yet
        {
            int tempRow = 0;
            //int tempCol=0;
            for (int tempCol = 1; tempCol < 6; tempCol++)
            {
                foreach (Invader invader in Invaders)
                {
                    if (invader.Row > tempRow && invader.Column == tempCol)
                    {
                        tempRow += invader.Row;
                        invader.IsOnBottom = true;
                    }
                }
            }
            //return true;
        }
        
    }
}
