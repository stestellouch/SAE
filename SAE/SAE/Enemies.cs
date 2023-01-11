using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public Texture2D _texture;
        public Vector2 _position;
        //Rectangle fantome;
        public double originWidth;
        public double originHeight;
        public int _vieMonstre;
        public bool _estEnVie;
        public Rectangle Collision;

        public bool isVisible = true;
         

        public Enemies(Texture2D newTexture, Vector2 newPosition, int vie, bool enVie)
        {
            _textureEnemy = newTexture;
            _positionEnemy = newPosition;
            
            _originWidth = (_textureEnemy.Width / 2);
            _originHeight = (_textureEnemy.Height / 2);
            _texture = newTexture;
            _position = newPosition;
            _vieMonstre = vie;
            _estEnVie = enVie;
            
            originWidth = (_texture.Width / 2);
            originHeight = (_texture.Height / 2);


        }
        
        
        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = (float)(deltaTime * (Perso._vitessePerso - 30));
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
                if (this._position.X > Perso._positionPerso.X - originWidth)
                {
                    _position.X -= walkSpeed;
                }
                //Si le monstre est à gauche du personnage
                if (this._position.X < Perso._positionPerso.X)
                {
                    _position.X += walkSpeed;
                }
                //Si le monstre est au dessous du personnage
                if (this._position.Y > Perso._positionPerso.Y - originHeight)
                {
                    _position.Y -= walkSpeed;
                }
                //Si le monstre est au dessus du personnage            
                if (this._position.Y < Perso._positionPerso.Y)
                {
                    _position.Y += walkSpeed;
                }

                ///////////////////////////////////////////////
                ///                Degats                   ///
                ///////////////////////////////////////////////
                if (Perso._positionPerso.Y == this._position.Y && Perso._positionPerso.X == this._position.X)
                {
                    Console.WriteLine("Collision");
                    if (Perso.keyboardState.IsKeyDown(Keys.Space))
                    {
                        this._vieMonstre -= 25;
                    }
                    else
                    {
                        Perso._viePerso -= 10;
                    }
                }
                //else if (Perso._positionPerso.X == this._position.Y)
                //{
                //    Console.WriteLine("Collision");
                //    if (Perso.keyboardState.IsKeyDown(Keys.Space))
                //    {
                //        this._vieMonstre -= 25;
                //    }
                //    else
                //    {
                //        Perso._viePerso -= 10;
                //    }
                //}
                //else if (Perso._positionPerso.X  == this._position.X)
                //{
                //    if (Perso.keyboardState.IsKeyDown(Keys.Space))
                //    {
                //        this._vieMonstre -= 25;
                //    }
                //    else
                //    {
                //        Perso._viePerso -= 10;
                //    }
                //}
                //else if (Perso._positionPerso.X  == this._position.X)
                //{
                //    if (Perso.keyboardState.IsKeyDown(Keys.Space))
                //    {
                //        this._vieMonstre -= 25;
                //    }
                //    else
                //    {
                //        Perso._viePerso -= 10;
                //    }
                //}

                if (this._vieMonstre <= 0)
                {
                    this._estEnVie = false;
                }
                //else if (Perso._viePerso <= 0)
                //{

                //}
            }
           
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureEnemy, _positionEnemy, Color.White);
            spriteBatch.Draw(_texture, _position, Color.White);


        }



    }
}
