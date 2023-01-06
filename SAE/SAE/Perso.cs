using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace SAE
{
    internal class Perso
    {
        private Vector2 _positionPerso;
        private AnimatedSprite _Perso;
        private int _sensPersoX;
        private int _sensPersoY;
        private int _vitessePerso;
        private string _sens;
        private KeyboardState _keyboardState;

        public void Initialize()
        {
            
            _positionPerso = new Vector2(100, 100);
            _sensPersoY = 0;
            _sensPersoX = 0;
            _vitessePerso = 100;
            _sens = "nothing";
        }
        public void LoadContent(Game game, int numJoueur)
        {
            if (numJoueur == 1)
            {
                SpriteSheet SpriteMC = game.Content.Load<SpriteSheet>("Animation/MC.sf", new JsonContentLoader());
                _Perso = new AnimatedSprite(SpriteMC);
            }
            else if (numJoueur == 2)
            {
                SpriteSheet SpriteMC2A = game.Content.Load<SpriteSheet>("Animation/perso_bleu/perso_bleu_attack.sf", new JsonContentLoader());
                SpriteSheet SpriteMC2M = game.Content.Load<SpriteSheet>("Animation/perso_bleu/perso_bleu_marche.sf", new JsonContentLoader());
                _Perso = new AnimatedSprite(SpriteMC2A);
                _Perso = new AnimatedSprite(SpriteMC2M);
            }
        }
        public void Update(GameTime gameTime, int numJoueur)
        {
            _sensPersoX = 0;
            _sensPersoY = 0;


            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _Perso.Update(deltaTime);
            _keyboardState = Keyboard.GetState();

            if (numJoueur == 1)
            {
                //Si la touche droite est pressé
                if (_keyboardState.IsKeyDown(Keys.D) && !(_keyboardState.IsKeyDown(Keys.Q)) && !(_keyboardState.IsKeyDown(Keys.S)) && !(_keyboardState.IsKeyDown(Keys.Z)))
                {
                    _sensPersoX = 1;
                    _sensPersoY = 0;
                    _sens = "droite";
                    _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;

                }
                //Si la touche gauche est pressé
                if (_keyboardState.IsKeyDown(Keys.Q) && !(_keyboardState.IsKeyDown(Keys.D)) && !(_keyboardState.IsKeyDown(Keys.S)) && !(_keyboardState.IsKeyDown(Keys.Z)))
                {
                    _sensPersoX = -1;
                    _sensPersoY = 0;
                    _sens = "gauche";
                    _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;

                }
                //Si la touche haut est pressé
                if (_keyboardState.IsKeyDown(Keys.Z) && !(_keyboardState.IsKeyDown(Keys.S)))
                {
                    _sensPersoY = -1;
                    _sensPersoX = 0;
                    _sens = "haut";
                    _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

                }
                //Si la touche bas est pressé
                if (_keyboardState.IsKeyDown(Keys.S) && !(_keyboardState.IsKeyDown(Keys.Z)))
                {
                    _sensPersoY = 1;
                    _sensPersoX = 0;
                    _sens = "bas";
                    _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

                }
                //######################################################
                //                      ANIMATION
                //######################################################

                if (_sensPersoX == 1 && _sensPersoY == 0)
                {
                    _Perso.Play("right_Walk");

                }
                else if (_sensPersoX == -1 && _sensPersoY == 0)
                {
                    _Perso.Play("left_Walk");
                }
                else if (_sensPersoY == -1 && _sensPersoX == 0)
                {
                    _Perso.Play("up_Walk");
                }
                else if (_sensPersoY == 1 && _sensPersoX == 0)
                {
                    _Perso.Play("down_Walk");
                }
                else if (_sensPersoY == 0 && _sensPersoX == 0)
                {
                    if (_sens == "bas") _Perso.Play("idle_down");
                    else if (_sens == "haut") _Perso.Play("idle_up");
                    else if (_sens == "droite") _Perso.Play("idle_right");
                    else if (_sens == "left") _Perso.Play("idle_left");
                }


                //######################################################
                //                      ATTAQUE
                //######################################################

                if (_keyboardState.IsKeyDown(Keys.Space) && (_sensPersoX == 1 && _sensPersoY == 0))
                    _Perso.Play("right_swing");
                if (_keyboardState.IsKeyDown(Keys.Space) && (_sensPersoX == -1 && _sensPersoY == 0))
                    _Perso.Play("left_swing");
                if (_keyboardState.IsKeyDown(Keys.Space) && (_sensPersoY == 1 && _sensPersoX == 0))
                    _Perso.Play("down_swing");
                if (_keyboardState.IsKeyDown(Keys.Space) && (_sensPersoY == -1 && _sensPersoX == 0))
                    _Perso.Play("up_swing");
            }
            if (numJoueur == 2)
            {
                //Si la touche droite est pressé
                if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)) && !(_keyboardState.IsKeyDown(Keys.Down)) && !(_keyboardState.IsKeyDown(Keys.Up)))
                {
                    _sensPersoX = 1;
                    _sensPersoY = 0;
                    _sens = "droite";
                    _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;

                }
                //Si la touche gauche est pressé
                if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)) && !(_keyboardState.IsKeyDown(Keys.Down)) && !(_keyboardState.IsKeyDown(Keys.Up)))
                {
                    _sensPersoX = -1;
                    _sensPersoY = 0;
                    _sens = "gauche";
                    _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;

                }
                //Si la touche haut est pressé
                if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
                {
                    _sensPersoY = -1;
                    _sensPersoX = 0;
                    _sens = "haut";
                    _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

                }
                //Si la touche bas est pressé
                if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
                {
                    _sensPersoY = 1;
                    _sensPersoX = 0;
                    _sens = "bas";
                    _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

                }
                //######################################################
                //                      ANIMATION
                //######################################################

                if (_sensPersoX == 1 && _sensPersoY == 0)
                {
                    _Perso.Play("right");

                }
                else if (_sensPersoX == -1 && _sensPersoY == 0)
                {
                    _Perso.Play("left");
                }
                else if (_sensPersoY == -1 && _sensPersoX == 0)
                {
                    _Perso.Play("up");
                }
                else if (_sensPersoY == 1 && _sensPersoX == 0)
                {
                    _Perso.Play("down");
                }
                else if (_sensPersoY == 0 && _sensPersoX == 0)
                {
                    if (_sens == "bas") _Perso.Play("idle_down");
                    else if (_sens == "haut") _Perso.Play("idle_up");
                    else if (_sens == "droite") _Perso.Play("idle_right");
                    else if (_sens == "left") _Perso.Play("idle_left");
                }
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_Perso, _positionPerso);
        
        }
    }
}
