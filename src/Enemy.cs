using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space
{
    public class Enemy : Sprite
    {

        protected Random randomGen = new Random();
        protected int movement;
        protected float speed = 5.0f;
        protected Vector2 startingPosition;
     

        public Enemy(Texture2D texture, Vector2 startpos)
          : base(texture)
        {
            position = startpos;
            startingPosition = startpos;
            movement = randomGen.Next(10);
        }
        public void speedUp(float speed)
        {
            this.speed += speed;

        }
        public override void Update(GameTime time)
        {
            if (0 <= movement && movement < 3) this.AddToPosition(new Vector2(0, speed));
            if (3 <= movement && movement < 6) this.AddToPosition(new Vector2(speed / 3, speed));
            if (6 <= movement && movement < 10) this.AddToPosition(new Vector2(-speed / 3, speed));

            if (this.position.Y > 1000)
            {
                active = true;
                position = startingPosition;
                speedUp(0.4f);
                movement = randomGen.Next(10);
            }

            base.Update(time);
        } 
    }

}

