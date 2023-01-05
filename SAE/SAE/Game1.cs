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


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _positionMC = new Vector2(0, 0);
            _sensYMC = 0;
            _sensXMC = 0;
            _vitesseMC = 10;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _tiledMap = Content.Load<TiledMap>("Tile/Test");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteMC = Content.Load<SpriteSheet>("Animation/MC.sf", new JsonContentLoader());
            _MC = new AnimatedSprite(spriteMC);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);
            _MC.Update(deltaSeconds);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();

            //######################################################
            //                    DEPLACEMENT
            //######################################################
            if (_sensYMC == 0 && _sensXMC == 0)
                _MC.Play("idle");

            //Si la touche droite est pressé
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _sensXMC = 1;
                _positionMC.X += _sensXMC * _vitesseMC * deltaTime;

            }
            //Si la touche gauche est pressé
            if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                _sensXMC = -1;
                _positionMC.X += _sensXMC * _vitesseMC * deltaTime;

            }
            //Si la touche haut est pressé
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _sensYMC = -1;
                _positionMC.Y += _sensYMC * _vitesseMC * deltaTime;

            }
            //Si la touche bas est pressé
            if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                _sensYMC = 1;
                _positionMC.Y += _sensYMC * _vitesseMC * deltaTime;

            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(_MC, _positionMC);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}