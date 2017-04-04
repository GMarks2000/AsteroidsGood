using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    public partial class GameScreen : UserControl
    {
        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        //keypress variables
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, spaceDown;

        SolidBrush drawBrush = new SolidBrush(Color.White);

        //asteroid, explosion, star, and bullet lists
        List<Asteroid> asteroids = new List<Asteroid>();
        List<Bullet> bullets = new List<Bullet>();
        List<Explosion> explosions = new List<Explosion>();
        List<Star> stars = new List<Star>();


        //integer variables
        int playerX, playerY, playerWidth, playerHeight, playerSpeed, ticks, bulletSize, bulletSpeed, shotTime, spawnRate;

        //stores player's current orientation
        string playerDir;
        bool playerAlive; 

        //initializes game values
        private void OnStart()
        {   
            //starts player at center screen
            playerX = this.Width / 2 - playerWidth / 2;
            playerY = this.Height / 2 - playerHeight / 2;
            playerHeight = 30;
            playerWidth = 15;
            playerSpeed = 5;
            playerDir = "up";
            playerAlive = true;

            //initializes other variables

            ticks = 0;
            Form1.score = 0;
            shotTime = 0;
            bulletSize = 8;
            bulletSpeed = 10;

            Random rand = new Random();

            //randomly generates scren of stars
            for (int i = 0; i < 100; i++)
            {
                
                int x = rand.Next(0, this.Width);
                int y = rand.Next(0, this.Height);
                int size = rand.Next(1, 6);
                stars.Add(new Star(x, y, size));
            }
        }

        //tick method
        private void timer_Tick(object sender, EventArgs e)
        {
            //moves player if appropriate
            if (upArrowDown)
            {
                playerY -= playerSpeed;
                playerDir = "up";
            }
            else if (downArrowDown)
            {
                playerY += playerSpeed;
                playerDir = "down";
            }
            else if (leftArrowDown)
            {
                playerX -= playerSpeed;
                playerDir = "left";
            }
            else if (rightArrowDown)
            {
                playerX += playerSpeed;
                playerDir = "right";
            }

            //pops player out at other side of screen when necessary
            if (playerX + playerWidth < 0) { playerX = this.Width; }
            if (playerX > this.Width) { playerX = 0; }
            if (playerY < 0) { playerY = this.Height; }
            if (playerY > this.Height) { playerY = 0; }
            
            //code for each asteroid
            foreach (Asteroid a in asteroids)
            {   
                //used to prevent an exception
                bool loopBroken = false;

                a.Move();

                //removes offscreen asteroids
                if (a.checkOffscreen(this, a.size))
                {
                    asteroids.Remove(a);
                    break;
                }

                //checks for player-asteroid collision
                if (a.checkCollision(playerX, playerY, playerWidth, playerHeight, playerDir) && playerAlive)
                {   
                    //kills player and adds an explosion. When this explosion dissiptes, the game will end.
                    playerAlive = false;
                    explosions.Add(new Explosion(playerX, playerY, 1));
                }

                //ends the game when the player is dead and the last explosion is gone
                if (playerAlive == false && explosions.Count() == 0)
                {
                    GameOver();
                }
                //checks for collision with every extant bullet
                foreach (Bullet b in bullets)
                {
                    if (a.checkCollision(b))
                    {   
                        //increases player score
                        Form1.score += a.size;

                        //adds new explosion at the midpoint of the bullet and the asteroid
                        explosions.Add(new Explosion((a.x + a.size / 2 + b.x + b.size) / 2, (a.y + a.size / 2 + b.y + b.size) / 2, 1));

                        //adds "daughter" asteroids that break off as fragments if size is large enough
                        if (a.size >= 25)
                        {
                            asteroids.Add(new Asteroid(a.x + a.size / 4, a.y + a.size / 4, a.size / 2, -Math.Abs(a.xSpeed / 2), -Math.Abs(a.ySpeed / 2)));
                            asteroids.Add(new Asteroid(a.x + a.size* 3/4, a.y + a.size * 3/4, a.size / 2, Math.Abs(a.xSpeed / 2), Math.Abs(a.ySpeed / 2)));
                        }


                        //removes asteroid and bullet and breaks both loops to prevent errors
                        asteroids.Remove(a);
                        bullets.Remove(b);
                        loopBroken = true;
                        break;
                    }
                }
                //prevents exception by breaking out of both loops if one loop is broken due to collision
                if (loopBroken) { break; }      
            }

            //code for each bullet
            foreach (Bullet b in bullets)
            {
                b.Move();

                //removes offscreen asteroids
                if (b.checkOffscreen(this))
                {
                    bullets.Remove(b);
                    break;
                }
            }

            //code for each explosion
            foreach (Explosion ex in explosions)
            {
                if (ex.expandAndCheckSize(100))
                {
                    explosions.Remove(ex);
                    break;
                }
            }

            //code for each star
            foreach (Star s in stars)
            {
                s.Move(this);
            }

            //asteroid generation
            if (ticks == 60)
            {
                Random rand = new Random();

                //decides randomly what side to add the asteroid to
                int sideDecider = rand.Next(0, 4);

                //randomize start location and speed
                int Locator = rand.Next(100, this.Width - 100);
                int shortSpeed = rand.Next(0, 4);
                int longSpeed = rand.Next(3, 10);
                int size = rand.Next(10, 76);

                switch (sideDecider)
                {
                    case 0:
                        asteroids.Add(new Asteroid(0, Locator, size, longSpeed, shortSpeed));
                        break;
                    case 1:
                        asteroids.Add(new Asteroid(Locator, 0, size, shortSpeed, longSpeed));
                        break;
                    case 2:
                        asteroids.Add(new Asteroid(this.Width, Locator, size, -longSpeed, -shortSpeed));
                        break;
                    case 3:
                        asteroids.Add(new Asteroid(Locator, this.Height, size, -shortSpeed, -longSpeed));
                        break;
                    default:
                        break;
                }
                //decreases the spawn rate of asteroids until a certain threshold
                if (spawnRate > 15) { spawnRate--; }
                
                //
                ticks = 0;
            }

            //bullet generation. Makes bullet if space is down and 10 frames have passed since last shot
            if (spaceDown && shotTime >= 10 && playerAlive)
            {
                shotTime = 0;

                //generates bullet at the appropriate point relative to the player's direction
                switch (playerDir)
                {
                    case "up":
                        bullets.Add(new Bullet(playerX + playerWidth / 2 - bulletSize / 2, playerY - bulletSize, bulletSize, bulletSpeed, "up"));
                        break;
                    case "down":
                        bullets.Add(new Bullet(playerX + playerWidth / 2 - bulletSize / 2, playerY + playerHeight, bulletSize, bulletSpeed, "down"));
                        break;
                    case "left":
                        bullets.Add(new Bullet(playerX - bulletSize, playerY + playerWidth / 2 - bulletSize / 2, bulletSize, bulletSpeed, "left"));
                        break;
                    case "right":
                        bullets.Add(new Bullet(playerX + playerHeight, playerY + playerWidth / 2 - bulletSize / 2, bulletSize, bulletSpeed, "right"));
                        break;
                    default:
                        break;
                }
            }

            //increments shot timer if it has not yet reached its max threshold for a new shot to appear
            if (shotTime <= 10) { shotTime++; }

            //updates score label
            scoreLabel.Text = "Score: " + Form1.score;

            //increments tick counter and calls paint method
            ticks++;
            Refresh();
        }

        //paint method
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {   
            drawBrush.Color = Color.White;

            //draws stars
            foreach (Star s in stars)
            {
                e.Graphics.FillEllipse(drawBrush, s.x, s.y, s.size, s.size);
            }

            //draws player
            if (playerAlive)
            {
                drawBrush.Color = Color.LightBlue;
                                            
                //creates and fills player triangle
                Point[] playerPoints = new Point[3];

                //changes triangle orientation based on player direction
                switch (playerDir)
                {
                    case "up":
                        playerPoints[0] = new Point(playerX + playerWidth / 2, playerY);
                        playerPoints[1] = new Point(playerX, playerY + playerHeight);
                        playerPoints[2] = new Point(playerX + playerWidth, playerY + playerHeight);
                        break;

                    case "down":
                        playerPoints[0] = new Point(playerX, playerY);
                        playerPoints[1] = new Point(playerX + playerWidth / 2, playerY + playerHeight);
                        playerPoints[2] = new Point(playerX + playerWidth, playerY);
                        break;

                    case "left":
                        playerPoints[0] = new Point(playerX, playerY + playerWidth / 2);
                        playerPoints[1] = new Point(playerX + playerHeight, playerY + playerWidth);
                        playerPoints[2] = new Point(playerX + playerHeight, playerY);
                        break;

                    case "right":
                        playerPoints[0] = new Point(playerX + playerHeight, playerY + playerWidth / 2);
                        playerPoints[1] = new Point(playerX, playerY + playerWidth);
                        playerPoints[2] = new Point(playerX, playerY);
                        break;

                    default:
                        break;
                }
                //fills player triangle
                e.Graphics.FillPolygon(drawBrush, playerPoints);
            }
            //draws asteroids
            drawBrush.Color = Color.SaddleBrown;
            foreach (Asteroid a in asteroids)
            {
                e.Graphics.FillEllipse(drawBrush, a.x, a.y, a.size, a.size);
            }
            //draws bullets
            drawBrush.Color = Color.Orange;
            foreach (Bullet b in bullets)
            {
                e.Graphics.FillEllipse(drawBrush, b.x, b.y, b.size, b.size);
            }

            //draws explosions as 3 concentric circles. Explosion color values decrease with size so they dim as they expand.
            foreach (Explosion ex in explosions)
            {   
                //yellow layer
                drawBrush.Color = Color.FromArgb(255 - 5/2 * ex.size, 255 - 2 * ex.size, 0);
                e.Graphics.FillEllipse(drawBrush, ex.x, ex.y, ex.size, ex.size);

                //orange layer
                drawBrush.Color = Color.FromArgb(255 - 2 * ex.size, 200 - 2 * ex.size, 0);
                e.Graphics.FillEllipse(drawBrush, ex.x + ex.size / 4, ex.y + ex.size / 4, ex.size / 2, ex.size / 2);

                //red layer
                drawBrush.Color = Color.FromArgb(255 - 2 * ex.size, 0, 0);
                e.Graphics.FillEllipse(drawBrush, ex.x + ex.size * 3/8, ex.y + ex.size * 3 / 8, ex.size / 4, ex.size / 4);
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;              
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    upArrowDown = false;
                    downArrowDown = false;
                    rightArrowDown = false;

                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    upArrowDown = false;
                    leftArrowDown = false;
                    rightArrowDown = false;

                    downArrowDown = true;
                    break;
                case Keys.Right:
                    upArrowDown = false;
                    downArrowDown = false;
                    leftArrowDown = false;

                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    downArrowDown = false;
                    leftArrowDown = false;
                    rightArrowDown = false;

                    upArrowDown = true;
                    break;              
                case Keys.Space:
                    spaceDown = true;
                    break;
                default:
                    break;
            }
        }     

       //game over method
       private void GameOver()
        {
            timer.Stop();

            //closes game screen and opens game over screen
            Form f = this.FindForm();

            //putting this in a try/catch stops random crashes. I have absolutely no idea why.
            try {
                f.Controls.Remove(this);
                GameOverScreen gos = new GameOverScreen();
                f.Controls.Add(gos);
            }
            catch { }
        }  
    }
}
