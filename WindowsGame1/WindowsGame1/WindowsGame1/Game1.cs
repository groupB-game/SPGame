using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Collision detection
        Texture2D hurdle1Texture;
        Texture2D runnerTexture;
        Rectangle hurdleBox;
        Rectangle runnerBox;
        Vector2 runnerPosition;
        Vector2 boxPosition;
        Vector2 boxVelocity;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            boxPosition = new Vector2(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height * 0.25f);
            runnerPosition = new Vector2(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height * 0.75f);
            boxVelocity = new Vector2(0, 1);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hurdle1Texture = Content.Load<Texture2D>("(b)bad_tutor_pickup");
            runnerTexture = Content.Load<Texture2D>("(g)a+_pickup");

            hurdleBox = new Rectangle((int)(boxPosition.X - hurdle1Texture.Width / 2), (int)(boxPosition.Y - hurdle1Texture.Height / 2), hurdle1Texture.Width, hurdle1Texture.Height);
            runnerBox = new Rectangle((int)(boxPosition.X - hurdle1Texture.Width / 2), (int)(boxPosition.Y - hurdle1Texture.Height / 2), hurdle1Texture.Width, hurdle1Texture.Height);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Collisions
            if (hurdleBox.Intersects(runnerBox) || !GraphicsDevice.Viewport.Bounds.Contains(hurdleBox))
            {
                boxVelocity = -boxVelocity;
                boxPosition += boxVelocity;
            }
            else
            {
                boxPosition += boxVelocity;
            }

            //Updates bounding boxes
            hurdleBox.X = (int)boxPosition.X;
            hurdleBox.Y = (int)boxPosition.Y;

            runnerBox.X = (int)runnerPosition.X;
            runnerBox.Y = (int)runnerPosition.Y;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(hurdle1Texture, boxPosition, Color.White);
            spriteBatch.Draw(runnerTexture, runnerPosition, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
