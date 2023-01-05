using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE
{
    public class Player : Sprite
    {
        public Player(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            var sens = new Vector2();

            var vitesse = 3f;

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                sens.Y = -vitesse;
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                sens.Y = vitesse;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                sens.X = -vitesse;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                sens.X = vitesse;
            }

            Position += sens;

            
        }
    }
}
