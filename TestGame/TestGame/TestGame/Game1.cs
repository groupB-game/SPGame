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
        



        //Score
        private int score = 0;
        private string playre = "";
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

        //Game Main Manu
        MainManu menu = new MainManu();

        //Collision
        Color backgroundclour = Color.Aquamarine;

        //Game state initialize
        private GameState gameState;
        private Thread backgroundThread;


        Animation runner;

        //Scrolling Path
        private ScrollingPath scrolling1, scrolling2;
        private BackGround backGround1, backGround2, backGround3, backGround4, backGround5, backGround6;

        //Screen parameters
        int screenWidth;
        int screeHeight;
        public int darkscreenmovement = 0;


        Vector2 velocity;

        Rectangle ballbox;
        Rectangle runnerBox, backGroundBox;
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
            playre = ReadPlayer();

            // TODO: Add your initialization logic here
            //Runner
            runner = new Animation(Content.Load<Texture2D>("Runner3"), new Vector2(640, 880), 288, 294);

            startButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 100, 410);
            exitButtonPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 95, 510);

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

            //Main Manu
            menu.LoadContent(Content);

            



            // TODO: use this.Content to load your game content here


            ballbox = new Rectangle(10, 10, 30, 30);

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

            backGround6 = new BackGround(Content.Load<Texture2D>("StartScreen/darkscreen"), new Rectangle(-1366 + darkscreenmovement, 0, 1366, 788));

            //Stady background
            //backGround = Content.Load<Texture2D>("LabBGNew");
            //backGroundBox = new Rectangle(0, 0, 2000, 1080);



            velocity.X = 3f;
            velocity.Y = 3f;

            screenWidth = GraphicsDevice.Viewport.Width;
            screeHeight = GraphicsDevice.Viewport.Height;



            hurdle1 = new Hurdles(Content.Load<Texture2D>("Pickups/(b)missed_alarm_pickup"), new Rectangle(2010, 790, 150, 150));
            hurdle2 = new Hurdles(Content.Load<Texture2D>("Pickups/(b)missed_alarm_pickup"), new Rectangle(3800, 650, 150, 150));

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
            //Manu Button Update
            menu.Update();


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                int Score = score;
                if (Score > Int32.Parse(ReadHighScore()))
                {
                    SaveHighScore(Score);
                    SavePlayer(playre);
                }

                this.Exit();
            }



            //score++;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                int Score = score;
                if (Score > Int32.Parse(ReadHighScore()))
                {
                    SaveHighScore(Score);
                    SavePlayer(playre);
                }

                this.Exit();

            }


            if (menu.Set == "Done")
            {
                mouseState = Mouse.GetState();
                if (previousMouseState.LeftButton == ButtonState.Pressed &&
                    mouseState.LeftButton == ButtonState.Released)
                {
                    MouseClick(mouseState.X, mouseState.Y);
                }

                previousMouseState = mouseState;
            }


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

                    //runner = new Animation(Content.Load<Texture2D>("RunnerWHat"), new Vector2(xpos, ypos), 330, 288);
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
                if (runner.PositionRectangle.Intersects(hurdle1.PositionRectangle))
                {
                    Random r = new Random();
                    int r1 = r.Next(0, 8);
                    List<string> Hurdlelist = HurdlesGood();
                    string randomname = Hurdlelist[r1];
                    int random = r.Next(1366, 1800);
                    int random2 = r.Next(100, 600);
                    score -= 25;
                    darkscreenmovement++;
                    hurdle1.rectangle.Y += 1000;
                    //hurdle1.rectangle.Y = random2;
                    // hurdle1.rectangle.X = random;
                    // hurdle1.rectangle.X -= 10;
                    hurdle1 = new Hurdles(Content.Load<Texture2D>("Pickups/" + randomname), new Rectangle(random, random2, 150, 150));
                    hurdle1.rectangle.X -= 10;
                }
                if (runner.PositionRectangle.Intersects(hurdle2.PositionRectangle))
                {
                    Random r = new Random();
                    int r1 = r.Next(0, 8);
                    List<string> Hurdlelist = HurdlesList();
                    string randomname = Hurdlelist[r1];
                    int random = r.Next(1366, 1800);
                    int random2 = r.Next(100, 600);
                    score -= 25;
                    darkscreenmovement++;
                    //hurdle2.rectangle.Y += 1000;
                    //hurdle2.rectangle.Y = random2;
                    //hurdle2.rectangle.X = random;
                    hurdle2 = new Hurdles(Content.Load<Texture2D>("Pickups/" + randomname), new Rectangle(random, random2, 150, 150));

                    hurdle2.rectangle.X -= 10;
                }
                backGround6 = new BackGround(Content.Load<Texture2D>("StartScreen/darkscreen"), new Rectangle(-1920 + darkscreenmovement, 0, 1920, 1080));





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
            //SaveHighScore(score);

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
                //spriteBatch.DrawString(font, "High Score " + saveScore, new Vector2(100, 10), Color.White);

                spriteBatch.Draw(startButton, startButtonPosition, Color.White);
                spriteBatch.Draw(exitButton, exitButtonPosition, Color.White);
                menu.Draw(spriteBatch);

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
                spriteBatch.DrawString(font, "Player: " + menu.PlayerName, new Vector2(800, 10), Color.White);
                if (score > Int32.Parse(checkScore))
                {
                    spriteBatch.DrawString(font, "High Score ", new Vector2(100, 10), Color.White);
                    playre = menu.PlayerName;
                }

            }
            if (gameState == GameState.Paused)
            {
                spriteBatch.DrawString(font, "Current Score  " + score, new Vector2(100, 10), Color.White);
                spriteBatch.Draw(resumeButton, resumeButtonPosition, Color.White);

                //Save Score
                SaveHighScore(score);
                SavePlayer(playre);
            }

            backGround6.Drow(spriteBatch);

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
        public void SavePlayer(String player)
        {
            try
            {
                TextWriter writer = new StreamWriter("player.txt");
                writer.WriteLine(player);
                writer.Close();



            }
            catch (Exception e)
            {

            }
        }

        public string ReadPlayer()
        {
            string read = "";
            try
            {
                TextReader readHig = new StreamReader("player.txt");
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

        public List<string> HurdlesList()
        {
            List<string> HurdlesName = new List<string>();
            HurdlesName.Add("(b)f_pickup");
            HurdlesName.Add("(b)flash_drive_pickup");
            HurdlesName.Add("(b)flu_pickup");
            HurdlesName.Add("(b)forgot_due_date_pickup");
            HurdlesName.Add("(b)goals_missed_pickup");
            HurdlesName.Add("(b)missed_alarm_pickup");
            HurdlesName.Add("(b)moodle_down_pickup");
            HurdlesName.Add("(b)not_enough_sleep_pickup");
            HurdlesName.Add("(b)repeat_paper_pickup");
            return HurdlesName;
        }

        public List<string> HurdlesGood()
        {
            List<string> HurdlesGood = new List<string>();
            HurdlesGood.Add("(g)a+_pickup");
            HurdlesGood.Add("(g)goals_met_pickup");
            HurdlesGood.Add("(g)good_health_pickup");
            HurdlesGood.Add("(g)good_sleep_pickup");
            HurdlesGood.Add("(g)good_tutor_pickup");
            HurdlesGood.Add("(g)new_skills_pickup");
            HurdlesGood.Add("(g)notes_taken_pickup");
            HurdlesGood.Add("(g)on_time_pickup");
            HurdlesGood.Add("(g)passed_paper_pickup");
            HurdlesGood.Add("(g)study_time_pickup");
            return HurdlesGood;
        }
    }
}

