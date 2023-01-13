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
using Microsoft.Xna.Framework.Audio;

namespace SAE
{
    internal class Perso
    {
        //Création des variables utiles pour le personnage
        public static Vector2 _positionPerso;
        public static AnimatedSprite _Perso;
        public static int _sensPersoX;
        public static int _sensPersoY;
        public static float _vitessePerso;
        public static string _sens;
        public static KeyboardState keyboardState;
        public static Vector2 _resetPosition;
        public static int _viePerso;
        public static Vector2 _positionVie;
        public static Rectangle _colisionPerso;
        public static bool _estEnViePerso;

        //création score du perso
        public static int _score;
        public static Vector2 _positionScore;
        public static SpriteFont _police;

        //son attaque perso
        public static SoundEffect _sonAttaque;

        public static void Initialize()
        {
            //Initialisation des variables
            _positionPerso = new Vector2(1300,1300);
            _sensPersoY = 0;
            _sensPersoX = 0;
            _vitessePerso = 100;
            _viePerso = 100;
            _sens = "nothing";
            _estEnViePerso = true;
            _resetPosition = new Vector2(50, 50);
            _colisionPerso = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, 32 ,32);
            _score = 0;
            
        }
        public static void LoadContent(Game game)
        {
             //Chargement des textures
             SpriteSheet SpriteMC = game.Content.Load<SpriteSheet>("Animation/nouveau_perso/perso.sf", new JsonContentLoader());
             _Perso = new AnimatedSprite(SpriteMC);

             _police = game.Content.Load<SpriteFont>("Font");

            _sonAttaque = game.Content.Load<SoundEffect>("HP5TWBW-sword");

        }
        public static void Update(GameTime gameTime)
        {
            _sensPersoX = 0;
            _sensPersoY = 0;
            
            //Position des textes
            _positionScore = new Vector2(Camera._cameraPosition.X + 100, Camera._cameraPosition.Y + 130);
            _positionVie = new Vector2(Camera._cameraPosition.X - 280, Camera._cameraPosition.Y + 130);

            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * Perso._vitessePerso; // Vitesse de déplacement du sprite
            
            
            _Perso.Update(deltaTime);
            
            keyboardState = Keyboard.GetState();
            String animation = "none";
            
            //Si on appuie sur aucune touche le personnage ne bouge pas
            if (!(keyboardState.IsKeyDown(Keys.Z) || keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Q)))
                animation = "idle";

            
            //Attribution des touches pour deplacer le personnage seulement si on attaque pas
            if (!(keyboardState.IsKeyDown(Keys.Space)) && animation != "idle")
            {
                //Pour se deplacer vers le haut
                if (keyboardState.IsKeyDown(Keys.Z))
                {
                    ushort tx = (ushort)(Perso._positionPerso.X / World._tiledMap.TileWidth);
                    ushort ty = (ushort)(Perso._positionPerso.Y / World._tiledMap.TileHeight);
                    animation = "haut";
                    if (!IsCollision(tx, ty))
                    {
                        _positionPerso.Y -= walkSpeed;
                        _sens = "haut";
                        _Perso.Play("up_Walk");
                    }
                }
                
                //Pour se deplacer vers le bas
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    ushort tx = (ushort)(_positionPerso.X / World._tiledMap.TileWidth);
                    ushort ty = (ushort)(_positionPerso.Y / World._tiledMap.TileHeight + 1);
                    animation = "bas";
                    if (!IsCollision(tx, ty))
                    {
                        _positionPerso.Y += walkSpeed;
                        _sens = "bas";
                        _Perso.Play("down_Walk");
                    }
                }
                
                //Pour se deplacer vers la droite
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    ushort tx = (ushort)(_positionPerso.X / World._tiledMap.TileWidth + 1);
                    ushort ty = (ushort)(_positionPerso.Y / World._tiledMap.TileHeight);
                    animation = "droite";
                    if (!IsCollision(tx, ty))
                    {
                        _positionPerso.X += walkSpeed;
                        _sens = "droite";
                        _Perso.Play("right_Walk");
                    }
                }
                
                //Pour se deplacer vers la gauche
                else if (keyboardState.IsKeyDown(Keys.Q))
                {
                    ushort tx = (ushort)(_positionPerso.X / World._tiledMap.TileWidth);
                    ushort ty = (ushort)(_positionPerso.Y / World._tiledMap.TileHeight);
                    animation = "gauche";
                    if (!IsCollision(tx, ty))
                    {
                        _positionPerso.X -= walkSpeed;
                        _sens = "gauche";
                        _Perso.Play("left_Walk");
                    }
                }
                
                //Deplacer le rectangle servant aux collision là où le personnage se trouve
                _colisionPerso.X = (int)_positionPerso.X;
                _colisionPerso.Y = (int)_positionPerso.Y;
            }
            if (keyboardState.IsKeyDown(Keys.Tab))
                _positionPerso = _resetPosition;
            
            
            //######################################################
            //                      ANIMATION
            //######################################################
            
            //Définir le sens du coup d'épée
            if (animation == "idle" && keyboardState.IsKeyDown(Keys.Space))
            {
                //Si la dernière position est vers le haut le coup d'épée sera vers le haut
                if (_sens == "haut")
                {
                    _Perso.Play("up_swing");
                }

                //Si la dernière position est vers le bas le coup d'épée sera vers le haut
                else if (_sens == "bas")
                {
                    _Perso.Play("down_swing");
                }

                //Si la dernière position est vers la droite le coup d'épée sera vers le haut
                else if (_sens == "droite")
                {
                    _Perso.Play("right_swing");
                }

                //Si la dernière position est vers la gauche le coup d'épée sera vers le haut
                else if (_sens == "gauche")
                {
                    _Perso.Play("left_swing");
                }
            }

            //Définir en tant que mort si sa vie atteint 0
            if (_viePerso <= 0)
                _estEnViePerso = false;

        }
        public static void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_Perso, _positionPerso);
            _spriteBatch.DrawString(_police, $"Score : {(int)_score}", _positionScore, Color.White);
            _spriteBatch.DrawString(_police, $"Vie : {(int)_viePerso}", _positionVie, Color.White);
        }
        public static bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (World.mapLayer.TryGetTile(x, y, out tile) == false)
            {
                return false;

            }
            if (!tile.Value.IsBlank)
            {
                return true;
                
            }
            return false;
        }
    }
}
