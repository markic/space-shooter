using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space
{
    public class Menu
    {
        protected SpriteBatch spriteBatch;
        protected SpriteFont []spriteFont;
        protected int choise = 0;
        protected Color c1,c2,c3,c4;
        protected bool downLastFrameDown = false;
        protected bool upLastFrameDown = false;
        protected bool escLastFrameDown = false;
        protected string []text = new string[10];

        public void LoadContent(SpriteBatch spriteBatch, SpriteFont []spriteFont)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            text[0] = "SPACESHOOTER";
            text[1] = "PAUSED";
            text[2] = "GAME OVER";
            text[3] = "NEW GAME";
            text[4] = "RESUME";
            text[5] = "OPTIONS";
            text[6] = "SCORES";
            text[7] = "EXIT";
            text[8] = "by Medivh - mane90bg@gmail";
        }
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && upLastFrameDown == false)
            { 
                choise--; 
                if (choise <= 0) 
                    choise = 0;
                upLastFrameDown = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && downLastFrameDown == false)
            {
                choise++;
                if (choise >= 3)
                    choise = 3;
                downLastFrameDown = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && escLastFrameDown == false)
            {
                if (SpaceShooter.gamestate == SpaceShooter.GameState.PAUSE)
                    SpaceShooter.gamestate = SpaceShooter.GameState.PLAY;
                else SpaceShooter.gamestate = SpaceShooter.GameState.EXIT;
                escLastFrameDown = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Up))
                upLastFrameDown = false;
            if (Keyboard.GetState().IsKeyUp(Keys.Down))
                downLastFrameDown = false;
            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
                escLastFrameDown = false;
            
                  
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if(choise < 3)
                {

                            if (SpaceShooter.gamestate == SpaceShooter.GameState.FINISH)
                            {
                                SpaceShooter.gamestate = SpaceShooter.GameState.PLAY;
                                Gameplay.Score = 0;
                            }

                            else SpaceShooter.gamestate = SpaceShooter.GameState.PLAY; 
                }
                else { SpaceShooter.gamestate = SpaceShooter.GameState.EXIT; }
                
            }

            c1 = c2 = c3 = c4 = Color.AliceBlue;
            switch (choise)
            {
                case 0: { c1 = Color.Orange; break; }
                case 1: { c2 = Color.Orange; break; }
                case 2: { c3 = Color.Orange; break; }
                case 3: { c4 = Color.Orange; break; }
            }

        }
        public void DrawMenu()
        {
            string test;
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont[2], text[0], new Vector2(SpaceShooter.Width / 3, 100), Color.Orange);

            if (SpaceShooter.gamestate == SpaceShooter.GameState.PAUSE)
            {
                spriteBatch.DrawString(spriteFont[1], text[1] + "    Score: " + Gameplay.Score.ToString(), new Vector2(SpaceShooter.Width / 5, 200), Color.White);
                test = text[4];
            }
            else if (SpaceShooter.gamestate == SpaceShooter.GameState.FINISH)
            {
                spriteBatch.DrawString(spriteFont[1], text[2] +"    Score: " + Gameplay.Score.ToString(), new Vector2(SpaceShooter.Width / 5, 200), Color.White);
                test = text[3];
            }
            else test = text[3];

            spriteBatch.DrawString(spriteFont[0], test, new Vector2(SpaceShooter.Width / 5, 270), c1);
            spriteBatch.DrawString(spriteFont[0], text[5], new Vector2(SpaceShooter.Width / 5, 300), c2);
            spriteBatch.DrawString(spriteFont[0], text[6], new Vector2(SpaceShooter.Width/5, 330), c3);
            spriteBatch.DrawString(spriteFont[0], text[7], new Vector2(SpaceShooter.Width/5, 360), c4);
            spriteBatch.DrawString(spriteFont[0], text[8], new Vector2(SpaceShooter.Width / 3, 700), Color.AliceBlue);
            spriteBatch.End();

        }
    }
}
