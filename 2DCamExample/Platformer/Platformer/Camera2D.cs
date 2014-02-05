using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Platformer
{
    /// <summary>
    /// A simple camera for 2D xna games.
    /// </summary>
    public class Camera2D
    {
        protected float zoom;
        protected float rotation;

        protected Matrix transform; 
        protected Vector2 position;
        
        protected int viewportWidth;
        protected int viewportHeight;

        // the cameras zoom property. Higher
        // values will make sprites appear larger
        public float Zoom
        {
            get { return zoom; }
            set 
            { 
                zoom = value; 
                // we want to ensure that the zoom
                // doesn't get too small with a simple
                // check. If we allow the zoom to be zero
                // the screen will appear blank. If we allow
                // it to be negative, sprites will be drawn
                // upside down!
                if (zoom < 0.1f) zoom = 0.1f; 
            }
        }

        // the cameras rotation property. For
        // most simple games this can be ignored,
        // but could be useful for example, in a top
        // down shooter game.
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        // the cameras position in the game.
        // The camera will be centered on this
        // point.
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// the camera constructor, sets up the camera
        /// with some default values.
        /// </summary>
        /// <param name="initialPos">the cameras starting position in the game</param>
        public Camera2D(Vector2 initialPos)
        {
            zoom = 1f;
            rotation = 0.0f;
            position = initialPos;
        }

        /// <summary>
        /// moves the camera a specified amount.
        /// </summary>
        /// <param name="amount">the vector to add to the current position.</param>
        public void Move(Vector2 amount)
        {
            position += amount;
        }
        
        /// <summary>
        /// gets the cameras transformation matrix.
        /// </summary>
        /// <param name="graphicsDevice">the graphics device.</param>
        /// <returns>the transformation matrix</returns>
        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            viewportWidth = graphicsDevice.Viewport.Width;
            viewportHeight = graphicsDevice.Viewport.Height;

            // we create a transformation matrix using the various properties of
            // of the camera. This matrix positions, rotates and scales everything in
            // the game world respective to the cameras properties, which gives us the illusion
            // that we have a camera that can move around, rotate and zoom in on our game world
            transform = Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(viewportWidth * 0.5f, viewportHeight * 0.5f, 0));
            return transform;
        }

        /// <summary>
        /// Inverts the cameras transformation matrix.
        /// </summary>
        /// <returns>the inverse transformation matrix</returns>
        public Matrix InverseTransform()
        {
            //this is useful for when we want to negate
            //the cameras transformation matrix. If we
            //have a game that requires mouse input as well 
            //as a camera, then we would need this method 
            //to ensure that the mouses position is correct.
            //You would call this method as follows:
            //Vector2 mousePos = new Vector2(mousestate.X, mousestate.Y);
            //mousePos = Vector2.Transform(mousePos, ContactGame.camera.GetInverse()));
            //which would ensure the mouse position in the game is correct.
            
            Matrix inverse = Matrix.Invert(transform);
            return inverse;
        }
    }
}

