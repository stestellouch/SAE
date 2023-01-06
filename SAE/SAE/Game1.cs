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


namespace SAE
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private KeyboardState _keyboardState;
        private float deltaSeconds;
        private Vector2 _positionPerso;
        private AnimatedSprite _Perso;
        private int _sensPersoX;
        private int _sensPersoY;
        private int _vitessePerso;
        

        //taille écran pour caméra
        public int ScreenHeight;
        public int ScreenWidth;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _positionPerso = new Vector2(0, 0);
            _sensPersoY = 0;
            _sensPersoX = 0;
            _vitessePerso = 100;


            
            _graphics.PreferredBackBufferHeight = 1050;
            ScreenHeight= _graphics.PreferredBackBufferHeight;
            _graphics.PreferredBackBufferWidth = 1680;
            ScreenWidth = _graphics.PreferredBackBufferHeight;
            _graphics.ApplyChanges();

            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            _tiledMap = Content.Load<TiledMap>("Tile/Test");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet SpriteMC = Content.Load<SpriteSheet>("Animation/MC.sf", new JsonContentLoader());
            _Perso = new AnimatedSprite(SpriteMC);
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _sensPersoX = 0;
            _sensPersoY = 0;

            _tiledMapRenderer.Update(gameTime);
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _Perso.Update(deltaTime);
            _keyboardState = Keyboard.GetState();
            //Si la touche droite est pressé
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _sensPersoX = 1;
                _sensPersoY = 0;
                _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;

            }
            //Si la touche gauche est pressé
            if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                _sensPersoX = -1;
                _sensPersoY = 0;
                _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;

            }
            //Si la touche haut est pressé
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _sensPersoY = -1;
                _sensPersoX = 0;
                _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;

            }
            //Si la touche bas est pressé
            if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                _sensPersoY = 1;
                _sensPersoX = 0;
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
                _Perso.Play("idle_down");
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





            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = Camera._camera.GetViewMatrix();

            //affichage avec caméra
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_Perso, _positionPerso);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}