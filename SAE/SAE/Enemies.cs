﻿using Microsoft.Xna.Framework;
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
        public Rectangle collision;

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

            collision = new Rectangle((int)this._position.X, (int)this._position.Y, 40, 25);

        }


        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = (float)(deltaTime * (Perso._vitessePerso - 30));
            //Si le monstre est à droite du personnage
            if (this._position.X-30 >= Perso._positionPerso.X && _estEnVie == true)
            {
                this._position.X -= walkSpeed;
                //this.collision.X -= (int)walkSpeed;
            }
            //Si le monstre est à gauche du personnage
            if (this._position.X-30 <= Perso._positionPerso.X && _estEnVie == true)
            {
                this._position.X += walkSpeed;
                //this.collision.X += (int)walkSpeed;
            }
            //Si le monstre est au dessous du personnage
            if (this._position.Y+30 >= Perso._positionPerso.Y && _estEnVie == true)
            {
                this._position.Y -= walkSpeed;
                //this.collision.Y -= (int)walkSpeed;
            }
            //Si le monstre est au dessus du personnage            
            if (this._position.Y+30 <= Perso._positionPerso.Y && _estEnVie == true)
            {
                this._position.Y += walkSpeed;
                //this.collision.Y += (int)walkSpeed;
            }
            this.collision.X = (int)this._position.X-30;
            this.collision.Y = (int)this._position.Y+30;
            ///////////////////////////////////////////////
            ///                Degats                   ///
            ///////////////////////////////////////////////

            Console.WriteLine(Perso.colisionPerso.X + " perso" + Perso.colisionPerso.Y);
            Console.WriteLine(this.collision.X + " fantome" + this.collision.Y);
            if ((Perso.colisionPerso.X)<= (this.collision.X ) && (Perso.colisionPerso.X+64) >= (this.collision.X)
                && (Perso.colisionPerso.Y) <= (this.collision.Y ) && (Perso.colisionPerso.Y+64) >= (this.collision.Y)
                && Perso.keyboardState.IsKeyDown(Keys.Space))
            {
                this._vieMonstre -= 25;
                Console.WriteLine("attaque");
            }
            if (this.collision.Intersects(Perso.colisionPerso))
            {
               Console.WriteLine("Collision");
               
               
               Perso._viePerso -= 10;
                
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
                _estEnVie = false;
            }
            //else if (Perso._viePerso <= 0)
            //{

            //}
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureEnemy, _positionEnemy, Color.White);
            spriteBatch.Draw(_texture, _position, Color.White);


        }

    }

    
}
