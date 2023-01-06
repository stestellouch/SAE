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
        public static TiledMap _tiledMap;
        public static TiledMapRenderer _tiledMapRenderer;
        public static int _mapWidth;
        public static int _mapHeight;

        public static void Initialize()
        {
            _mapWidth = 1600;
            _mapHeight = 1600;

        }
        public static void LoadContent(Game game)
        {
            _tiledMap = game.Content.Load<TiledMap>("Tile/Test");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);
        }
        public static void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);


        }
        public static void Draw(Matrix transformMatrix)
        {
            _tiledMapRenderer.Draw(transformMatrix);

        }


    }
}
