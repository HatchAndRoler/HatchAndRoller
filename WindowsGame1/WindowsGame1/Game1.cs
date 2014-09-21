using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        KeyboardState keyboard;
        Texture2D Hatch;
        Texture2D Roller;
        Texture2D Background;
        Texture2D Floor1;
        Texture2D Floor2;
        int screenWidth;
        int screenHeight;
        int AnimationCount = 0;
        int JumpCount = 0;
        float distance = 0;
        int DJumpCount = 0;
        float Ddistance = 0;
        Vector2 HatchPos = new Vector2(100, 515);
        Vector2 RollerPos = new Vector2();
        Vector2 BackGroundPos= new Vector2(0,0);
        Vector2 FloorPos = new Vector2(0, 0);
        Vector2 Floor2Pos = new Vector2(255, 0);
        scrolling scrolling1;
        scrolling scrolling2;
        scrolling scrolling3;
        scrolling scrolling4;
        scrolling scrolling5;
        scrolling scrolling6;
        bool IsSpacePressed = false;
        bool IsDoublePressed = false;
        SpriteEffects s;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Hatch & Roller";
            RollerPos = new Vector2(HatchPos.X - 75, HatchPos.Y - 130);
            base.Initialize();
        }


        protected override void LoadContent()
        {
            device = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            Background = Content.Load<Texture2D>("background");
            Floor1 = Content.Load<Texture2D>("envionment block 1");
            Floor2 = Content.Load<Texture2D>("envionment block 2");
            Hatch = Content.Load<Texture2D>("hatch");
            Roller = Content.Load<Texture2D>("roller");
            scrolling1 = new scrolling(Content.Load<Texture2D>("envionment block 1"), new Rectangle(0, 631, 270, 89));
            scrolling2 = new scrolling(Content.Load<Texture2D>("envionment block 1"), new Rectangle(255, 631, 270, 89));
            scrolling3 = new scrolling(Content.Load<Texture2D>("envionment block 1"), new Rectangle(519, 631, 270, 89));
            scrolling4 = new scrolling(Content.Load<Texture2D>("envionment block 1"), new Rectangle(783, 631, 270, 89));
            scrolling5 = new scrolling(Content.Load<Texture2D>("envionment block 1"), new Rectangle(1047, 631, 270, 89));
            scrolling6 = new scrolling(Content.Load<Texture2D>("envionment block 1"), new Rectangle(1311, 631, 270, 89));

        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            RollerAnimation();
            HatchMovement();
            HatchJump();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(Background, BackGroundPos, Color.White);
            scrolling1.draw(spriteBatch);
            scrolling2.draw(spriteBatch);
            scrolling3.draw(spriteBatch);
            scrolling4.draw(spriteBatch);
            scrolling5.draw(spriteBatch);
            scrolling6.draw(spriteBatch);
            //spriteBatch.Draw(Hatch, HatchPos, Color.White);
           // spriteBatch.Draw(Roller, RollerPos, Color.White);
            spriteBatch.Draw(Hatch, new Rectangle((int)HatchPos.X, (int)HatchPos.Y/1, 72, 156), null, Color.White, 0, new Vector2(0, 0), s, 0f);
            spriteBatch.Draw(Roller, new Rectangle((int)RollerPos.X, (int)RollerPos.Y / 1, 66, 45), null, Color.White, 0, new Vector2(0, 0), s, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void RollerAnimation()
        {
            AnimationCount++;
            RollerPos.Y = HatchPos.Y - 50;
            if ((AnimationCount >= 0) && (AnimationCount <= 5))
            {
                RollerPos.Y += 2;
            }
            if ((AnimationCount >= 6) && (AnimationCount <= 11))
            {
                RollerPos.Y -= 2;
            }
            if (AnimationCount == 11)
                AnimationCount = -1;
        }

        public void HatchMovement()
        {
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {

                if (HatchPos.X >= 100 && HatchPos.X <= 1000)
                {
                    HatchPos.X += 5;
                    s = SpriteEffects.None;
                    RollerPos.X = HatchPos.X - 75;
                    
                }
                else
                {
                    if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
                        scrolling1.rectangle.X = scrolling6.rectangle.X + scrolling6.texture.Width;
                    if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
                        scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;
                    if (scrolling3.rectangle.X + scrolling3.texture.Width <= 0)
                        scrolling3.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
                    if (scrolling4.rectangle.X + scrolling4.texture.Width <= 0)
                        scrolling4.rectangle.X = scrolling3.rectangle.X + scrolling3.texture.Width;
                    if (scrolling5.rectangle.X + scrolling5.texture.Width <= 0)
                        scrolling5.rectangle.X = scrolling4.rectangle.X + scrolling4.texture.Width;
                    if (scrolling6.rectangle.X + scrolling6.texture.Width <= 0)
                        scrolling6.rectangle.X = scrolling5.rectangle.X + scrolling5.texture.Width;
                    scrolling1.update();
                    scrolling2.update();
                    scrolling3.update();
                    scrolling4.update();
                    scrolling5.update();
                    scrolling6.update();
                }
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                if (HatchPos.X > 100)
                {
                    HatchPos.X -= 5;
                    RollerPos.X = HatchPos.X + 75;
                    s = SpriteEffects.FlipHorizontally;
                }
            }


        }

        public void HatchJump()
        {
            HatchPos.Y += distance;
            IsDoublePressed = false;
            if (IsSpacePressed)
            {
                JumpCount++;
                distance += 0 - (distance * 0.18f);

                if (keyboard.IsKeyDown(Keys.Z))
                {
                    IsDoublePressed = true;
                }
               
            }
            if (keyboard.IsKeyDown(Keys.Space))
            {
                IsSpacePressed = true;
            }
            if (HatchPos.Y == 515 && IsSpacePressed)
            {
            distance = -30;

            }
           

            if (JumpCount == 15)
            {
                JumpCount++;
                IsSpacePressed = false;
                distance += -(distance * (1.1f));
            }

            if (JumpCount == 16 )
            {
                if (keyboard.IsKeyDown(Keys.Z))
                {
                    IsDoublePressed = true;
                }
                IsSpacePressed = false;
                distance += (distance * (0.2f));
            }
            if (HatchPos.Y > 515)
            {
                HatchPos.Y = 515;
                DJumpCount = 0;
                Ddistance = 0;
                HatchPos.Y = 515;
                JumpCount = 0;
                distance = 0;
            }
            
          if(IsDoublePressed)
          DoubleJump(IsDoublePressed, HatchPos);


        }


        public void DoubleJump(bool IsPressed, Vector2 CurrentPos)
        {
            HatchPos.Y += Ddistance;
            if (IsPressed)
            {
                DJumpCount++;
                Ddistance += 0 - (Ddistance * 0.3f);
            }
            if (HatchPos.Y == CurrentPos.Y && IsPressed)
            {

                Ddistance = -50;
            }


            if (DJumpCount == 61)
            {
                DJumpCount++;
                IsPressed = false;
                Ddistance += -(Ddistance * (1.4f));
            }

            if (DJumpCount == 62)
            {
                IsPressed = false;
                Ddistance += (Ddistance * (0.2f));
            }
            if (HatchPos.Y > 515)
            {
                HatchPos.Y = 515;
                DJumpCount = 0;
                Ddistance = 0;
                HatchPos.Y = 515;
                JumpCount = 0;
                distance = 0;
            }
           
        }
    }
}
