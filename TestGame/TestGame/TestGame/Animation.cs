using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    /// <summary>
    /// Class to hold the variables and methods for the Animated sprites.
    /// </summary>
    class Animation
    {
        //Public variable to hold the texture of the animated sprite.
        public Texture2D texture;
        //Public variable to hold the rectangle details of the animated sprite.
        public Rectangle rectangle;
        //Public variable holds the position of the animated sprite.
        public Vector2 position;
        //Public variable holds the original position of the animated sprite.
        Vector2 origin;
        //Public variable holds the velocity of the animated sprite.
        public Vector2 velocity;
        //Public variable holds which frame is currently showing
        public int currentFrame;
        int frameHeight;
        int frameWidth;

        float timer;
        float interval=50;
        Boolean hasJumped;

        /// <summary>
        ///  Public variable that only allows the retreval of the data for the bounding box details for use in collision detection.
        /// </summary>
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
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

            
             
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped ==false )
            {
               
                position.Y -= 60f;
                velocity.Y = -15f;
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
                
                hasJumped =false;
            }
            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }
            //if (position.Y <= 650)
            //{
            //    currentFrame = 2;
            //}
            if (velocity.Y >0)
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
                if (currentFrame > 7 || currentFrame <4)
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
            spriteBatch.Draw(texture,position , rectangle, Color.White,0f,origin,1.0f,SpriteEffects.None ,0);
        }
    }
    
}
