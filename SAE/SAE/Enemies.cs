using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SAE
{
    class Enemies
    {
        public Texture2D texture;
        public Vector2 position;
        Rectangle fantome;

        public bool isVisible = true;


        public Enemies(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            fantome = new Rectangle();

        }
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = (float)(deltaTime * (Perso._vitessePerso -30));
            //Si le monstre est à droite du personnage
            if (this.position.X > Perso._positionPerso.X)
            {
                position.X -= walkSpeed;
            }
            //Si le monstre est à gauche du personnage
            if (this.position.X < Perso._positionPerso.X)
            {
                position.X += walkSpeed;
            }
            //Si le monstre est au dessous du personnage
            if (this.position.Y > Perso._positionPerso.Y)
            {
                position.Y -= walkSpeed;
            }
            //Si le monstre est au dessus du personnage
            if (this.position.Y < Perso._positionPerso.Y)
            {
                position.Y += walkSpeed;
            }
            //else if (this.position.Y == Perso._positionPerso.Y && this.position.X == Perso._positionPerso.X)
            //{

            //}
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);


        }



    }
}
