using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    /// <summary>
    /// Class to hold the variables and methods for the Hurdles.
    /// </summary>
    class Hurdles
    {
        //Public variable to hold the texture of the hurdle.
        public Texture2D  texture;
        //Public variable to hold the rectangle details of the hurdle.
        public Rectangle rectangle;
        //Public variable holds the position of the hurdle.
        public Vector2 position;
        //Public variable to hold a list of different hurdle textures.
        public List<Hurdles> hurdlepickuplist;

        /// <summary>
        /// Method the replaces one hurdle texture with a new hurdle texture.
        /// </summary>
        /// <param name="newTexture">New texture to replace the old texture</param>
        public Hurdles(Texture2D newTexture)
        {
            this.texture = newTexture;
        }

        /// <summary>
        /// Public variable that only allows the retreval of the data for the bounding box details for use in collision detection.
        /// </summary>
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X,(int)position.Y, texture.Width,texture.Height);}
        }

        /// <summary>
        /// Update method updates the x-axis location of the hurdle.
        /// </summary>
        public void Update()
        {
            rectangle.X -= 10;
        }

        /// <summary>
        /// Draw method draws the hurdle on the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        
    }
}

