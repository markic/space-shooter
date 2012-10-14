using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space
{
   public class Shoot : Sprite
    {
        protected float speed = 10.0f;
        public enum Type
        { DEF, L, R, LFLANK, RFLANK }
        protected Type type;

        public Shoot(Texture2D texture, Vector2 pos, Type type = Type.DEF)
            : base(texture)
        {
            position = pos;
            this.type = type;
        }
        public override void Update(GameTime time)
        {
            if (type == Type.DEF)
                this.AddToPosition(new Vector2(0, -speed));
            else if (type == Type.L)
            {
                this.AddToPosition(new Vector2(-speed, -speed));
                this.rotation = -0.7f;
            }
            else if (type == Type.R)
            {
                this.AddToPosition(new Vector2(speed, -speed));
                this.rotation = 0.7f;
            }
            else if (type == Type.LFLANK)
            {
                this.AddToPosition(new Vector2(-speed / 2, -speed));
                this.rotation = -0.5f;
            }
            else
            {
                this.AddToPosition(new Vector2(speed / 2, -speed));
                this.rotation = 0.5f;
            }


            if (this.position.Y < -20)
            {
                active = false;
            }

            base.Update(time);
        }
    }
}
