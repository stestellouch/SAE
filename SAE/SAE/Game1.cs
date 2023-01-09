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
using System;
using System.Linq;

namespace SAE
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        
        public  KeyboardState _keyboardState;
        public static float deltaSeconds;
        float spawn = 0;
        public TiledMapTileLayer mapLayer;
        List<Enemies> enemies = new List<Enemies>();
        Random random = new Random();




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

            //créaion du perso           
            Perso.Initialize();
                       
            //création de la taille de la fenêtre
            _graphics.PreferredBackBufferHeight = 900;
            ScreenHeight= _graphics.PreferredBackBufferHeight;
            _graphics.PreferredBackBufferWidth = 1480;
            ScreenWidth = _graphics.PreferredBackBufferHeight;
            _graphics.ApplyChanges();

            //création de la map
            World.Initialize();
            

            //création de la caméra
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, ScreenWidth, ScreenHeight);
            Camera.Initialize(viewportadapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            World.LoadContent(this);
            Perso.LoadContent(this);
            mapLayer = World._tiledMap.GetLayer<TiledMapTileLayer>("obstacles");

            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            World.Update(gameTime);           
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();

            Perso.Update(gameTime);
            
            Camera.Update(gameTime);

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds; // DeltaTime
            float walkSpeed = deltaSeconds * Perso._vitessePerso; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();
            
           
            String animation = "idle";
        
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(Perso._positionPerso.X / World._tiledMap.TileWidth);
                ushort ty = (ushort)(Perso._positionPerso.Y / World._tiledMap.TileHeight);
                animation = "walkNorth";
                if (!IsCollision(tx, ty))
                    Perso._positionPerso.Y -= walkSpeed;
                else
                    Perso._positionPerso.Y += 1;


            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(Perso._positionPerso.X / World._tiledMap.TileWidth);
                ushort ty = (ushort)(Perso._positionPerso.Y / World._tiledMap.TileHeight +1);
                animation = "walkSouth";
                if (!IsCollision(tx, ty))
                    Perso._positionPerso.Y += walkSpeed;
                else
                    Perso._positionPerso.Y -= 1;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)(Perso._positionPerso.X / World._tiledMap.TileWidth);
                ushort ty = (ushort)(Perso._positionPerso.Y / World._tiledMap.TileHeight);
                animation = "walkEast";
                if (!IsCollision(tx, ty))
                    Perso._positionPerso.X += walkSpeed;
                else
                    Perso._positionPerso.X -= 1;


            }
            else if (keyboardState.IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(Perso._positionPerso.X / World._tiledMap.TileWidth);
                ushort ty = (ushort)(Perso._positionPerso.Y / World._tiledMap.TileHeight +1);
                animation = "walkWest";
                if (!IsCollision(tx, ty))
                    Perso._positionPerso.X -= walkSpeed;
                else
                    Perso._positionPerso.X += 1;

            }

            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Enemies enemy in enemies)
                enemy.Update(_graphics.GraphicsDevice);
            LoadEnemies();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = Camera._camera.GetViewMatrix();

            //affichage avec caméra
            //_spriteBatch.Begin();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            World.Draw(transformMatrix);
            Perso.Draw(_spriteBatch);

            foreach (Enemies enemy in enemies)
                enemy.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;

        }
        public void LoadEnemies()
        {
            int randY = random.Next(1, 10);
            
            if(spawn >= 1)
            {
                spawn = 0;
                if (enemies.Count() < 4)
                    enemies.Add(new Enemies(Content.Load<Texture2D>("Animation/perso_violet/perso_violet3"), new Vector2(1,randY)));
            }
            for(int i =0; i < enemies.Count(); i++)
            {
                if(!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }


        }
        

    }
}