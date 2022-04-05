using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SpaceInvaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteBatch spriteBatch;
        Texture2D playerShipBild;
        Rectangle playerShipRectangle = new Rectangle(350, 420, 55, 35);

        Texture2D shipYellow_mannedBild;
        List<Rectangle> shipYellow_mannedRect = new List<Rectangle>();

        Texture2D greenAlienBild;
        List<Rectangle> greenAlienRect = new List<Rectangle>();

        Texture2D laserBild;
        List<Rectangle> laserRects = new List<Rectangle>();

        int updatesTillNyLaser = 60;
        int laserMovements = 5;
        int movingSpeed = 30;
        int updatesTillAlienMovements = 60;


        KeyboardState tangentbord = Keyboard.GetState();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            aliens();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerShipBild = Content.Load<Texture2D>("playerShip");

            greenAlienBild = Content.Load<Texture2D>("greenAlien");
            shipYellow_mannedBild = Content.Load<Texture2D>("shipYellow_manned");

            laserBild = Content.Load<Texture2D>("laser");
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            shipMovements();
            laserShooter();
            collision();
            alienMovements();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(playerShipBild, playerShipRectangle, Color.White);

            foreach (Rectangle laserPosition in laserRects)
            {
                spriteBatch.Draw(laserBild, laserPosition, Color.White);
            }

            foreach (Rectangle yellowShip in shipYellow_mannedRect)
            {
                spriteBatch.Draw(shipYellow_mannedBild, yellowShip, Color.White);
            }

            foreach (Rectangle alien in greenAlienRect)
            {
                spriteBatch.Draw(greenAlienBild, alien, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void shipMovements()
        {
            tangentbord = Keyboard.GetState();

            if (tangentbord.IsKeyDown(Keys.Left) == true)
            {
                playerShipRectangle.X -= 5;
            }

            if (tangentbord.IsKeyDown(Keys.Right) == true)
            {
                playerShipRectangle.X += 5;
            }
        }

        void laserShooter()
        {
            tangentbord = Keyboard.GetState();

            updatesTillNyLaser--;
            if (tangentbord.IsKeyDown(Keys.Space) && updatesTillNyLaser <= 0)
            {
                updatesTillNyLaser = 60;
                for (int x = 0; x < 1; x++)
                {
                    laserRects.Add(new Rectangle(playerShipRectangle.X + 23, 390, 9, 20));
                }

            }
            
            for (int i = 0; i < laserRects.Count; i++)
            {

                Rectangle temp = laserRects[i];

                if (temp.Y > 0)
                {
                    temp.Y -= laserMovements;
                    laserRects[i] = temp;
                }

                if (temp.Y <= 0)
                {
                    laserRects.RemoveAt(i);
                }
            }
            
        }

        void aliens()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    shipYellow_mannedRect.Add(new Rectangle(20 + 80 * x, 20 + 60 * y, 50, 50));
                }
            }

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    shipYellow_mannedRect.Add(new Rectangle(20 + 80 * x, 20 + 60 * y, 50, 50));
                }
            }

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 1; y++)
                {
                    greenAlienRect.Add(new Rectangle(20 + 80 * x, 140 + 60 * y, 50, 50));
                }
            }
        }

        void collision()
        {

            if (laserRects.Count <= shipYellow_mannedRect.Count)
            {
                if (shipYellow_mannedRect.Count >= laserRects.Count)
                {
                    for (int i = 0; i < shipYellow_mannedRect.Count; i++)
                    {
                        Rectangle temp = shipYellow_mannedRect[i];

                        for (int j = 0; j < laserRects.Count; j++)
                        {
                            Rectangle temp2 = laserRects[j];

                            if (temp.Intersects(temp2))
                            {
                                laserRects.Remove(temp2);
                                shipYellow_mannedRect.Remove(temp);
                            }
                        }
                        
                    }
                }
            }
            /*
            else
            {
                for (int i = 0; i < laserRects.Count; i++)
                {
                    Rectangle temp = laserRects[i];

                    for (int j = 0; j < shipYellow_mannedRect.Count; j++)
                    {
                        Rectangle temp2 = shipYellow_mannedRect[j];

                        if (temp.Intersects(temp2))
                        {
                            laserRects.Remove(temp);
                            shipYellow_mannedRect.Remove(temp2);
                        }
                    }
                }

            }
            */

            if (laserRects.Count <= greenAlienRect.Count)
            {
                if (greenAlienRect.Count >= laserRects.Count)
                {
                    for (int i = 0; i < greenAlienRect.Count; i++)
                    {
                        Rectangle temp = greenAlienRect[i];

                        for (int j = 0; j < laserRects.Count; j++)
                        {
                            Rectangle temp2 = laserRects[j];

                            if (temp.Intersects(temp2))
                            {
                                laserRects.Remove(temp2);
                                greenAlienRect.Remove(temp);
                            }
                        }

                    }
                }
            }
            /*
            else
            {
                for (int i = 0; i < laserRects.Count; i++)
                {
                    Rectangle temp = laserRects[i];

                    for (int j = 0; j < greenAlienRect.Count; j++)
                    {
                        Rectangle temp2 = greenAlienRect[j];

                        if (temp.Intersects(temp2))
                        {
                            laserRects.Remove(temp);
                            greenAlienRect.Remove(temp2);
                        }
                    }
                }

            }
            */
        }

        void alienMovements()
        {
            updatesTillAlienMovements--;

            if (updatesTillAlienMovements <= 0)
            {
                
                for (int i = 0; i < greenAlienRect.Count; i++)
                {
                    Rectangle temp = greenAlienRect[i];

                    if (temp.Y < 420)
                    {
                        
                        temp.Y += 5;
                        greenAlienRect[i] = temp;
                    }
                }
                

                for (int i = 0; i < greenAlienRect.Count; i++)
                {
                    Rectangle temp = greenAlienRect[i];
                    if (temp.X < 800 - temp.Width || temp.X > 0)
                    {
                        temp.X += movingSpeed;
                        greenAlienRect[i] = temp;
                    }
                }
                 // ändra till yellow ship
                for (int i = 0; i < shipYellow_mannedRect.Count; i++)
                {
                    Rectangle temp = shipYellow_mannedRect[i];

                    if (temp.Y < 420)
                    {

                        temp.Y += 5;
                        greenAlienRect[i] = temp;
                    }
                }


                for (int i = 0; i < greenAlienRect.Count; i++)
                {
                    Rectangle temp = greenAlienRect[i];
                    if (temp.X < 800 - temp.Width || temp.X > 0)
                    {
                        temp.X += movingSpeed;
                        greenAlienRect[i] = temp;
                    }
                }
            

                Rectangle first = greenAlienRect[0];
                Rectangle last = greenAlienRect[greenAlienRect.Count - 1];

                if (first.X <= 0)
                {
                    movingSpeed *= -1;
                }

                if (last.X >= 800 - last.Width)
                {
                    movingSpeed *= -1;
                }

                updatesTillAlienMovements = 60;
            }
            
        }
    }
}
