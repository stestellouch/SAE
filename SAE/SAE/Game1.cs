﻿using Microsoft.Xna.Framework;
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
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

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

        //création son
        public  Song _musique;

        //taille écran pour caméra
        public static int ScreenHeight;
        public static int ScreenWidth;

        private KeyboardState keyboardState, lastKeyboardState;

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

            //création de la map, appel à Initialize de la classe World
            World.Initialize();


            //création de la caméra
            BoxingViewportAdapter viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, ScreenWidth, ScreenHeight);
            Camera.Initialize(viewportadapter);


            //jouer le son son en boucle
            MediaPlayer.IsRepeating = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //appel au loadcontent des deux classes
            World.LoadContent(this);
            Perso.LoadContent(this);

            //load de la musique
            _musique = Content.Load<Song>("Caketown 1");
            //Jouer la musique
            MediaPlayer.Play(_musique);


        }

        protected override void Update(GameTime gameTime)
        {

            //compteur temps jeux 
            tempsJeux += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Si temps de jeux est de 30 secondes alors les enemies spawn à un rythme de 1 secondes plus vite
            //Note : seulement jusqu'à 3 secondes car après ça irait trop vite, trop de monstres
            if(tempsJeux %30 == 0)
            {
                if(tempsCreationEnemie >= 3)
                {
                    tempsCreationEnemie -= 1;

                }
            }

            //Pour sortir du jeux plus facillement
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            World.Update(gameTime);

            //Quand on appuie sur P ça met la musique en mute (sur pause)
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.P) && lastKeyboardState.IsKeyUp(Keys.P))
            {
                // P button has been pressed - change MediaPlayer state
                if (MediaPlayer.State == MediaState.Paused) // Paused to playing
                    MediaPlayer.Resume();
                else if (MediaPlayer.State == MediaState.Playing) // Playing to paused
                    MediaPlayer.Pause();
            }
            lastKeyboardState = keyboardState;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();


            //appel à l'update des deux classes
            Perso.Update(gameTime); //A besoin d'être constamment update pour bouger le perso et faire les animations
            Camera.Update(gameTime); //A besoin d'être constamment update pour suivre le perso et donc changer ce qu'on voit

            
            //Appelle a LoadEnemies pour créer les enemies en utilisant une boucle grâce à la liste
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds; //Le compteur pour spawn (utilisé dans LoadEnemies)
            foreach (Enemies enemy in enemies) //Création d'une variable locale pour représenter chaque monstre présent dans la classe Enemies et donc la liste enemies
                enemy.Update(_graphics.GraphicsDevice, gameTime);
            LoadEnemies();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //création de la matrice afin d'ajuster la caméra pour l'appel draw
            Matrix transformMatrix = Camera._camera.GetViewMatrix();

            //affichage avec caméra avec la matrice comme paramètre pour ce qui nécessite d'être zoomer : la map
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            World.Draw(transformMatrix);
            Perso.Draw(_spriteBatch);

            //Appelle a Draw pour dessiner les enemies en utilisant une boucle grâce à la liste (suite du LoadEnemies)
            foreach (Enemies enemy in enemies)
                if (enemy._estEnVie)
                    enemy.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public void LoadEnemies()
        {

            //Création des positions random des monstres
            int randY = random.Next(0, ScreenHeight);
            int randX = random.Next(0, ScreenWidth);
            int vie = 50;
            bool enVie = true;
            if (spawn >= tempsCreationEnemie)
            {
                spawn = 0; //On remet spawn à 0 pour remonter jusque 10 etc.
                
                enemies.Add(new Enemies(Content.Load<Texture2D>("Animation/sprite_0"), new Vector2(randX, randY), vie, enVie));
                
            }
            
        }
    }
}