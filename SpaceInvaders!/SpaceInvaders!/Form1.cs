using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

///////////////////////////////////////////////////////////////////////////////
//
//Team D
//
//
namespace SpaceInvaders_
{
    public partial class Form1 : Form
    {
       

        static int gameHeight = 700;//Screen Size in Weight
        static int gameWidth = 1000;//Screen Size in Width
        int timerTick = 0;
        int animationTick = 0;
        int ShotsFired = 0;
        int PreloaderTimer = 100;//time for intro screen in frames
        int NextLevelTime = 100;//time for next level screen in frames
        bool NextLevelScreen = false;
        static int numberOfStars = 300;
        bool pause = true;
        
        List<Keys> KeysPressed = new List<Keys>();
        System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();//Code to initialize sound
        System.Media.SoundPlayer SoundFX = new System.Media.SoundPlayer();
        Random rand = new Random();
       
        Game SpaceInvaders;
        public Form1()
        {
            InitializeComponent();
            SpaceInvaders = new Game(numberOfStars, gameHeight, gameWidth);
            //System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
            myPlayer.SoundLocation = @"..\..\invaders.wav";
            myPlayer.PlayLooping();
            
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (SpaceInvaders.GameOver())
            {
                g.FillRectangle(Brushes.Black, ClientRectangle);// draws the black background

               
                if (timerTick < PreloaderTimer)//gets rid of Space Invaders title
                {
                    g.DrawImage(Properties.Resources.spaceinvadersplash,ClientRectangle);
                }

                else
                {
                   
                    foreach (Star star in SpaceInvaders.StarField) //draws all the stars
                    {
                        if (star.MyVisibility)
                            SpaceInvaders.DrawStars(g, star);
                        if (timerTick % 5 == 0)
                        {
                            star.MyStarLocation.Y += star.MyStarMovementSpeed;//This gives the stars moving effect
                        }
                    }
                    

                    Boom tempBoom = null;
                    foreach (Boom boom in SpaceInvaders.BoomField)
                    {
                        boom.DrawBoom(g, boom.Location);
                        boom.BoomLife--;
                        if (boom.BoomLife == 0)
                        {
                            tempBoom = boom;
                            //SoundFX.SoundLocation = @"..\..\explode.wav";
                            //SoundFX.Play();
                        }
                    }
                    if (tempBoom != null)
                        SpaceInvaders.BoomField.Remove(tempBoom);

                    foreach (Invader invader in SpaceInvaders.Invaders)//draws all the invaders on the screen
                    {
                        invader.Draw(g, invader.Area, invader.InvaderType );
                    }
                    
                    Shot PlayerTemp=null;

                    foreach (Shot shot in SpaceInvaders.ShotsFired)//draws all the shots that are on the screen, then moves them
                    {
                        shot.DrawShots(g, shot);
                        shot.ShotMove();
                        shot.ShotLife--;
                            
                        if (shot.ShotLife == 0)
                        {
                            PlayerTemp = shot;
                            break;
                        }
                    }
                    if (PlayerTemp !=null)
                    SpaceInvaders.ShotsFired.Remove(PlayerTemp);

                    Shot InvaderTemp = null;
                    foreach (Shot shot in SpaceInvaders.InvaderShots)//draws all the shots that are on the screen, then moves them
                    {

                        shot.DrawShots(g, shot);
                        shot.ShotMove();
                        shot.ShotLife--;
                            
                        if (shot.ShotLife == 0)
                        {
                            InvaderTemp = shot;
                            break;
                        }
                    }
                    if (InvaderTemp != null)
                        SpaceInvaders.InvaderShots.Remove(InvaderTemp);
                    /////////////////////////////////////////////////HUD INFO///////////////////////////////////////////////////////
                    g.FillRectangle(Brushes.DarkRed, 0, 0, gameWidth, 50);//Game info screen at the top
                    using (Font stencil12Bold = new Font("Stencil", 12, FontStyle.Bold))
                    {
                        g.DrawString("High Score", stencil12Bold, Brushes.White, 500, 1);
                        g.DrawString(""+ SpaceInvaders.HighScore , stencil12Bold, Brushes.White, 500, 20);

                        g.DrawString("Score", stencil12Bold, Brushes.White, 350, 1);
                        g.DrawString(""+SpaceInvaders.score, stencil12Bold, Brushes.White, 350, 20);

                        g.DrawString("Lives Left", stencil12Bold, Brushes.White, 750, 1);
                        g.DrawString(""+ SpaceInvaders.livesLeft, stencil12Bold, Brushes.White, 750, 20);

                        g.DrawString("Level", stencil12Bold, Brushes.White, 875, 1);
                        g.DrawString("" + SpaceInvaders.wave , stencil12Bold, Brushes.White, 875, 20);
                    }

                    using (Font stencil24Bold = new Font("Stencil", 24, FontStyle.Bold))
                    {
                        g.DrawString("SPACE INVADERS", stencil24Bold, Brushes.White, 5, 5);
                    }


                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    SpaceInvaders.MyPlayerShip.Draw(g, SpaceInvaders.MyPlayerShip.Area);//draws the player ship
                }
            }

           /* else if(NextLevelScreen)//Next Level Screen
            {
                g.FillRectangle(Brushes.Black, ClientRectangle);// draws the black background
                using (Font arial48Bold = new Font("Arial", 48, FontStyle.Bold))
                {
                    g.DrawString("Next Wave Incoming!", arial48Bold, Brushes.Red, 330, 300);
                }
                using (Font arial24Bold = new Font("Arial", 24, FontStyle.Bold))
                {
                    g.DrawString("Press R to Restart", arial24Bold, Brushes.Red, 370, 400);
                }
                NextLevelTime--;
                if (NextLevelTime < 0)
                    NextLevelScreen = false;
            }*/
            else 
            {
                g.FillRectangle(Brushes.Black, ClientRectangle);// draws the black background
                using (Font arial48Bold = new Font("Arial", 48, FontStyle.Bold))
                {
                    g.DrawString("Game Over!", arial48Bold, Brushes.Red, 330, 300);
                }
                using (Font arial24Bold = new Font("Arial", 24, FontStyle.Bold))
                {
                    g.DrawString("Press R to Restart", arial24Bold, Brushes.Red, 370, 400);
                }
            }
        }
         
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            
            if (SpaceInvaders.GameOver())
            {
                if (SpaceInvaders.score > SpaceInvaders.HighScore)
                    SpaceInvaders.HighScore = SpaceInvaders.score;
                if (SpaceInvaders.Invaders.Count() == 0)
                {
                    NextLevelScreen = true;
                    NextLevel();
                    
                }
                timerTick++;
                
                
                Invalidate();//This causes the paint event to redraw the screen

                if (timerTick > PreloaderTimer/* && !NextLevelScreen*/)//working on next level screen
                {
                    if (timerTick % 5 == 0)//slows down the create star process
                        SpaceInvaders.CreateStar(3, false);
                    SpaceInvaders.Go();
                    foreach (Keys key in KeysPressed)
                    {
                        if (key == Keys.Left)
                        {
                            SpaceInvaders.MyPlayerShip.MyPlayerShipImage = Properties.Resources.playershipLeft;
                            SpaceInvaders.MyPlayerShip.MoveLeft();
                            return;
                        }

                        else if (key == Keys.Right)
                        {
                            SpaceInvaders.MyPlayerShip.MyPlayerShipImage = Properties.Resources.playershipRight;
                            SpaceInvaders.MyPlayerShip.MoveRight();
                            return;
                        }
                    }
                }

            }
        }
        
        
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            foreach (Invader invader in SpaceInvaders.Invaders)
            {
                invader.CellNumber = animationTick % 4;

            }
            animationTick++;
            
           
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();

          

            if (e.KeyCode == Keys.R/* && !SpaceInvaders.GameOver()*/)
            {
                ResetGame();
                
            }

            if (e.KeyCode == Keys.Space && timerTick > PreloaderTimer)
            {

                if (SpaceInvaders.ShotsFired.Count() < 1)
                {
                    SpaceInvaders.Fire(true, SpaceInvaders.MyPlayerShip.Location);
                    ShotsFired++;
                }
                
            }

            if (e.KeyCode == Keys.P)
            {
                if (pause)
                {
                    gameTimer.Stop();
                    animationTimer.Stop();
                    pause = false;
                }
                else
                {
                    gameTimer.Start();
                    animationTimer.Start();
                    pause = true;

                }
                //myPlayer
                
            }

            if (e.KeyCode == Keys.M)
            {

                SoundFX.SoundLocation = @"..\..\invaders.wav";
                SoundFX.Play();
            }

            if (KeysPressed.Contains(e.KeyCode))
                KeysPressed.Remove(e.KeyCode);
            KeysPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            SpaceInvaders.MyPlayerShip.MyPlayerShipImage = Properties.Resources.playershipTop;
            if (KeysPressed.Contains(e.KeyCode))
                KeysPressed.Remove(e.KeyCode);
        }

        public void ResetGame()
        {
            SpaceInvaders.score = 0;
            SpaceInvaders.livesLeft = 2;
            SpaceInvaders.wave = 1;
            
            SpaceInvaders.ShotsFired.Clear();
            SpaceInvaders.InvaderShots.Clear();
            SpaceInvaders.Invaders.Clear();
            SpaceInvaders.InitializeInvader();
            SpaceInvaders.StarField.Clear();
            SpaceInvaders.MyPlayerShip.StartingPosition();
            SpaceInvaders.CreateStar(numberOfStars, true);
            timerTick = 0;
            animationTick = 0;
            ShotsFired = 0;

        }
        public void NextLevel()
        {
            SpaceInvaders.livesLeft++;
            SpaceInvaders.wave++;

            SpaceInvaders.ShotsFired.Clear();
            SpaceInvaders.InvaderShots.Clear();
            SpaceInvaders.Invaders.Clear();
            SpaceInvaders.InitializeInvader();
            SpaceInvaders.StarField.Clear();
            SpaceInvaders.MyPlayerShip.StartingPosition();
            SpaceInvaders.CreateStar(numberOfStars, true);
            timerTick = PreloaderTimer;
            animationTick = 0;
            ShotsFired = 0;
        }

        
    }
}
