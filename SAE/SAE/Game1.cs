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
        //public static float deltaSeconds;
        //Création enemies
        List<Enemies> enemies = new List<Enemies>();
        Random random = new Random();
        //Créations des différents compteurs
        float spawn = 0;
        float tempsJeux = 0;
        public int tempsCreationEnemie = 10;


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
            ScreenWidth = _graphics.PreferredBackBufferWidth;
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

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {

            //compteur temps jeux 
            tempsJeux += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(tempsJeux %30 == 0)
            {
                if(tempsCreationEnemie >= 3)
                {
                    tempsCreationEnemie -= 1;

                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            World.Update(gameTime);           
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();

            Perso.Update(gameTime);
            Camera.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();
            
            //Appelle a LoadEnemies pour créer les enemies
            
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Enemies enemy in enemies)
                enemy.Update(_graphics.GraphicsDevice, gameTime);
            LoadEnemies();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = Camera._camera.GetViewMatrix();

            //affichage avec caméra
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            World.Draw(transformMatrix);
            Perso.Draw(_spriteBatch);

            foreach (Enemies enemy in enemies)
                enemy.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void LoadEnemies()
        {
            int randY = random.Next(0, ScreenHeight);
            int randX = random.Next(0, ScreenWidth);
            if (spawn >= tempsCreationEnemie)
            {
                spawn = 0;
                //if (enemies.Count() < 4)
                //{
                    enemies.Add(new Enemies(Content.Load<Texture2D>("Animation/fantome"), new Vector2(randX, randY)));
                //}
            }
            for (int i = 0; i < enemies.Count(); i++)
            {
                if (!enemies[i].isVisible)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}