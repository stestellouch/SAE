﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Collections.Generic;


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
        private Vector2 _positionMC;
        private AnimatedSprite _MC;
        private int _sensXMC;
        private int _sensYMC;
        private int _vitesseMC;
        private Camera _camera;

        //taille écran pour caméra
        public static int ScreenHeight;
        public static int ScreenWidth;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _positionMC = new Vector2(0, 0);
            _sensYMC = 0;
            _sensXMC = 0;
            _vitesseMC = 100;
            _camera = new Camera();
            _camera.Zoom = 10f;
            _camera.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            _graphics.PreferredBackBufferHeight = 1920;
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            _tiledMap = Content.Load<TiledMap>("Tile/Test");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet SpriteMC = Content.Load<SpriteSheet>("Animation/MC.sf", new JsonContentLoader());
            _MC = new AnimatedSprite(SpriteMC);
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            _tiledMapRenderer.Update(gameTime);
            _MC.Update(deltaSeconds);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();
            //Si la touche droite est pressé
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _sensXMC = 1;
                _sensYMC = 0;
                _positionMC.X += _sensXMC * _vitesseMC * deltaTime;

            }
            //Si la touche gauche est pressé
            if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                _sensXMC = -1;
                _sensYMC = 0;
                _positionMC.X += _sensXMC * _vitesseMC * deltaTime;

            }
            //Si la touche haut est pressé
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _sensYMC = -1;
                _sensXMC = 0;
                _positionMC.Y += _sensYMC * _vitesseMC * deltaTime;

            }
            //Si la touche bas est pressé
            if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                _sensYMC = 1;
                _sensXMC = 0;
                _positionMC.Y += _sensYMC * _vitesseMC * deltaTime;

            }
            //######################################################
            //                      ANIMATION
            //######################################################

            if (_sensXMC == 1 && _sensYMC == 0)
                _MC.Play("right_Walk");
            else if (_sensXMC == -1 && _sensYMC == 0)
                _MC.Play("left_Walk");
            else if (_sensYMC == -1 && _sensXMC == 0)
                _MC.Play("up_Walk");
            else if (_sensYMC == 1 && _sensXMC == 0)
                _MC.Play("down_Walk");
            if (_sensXMC == 0 && _sensYMC == 0)
                _MC.Play("idle");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_MC, _positionMC);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}