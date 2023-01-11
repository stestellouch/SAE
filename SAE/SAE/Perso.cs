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
using Microsoft.Xna.Framework.Media;

namespace SAE
{
    internal class Perso
    {
        public static Vector2 _positionPerso;
        public static AnimatedSprite _Perso;
        public static int _sensPersoX;
        public static int _sensPersoY;
        public static float _vitessePerso;
        public static string _sens;
        public static KeyboardState _keyboardState;
        public static Vector2 _resetPosition;


        public Perso()
        {


        }

        public static void Initialize()
        {
            _positionPerso = new Vector2(50,50);
            _sensPersoY = 0;
            _sensPersoX = 0;
            _vitessePerso = 100;
            _sens = "nothing";
            _resetPosition = new Vector2(50, 50);
        }
        public static void LoadContent(Game game)
        {
           
             SpriteSheet SpriteMC = game.Content.Load<SpriteSheet>("Animation/nouveau_perso/perso.sf", new JsonContentLoader());
             _Perso = new AnimatedSprite(SpriteMC);
                
        }
        public static void Update(GameTime gameTime)
        {
            _sensPersoX = 0;
            _sensPersoY = 0;
            

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _Perso.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            float walkSpeed = deltaTime * Perso._vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            String animation = "none";
            
            if (!(keyboardState.IsKeyDown(Keys.Z) || keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Q)))
                animation = "idle";

            if (!(keyboardState.IsKeyDown(Keys.Space)) && animation != "idle")
            {
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
            }
            if (keyboardState.IsKeyDown(Keys.Tab))
                _positionPerso = _resetPosition;
            //######################################################
            //                      ANIMATION
            //######################################################

            if (animation == "idle" && keyboardState.IsKeyDown(Keys.Space))
            {
                if (_sens == "haut") _Perso.Play("up_swing");
                else if (_sens == "bas") _Perso.Play("down_swing");
                else if (_sens == "droite") _Perso.Play("right_swing");
                else if (_sens == "gauche") _Perso.Play("left_swing");
            }
        }
        public static void Draw(SpriteBatch _spriteBatch)
        {
            
            _spriteBatch.Draw(_Perso, _positionPerso);
        
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
