using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;

namespace SAE
{
    internal class Monstres : Game1
    {
        private  Vector2 _positionMonstre;
        private static AnimatedSprite _Monstre;
        
        private  int _sensMonstreX;
        private  int _sensMonstreY;
        private static int _vitesseMonstre;
        private  string _sensMonstre;

        public Monstres()
        {
            this.Initialize();
        }

        public static void LoadContent(Game game)
        {
            SpriteSheet SpriteMC = game.Content.Load<SpriteSheet>("Animation/free_monsters_0.sf", new JsonContentLoader());
            _Monstre = new AnimatedSprite(SpriteMC);

        }
        public void Initialize()
        {
            //Random rand = new Random();
            
            //if (rand.Next(1,2) == 1)
            //    _Monstre = _texture1;
            //else
            //    _Monstre = _texture2;

            //int posX = rand.Next(Game1.ScreenWidth );//attention ça peut sortir il faut soustraire la largeur du monstre
            //int posY = rand.Next(Game1.ScreenHeight );//pareil
            //_positionMonstre = new Vector2(posX, posY);
            this._positionMonstre = new Vector2(250, 250);
            this._sensMonstreY = 0;
            this._sensMonstreX = 0;
            this._vitesseMonstre = 100;
            this._sensMonstre = "nothing";

        }
        public void Update(GameTime gameTime)
        {
            this._sensMonstreX = 0;
            this._sensMonstreY = 0;

            //Sprite monstre = new Sprite(this);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _Monstre.Update(deltaTime);

            if (this._positionMonstre.X > Perso._positionPerso.X)
            {
                this._sensMonstreX = 1;
                this._sensMonstreY = 0;
                this._sensMonstre = "droite";
                this._positionMonstre.X += this._sensMonstreX * _vitesseMonstre * deltaTime;

            }
            else if (this._positionMonstre.X < Perso._positionPerso.X)
            {
                this._sensMonstreX = -1;
                this._sensMonstreY = 0;
                this._sensMonstre = "gauche";
                this._positionMonstre.X += this._sensMonstreX * _vitesseMonstre * deltaTime;
            }
            if (this._positionMonstre.Y > Perso._positionPerso.Y)
            {
                this._sensMonstreX = 0;
                this._sensMonstreY = -1;
                this._sensMonstre = "haut";
                this._positionMonstre.X += this._sensMonstreY * _vitesseMonstre * deltaTime;
            }
            else if (this._positionMonstre.Y < Perso._positionPerso.Y)
            {
                this._sensMonstreX = 0;
                this._sensMonstreY = 1;
                this._sensMonstre = "bas";
                this._positionMonstre.X += this._sensMonstreY * _vitesseMonstre * deltaTime;
            }
            else if (this._positionMonstre.Y == Perso._positionPerso.Y && this._positionMonstre.X == Perso._positionPerso.X)
            {
                this._sensMonstreX = 0;
                this._sensMonstreY = 0;
                this._sensMonstre = "normal";
                this._positionMonstre.X += this._sensMonstreY * _vitesseMonstre * deltaTime;
            }

            //Annimations
            if (this._sensMonstreX == 1 && this._sensMonstreY == 0)
            {
                _Monstre.Play("right");

            }
            else if (this._sensMonstreX == -1 && this._sensMonstreY == 0)
            {
                _Monstre.Play("left");
            }
            else if (this._sensMonstreY == -1 && this._sensMonstreX == 0)
            {
                _Monstre.Play("up");
            }
            else if (this._sensMonstreY == 1 && this._sensMonstreX == 0)
            {
                _Monstre.Play("down");
            }
            else if (this._sensMonstreY == 0 && this._sensMonstreX == 0)
            {
                if (this._sensMonstre == "bas") _Monstre.Play("idle_down");
                else if (this._sensMonstre == "haut") _Monstre.Play("idle_up");
                else if (this._sensMonstre == "droite") _Monstre.Play("idle_right");
                else if (this._sensMonstre == "left") _Monstre.Play("idle_left");
            }

        }
        public  void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(_Monstre, this._positionMonstre);

        }

    }
}
