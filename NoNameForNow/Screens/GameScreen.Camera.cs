using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Gui;
using FlatRedBall.Input;
using FlatRedBall.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NoNameForNow.Screens
{
    public partial class GameScreen : Screen
    {
        private bool debugMode;
        private float cameraMovementSpeed = 10f;
        private float cameraRotationSpeed = 0.006f;

        MouseState originalMouseState;
        private void SetUpCamera(bool _debug)
        {
            debugMode = _debug;

            Camera.Main.UpVector = new Vector3(0, 1, 0);
            Camera.Main.CameraCullMode = FlatRedBall.Graphics.CameraCullMode.None;
            Camera.Main.FarClipPlane = 10000.0f;
            Microsoft.Xna.Framework.Input.Mouse.SetPosition(FlatRedBallServices.GraphicsDevice.Viewport.Width / 2, FlatRedBallServices.GraphicsDevice.Viewport.Height / 2);
            originalMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            if(!debugMode)
            {
                Camera.Main.AttachTo(PlayerInstance, false);
            }

        }

        private void CameraActivity()
        {
            CameraMovement();
            CameraRotation();
        }

        private void CameraMovement()
        {
            if (debugMode)
            {
                if (InputManager.Keyboard.KeyDown(Keys.W))
                {
                    SpriteManager.Camera.Position += SpriteManager.Camera.RotationMatrix.Forward * cameraMovementSpeed;
                }
                else if (InputManager.Keyboard.KeyDown(Keys.S))
                {
                    SpriteManager.Camera.Position += SpriteManager.Camera.RotationMatrix.Forward * -cameraMovementSpeed;
                }

                if (InputManager.Keyboard.KeyDown(Keys.A))
                {
                    SpriteManager.Camera.Position += SpriteManager.Camera.RotationMatrix.Right * -cameraMovementSpeed;
                }
                else if (InputManager.Keyboard.KeyDown(Keys.D))
                {
                    SpriteManager.Camera.Position += SpriteManager.Camera.RotationMatrix.Right * cameraMovementSpeed;
                }

                if (InputManager.Keyboard.KeyDown(Keys.Q))
                {
                    SpriteManager.Camera.Position += SpriteManager.Camera.RotationMatrix.Up * cameraMovementSpeed;
                }
                else if (InputManager.Keyboard.KeyDown(Keys.E))
                {
                    SpriteManager.Camera.Position += SpriteManager.Camera.RotationMatrix.Up * -cameraMovementSpeed;
                }
            }
            else
            {
                PlayerInstance.YVelocity = -100;
                Vector3 forward = PlayerInstance.RotationMatrix.Forward;
                forward.Y = 0;
                if(forward.LengthSquared() != 0)
                {
                    forward.Normalize();
                }

                if (InputManager.Keyboard.KeyDown(Keys.W))
                {
                    PlayerInstance.Position += forward * cameraMovementSpeed;
                }
                else if (InputManager.Keyboard.KeyDown(Keys.S))
                {
                    PlayerInstance.Position += forward * -cameraMovementSpeed;
                }

                if (InputManager.Keyboard.KeyDown(Keys.A))
                {
                    PlayerInstance.Position += SpriteManager.Camera.RotationMatrix.Right * -cameraMovementSpeed;
                }
                else if (InputManager.Keyboard.KeyDown(Keys.D))
                {
                    PlayerInstance.Position += SpriteManager.Camera.RotationMatrix.Right * cameraMovementSpeed;
                }
            }
        }

        private void CameraRotation()
        {
            if (debugMode)
            {
                MouseState currentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

                float xMovement = currentMouseState.X - originalMouseState.X;
                float yMovement = currentMouseState.Y - originalMouseState.Y;

                Vector3 absoluteYAxis = new Vector3(0, 1, 0);
                Camera.Main.RotationMatrix *= Matrix.CreateFromAxisAngle(absoluteYAxis, xMovement * -cameraRotationSpeed);
                Vector3 relativeXAxis = Camera.Main.RotationMatrix.Right;
                Camera.Main.RotationMatrix *= Matrix.CreateFromAxisAngle(relativeXAxis, yMovement * -cameraRotationSpeed);

                Microsoft.Xna.Framework.Input.Mouse.SetPosition(FlatRedBallServices.GraphicsDevice.Viewport.Width / 2, FlatRedBallServices.GraphicsDevice.Viewport.Height / 2);
            }
            else
            {
                MouseState currentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

                float xMovement = currentMouseState.X - originalMouseState.X;
                float yMovement = currentMouseState.Y - originalMouseState.Y;

                Vector3 absoluteYAxis = new Vector3(0, 1, 0);
                PlayerInstance.RotationMatrix *= Matrix.CreateFromAxisAngle(absoluteYAxis, xMovement * -cameraRotationSpeed);
                Vector3 relativeXAxis = PlayerInstance.RotationMatrix.Right;
                PlayerInstance.RotationMatrix *= Matrix.CreateFromAxisAngle(relativeXAxis, yMovement * -cameraRotationSpeed);

                Microsoft.Xna.Framework.Input.Mouse.SetPosition(FlatRedBallServices.GraphicsDevice.Viewport.Width / 2, FlatRedBallServices.GraphicsDevice.Viewport.Height / 2);
            }
        }
    }
}