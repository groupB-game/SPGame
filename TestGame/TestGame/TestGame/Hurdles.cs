using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    class Hurdles
    {
        public Texture2D  texture;
        public Rectangle rectangle;
        public List<Hurdles> hurdlepickuplist;

        public Hurdles(Texture2D newTexture, Rectangle newRectangle)
        {
            this.texture = newTexture;
            this.rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.X -= 10;
        }

        public void Drow(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void GetPickups()
        {
            var hurdlepickup = new List<Hurdles>
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
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

