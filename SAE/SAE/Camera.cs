using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAE;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace SAE
{
    public class Camera
    {
        public static OrthographicCamera _camera;
        public static Vector2 _cameraPosition;

        //public static void Initialise(BoxingViewportAdapter viewportadapter)
        //{
        //    _camera = new OrthographicCamera(viewportadapter);
        //    _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);
        //    _camera.ZoomIn(1.5f);
        //}

        //public static void Update()
        //{
        //    //exterieur
        //    _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);

        //    //angle a droite
        //    if (Perso._positionPerso.X > (Map._mapWidth - Game1.ScreenWidth / 5))
        //        _cameraPosition.X = (Map._mapWidth - Game1.ScreenWidth / 5);
        //    //angle a gauche
        //    if (Perso._positionPerso.X < Game1.ScreenWidth / 5)
        //        _cameraPosition.X = Game1.ScreenWidth / 5;
        //    //en haut
        //    if (Perso._positionPerso.Y < Game1.ScreenHeight / 5)
        //        _cameraPosition.Y = Game1.ScreenHeight / 5;
        //    //en bas
        //    if (Perso._positionPerso.Y > (Map._mapHeight - Game1.ScreenHeight / 5))
        //        _cameraPosition.Y = (Map._mapHeight - Game1.ScreenHeight / 5);

        //    _camera.LookAt(_cameraPosition);

        //}
    }
}
