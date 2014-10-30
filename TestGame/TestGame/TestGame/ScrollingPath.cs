using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    /// <summary>
    /// Scroll background.
    /// </summary>
    class ScrollingPath
    {
        public Texture2D  texture;
        public Rectangle rectangle;

        /// <summary>
        /// Initialise variable.
        /// </summary>
        /// <param name="newTexture"></param>
        /// <param name="newRectangle"></param>
        public ScrollingPath(Texture2D newTexture, Rectangle newRectangle)
        {
            this.texture = newTexture;
            this.rectangle = newRectangle;
        }

        /// <summary>
        /// Set background speed.
        /// </summary>
        public void Update()
        {
            rectangle.X -= 10;
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
