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

namespace SAE
{
    internal class player
    {
        private int nbPoints;
        private int numJoueur;
        private string pseudo;
        private Vector2 _positionPerso;
        private int _sensPersoX;
        private int _sensPersoY;
        private int _vitessePerso;
        public AnimatedSprite _Perso;
        private KeyboardState _keyboardState;

        public player(int numJoueur, string pseudo)
        {
            this.NbPoints = 0;
            this.NumJoueur = numJoueur;
            this.Pseudo = pseudo;
            this.PositionPerso = new Vector2(100, 100); 
            this.SensPersoX = 0;
            this.SensPersoY = 0;
            this.VitessePerso = 100;
            this.Perso = _Perso;
        }

        public int NbPoints
        {
            get
            {
                return this.nbPoints;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Peut pas avoir un score < 0");
                this.nbPoints = value;
            }
        }

        public int NumJoueur
        {
            get
            {
                return this.numJoueur;
            }

            set
            {
                this.numJoueur = value;
            }
        }

        public string Pseudo
        {
            get
            {
                return this.pseudo;
            }

            set
            {
                this.pseudo = value;
            }
        }

        public Vector2 PositionPerso
        {
            get
            {
                return this._positionPerso;
            }

            set
            {
                this._positionPerso = value;
            }
        }

        public int SensPersoX
        {
            get
            {
                return this._sensPersoX;
            }

            set
            {
                this._sensPersoX = value;
            }
        }

        public int SensPersoY
        {
            get
            {
                return this._sensPersoY;
            }

            set
            {
                this._sensPersoY = value;
            }
        }

        public int VitessePerso
        {
            get
            {
                return this._vitessePerso;
            }

            set
            {
                this._vitessePerso = value;
            }
        }

        public AnimatedSprite Perso
        {
            get
            {
                return this._Perso;
            }

            set
            {
                this._Perso = value;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is player player &&
                   this.numJoueur == player.numJoueur &&
                   this._positionPerso.Equals(player._positionPerso) &&
                   EqualityComparer<AnimatedSprite>.Default.Equals(this._Perso, player._Perso);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.numJoueur, this._positionPerso, this._Perso);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(player left, player right)
        {
            return EqualityComparer<player>.Default.Equals(left, right);
        }

        public static bool operator !=(player left, player right)
        {
            return !(left == right);
        }

        public static void DeplacementMC(player joueur, GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            joueur._Perso.Update(deltaTime);
            joueur._keyboardState = Keyboard.GetState();
            Vector2 position = joueur.PositionPerso;

            if (joueur._keyboardState.IsKeyDown(Keys.Right) && !(joueur._keyboardState.IsKeyDown(Keys.Left)))
            {
                joueur.SensPersoX = 1;
                position.X += joueur.SensPersoX * joueur.VitessePerso * deltaTime;

            }
            //Si la touche gauche est pressé
            if (joueur._keyboardState.IsKeyDown(Keys.Left) && !(joueur._keyboardState.IsKeyDown(Keys.Right)))
            {
                joueur.SensPersoX = -1;
                position.X += joueur.SensPersoX * joueur.VitessePerso * deltaTime;

            }
            //Si la touche haut est pressé
            if (joueur._keyboardState.IsKeyDown(Keys.Up) && !(joueur._keyboardState.IsKeyDown(Keys.Down)))
            {
                joueur.SensPersoY = -1;
                position.Y += joueur.SensPersoY * joueur.VitessePerso * deltaTime;

            }
            //Si la touche bas est pressé
            if (joueur._keyboardState.IsKeyDown(Keys.Down) && !(joueur._keyboardState.IsKeyDown(Keys.Up)))
            {
                joueur.SensPersoY = 1;
                position.Y += joueur.SensPersoY * joueur.VitessePerso * deltaTime;

            }

            //######################################################
            //                      ANIMATION
            //######################################################

            if (joueur.SensPersoX == 1 && joueur.SensPersoY == 1 || joueur.SensPersoX == 1 && joueur.SensPersoY == -1)
                joueur._Perso.Play("right_Walk");
            else if (joueur.SensPersoX == -1 && joueur.SensPersoY == 1 || joueur.SensPersoX == -1 && joueur.SensPersoY == -1)
                joueur._Perso.Play("left_Walk");
            else if (joueur.SensPersoY == -1 && joueur.SensPersoX == 0)
                joueur._Perso.Play("up_Walk");
            else if (joueur.SensPersoY == 1 && joueur.SensPersoX == 0)
                joueur._Perso.Play("down_Walk");
            
            //######################################################
            //                      ATTAQUE
            //######################################################
            if (joueur._keyboardState.IsKeyDown(Keys.Space) && (joueur.SensPersoX == 1 && joueur.SensPersoY == 0))
                joueur._Perso.Play("right_swing");
            if (joueur._keyboardState.IsKeyDown(Keys.Space) && (joueur.SensPersoX == -1 && joueur.SensPersoY == 0))
                joueur._Perso.Play("left_swing");
            if (joueur._keyboardState.IsKeyDown(Keys.Space) && (joueur.SensPersoY == 1 && joueur.SensPersoX == 0))
                joueur._Perso.Play("down_swing");
            if (joueur._keyboardState.IsKeyDown(Keys.Space) && (joueur.SensPersoY == -1 && joueur.SensPersoX == 0))
                joueur._Perso.Play("up_swing");
        }
    }
}
