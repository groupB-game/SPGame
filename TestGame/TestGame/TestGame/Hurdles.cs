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
        public List<Hurdles> hurdlepickuplist;

        public Hurdles(Texture2D newTexture, Rectangle newRectangle)
        {
            this.texture = newTexture;
            this.rectangle = newRectangle;
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

