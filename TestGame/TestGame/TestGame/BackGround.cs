using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TestGame
{
    class BackGround
    {
        public Texture2D  texture;
        public Rectangle rectangle;

        public BackGround(Texture2D newTexture, Rectangle newRectangle)
        {
            this.texture = newTexture;
            this.rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.X -= 2;
        }

        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
    }

