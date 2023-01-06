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
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        public static int _mapWidth;
        public static int _mapHeight;

        public void Initialize()
        {
            _mapWidth = 0;
            _mapHeight = 0;

        }
        public void LoadContent(Game game)
        {
            _tiledMap = game.Content.Load<TiledMap>("Tile/Test");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);
        }
        public void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);


        }
        public void Draw()
        {
            _tiledMapRenderer.Draw();

        }


    }
}
