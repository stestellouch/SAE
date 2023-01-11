using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SAE
{
    class Enemies
    {
        public Texture2D _textureEnemy;
        public Vector2 _positionEnemy;
        //Rectangle fantome;
        public double _originWidth;
        public double _originHeight;

        public bool isVisible = true;
         

        public Enemies(Texture2D newTexture, Vector2 newPosition)
        {
            _textureEnemy = newTexture;
            _positionEnemy = newPosition;
            
            _originWidth = (_textureEnemy.Width / 2);
            _originHeight = (_textureEnemy.Height / 2);


        }
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = (float)(deltaTime * (Perso._vitessePerso -30));
            //Si le monstre est à droite du personnage
            if (this._positionEnemy.X > Perso._positionPerso.X-_originWidth)
            {
                _positionEnemy.X -= walkSpeed;
            }
            
            if (this._positionEnemy.X < Perso._positionPerso.X)
            {
                _positionEnemy.X += walkSpeed;
            }
            //Si le monstre est au dessous du personnage
            if (this._positionEnemy.Y > Perso._positionPerso.Y- _originHeight)
            {
                _positionEnemy.Y -= walkSpeed;
            }
                        
            if (this._positionEnemy.Y < Perso._positionPerso.Y)
            {
                _positionEnemy.Y += walkSpeed;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureEnemy, _positionEnemy, Color.White);


        }



    }
}
