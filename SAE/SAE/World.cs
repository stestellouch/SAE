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
    internal class World
    {
        //Création champs utiles pour la map
        public static TiledMap _tiledMap;
        public static TiledMapRenderer _tiledMapRenderer;
        public static int _mapWidth;
        public static int _mapHeight;
        //Champ pour le calque obstacles
        public static TiledMapTileLayer mapLayer;


        public static void Initialize()
        {
            //100 fois des tuiles de 16 
            _mapWidth = 1600;
            _mapHeight = 1600;

        }
        public static void LoadContent(Game game)
        {
            //Chargement des différents calques : sol et obstacles
            _tiledMap = game.Content.Load<TiledMap>("Tile/Test");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
        }
        public static void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);


        }
        public static void Draw(Matrix transformMatrix)
        {
            //Draw de la map en faisant appel à la matrice afin de pouvoir dessiner avec le zoom
            _tiledMapRenderer.Draw(transformMatrix);

        }


    }
}
