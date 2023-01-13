using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SAE
{
    public class Enemies
    {
        public Texture2D _textureEnemy;
        public Texture2D _texture;
        public Vector2 _positionMonstre;
        public int _vieMonstre;
        public bool _estEnVie;
        public Rectangle collision;
        public float _compteurAttaqueMonstre;
        public float _compteurAttaquePerso;
        public bool _attaqueMonstre;
        public bool _attaquePerso;

        public bool isVisible = true;


        public Enemies(Texture2D newTexture, Vector2 newPosition, int vie, bool enVie)
        {
            _textureEnemy = newTexture;
            _texture = newTexture;
            _positionMonstre = newPosition;
            _vieMonstre = vie;
            _estEnVie = enVie;

            _compteurAttaqueMonstre = 0;
            _compteurAttaquePerso = 0;

            collision = new Rectangle((int)this._positionMonstre.X, (int)this._positionMonstre.Y, 40, 25);
        }


        public void Update(GraphicsDevice graphics, GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _compteurAttaquePerso += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _compteurAttaqueMonstre += (float)gameTime.ElapsedGameTime.TotalSeconds;


            float walkSpeed = (float)(deltaTime * (Perso._vitessePerso - 30));
            //Si le monstre est à droite du personnage
            if (this._positionMonstre.X+25 >= Perso._positionPerso.X && _estEnVie == true)
            {
                this._positionMonstre.X -= walkSpeed;
            }
            //Si le monstre est à gauche du personnage
            if (this._positionMonstre.X+25 <= Perso._positionPerso.X && _estEnVie == true)
            {
                this._positionMonstre.X += walkSpeed;
            }
            //Si le monstre est au dessous du personnage
            if (this._positionMonstre.Y+25 >= Perso._positionPerso.Y && _estEnVie == true)
            {
                this._positionMonstre.Y -= walkSpeed;
            }
            //Si le monstre est au dessus du personnage            
            if (this._positionMonstre.Y+25 <= Perso._positionPerso.Y && _estEnVie == true)
            {
                this._positionMonstre.Y += walkSpeed;
            }

            this.collision.X = (int)this._positionMonstre.X+25;
            this.collision.Y = (int)this._positionMonstre.Y+35;

            ///////////////////////////////////////////////
            ///                Degats                   ///
            ///////////////////////////////////////////////
            
            //Augmente la collision entre notre personnage et le monstre lorsque l'on appuie sur la touche espace
            //de façon à ce que l'on puisse attaquer le monstre sans que l'on reçoit forcement des dégâts.
            if (((Perso._colisionPerso.X -45)<= (this.collision.X ) && (Perso._colisionPerso.X+64 ) >= (this.collision.X)
                && (Perso._colisionPerso.Y -30) <= (this.collision.Y ) && (Perso._colisionPerso.Y+64 ) >= (this.collision.Y)
                && Perso.keyboardState.IsKeyDown(Keys.Space)) && _attaquePerso == true && this._estEnVie == true)
            {
                this._vieMonstre -= 50;
                
                _attaquePerso = false;
                _compteurAttaquePerso = 0;
                Perso._score = Perso._score + 10;
                Perso._sonAttaque.Play();
                Console.WriteLine(_vieMonstre);
            }
            if(_compteurAttaquePerso > 2)
            {
                _attaquePerso = true;
            }

            //Permet de détecter la collision entre le personnage et le monstre lorsque l'on appuie sur espace
            //afin de prendre des dégâts
            if (this.collision.Intersects(Perso._colisionPerso) && this._estEnVie == true && this._attaqueMonstre == true)
            {
                
                this._compteurAttaqueMonstre = 0;
                this._attaqueMonstre = false;
                Perso._viePerso -= 10;
                Console.WriteLine(Perso._viePerso);
                
            }
            //_compteurAttaque un chrono qui se réinitialise à 0 lorsqu’un monstre attaque. Lorsqu’il passe au-dessus de 5 soit 5 secondes, il remet _attaqueMonstre à true.
            //_attaqueMonstre true passe à false lorsque le monstre a frappé ce qui lui empêche de frapper, il permet donc de déterminer si le monstre peut attaquer ou non. 
            if (this._compteurAttaqueMonstre >5)
            {
                this._attaqueMonstre = true;
            }
           
            //Si le monstre n'a plus de vie il rentre donc dans l'état false de _estEnVie
            if (this._vieMonstre <= 0)
            {
                this._estEnVie = false;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _positionMonstre, Color.White);
        }

    }

    
}
