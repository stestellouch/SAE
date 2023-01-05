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
    }
}
