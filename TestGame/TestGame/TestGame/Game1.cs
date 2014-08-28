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

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ball,backGround;
        Animation runner;

        //Scrolling Path
        private ScrollingPath scrolling1,scrolling2;

        //Screen parameters
        int screenWidth;
        int screeHeight;

        Vector2 velocity;

        Rectangle ballbox;
        Rectangle runnerBox,backGroundBox;

        //Animater object

        

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
            //Runner
            runner = new Animation(Content.Load<Texture2D>("Runner3"), new Vector2(200, 720), 288, 294);
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

            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("boll");

            ballbox = new Rectangle(10, 10, 30, 30);

            //Scrolling path
            scrolling1 = new ScrollingPath(Content.Load<Texture2D>("path"), new Rectangle(0,850,1920,150));
            scrolling2 = new ScrollingPath(Content.Load<Texture2D>("path2"), new Rectangle(1920, 850, 1920, 150));
            backGround = Content.Load<Texture2D>("fred");
            backGroundBox = new Rectangle(0, 0, 2000, 1080);
            
            

            velocity.X = 3f;
            velocity.Y = 3f;

            screenWidth = GraphicsDevice.Viewport.Width;
            screeHeight = GraphicsDevice.Viewport.Height;


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

            //graphics.IsFullScreen 1920 1080;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1900;
            graphics.ApplyChanges();
            // TODO: Add your update logic here

            ballbox.X = ballbox.X + (int)velocity.X;
            ballbox.Y = ballbox.Y + (int)velocity.Y;

            ballbox.X = ballbox.X + (int)velocity.X;
            ballbox.Y = ballbox.Y + (int)velocity.Y;

            if (ballbox.X <= 0)
            {
                velocity.X = -velocity.X;
            }

            if (ballbox.X + ballbox.Width >= screenWidth)
            {
                velocity.X = -velocity.X;
            }

            if (ballbox.Y <= 0)
            {
                velocity.Y = -velocity.Y;
            }

            if (ballbox.Y + ballbox.Height >= screeHeight)
            {
                velocity.Y = -velocity.Y;
                //velocity.X = -velocity.X;
            }

            

            //runner 
            runner.Update(gameTime);

            //Scrolling path
            

            if (scrolling1.rectangle.X + 1920 <= 0)
            {
                scrolling1.rectangle.X = scrolling2.rectangle.X + 1920;
            }
            if (scrolling2.rectangle.X + 1920 <= 0)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + 1920;
            }

            scrolling1.Update();
            scrolling2.Update();
           
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

             //spriteBatch.Draw(ball, ballbox, Color.White);
            spriteBatch.Draw(backGround ,backGroundBox ,Color.White );
            scrolling1.Drow(spriteBatch);
            scrolling2.Drow(spriteBatch);
            runner.Draw(spriteBatch);
            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
