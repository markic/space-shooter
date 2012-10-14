using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Space
{
    public class Gameplay
    {
        protected SpriteDrawer spriteDrawer;
        protected Texture2D[] textures;
        protected SpriteFont[] spriteFont;
        protected SpriteBatch spriteBatch;
        protected SoundEffect sound;

        public static long Score;
        protected bool spaceLastFrameDown = false;
        
        public Player player;

        public void LoadContent(Texture2D[] textures, SpriteBatch spriteBatch, SpriteFont[] spriteFont,SoundEffect sound)
        {
            this.textures = textures;
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.sound = sound;
            spriteDrawer = new SpriteDrawer(this.spriteBatch);

            LoadShips();
        }
        
        public void LoadShips()
        {
            int viewport = SpaceShooter.Width / 128;
            for (int i = 0; i < viewport; i++)
            {
                spriteDrawer.AddSprite(new Enemy(textures[2], new Vector2(viewport + 128 * i, -100)));
                spriteDrawer.AddSprite(new Enemy(textures[3], new Vector2(viewport + 128 * i, -220)));
                spriteDrawer.AddSprite(new Enemy(textures[2], new Vector2(viewport + 128 * i, -350)));
            }

            player = new Player(textures[1]);
            spriteDrawer.AddSprite(player);

            spriteDrawer.AddSprite(new Rock(textures[7]));

        }

        public void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
                player.goLeft();
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                player.goRight();
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
                player.goUp();
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
                player.goDown();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                SpaceShooter.gamestate = SpaceShooter.GameState.PAUSE;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && player.Active && spaceLastFrameDown == false)
            {
                Texture2D Guns = textures[4];
                sound.Play();
                
                if (Score < 300)
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-50, 50)));

                else if(300 <= Score && Score < 800)
                {
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-25, 20)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-75, 20)));
                }
                else if(800 <= Score && Score < 1200)
                {
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-5, 20)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-50, 70)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-95, 20)));
                }
                else if (1200 <= Score && Score < 1700)
                {
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-5, 50), Shoot.Type.L));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-50, 50)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-95, 60), Shoot.Type.R));
                }
                else if (1700 <= Score && Score < 2500)
                {
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-25, 50), Shoot.Type.LFLANK));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-5, 90)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-50, 120)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-95, 90)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-75, 60), Shoot.Type.RFLANK));
                }
                else
                {
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-5, 20), Shoot.Type.L));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-25, 50), Shoot.Type.LFLANK));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-5, 90)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-50, 120)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-95, 90)));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-75, 60), Shoot.Type.RFLANK));
                    spriteDrawer.AddSprite(new Shoot(Guns, player.Position - new Vector2(-95, 30), Shoot.Type.R));

                }              
                spaceLastFrameDown = true;
            }
            
            if (Keyboard.GetState().IsKeyUp(Keys.Space))
                spaceLastFrameDown = false;

            if (player.Lives < 0)
            {
                spriteDrawer.RemoveAll();
                player.Lives = 3;
                LoadShips();
                SpaceShooter.gamestate = SpaceShooter.GameState.FINISH;
            
            }
            
            Collision();
            spriteDrawer.Update(gameTime);      
            spriteDrawer.RemoveAllInactive();
        }

        public void Draw(GameTime gameTime)
        {
            
            if (SpaceShooter.gamestate == SpaceShooter.GameState.PLAY)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textures[0], new Rectangle(0, 0, SpaceShooter.Width, SpaceShooter.Height), Color.WhiteSmoke);
                spriteBatch.DrawString(spriteFont[0], "Lives:" + player.Lives.ToString(), new Vector2(5), Color.AliceBlue);
                spriteBatch.DrawString(spriteFont[0], "Score:" + Score.ToString(), new Vector2(SpaceShooter.Width - 200, 5), Color.AliceBlue);
                spriteBatch.End();

                spriteDrawer.Render(gameTime);
            }
        }

        protected void Collision()
        {
            Rectangle small_player = new Rectangle(player.MapPosition.X + 20, player.MapPosition.Y + 20, player.MapPosition.Width - 40, player.MapPosition.Height - 40);

            foreach (Sprite sprite in spriteDrawer.Sprites)
            {
                if (sprite is Player || sprite is Shoot || sprite.Active == false) continue;

                foreach (Sprite e in spriteDrawer.Sprites)
                {
                    if (e is Player && e.Active)
                        if (sprite.MapPosition.Intersects(small_player))
                        {
                            sprite.Active = false;
                            if (player.Shield == false)
                            {
                                player.Lives--;
                                player.Shield = true;
                            }
                            continue;

                        }
                    if (sprite is Rock) continue;
                    if (e is Shoot && e.Active)
                        if (sprite.MapPosition.Intersects(e.MapPosition))
                        {
                            e.Active = false;
                            sprite.Active = false;
                            Score += 5;
                            continue;

                        }
                }

            }
        }
    
    }
}
