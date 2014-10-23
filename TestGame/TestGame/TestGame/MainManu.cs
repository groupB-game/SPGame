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
    class MainManu
    {

        enum GameState { mainMenu, start, highscore, play }
        GameState gamestate;

        List<ManuUI> UImain = new List<ManuUI>();
        List<ManuUI> NameEnter = new List<ManuUI>();
        List<ManuUI> ScoreBoard = new List<ManuUI>();
        public String Set = "";

        private SpriteFont sf;
        public string PlayerName = string.Empty;

        private Keys[] lastpressedkeys = new Keys[5];

        public MainManu()
        {
            UImain.Add(new ManuUI("StartScreen/mainmanu"));
            UImain.Add(new ManuUI("StartScreen/start"));
            UImain.Add(new ManuUI("StartScreen/highscore"));



            NameEnter.Add(new ManuUI("StartScreen/entername"));
            NameEnter.Add(new ManuUI("StartScreen/done"));

            ScoreBoard.Add(new ManuUI("StartScreen/scoreboard"));
            ScoreBoard.Add(new ManuUI("StartScreen/ok"));

        }

        public void LoadContent(ContentManager content)
        {
            sf = content.Load<SpriteFont>("ScoreFont/Score");
            foreach (ManuUI element in UImain)
            {
                element.LoadContent(content);
                element.SetCenter(1080, 1920);
                element.clickEvent += OnClick;
            }
            UImain.Find(x => x.ButtonName1 == "StartScreen/start").ChangePosition(0, -150);

            foreach (ManuUI element in NameEnter)
            {
                element.LoadContent(content);
                element.SetCenter(1080, 1920);
                element.clickEvent += OnClick;
            }

            NameEnter.Find(x => x.ButtonName1 == "StartScreen/done").ChangePosition(0, 78);

            foreach (ManuUI element in ScoreBoard)
            {
                element.LoadContent(content);
                element.SetCenter(1080, 1920);
                element.clickEvent += OnClick;
            }
            ScoreBoard.Find(x => x.ButtonName1 == "StartScreen/ok").ChangePosition(0, 100);
        }
        public void Update()
        {

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

        public void Draw(SpriteBatch spritebatch)
        {

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

        public void OnkeyUp(Keys key)
        {

        }

        public void OnkeyDown(Keys key)
        {

            List<Keys> Allotherkey = new List<Keys>();
            Allotherkey.Add(Keys.Back);
            Allotherkey.Add(Keys.Enter);
            Allotherkey.Add(Keys.Space);
            if (key == Keys.Back && PlayerName.Length > 0)
            {
                PlayerName = PlayerName.Remove(PlayerName.Length - 1);
            }
            else if (key == Keys.Back || key == Keys.Enter || key == Keys.Space || key == Keys.RightAlt || key == Keys.RightControl)
            {

            }
            else if (key == Keys.RightShift || key == Keys.LeftShift || key == Keys.CapsLock || key == Keys.Tab || key == Keys.LeftAlt)
            {

            }
            else if (key == Keys.LeftControl || key == Keys.RightShift || key == Keys.Delete || key == Keys.LeftWindows || key == Keys.RightWindows)
            {

            }
            else
            {
                PlayerName += key.ToString();
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
