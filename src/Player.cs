using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Space
{
    public class Player : Sprite
    {

        protected int lives;
        protected bool shield = false;
        protected Rectangle movingArea = new Rectangle(5,5,SpaceShooter.Width - 128 - 5,SpaceShooter.Height - 128 -5);
        protected float movingSpeed = 16.0f;
        double elapsedTime = 0;

        public Player(Texture2D texture):base(texture)
        {
            position = new Vector2(movingArea.Width/2,movingArea.Height * 9/10);
            lives = 3;
        }
        public void goLeft()
        {
            if (position.X - movingSpeed > movingArea.X)
                position.X -= movingSpeed;
            else position.X = movingArea.X;
        }
        public void goRight()
        {
            if (position.X + movingSpeed < movingArea.Width)
                position.X += movingSpeed;
            else position.X = movingArea.Width;
        }
        public void goUp()
        {
            if (position.Y - movingSpeed > movingArea.Y)
                position.Y -= movingSpeed;
            else position.Y = movingArea.Y;
        }
        public void goDown()
        {
            if (position.Y + movingSpeed < movingArea.Height)
                position.Y += movingSpeed;
            else position.Y = movingArea.Height;
        }

        public int Lives
        {
            get {return lives; }
            set {lives = value;}
        }
        public bool Shield
        {
            get { return shield; }
            set { shield = value; }
        }
        public override void Update(GameTime time)
        {

            elapsedTime += time.ElapsedGameTime.TotalSeconds;
            if (elapsedTime > 5.0)
            {
                shield = false;
                elapsedTime = 0;
            }
            if (lives < 0)
            {
                active = false;
            }
            base.Update(time);
        }
    
    }
}
