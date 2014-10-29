using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    /// <summary>
    /// Create hurdles.
    /// </summary>
    class Hurdles
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public List<string> hurdlepickup;

        /// <summary>
        /// Get the current status of the hurdle possition.
        /// </summary>
        public Rectangle PositionRectangle
        {
            get
            {
                return (new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height));
            }
        }

        /// <summary>
        /// Initialise variables.
        /// </summary>
        /// <param name="newTexture"></param>
        /// <param name="newRectangle"></param>
        public Hurdles(Texture2D newTexture, Rectangle newRectangle)
        {
            this.texture = newTexture;
            this.rectangle = newRectangle;
        }

        /// <summary>
        /// Set hurdles speed.
        /// </summary>
        public void Update()
        {
            rectangle.X -= 10;
        }

        /// <summary>
        /// Draw hurdles.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}

