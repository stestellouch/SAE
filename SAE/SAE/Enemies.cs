using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SAE
{
    class Enemies
    {
        public Texture2D texture;
        public Vector2 position;
        public int _vitesseMonstre;
        //public Vector2 velocity;

        public bool isVisible = true;

        Random random = new Random();
        int randX, randY;

        public Enemies(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;

            randY = random.Next(1, 10);
            randX = random.Next(1, 10);

            //velocity = new Vector2(randX, randY);

        }
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * Perso._vitessePerso+1;
            //Si le monstre est à droite du personnage
            if (this.position.X > Perso._positionPerso.X)
            {
                position.X -= walkSpeed;
            }
            //Si le monstre est à gauche du personnage
            else if (this.position.X < Perso._positionPerso.X)
            {
                position.X += walkSpeed;
            }
            //Si le monstre est au dessous du personnage
            if (this.position.Y > Perso._positionPerso.Y)
            {
                position.Y -= walkSpeed;
            }
            //Si le monstre est au dessus du personnage
            else if (this.position.Y < Perso._positionPerso.Y)
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
