using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestGame
{
    /// <summary>
    /// Create menu views.
    /// </summary>
    class MainManu
    {
        /// <summary>
        /// Define game states.
        /// </summary>
        enum GameState { mainMenu, start, highscore, play }
        GameState gamestate;

        //Create list to hold element for different state.
        List<ManuUI> UImain = new List<ManuUI>();
        List<ManuUI> NameEnter = new List<ManuUI>();
        List<ManuUI> ScoreBoard = new List<ManuUI>();
        public String Set = "";

        //Declare font.
        private SpriteFont sf;

        public string PlayerName = string.Empty;

        //Declare key array to hold input.
        private Keys[] lastpressedkeys = new Keys[5];

        public MainManu()
        {
            //Initialise Main menu items.
            UImain.Add(new ManuUI("StartScreen/mainmanu"));
            UImain.Add(new ManuUI("StartScreen/start"));
            UImain.Add(new ManuUI("StartScreen/highscore"));
            
            //Initialise Start memu items.
            NameEnter.Add(new ManuUI("StartScreen/entername"));
            NameEnter.Add(new ManuUI("StartScreen/done"));

            //Initialise Score board items.
            ScoreBoard.Add(new ManuUI("StartScreen/scoreboard"));
            ScoreBoard.Add(new ManuUI("StartScreen/ok"));

        }

        public void LoadContent(ContentManager content)
        {
            //Initialise game font.
            sf = content.Load<SpriteFont>("ScoreFont/Score");

            //Load and set click event for Main menu elements.
            foreach (ManuUI element in UImain)
            {
                element.LoadContent(content);
                element.SetCenter(1080, 1920);
                element.clickEvent += OnClick;
            }

            //Change start button possition.
            UImain.Find(x => x.ButtonName1 == "StartScreen/start").ChangePosition(0, -150);

            //Load and set click event for Enter name menu elements.
            foreach (ManuUI element in NameEnter)
            {
                element.LoadContent(content);
                element.SetCenter(1080, 1920);
                element.clickEvent += OnClick;
            }

            //Change done button possition.
            NameEnter.Find(x => x.ButtonName1 == "StartScreen/done").ChangePosition(0, 78);

            //Load and set click event for ScoreBoard menu elements.
            foreach (ManuUI element in ScoreBoard)
            {
                element.LoadContent(content);
                element.SetCenter(1080, 1920);
                element.clickEvent += OnClick;
            }

            //Change ok button possition.
            ScoreBoard.Find(x => x.ButtonName1 == "StartScreen/ok").ChangePosition(0, 100);
        }

        /// <summary>
        /// Update each menu item.
        /// </summary>
        public void Update()
        {
            //Update memu item in each gamestate.
            switch (gamestate)
            {
                case GameState.mainMenu:
                    foreach (ManuUI element in UImain)
                    {
                        element.Update();
                    }
                    break;
                case GameState.start:
                    foreach (ManuUI element in NameEnter)
                    {
                        element.Update();
                    }
                    Getkeys();

                    break;

                case GameState.highscore:
                    foreach (ManuUI element in ScoreBoard)
                    {
                        element.Update();
                    }
                    break;
            }
        }

        /// <summary>
        /// Draw button for each game state.
        /// </summary>
        /// <param name="spritebatch"></param>
        public void Draw(SpriteBatch spritebatch)
        {
            //Draw button for each game state.
            switch (gamestate)
            {
                case GameState.mainMenu:
                    foreach (ManuUI element in UImain)
                    {
                        element.Draw(spritebatch);
                    }
                    break;
                case GameState.start:
                    foreach (ManuUI element in NameEnter)
                    {
                        element.Draw(spritebatch);
                    }
                    spritebatch.DrawString(sf, PlayerName, new Vector2(800, 520), Color.Black);
                    break;
                case GameState.highscore:
                    foreach (ManuUI element in ScoreBoard)
                    {
                        element.Draw(spritebatch);
                    }

                    spritebatch.DrawString(sf, ReadPlayer() + "                  " + ReadHighScore(), new Vector2(700, 460), Color.Black);
                    break;

            }

        }

        /// <summary>
        /// Set click event for each button.
        /// </summary>
        /// <param name="element"></param>
        public void OnClick(string element)
        {
            if (element == "StartScreen/start")
            {
                gamestate = GameState.start;
            }
            if (element == "StartScreen/done")
            {
                gamestate = GameState.play;
                Set = "Done";
            }
            if (element == "StartScreen/highscore")
            {
                gamestate = GameState.highscore;
            }
            if (element == "StartScreen/ok")
            {
                gamestate = GameState.mainMenu;
            }
        }

        /// <summary>
        /// Get keyboard pressed key.
        /// </summary>
        public void Getkeys()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();

            foreach (Keys key in lastpressedkeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    //key is no longer pressed.
                    OnkeyUp(key);
                }
            }

            foreach (Keys key in pressedKeys)
            {
                if (!lastpressedkeys.Contains(key))
                {
                    OnkeyDown(key);
                }
            }

            lastpressedkeys = pressedKeys;
        }

        /// <summary>
        /// pressed key up event.
        /// </summary>
        /// <param name="key"></param>
        public void OnkeyUp(Keys key)
        {
            //Todo
        }

        /// <summary>
        /// Pressed key down event.
        /// </summary>
        /// <param name="key"></param>
        public void OnkeyDown(Keys key)
        {
            List<Keys> Allotherkey = new List<Keys>();

            //Delete name character.
            if (key == Keys.Back && PlayerName.Length > 0)
            {
                PlayerName = PlayerName.Remove(PlayerName.Length - 1);
            }

            //Filter character keys.
            else if (key == Keys.Back || key == Keys.Enter || key == Keys.Space || key == Keys.RightAlt || key == Keys.RightControl)
            {

            }
            //Filter character keys.
            else if (key == Keys.RightShift || key == Keys.LeftShift || key == Keys.CapsLock || key == Keys.Tab || key == Keys.LeftAlt)
            {

            }
            //Filter character keys.
            else if (key == Keys.LeftControl || key == Keys.RightShift || key == Keys.Delete || key == Keys.LeftWindows || key == Keys.RightWindows)
            {

            }
            //Assign player name form key values.
            else
            {
                PlayerName += key.ToString();
            }


        }

        /// <summary>
        /// Read high score.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Read high score holder name.
        /// </summary>
        /// <returns></returns>
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
    }
}
