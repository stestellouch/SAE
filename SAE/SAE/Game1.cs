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

namespace SAE
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        
        public  KeyboardState _keyboardState;
        public static float deltaSeconds;
        //public Monstres monstres = new Monstres();
        

       




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
            
            //création de liste des sprite
            

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
            //Monstres.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        


    }
}