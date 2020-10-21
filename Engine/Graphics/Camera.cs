using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cataclysm.Graphics
{
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

        private float zoom = 3.5f;
        private float rotation = 0;

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

        public float Rotation {
            get { return rotation; }
            set { rotation = value; }
        }

        public Camera() {
            if (Utilities.mainCamera == null)
                Utilities.mainCamera = this;
        }

        public void Update(Vector2 Position) {
            //Console.WriteLine("Viewport: " + viewPort.ToString());
            center = Position;
            transform = Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(viewPort.Width / 2, viewPort.Height / 2, 0));
        }

    }
}
