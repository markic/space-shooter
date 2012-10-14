using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space
{
    public class Sprite
    {

        protected Vector2 position = Vector2.Zero;
        protected Texture2D texture;
        protected Rectangle mapPosition;
        protected float rotation = 0.0f;
        protected Color color = Color.White;
        protected SpriteEffects spriteEffects = SpriteEffects.None;
        protected float depth = 1.0f;
        protected float scale = 1.0f;
        protected bool active = true;

  
        public Sprite(Texture2D texture)
        {     
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime time)
        {
            if(active && (this is Player || this is Enemy || this is Rock))
                spriteBatch.Draw(texture, position, new Rectangle(0,0,texture.Width,texture.Height), color, rotation, Vector2.Zero, scale, spriteEffects, depth);
            if(active && this is Shoot)
                spriteBatch.Draw(texture, position, new Rectangle(0, 0,texture.Width,texture.Height), color, rotation, Vector2.Zero, scale, spriteEffects, depth);

        }
        public virtual void Update(GameTime time)
        {
            mapPosition = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
        public Rectangle MapPosition
        {
            get { return mapPosition; }
        }
        
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public void AddToPosition(Vector2 diff)
        {
            position += diff;
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
    }

    public class SpriteDrawer
    {
        protected List<Sprite> sprites = new List<Sprite>();
        protected SpriteBatch spriteBatch;

        public SpriteDrawer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        public void Render(GameTime time)
        {
            spriteBatch.Begin();
            foreach (Sprite sprite in this.sprites)
                sprite.Draw(spriteBatch, time);
            spriteBatch.End();
        }
        public void Update(GameTime time)
        {
            foreach (Sprite sprite in this.sprites)
                sprite.Update(time);
        }
        
        public void RemoveSprite(Sprite sprite)
        {
            sprites.Remove(sprite);
        }
        
        public void RemoveAllInactive()
        {
            List<Sprite> temp = new List<Sprite>();
            foreach (Sprite sprite in this.sprites)
            {
                if (sprite is Shoot && sprite.Active == false) continue;
                temp.Add(sprite);
            }
            sprites = temp;
        }
        
        public void RemoveAll()
        {
            sprites.Clear();
        }
        
        public List<Sprite> Sprites
        {
            get { return sprites; }
        }
    
    }
}
