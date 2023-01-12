﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SAE
{
    public class Enemies
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
        public static float _compteurAttaqueMonstre;
        public static float _compteurAttaquePerso;
        public bool _attaqueMonstre;
        public bool _attaquePerso;

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

            _compteurAttaqueMonstre = 0;
            _compteurAttaquePerso = 0;

            

            originWidth = (_texture.Width / 2);
            originHeight = (_texture.Height / 2);

            collision = new Rectangle((int)this._position.X, (int)this._position.Y, 40, 25);

        }


        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _compteurAttaquePerso += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _compteurAttaqueMonstre += (float)gameTime.ElapsedGameTime.TotalSeconds;


            float walkSpeed = (float)(deltaTime * (Perso._vitessePerso - 30));
            //Si le monstre est à droite du personnage
            if (this._position.X+25 >= Perso._positionPerso.X && _estEnVie == true)
            {
                this._position.X -= walkSpeed;
            }
            //Si le monstre est à gauche du personnage
            if (this._position.X+25 <= Perso._positionPerso.X && _estEnVie == true)
            {
                this._position.X += walkSpeed;
            }
            //Si le monstre est au dessous du personnage
            if (this._position.Y+25 >= Perso._positionPerso.Y && _estEnVie == true)
            {
                this._position.Y -= walkSpeed;
            }
            //Si le monstre est au dessus du personnage            
            if (this._position.Y+25 <= Perso._positionPerso.Y && _estEnVie == true)
            {
                this._position.Y += walkSpeed;
            }

            this.collision.X = (int)this._position.X+25;
            this.collision.Y = (int)this._position.Y+35;

            ///////////////////////////////////////////////
            ///                Degats                   ///
            ///////////////////////////////////////////////
            if (((Perso.colisionPerso.X -45)<= (this.collision.X ) && (Perso.colisionPerso.X+64 ) >= (this.collision.X)
                && (Perso.colisionPerso.Y -30) <= (this.collision.Y ) && (Perso.colisionPerso.Y+64 ) >= (this.collision.Y)
                && Perso.keyboardState.IsKeyDown(Keys.Space)) && _attaquePerso == true && this._estEnVie == true)
            {
                this._vieMonstre -= 50;
                Console.WriteLine("attaque");
                _attaquePerso = false;
                _compteurAttaquePerso = 0;
                Perso._score = Perso._score + 10;
                Console.WriteLine(_vieMonstre);
            }
            if(_compteurAttaquePerso > 2)
            {
                _attaquePerso = true;
            }


            if (this.collision.Intersects(Perso.colisionPerso) && this._estEnVie == true && this._attaqueMonstre == true)
            {
                Console.WriteLine("Collision");
                _compteurAttaqueMonstre = 0;
                this._attaqueMonstre = false;
                Perso._viePerso -= 10;
                Console.WriteLine(Perso._viePerso);
                
            }
            if(_compteurAttaqueMonstre >5)
            {
                _attaqueMonstre = true;
            }
           

            if (this._vieMonstre <= 0)
            {
                this._estEnVie = false;
            }

            //if (Perso.estEnViePerso == false)
            //    this.
            
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureEnemy, _positionEnemy, Color.White);
            spriteBatch.Draw(_texture, _position, Color.White);


        }

    }

    
}
