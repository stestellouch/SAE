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
        
        private KeyboardState _keyboardState;
        private float deltaSeconds;
        private Perso mainCharacter1;
        private Perso mainCharacter2;
        private int _num;
        private Camera _camera;
        private World _world;




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
            _num = 0;
            mainCharacter1 = new Perso();
            mainCharacter1.Initialize();
            _num += 1;
            mainCharacter2 = new Perso();
            mainCharacter2.Initialize();
           
            _graphics.PreferredBackBufferHeight = 1050;
            ScreenHeight= _graphics.PreferredBackBufferHeight;
            _graphics.PreferredBackBufferWidth = 1680;
            ScreenWidth = _graphics.PreferredBackBufferHeight;
            _graphics.ApplyChanges();
            _camera = new Camera();
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, ScreenWidth, ScreenHeight);
            _camera.Initialize(viewportadapter);
            _world = new World();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _world.LoadContent(this);
            mainCharacter1.LoadContent(this, _num);
            mainCharacter2.LoadContent(this, _num);
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _world.Update(gameTime);           
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();

            mainCharacter1.Update(gameTime, _num);
            mainCharacter2.Update(gameTime, _num);
            _camera.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = Camera._camera.GetViewMatrix();

            //affichage avec caméra
            //_spriteBatch.Begin();
            _world.Draw();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            mainCharacter1.Draw(_spriteBatch);
            mainCharacter2.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}