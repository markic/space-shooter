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

namespace Space
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SpaceShooter : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager graphics;
        
        public Gameplay gameplay = new Gameplay();
        public Menu menu = new Menu();
        public static int Width, Height;

        public enum GameState
        {
            MENU,
            PLAY,
            PAUSE,
            FINISH,
            EXIT
        }

       public static GameState gamestate = GameState.MENU;

        public SpaceShooter()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            setScreen();
            base.Initialize();
        }

        protected override void LoadContent()
        {

            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D[] textures = new Texture2D[8];
            SpriteFont [] spriteFont = new SpriteFont[3];
            SoundEffect soundEffect = Content.Load<SoundEffect>("beep-07");
            spriteFont[0] = Content.Load<SpriteFont>("font1");
            spriteFont[1] = Content.Load<SpriteFont>("font2");
            spriteFont[2] = Content.Load<SpriteFont>("font3");
            textures[0] = Content.Load<Texture2D>("background");
            textures[1] = Content.Load<Texture2D>("player");
            textures[2] = Content.Load<Texture2D>("enemy1");
            textures[3] = Content.Load<Texture2D>("enemy2");
            textures[4] = Content.Load<Texture2D>("bullet0");
            textures[5] = Content.Load<Texture2D>("bullet1");
            textures[6] = Content.Load<Texture2D>("bullet2");
            textures[7] = Content.Load<Texture2D>("bullet3");
            gameplay.LoadContent(textures, spriteBatch, spriteFont, soundEffect);
            menu.LoadContent(spriteBatch, spriteFont);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (gamestate == GameState.MENU || gamestate == GameState.PAUSE || gamestate == GameState.FINISH)
                menu.Update(gameTime);
            if (gamestate == GameState.PLAY)
                gameplay.Update(gameTime);
            if (gamestate == GameState.EXIT)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F10))
            {
                setScreen(1024, 768);
                gamestate = GameState.FINISH;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F11))
            {
                setScreen();
                gamestate = GameState.FINISH;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (gamestate != GameState.PLAY)
                menu.DrawMenu();
            else
                gameplay.Draw(gameTime);
            
            base.Draw(gameTime);
        }
        
        protected void setScreen(int screenWidth = 1024, int screenHeight = 840, bool isFullScreen = false)
        {

            graphics.IsFullScreen = false;
            if (isFullScreen) graphics.IsFullScreen = true;

            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;

            graphics.ApplyChanges();

            Width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            Height = GraphicsDevice.PresentationParameters.BackBufferHeight;

        }
    
    }
}
