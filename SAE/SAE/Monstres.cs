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
    internal class Monstres
    {
        private static Vector2 _positionMonstre;
        private static AnimatedSprite _Monstre;
        private static int _sensMonstreX;
        private static int _sensMonstreY;
        private static int _vitesseMonstre;
        private static string _sensMonstre;
        long spawnTime;
        bool finish;

        private void SpawnMonstres()
        {
            Sprite monstre = new Sprite(this);

            Random rand = new Random();
            if (rand.Next(3) % 2 == 0)
                monstre.Content.Load<Sprite>("Tile/free_monsters.png");
            else
                monstre.Content.Load<Sprite>("Tile/free_monsters.png");

            int posX = rand.Next(Game1.ScreenWidth - monstre.Width);
            int posY = rand.Next(Game1.ScreenHeight - monstre.Height);

            if (rand.Next(6) % 5 == 0)
                robot.Speed = new Vector2(0, 4);
            else
                robot.Speed = new Vector2(0, 2);

            Monstres.Add(monstre);
        }
        protected override void Initialize()
        {
            // Autre initialisations
            Monstres.Clear();
            spawnTime = 0;
            // Determine si le jeu doit être réinitialisé
            finish = false;
        }

    }
}
