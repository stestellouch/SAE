using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SAE
{
    class Enemies
    {
        public Texture2D texture;
        public Vector2 position;
        //Rectangle fantome;
        public double originWidth;
        public double originHeight;

        public bool isVisible = true;


        public Enemies(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            //fantome = new Rectangle();
            originWidth = (texture.Width / 2);
            originHeight = (texture.Height / 2);


        }
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * Perso._vitessePerso;
            //Si le monstre est à droite du personnage
            if (this.position.X > Perso._positionPerso.X-originWidth)
            {
                position.X -= walkSpeed;
            }
            //Si le monstre est à gauche du personnage
            else if (this.position.X < Perso._positionPerso.X- originWidth)
            {
                position.X += walkSpeed;
            }
            //Si le monstre est au dessous du personnage
            if (this.position.Y > Perso._positionPerso.Y- originHeight)
            {
                position.Y -= walkSpeed;
            }
            //Si le monstre est au dessus du personnage
            else if (this.position.Y < Perso._positionPerso.Y- originHeight)
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
