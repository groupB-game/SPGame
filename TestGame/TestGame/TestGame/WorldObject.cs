using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    struct WorldObject
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    texture.Width,
                    texture.Height);
            }
        }       

        public WorldObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
        }
        public WorldObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            velocity = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
