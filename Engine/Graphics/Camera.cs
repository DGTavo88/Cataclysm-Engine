using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cataclysm.Graphics
{
    //[Camera Class]
    //This class contains the standard functionality for a Camera.
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform {
            get {
                return transform;
            }
        }

        private Vector2 center;
        private Viewport viewPort = Utilities.mainGraphicsManager.GraphicsDevice.Viewport;

        private float zoom = 1f; //3.5f
        private float Rotation = 0;

        public float X {
            get { return center.X; }
            set { center.X = value; }
        }

        public float Y {
            get { return center.Y; }
            set { center.Y = value; }
        }

        public float Zoom {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) { zoom = 0.1f; } }
        }


        public Camera() {
            if (Utilities.mainCamera == null)
                Utilities.mainCamera = this;
        }

        public void Update(Vector2 Position) {
            center = Position;
            transform = Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(viewPort.Width / 2, viewPort.Height / 2, 0));
        }

    }
}
