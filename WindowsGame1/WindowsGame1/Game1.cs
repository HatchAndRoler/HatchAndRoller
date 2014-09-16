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
        Texture2D Enemy;
        Texture2D Background;
        Texture2D Floor1;
        Texture2D Floor2;
        int screenWidth;
        int screenHeight;
        int AnimationCount = 0;
        int FloorCount=0;
        Vector2 HatchPos = new Vector2(100, 380);
        Vector2 RollerPos = new Vector2(25, 250);
        Vector2 BackGroundPos = new Vector2(0, 0);
        Vector2 FloorPos = new Vector2(0, 433);
        Vector2 Floor2Pos = new Vector2(1100, 433);
        scrolling scrolling1;
        scrolling scrolling2;
        
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
            base.Initialize();
        }

       
        protected override void LoadContent()
        {
            device = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            Background = Content.Load<Texture2D>("background");
            Floor1 = Content.Load<Texture2D>("floor1");
            Floor2 = Content.Load<Texture2D>("floor_rev");
            Hatch = Content.Load<Texture2D>("hatchfordemo");
            Roller = Content.Load<Texture2D>("roller");
            Enemy = Content.Load<Texture2D>("enemy");
            scrolling1= new scrolling(Content.Load<Texture2D>("floor1"), new Rectangle(0,433,1400,720));
            scrolling2 = new scrolling(Content.Load<Texture2D>("floor_rev"), new Rectangle(1400, 433, 2800, 720));

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

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(Background, BackGroundPos , Color.White);
            scrolling1.draw(spriteBatch);
            scrolling2.draw(spriteBatch);
            spriteBatch.Draw(Hatch, HatchPos, Color.White);
            spriteBatch.Draw(Roller, RollerPos, Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void RollerAnimation()
        {
            AnimationCount++;
            
                if ((AnimationCount >= 0) && (AnimationCount <= 5))
                    RollerPos.Y += 2;
                if ((AnimationCount >= 6) && (AnimationCount <= 11))
                    RollerPos.Y -= 2;

            if (AnimationCount == 11)
                AnimationCount = -1;
        }

        public void HatchMovement()
        {
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                FloorCount++;
                FloorPos.X -= 3;
                Floor2Pos.X -= 3;

                if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
                    scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
                if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
                    scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;
                scrolling1.update();
                scrolling2.update();
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                FloorCount--;
                FloorPos.X += 3;
                Floor2Pos.X += 3;
            }

           
        }
    }
}
