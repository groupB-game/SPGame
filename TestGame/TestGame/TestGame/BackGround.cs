using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TestGame
{
    /// <summary>
    /// Create background.
    /// </summary>
    class BackGround
    {
        public Texture2D  texture;
        public Rectangle rectangle;

        public BackGround(Texture2D newTexture, Rectangle newRectangle)
        {
            this.texture = newTexture;
            this.rectangle = newRectangle;
        }

        /// <summary>
        /// Set background speed.
        /// </summary>
        public void Update()
        {
            rectangle.X -= 2;
        }

        /// <summary>
        /// Draw background.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
    }

