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
        public Vector3 position;
        public Vector3 velocity;
        public Model model;
        public Texture2D texture2D;
        public Vector3 lastPosition;

        public void MoveForward()
        {
            lastPosition = position;
            position += velocity;
        }

        public void Backup()
        {
            position -= velocity;
        }

        public void ReverseVelocity()
        {
            velocity.X = -velocity.X;
        }
    }
}
