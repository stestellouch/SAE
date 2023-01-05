using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAE;
using Microsoft.Xna.Framework;

namespace SAE
{
    public class Camera
    {
        private float _zoom;
        private bool _isViewTransformationDirty = true;
        private Vector2 _position;
        private Vector3 _camTranslationVector = Vector3.Zero;
        private Matrix _camTranslationMatrix = Matrix.Identity;
        private float _rotation;
        private Matrix _camRotationMatrix = Matrix.Identity;
        private Vector3 _camScaleVector = Vector3.Zero;
        private Matrix _camScaleMatrix = Matrix.Identity;
        private Vector3 _resTranslationVector = Vector3.Zero;
        private GraphicsDeviceManager _graphics;
        private Matrix _resTranslationMatrix = Matrix.Identity;
        private Matrix _transform = Matrix.Identity;


        public float Zoom
        {

            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < 0.1f)
                {
                    _zoom = 0.1f;
                }
                _isViewTransformationDirty = true;
            }

        }
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _isViewTransformationDirty = true;
            }

        }

        public void SetPosition(Vector2 position)
        {

            Position = position;

        }
        public Matrix GetViewTransformationMatrix()
        {
            if (_isViewTransformationDirty)
            {
                _camTranslationVector.X = -_position.X;
                _camTranslationVector.Y = -_position.Y;

                Matrix.CreateTranslation(ref _camTranslationVector, out _camTranslationMatrix);
                Matrix.CreateRotationZ(_rotation, out _camRotationMatrix);

                _camScaleVector.X = _zoom;
                _camScaleVector.Y = _zoom;
                _camScaleVector.Z = 1;

                Matrix.CreateScale(ref _camScaleVector, out _camScaleMatrix);

                _resTranslationVector.X = _graphics.PreferredBackBufferWidth * 0.5f;
                _resTranslationVector.Y = _graphics.PreferredBackBufferHeight * 0.5f;
                _resTranslationVector.Z = 0;

                Matrix.CreateTranslation(ref _resTranslationVector, out _resTranslationMatrix);

                   _transform  = _camTranslationMatrix * _camRotationMatrix * _camScaleMatrix * _resTranslationMatrix;
                _isViewTransformationDirty = false;

            }
            return _transform;

        }
    }
}
