using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    class Animation
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 position;
        Vector2 origin;
        public Vector2 velocity;

        public int currentFrame;
        int frameHeight;
        int frameWidth;

        float timer;
        float interval = 50;
        Boolean hasJumped;

        public Rectangle PositionRectangle
        {
            get
            {
                return (new Rectangle((int)position.X, (int)position.Y, 60, 100));
            }
        }


        public Animation(Texture2D newtexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth)
        {
            this.texture = newtexture;
            this.position = newPosition;
            this.frameHeight = newFrameHeight;
            this.frameWidth = newFrameWidth;
            hasJumped = true;
        }

        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position = position + velocity;


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {

                position.Y -= 60f;
                velocity.Y = -20f;
                hasJumped = true;

            }
            else
            {
                AnimateReight(gameTime);
                //velocity.X = 0.2f; moved character forward, not needed for now
            }


            if (hasJumped == true)
            {
                float i = 0.75f;
                velocity.Y += 0.45f * i;
            }
            if (position.Y + texture.Height >= 1175)
            {

                hasJumped = false;
            }
            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }

            if (velocity.Y > 0)
            {
                currentFrame = 2;
            }
            if (velocity.Y < 0)
            {
                currentFrame = 1;
            }



        }




        //Collision



        public void AnimateReight(GameTime gameTiem)
        {
            timer += (float)gameTiem.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 5)
                {
                    currentFrame = 0;
                }
            }
        }

        public void AnimateLeft(GameTime gameTiem)
        {
            timer += (float)gameTiem.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 7 || currentFrame < 4)
                {
                    currentFrame = 4;

                }
            }
        }

        public void AnimateJumpingUp(GameTime gameTime)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                
                currentFrame++;
                timer = 0;
                if (currentFrame > 1)
                {
                    currentFrame = 1;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }
    }

}


