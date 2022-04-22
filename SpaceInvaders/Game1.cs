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

        Texture2D blueButtonBild;
        Rectangle blueButtonRect = new Rectangle(300, 200, 190, 49);

        SpriteFont arialFont;

        string start = "Press the button to play";
        Vector2 startPosition = new Vector2(150, 100);

        string lostText = "You lost :(";
        Vector2 lostTextPosition = new Vector2(300, 200);

        string winText = "You won! :)";
        Vector2 winTextPosition = new Vector2(300, 200);

        public Dictionary<string, int> settingsDict = new Dictionary<string, int>();

        int scene = 0;

        MouseState mouse = Mouse.GetState();
        MouseState oldMouse = Mouse.GetState();

        KeyboardState tangentbord = Keyboard.GetState();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //variables in dictionary
            settingsDict["movingSpeed"] = 30;
            settingsDict["updatesToNewLaser"] = 60;
            settingsDict["laserMovements"] = 5;
            settingsDict["movingSpeedY"] = 30;
            settingsDict["updatesToAlienMovements"] = 60;
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

            blueButtonBild = Content.Load<Texture2D>("blueButton");

            arialFont = Content.Load<SpriteFont>("arial");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            oldMouse = mouse;
            mouse = Mouse.GetState();
            
            if (ButtonPressed() == true)
            {
                scene = 1;
            }

            if (win() == true)
            {
                scene = 3;
            }

            lost();

            switch (scene)
            {
                case 0:
                    
                    break;

                case 1:
                    
                    shipMovements();
                    laserShooter();
                    collision();
                    alienMovements();
                    
                    break;

                case 2:
                    break;

                case 3:
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            switch (scene)
            {
                case 0:
                    DrawFirstScene();
                    break;

                case 1:
                    DrawGame();
                    break;

                case 2:
                    drawGameOverScene();
                    break;

                case 3:
                    drawWinningScene();
                    break;
            }
            base.Draw(gameTime);
        }

        void DrawGame()
        {
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

            settingsDict["updatesToNewLaser"]--;
            if (tangentbord.IsKeyDown(Keys.Space) && settingsDict["updatesToNewLaser"] <= 0)
            {
                settingsDict["updatesToNewLaser"] = 60;
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
                    temp.Y -= settingsDict["laserMovements"];
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
            for (int i = 0; i < 2; i++)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        shipYellow_mannedRect.Add(new Rectangle(20 + 80 * x, 20 + 60 * y, 50, 50));
                    }
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

        }

        void alienMovements()
        {
            settingsDict["updatesToAlienMovements"]--;

            if (settingsDict["updatesToAlienMovements"] <= 0)
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
                        temp.X += settingsDict["movingSpeed"];
                        greenAlienRect[i] = temp;
                    }
                }

                for (int i = 0; i < shipYellow_mannedRect.Count; i++)
                {
                    Rectangle temp = shipYellow_mannedRect[i];

                    if (temp.Y < 420)
                    {
                        temp.Y += 5;
                        shipYellow_mannedRect[i] = temp;
                    }
                }

                for (int i = 0; i < shipYellow_mannedRect.Count; i++)
                {
                    Rectangle temp = shipYellow_mannedRect[i];
                    if (temp.X < 800 - temp.Width || temp.X > 0)
                    {
                        temp.X += settingsDict["movingSpeedY"];
                        shipYellow_mannedRect[i] = temp;
                    }
                }

                if (greenAlienRect.Count >= 1)
                { 
                    Rectangle first = greenAlienRect[0];
                    Rectangle last = greenAlienRect[greenAlienRect.Count - 1];

                    if (last.X >= 800 - last.Width || first.X <= 0)
                    {
                        settingsDict["movingSpeed"] *= -1;
                    }
                }

                if (shipYellow_mannedRect.Count >= 1)
                {
                    Rectangle lastY = shipYellow_mannedRect[shipYellow_mannedRect.Count - 1];
                    Rectangle firstY = shipYellow_mannedRect[0];

                    if (lastY.X >= 800 - lastY.Width || firstY.X <= 0)
                    {
                        settingsDict["movingSpeedY"] *= -1;
                    }
                }

                settingsDict["updatesToAlienMovements"] = 60;
            }
        }
        void DrawFirstScene()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(blueButtonBild, blueButtonRect, Color.White);
            spriteBatch.DrawString(arialFont, start, startPosition, Color.Black);
            spriteBatch.End();

        }

        bool ButtonPressed()
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        void lost()
        {

            if (shipYellow_mannedRect.Count >= 1)
            {
                Rectangle last = shipYellow_mannedRect[shipYellow_mannedRect.Count - 1];
                if (last.Y >= 400)
                {
                    scene = 2;
                }
            }

            if (greenAlienRect.Count >= 1)
            {
                if (greenAlienRect[0].Y >= 400)
                {
                    scene = 2;
                }
            }
        }

        void drawGameOverScene()
        {
            GraphicsDevice.Clear(Color.PaleVioletRed);

            spriteBatch.Begin();
            spriteBatch.DrawString(arialFont, lostText, lostTextPosition, Color.Black);
            spriteBatch.End();
        }

        bool win()
        {
            if(greenAlienRect.Count == 0 && shipYellow_mannedRect.Count == 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        void drawWinningScene()
        {
            GraphicsDevice.Clear(Color.LightSeaGreen);

            spriteBatch.Begin();
            spriteBatch.DrawString(arialFont, winText, winTextPosition, Color.Black);
            spriteBatch.End();
        }
    }
}
