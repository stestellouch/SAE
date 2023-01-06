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
        private Perso mainCharacter1;
        
        

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

            mainCharacter1 = new Perso();
            mainCharacter1.Initialize();
           
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

            mainCharacter1.LoadContent(this);
            
            _tiledMap = Content.Load<TiledMap>("Tile/Test");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _tiledMapRenderer.Update(gameTime);
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();

            mainCharacter1.Update(gameTime);

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
            mainCharacter1.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}