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
    public static class Camera
    {

        //Création du champ camera et sa position
        public static OrthographicCamera _camera;
        public static Vector2 _cameraPosition;

        public static void Initialize(BoxingViewportAdapter viewportadapter)
        {
            //Initialisation de la caméra présente dans Monogame.Extended
            _camera = new OrthographicCamera(viewportadapter);
            //La position de la caméra doit être égale à celle du personnage afin de le laisser au milieu constamment
            _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);
            //Reglage du Zoom de la camera en float
            _camera.ZoomIn(1.5f); 
        }

        public static void Update(GameTime gameTime)
        {
            //La camera suit le perso de partout cependant quand on se rapproche de la bordure de la map on voit du bleu claire
            //Afin de ne pas voir cette partie nous devons définir des limites pour la camera

            //Pour que la camera suivent le perso même si il peut pas tout "voir" peu importe l'endroit
            _cameraPosition = new Vector2(Perso._positionPerso.X, Perso._positionPerso.Y);

            //pour le haut
            if (Perso._positionPerso.Y < Game1._screenHeight / 5)
                _cameraPosition.Y = Game1._screenHeight / 5;
            //pour le hautbas
            if (Perso._positionPerso.Y > (World._mapHeight - Game1._screenHeight / 5))
                _cameraPosition.Y = (World._mapHeight - Game1._screenHeight / 5);
            //pour l'angle a droite
            if (Perso._positionPerso.X > (World._mapWidth - Game1._screenWidth / 5))
                _cameraPosition.X = (World._mapWidth - Game1._screenWidth / 5);
            //pour l'angle a gauche
            if (Perso._positionPerso.X < Game1._screenWidth / 5)
                _cameraPosition.X = Game1._screenWidth / 5;
             _camera.LookAt(_cameraPosition);

        }
    }
}
