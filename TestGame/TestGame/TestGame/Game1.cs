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

        int count = 0;

        //Scrolling Path
        private ScrollingPath scrolling1,scrolling2;
        private BackGround backGround1, backGround2, backGround3;

        //Screen parameters
        int screenWidth;
        int screeHeight;

        Vector2 velocity;

        Rectangle ballbox;
        Rectangle runnerBox,backGroundBox;
        Random randomNumber;

        //Hurdles
        private Hurdles hurdle1, hurdle2, hurdle3;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

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
            runner = new Animation(Content.Load<Texture2D>("Runner3"), new Vector2(640, 880), 288, 288);

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

            //Random number generator
            randomNumber = new Random();
            int newrandom = randomNumber.Next(2010,3000);


            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("boll");

            ballbox = new Rectangle(10, 10, 30, 30);

            //Scrolling path
            scrolling1 = new ScrollingPath(Content.Load<Texture2D>("path"), new Rectangle(0,930,1920,150));
            scrolling2 = new ScrollingPath(Content.Load<Texture2D>("path2"), new Rectangle(1920, 930, 1920, 150));
            backGround1 = new BackGround(Content.Load<Texture2D>("LabBGNew"), new Rectangle(0, 0, 2000, 1080));
            backGround2 = new BackGround(Content.Load<Texture2D>("LabBGNew"), new Rectangle(1920, 0, 2000, 1080));
            backGround3 = new BackGround(Content.Load<Texture2D>("GraduationBG"), new Rectangle(3840, 0, 2000, 1080));

           
            //Stady background
            //backGround = Content.Load<Texture2D>("LabBGNew");
            //backGroundBox = new Rectangle(0, 0, 2000, 1080);
            
            

            velocity.X = 3f;
            velocity.Y = 3f;

            screenWidth = GraphicsDevice.Viewport.Width;
            screeHeight = GraphicsDevice.Viewport.Height;

            //Hurdles load

            hurdle1 = new Hurdles(Content.Load<Texture2D>("(g)a+_pickup"), new Rectangle(2010, 790, 150, 150));
            hurdle2 = new Hurdles(Content.Load<Texture2D>("(b)f_pickup"), new Rectangle(3800, 650, 150, 150));
            hurdle3 = new Hurdles(Content.Load<Texture2D>("Grad Hat Icon"), new Rectangle(11000, 650, 150, 150));



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
            {
                this.Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //runner 
            runner.Update(gameTime);
            count++;
            if(count == 500)
            {
                runner = new Animation(Content.Load<Texture2D>("RunnerWHat"), new Vector2(640, 865), 330, 288);
                runner.currentFrame = 2;
            }

            //Scrolling path

            if (scrolling1.rectangle.X + 1920 <= 0)
            {
                scrolling1.rectangle.X = scrolling2.rectangle.X + 1920;
            }
            if (scrolling2.rectangle.X + 1920 <= 0)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + 1920;
            }

            //Scrolling Background

            if (backGround1.rectangle.X + 2000 <= 0)
            {
                backGround1.rectangle.X = backGround2.rectangle.X + 2000;
            }
            if (backGround2.rectangle.X + 2000 <= 0)
            {
                backGround2.rectangle.X = backGround3.rectangle.X + 2000;
            }
            if (backGround3.rectangle.X + 2000 <= 0)
            {
                backGround3.rectangle.X = backGround1.rectangle.X + 2000;
            }

            //Hurdle looping
            if (hurdle1.rectangle.X + 2000 <= 0)
            {
                hurdle1.rectangle.X = hurdle2.rectangle.X + 2000;
            }
            if (hurdle2.rectangle.X + 2000 <= 0)
            {
                hurdle2.rectangle.X = hurdle1.rectangle.X + 2000;
            }


            scrolling1.Update();
            scrolling2.Update();
            backGround1.Update();
            backGround2.Update();
            backGround3.Update();
            hurdle1.Update();
            hurdle2.Update();
            hurdle3.Update(); ;
           
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
           //spriteBatch.Draw(backGround ,backGroundBox ,Color.White );
            backGround1.Drow(spriteBatch);
            backGround2.Drow(spriteBatch);
            backGround3.Drow(spriteBatch);
            scrolling1.Drow(spriteBatch);
            scrolling2.Drow(spriteBatch);
            runner.Draw(spriteBatch);
            hurdle1.Drow(spriteBatch);
            hurdle2.Drow(spriteBatch);
            hurdle3.Drow(spriteBatch);
            spriteBatch.End();

            

            base.Draw(gameTime);
        }

        public void Collition()
        {
            if (runner.rectangle.Intersects(hurdle1.rectangle))
            {
              
            }
        }

        private void checkBoundries()
        {
            if (runner.position.X >= hurdle1.rectangle.X)
                hurdle1.rectangle.Offset(5, 8);

            else if (runner.position.Y >= hurdle1.rectangle.Y)
                hurdle1.rectangle.Offset(5, 8);
            hurdle1.Update();
        }
    }
}
