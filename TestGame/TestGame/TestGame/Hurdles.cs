using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    class Hurdles
    {
        public Texture2D  texture;
        public Rectangle rectangle;
        public Vector2 position;
        public List<Hurdles> hurdlepickuplist;

        public Hurdles(Texture2D newTexture)
        {
            this.texture = newTexture;
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X,(int)position.Y, texture.Width,texture.Height);}
        }

        public void Update()
        {
            rectangle.X -= 10;
        }

        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        
    }
}

