//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using MonoGame.Extended.Content;
//using MonoGame.Extended.Serialization;
//using MonoGame.Extended.Sprites;
//using MonoGame.Extended.Tiled;
//using MonoGame.Extended.Tiled.Renderers;
//using System.Collections.Generic;
//using MonoGame.Extended;
//using MonoGame.Extended.ViewportAdapters;
//using System;

//namespace SAE
//{
//    internal class Monstres
//    {
//        private static Vector2 _positionMonstre;
//        private static AnimatedSprite _Monstre;
//        private static Texture2D _Monstre2D;
//        private static int _sensMonstreX;
//        private static int _sensMonstreY;
//        private static int _vitesseMonstre;
//        private static string _sensMonstre;
//        private static Texture2D _texture1;
//        private static Texture2D _texture2;

//        public static long spawnTime;
//        public static bool finish;

//        protected static void LoadContent(Game game)
//        {
//            _texture1 = game.Content.Load<Texture2D>("Tile/free_monsters.png");
//            _texture2 = game.Content.Load<Texture2D>("Tile/free_monsters.png");

//        }
//        protected static void Initialize()
//        {
            
//            //spawnTime = 0;
//            //// Determine si le jeu doit être réinitialisé
//            //finish = false;
//        }
//        public static void SpawnMonstres()
//        {
//            Sprite monstre = new Sprite(this);

//            Random rand = new Random();
//            if (rand.Next(1,2) == 1)
//                _Monstre2D = _texture1;
//            else
//                _Monstre2D = _texture2;

//            int posX = rand.Next(Game1.ScreenWidth );//attention ça peut sortir il faut soustraire la largeur du monstre
//            int posY = rand.Next(Game1.ScreenHeight );//pareil

//            if (rand.Next(6) % 5 == 0)
//                _vitesseMonstre = 20;
//            else
//                _vitesseMonstre = 40;

//            Monstres.Add(monstre);
//        }
        

//    }
//}
