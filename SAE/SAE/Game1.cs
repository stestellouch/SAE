using Microsoft.Xna.Framework;
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
        private player mc1;
        private player mc2;
        public AnimatedSprite _Perso;



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
            player mc1 = new player(1, "joueur 1");


            
            _graphics.PreferredBackBufferHeight = 1050;
            _graphics.PreferredBackBufferWidth = 1680;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            _tiledMap = Content.Load<TiledMap>("Tile/Test");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet SpriteMC = Content.Load<SpriteSheet>("Animation/MC.sf", new JsonContentLoader());
            /*SpriteSheet SpriteMC2A = Content.Load<SpriteSheet>("Animation/perso_bleu/perso_bleu_attack.sf", new JsonContentLoader());
            SpriteSheet SpriteMC2M = Content.Load<SpriteSheet>("Animation/perso_bleu/perso_bleu_marche.sf", new JsonContentLoader());
            SpriteSheet SpriteMC3A = Content.Load<SpriteSheet>("Animation/perso_violet/perso_violet_attack.sf", new JsonContentLoader());
            SpriteSheet SpriteMC3M = Content.Load<SpriteSheet>("Animation/perso_violet/perso_violet_marche.sf", new JsonContentLoader());*/
            mc1._Perso = new AnimatedSprite(SpriteMC);
            //mc2.Perso = new AnimatedSprite(SpriteMC2A);
            //mc2.Perso = new AnimatedSprite(SpriteMC2M);
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            _tiledMapRenderer.Update(gameTime);
            player.DeplacementMC(mc1, gameTime);
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();
            _spriteBatch.Draw(mc1._Perso, mc1.PositionPerso);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}