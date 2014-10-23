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
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum GameState
        {
            StartMenu,
            Loading,
            Playing,
            Paused
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        int count = 0;
        //MassageBox



        //Score
        private int score = 0;
        private SpriteFont font;
        float timer;
        int interval = 100;
        Texture2D startButton;
        Texture2D exitButton;
        Texture2D resumeButton;
        Vector2 startButtonPosition;
        Vector2 exitButtonPosition;
        Vector2 resumeButtonPosition;
        Texture2D loadingScreen;
        Boolean isLoading = false;
        MouseState mouseState;
        MouseState previousMouseState;
        Texture2D pushButton;


        //Game state initialize
        private GameState gameState;
        private Thread backgroundThread;

        Texture2D ball, backGround;
        Animation runner;

        //Scrolling Path
        private ScrollingPath scrolling1, scrolling2;
        private BackGround backGround1, backGround2, backGround3, backGround4, backGround5;

        //Screen parameters
        int screenWidth;
        int screeHeight;

        Vector2 velocity;
        Rectangle backGroundBox;

        //Collision detection
        //List<Hurdles> hurdleList;


        Random randomNumber;

        //Hurdles
        private Hurdles hurdle1, hurdle2;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
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
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            //Runner
            runner = new Animation(Content.Load<Texture2D>("Runner3"), new Vector2(640, 880), 288, 294);

            startButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);
            exitButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 250);

            mouseState = Mouse.GetState();
            previousMouseState = mouseState;
            

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



            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("boll");

                //Load font 
            font = Content.Load<SpriteFont>("ScoreFont/Score");
            //Scrolling path
            scrolling1 = new ScrollingPath(Content.Load<Texture2D>("path"), new Rectangle(0, 930, 1920, 150));
            scrolling2 = new ScrollingPath(Content.Load<Texture2D>("path2"), new Rectangle(1920, 930, 1920, 150));
            backGround1 = new BackGround(Content.Load<Texture2D>("LabBGNew"), new Rectangle(0, 0, 2000, 1080));
            backGround2 = new BackGround(Content.Load<Texture2D>("LabBGNew"), new Rectangle(2000, 0, 2000, 1080));
            backGround3 = new BackGround(Content.Load<Texture2D>("GraduationBG"), new Rectangle(4000, 0, 2000, 1080));
            backGround4 = new BackGround(Content.Load<Texture2D>("WorkBG2"), new Rectangle(6000, 0, 2000, 1080));
            backGround5 = new BackGround(Content.Load<Texture2D>("WorkBG2"), new Rectangle(8000, 0, 2000, 1080));

            //Stady background
            //backGround = Content.Load<Texture2D>("LabBGNew");
            //backGroundBox = new Rectangle(0, 0, 2000, 1080);



            velocity.X = 3f;
            velocity.Y = 3f;

            screenWidth = GraphicsDevice.Viewport.Width;
            screeHeight = GraphicsDevice.Viewport.Height;

            //Hurdles load
            string[] hurdlepickup = new string[]
            {
                "Pickups/(b)bad_tutor_pickup",
                "Pickups/(b)f_pickup",
                "Pickups/(b)flash_drive_pickup",
                "Pickups/(b)flu_pickup",
                "Pickups/(b)forgot_due_date_pickup",
                "Pickups/(b)goals_missed_pickup",
                "Pickups/(b)missed_alarm_pickup",
                "Pickups/(b)moodle_down_pickup",
                "Pickups/(b)not_enough_sleep_pickup",
                "Pickups/(b)repeat_paper_pickup",
                "Pickups/(g)a+_pickup",
                "Pickups/(g)goals_met_pickup",
                "Pickups/(g)good_health_pickup",
                "Pickups/(g)good_sleep_pickup",
                "Pickups/(g)good_tutor_pickup",
                "Pickups/(g)new_skills_pickup",
                "Pickups/(g)notes_taken_pickup",
                "Pickups/(g)on_time_pickup",
                "Pickups/(g)passed_paper_pickup",
                "Pickups/(g)study_time_pickup"
            };

            for (int i = 0; i < 20; i++)
            {
                //int randomNumber = new Random.Next(0,hurdlepickup.Last);
                //int newrandom = randomNumber.Next(0, 19);

                Random randomNumber = new Random();
                String item1 = hurdlepickup[randomNumber.Next(hurdlepickup.Length)];
                String item2 = hurdlepickup[randomNumber.Next(hurdlepickup.Length)];

                //while (hurdlepickup.Count>0)
                //{
                hurdle1 = new Hurdles(Content.Load<Texture2D>(item1));
                hurdle2 = new Hurdles(Content.Load<Texture2D>(item2));

                //hurdle3 = new Hurdles(Content.Load<Texture2D>("Grad Hat Icon"), new Rectangle(11000, 650, 150, 150));
                //}
            }

            //hurdle1 = new Hurdles(Content.Load<Texture2D>("(g)A+ pickup"), new Rectangle(2010, 790, 150, 150));
            //hurdle2 = new Hurdles(Content.Load<Texture2D>("(g)A+ pickup"), new Rectangle(3800, 650, 150, 150));

            //Start Manu item Loading
            IsMouseVisible = true;
            startButton = Content.Load<Texture2D>("StartScreen/start");
            exitButton = Content.Load<Texture2D>("StartScreen/exit");
            resumeButton = Content.Load<Texture2D>("StartScreen/resume");
            loadingScreen = Content.Load<Texture2D>("StartScreen/loading");
            pushButton = Content.Load<Texture2D>("StartScreen/pause");
            resumeButton = Content.Load<Texture2D>("StartScreen/resume");



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
                int Score = score;
                if (Int32.Parse(ReadHighScore()) < score)
                {
                    SaveHighScore(score);
                }
                this.Exit();
            }

            //Score update 

            //score++;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                int Score = score;
                if (Int32.Parse(ReadHighScore()) < score)
                {
                    SaveHighScore(score);
                }
                this.Exit();

            }

            //load the game when needed
            if (gameState == GameState.Loading && !isLoading) //isLoading bool is to prevent the LoadGame method from being called 60 times a seconds
            {
                //set backgroundthread

                isLoading = true;

                //start backgroundthread

            }

            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {
                MouseClick(mouseState.X, mouseState.Y);
            }

            previousMouseState = mouseState;

            if (gameState == GameState.Playing)
            {

                //runner 
                runner.Update(gameTime);
                count++;
                if (count == 500)
                {
                    int cframe = runner.currentFrame;
                    float ypos = runner.position.Y;
                    float xpos = runner.position.X;

                    runner = new Animation(Content.Load<Texture2D>("RunnerWHat"), new Vector2(xpos, ypos), 330, 288);
                    runner.currentFrame = cframe;
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
                    backGround3.rectangle.X = backGround4.rectangle.X + 2000;
                }
                if (backGround4.rectangle.X + 2000 <= 0)
                {
                    backGround4.rectangle.X = backGround5.rectangle.X + 2000;
                }
                if (backGround5.rectangle.X + 2000 <= 0)
                {
                    backGround5.rectangle.X = backGround4.rectangle.X + 2000;
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

                //Collisions
                //HandleCollisions();



                scrolling1.Update();
                scrolling2.Update();
                backGround1.Update();
                backGround2.Update();
                backGround3.Update();
                backGround4.Update();
                backGround5.Update();
                hurdle1.Update();
                hurdle2.Update();
                ScoreUpadate(gameTime);

                isLoading = false;
            }






            base.Update(gameTime);



        }

        public void ScoreUpadate(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                timer = 0;
                score++;
            }

            
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
            String checkScore = ReadHighScore();

            if (gameState == GameState.StartMenu)
            {
                string saveScore = ReadHighScore();
                spriteBatch.DrawString(font, "High Score " + saveScore, new Vector2(100, 10), Color.White);
                spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
            }
            if (gameState == GameState.Loading)
            {
                spriteBatch.Draw(loadingScreen, new Vector2((GraphicsDevice.Viewport.Width / 2) - (loadingScreen.Width / 2), (GraphicsDevice.Viewport.Height / 2) - (loadingScreen.Height / 2)), Color.YellowGreen);
            }
            if (gameState == GameState.Playing)
            {
                               
                backGround1.Drow(spriteBatch);
                backGround2.Drow(spriteBatch);
                backGround3.Drow(spriteBatch);
                backGround4.Drow(spriteBatch);
                backGround5.Drow(spriteBatch);
                scrolling1.Drow(spriteBatch);
                scrolling2.Drow(spriteBatch);
                spriteBatch.Draw(pushButton, new Vector2(0, 0), Color.White);
                runner.Draw(spriteBatch);
                hurdle1.Drow(spriteBatch);
                hurdle2.Drow(spriteBatch);
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(1700, 10), Color.White);
                
                if (score > Int32.Parse(checkScore))
                {
                    spriteBatch.DrawString(font, "High Score ", new Vector2(100, 10), Color.White);
                }
            }
            if (gameState == GameState.Paused)
            {
                spriteBatch.DrawString(font, "Current Score  " + score, new Vector2(100, 10), Color.White);
                spriteBatch.Draw(resumeButton, resumeButtonPosition, Color.White);
                //Save Score
               
                
            }


            
            spriteBatch.End();



            base.Draw(gameTime);
        }


        public void MouseClick(int X, int Y)
        {
            Rectangle mouseClickRect = new Rectangle(X, Y, 10, 10);
            //check the startmenu
            if (gameState == GameState.StartMenu)
            {
                Rectangle startButtonRect = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y, 100, 20);
                Rectangle exitButtonRect = new Rectangle((int)exitButtonPosition.X, (int)exitButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(startButtonRect)) //player clicked start button
                {
                    gameState = GameState.Playing;
                    isLoading = false;
                }
                else if (mouseClickRect.Intersects(exitButtonRect)) //player clicked exit button
                {
                    Exit();
                }
            }

            //check the pausebutton
            if (gameState == GameState.Playing)
            {
                Rectangle pauseButtonRect = new Rectangle(0, 0, 70, 70);

                if (mouseClickRect.Intersects(pauseButtonRect))
                {
                    gameState = GameState.Paused;
                }

                isLoading = false;
            }

            if (gameState == GameState.Paused)
            {
                Rectangle resumeButtonRect = new Rectangle((int)resumeButtonPosition.X, (int)resumeButtonPosition.Y, 100, 20);

                if (mouseClickRect.Intersects(resumeButtonRect))
                {
                    gameState = GameState.Playing;
                }
            }
        }

        public void SaveHighScore(int score)
        {
            try
            {
                TextWriter writer = new StreamWriter("score.txt");
                writer.WriteLine(score);
                writer.Close();


            }
            catch (Exception e)
            {

            }
        }

        public string ReadHighScore()
        {
            string read = "";
            try
            {
                TextReader readHig = new StreamReader("score.txt");
                read = readHig.ReadLine();
                readHig.Close();

                return read;

            }
            catch (Exception e)
            {

            }
            return read;
        }


        public void LoadGame()
        {

        }

       // public void HandleCollisions()
       // {
       //     Hurdles toRemove = null;
//
         //   foreach(Hurdles hurdle in hurdleList)
        //    {
         //       if(runner.BoundingBox.Intersects(hurdle.BoundingBox))
         //       {
         //           toRemove = hurdle;
        //            break;
        //        }
         //   }
    //
        //    if (toRemove != null)
         //   {
         //       hurdleList.Remove(toRemove);
         //   }
       // }
    }
}
