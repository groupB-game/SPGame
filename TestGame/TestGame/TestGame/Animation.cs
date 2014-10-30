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
  /// Control main sprite.
  /// </summary>
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

        /// <summary>
        /// Get the current possition of the sprite.
        /// </summary>
        public Rectangle PositionRectangle
        {
            get
            {
                return (new Rectangle((int)position.X, (int)position.Y, 60, 100));
            }
        }

        /// <summary>
        /// Initialse sprite variabel.
        /// </summary>
        /// <param name="newtexture"></param>
        /// <param name="newPosition"></param>
        /// <param name="newFrameHeight"></param>
        /// <param name="newFrameWidth"></param>
        public Animation(Texture2D newtexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth)
        {
            this.texture = newtexture;
            this.position = newPosition;
            this.frameHeight = newFrameHeight;
            this.frameWidth = newFrameWidth;
            hasJumped = true;
        }

        /// <summary>
        /// Update sprite animation speed, change jumping fram.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position = position + velocity;

            //Set space key to jump.
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 60f;
                velocity.Y = -20f;
                hasJumped = true;
            }
            else
            {
                AnimateReight(gameTime);
            }

            //Set the gravity.
            if (hasJumped == true)
            {
                float i = 0.75f;
                velocity.Y += 0.45f * i;
            }

            //Detect flow level. 
            if (position.Y + texture.Height >= 1175)
            {

                hasJumped = false;
            }

            //Stop moving down.
            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }

            //Set the action when sprite goes up.
            if (velocity.Y > 0)
            {
                currentFrame = 2;
            }
            if (velocity.Y < 0)
            {
                currentFrame = 1;
            }
        }

        /// <summary>
        /// Set sprite animation. (change the sprite's frame in defferent time interval.) 
        /// </summary>
        /// <param name="gameTiem"></param>
        public void AnimateReight(GameTime gameTiem)
        {
            //Get game current time.
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

        /// <summary>
        /// Set sprite to move left.
        /// </summary>
        /// <param name="gameTiem"></param>
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

        /// <summary>
        /// Set sprite to jump.
        /// </summary>
        /// <param name="gameTime"></param>
        public void AnimateJumpingUp(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;

                //Set the frame to jumping action.
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


