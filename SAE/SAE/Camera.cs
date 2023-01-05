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
        //public Matrix GetViewTransformationMatrix()
        //{
        //    if (_isViewTransformationDirty)
        //    {
        //        _camTranslationVector.X = -_position.X;
        //        _camTranslationVector.Y = -_position.Y;

        //        Matrix.CreateTranslation(ref _camTranslationVector, out _camTranslationMatrix);
        //        Matrix.CreateRotationZ(_rotation, out _camRotationMatrix);

        //        _camScaleVector.X = _zoom;
        //        _camScaleVector.Y = _zoom;
        //        _camScaleVector.Z = 1;

        //        Matrix.CreateScale(ref _camScaleVector, out _camScaleMatrix);

        //        _resTranslationVector.X = ResolutionIndependentRenderer.VirtualWidth * 0.5f;
        //        _resTranslationVector.Y = ResolutionIndependentRenderer.VirtualHeight * 0.5f;
        //        _resTranslationVector.Z = 0;

        //        Matrix.CreateTranslation(ref _resTranslationVector, out _resTranslationMatrix);

        //        _transform = _camTranslationMatrix * _camRotationMatrix * _camScaleMatrix * _resTranslationMatrix * ResolutionIndependentRenderer.GetTransformationMatrix();
        //        _isViewTransformationDirty = false;

        //    }
        //    return _transform;

        //}
    }
}
