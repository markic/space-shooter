using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space
{
    public class Rock : Sprite
    {

        protected Random randomGen = new Random();
        protected const float PI = 3.14f;
        protected Vector2 speed = new Vector2(5);



        public Rock(Texture2D texture)
            : base(texture)
        {
            setNew();
        }

        public override void Update(GameTime time)
        {
            if (this.position.Y > 1000 || this.position.X < -15)
                setNew();


            this.AddToPosition(speed);
            
            base.Update(time);
        }
        public void setNew()
        {
            this.active = true;
            position = new Vector2(randomGen.Next(50, SpaceShooter.Width - 50), -50);
            
            if (randomGen.Next() % 2 == 0) speed.X = randomGen.Next(3, 5);
            else speed.X = -randomGen.Next(3, 5);      
            speed.Y = randomGen.Next(6, 10);
            
            //Math
            Vector2 ort = speed;
            ort.Normalize();
            if(speed.X < 0)
                rotation = PI - (float)Math.Atan((double)ort.Y/ort.X);
            else
                rotation = PI + (float)Math.Atan((double)ort.Y / -ort.X);

            
        }



    }
}
