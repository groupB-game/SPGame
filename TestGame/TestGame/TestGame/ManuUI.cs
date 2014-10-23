using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    /// <summary>
    /// Create memu button and set button event.
    /// </summary>
    class ManuUI
    {
        private Texture2D UITexture;

        private Rectangle UIRect;

        private string ButtonName;

        /// <summary>
        /// Get the click button name.
        /// </summary>
        public string ButtonName1
        {
            get { return ButtonName; }
            set { ButtonName = value; }
        }

        /// <summary>
        /// Set event.
        /// </summary>
        /// <param name="element"></param>
        public delegate void ElementClicked(string element);

        public event ElementClicked clickEvent;


        public ManuUI(string name)
        {
            this.ButtonName = name;
        }

        public void LoadContent(ContentManager content)
        {
            //Load button.
            UITexture = content.Load<Texture2D>(ButtonName);
            UIRect = new Rectangle(0, 0, UITexture.Width, UITexture.Height);
        }

        public void Update()
        {
            //Check button pressed event.
            if (UIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                clickEvent(ButtonName);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(UITexture, UIRect, Color.White);
        }

        /// <summary>
        /// Center element on screen.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public void SetCenter(int height, int width)
        {
            UIRect = new Rectangle((width / 2) - (this.UITexture.Width / 2), (height / 2) - (this.UITexture.Height / 2), UITexture.Width, UITexture.Height);
        }

        /// <summary>
        /// Change button possition.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ChangePosition(int x, int y)
        {
            UIRect = new Rectangle(UIRect.X += x, UIRect.Y + y, UIRect.Width, UIRect.Height);
        }
    }
}
