using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OpenGLPrimitives.Camera;
using OpenGLPrimitives.Parser;
using OpenGLPrimitives.Primitives.ThreeD;
using OpenGLPrimitives.Primitives.TwoD;
using OpenGLPrimitives.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGLPrimitives
{
    public class Window : GameWindow
    {
        private List<Object> _entities;

        private readonly ICamera _camera = new FirstPersonCamera();

        private Vector2 _lastMousePos;

        private Shader _shader;

        private LightSource _light;

        public Window() : base(1280, 720, OpenTK.Graphics.GraphicsMode.Default,
            "OpenGL Primitives | Ctrl + H: Open About Window")
        {
            VSync = VSyncMode.On;
            WindowState = WindowState.Maximized;
            TargetRenderFrequency = 0;
            TargetUpdateFrequency = 0;
            var about = new AboutWindow();
            about.ShowDialog();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _shader = new Shader("Shaders/vertexShader.glsl", "Shaders/fragmentShader.glsl");
            _light = new LightSource(new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 1));

            _entities = new List<Object>
            {
                /*GameObjectFactory.FromObj("Data/cube.obj", "Data/cube.mtl", new Vector4(0, 0, 0, 1), Vector3.Zero,
                    Vector3.One)*/
            };


            /*
            for (var x = -10; x < 10; x++)
            {
                for (int z = 0; z < 20; z++)
                {
                    var y = (int) Math.Round(Math.Cos((x + z) * 0.2) * 2);

                    var obj = GameObjectFactory.FromObj("Data/cube.obj", "Data/cube.mtl", "Data/Textures",
                        new Vector4(x, y, z, 1), Vector3.Zero, Vector3.One);

                    _entities.Add(obj);
                }
            }
            */


            _entities.Add(GameObjectFactory.FromObj("Data/girl/girl.obj", "Data/girl/girl.mtl",
                "Data/girl/Textures", new Vector4(0, 3, 0, 1), Vector3.Zero, new Vector3(0.05f, 0.05f, 0.05f)));

            _entities.Add(GameObjectFactory.FromObj("Data/teapot.obj", new Vector4(0, 0, 0, 1), Vector3.Zero,
                Vector3.One));

            SetupPerspective();

            _lastMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            CursorVisible = false;
        }

        private void SetupPerspective()
        {
            float aspectRatio = Width / (float) Height;
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 perspective =
                Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.1f, 100);

            GL.MultMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetupPerspective();
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);
            _lastMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            ProcessKeyboard();

            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            var viewMatrix = _camera.LookAtMatrix;
            GL.LoadMatrix(ref viewMatrix);

            var perspective =
                Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float) Width / Height, 0.1f, 100);

            _shader.Use();

            _light.Apply(_shader, true);

            foreach (var renderObject in _entities)
            {
                _shader.SetMat4("projection", perspective);
                renderObject.Render(_camera, _light, _shader);
            }

            _shader.Disable();
            DrawAxes();

            SwapBuffers();

            Title = $"FPS: {1f / e.Time}";
        }

        private void ProcessKeyboard()
        {
            if (!Focused)
                return;

            var sensivity = 0.1f;
            var kboardState = Keyboard.GetState();

            if (kboardState.IsKeyDown(Key.W))
            {
                if (kboardState.IsKeyDown(Key.LControl))
                    _light.Position += Vector4.UnitZ * sensivity;
                else
                    _camera.Move(0f, 1, 0f);
            }

            if (kboardState.IsKeyDown(Key.S))
            {
                if (kboardState.IsKeyDown(Key.LControl))
                    _light.Position -= Vector4.UnitZ * sensivity;
                else
                    _camera.Move(0f, -1, 0f);
            }

            if (kboardState.IsKeyDown(Key.A))
            {
                if (kboardState.IsKeyDown(Key.LControl))
                    _light.Position += Vector4.UnitX * sensivity;
                else
                    _camera.Move(-1, 0f, 0f);
            }

            if (kboardState.IsKeyDown(Key.D))
            {
                if (kboardState.IsKeyDown(Key.LControl))
                    _light.Position -= Vector4.UnitX * sensivity;
                else
                    _camera.Move(1, 0f, 0f);
            }

            if (kboardState.IsKeyDown(Key.Space))
            {
                if (kboardState.IsKeyDown(Key.LControl))
                    _light.Position += Vector4.UnitY * sensivity;
                else
                    _camera.Move(0f, 0f, 1);
            }

            if (kboardState.IsKeyDown(Key.ShiftLeft))
            {
                if (kboardState.IsKeyDown(Key.LControl))
                    _light.Position -= Vector4.UnitY * sensivity;
                else
                    _camera.Move(0f, 0f, -1);
            }

            if ((kboardState.IsKeyDown(Key.ControlLeft) || kboardState.IsKeyDown(Key.ControlRight))
                && kboardState.IsKeyDown(Key.H))
                new AboutWindow().Show();

            if (kboardState.IsKeyDown(Key.Escape))
                Exit();

            _camera.MoveSpeed = kboardState.IsKeyDown(Key.ShiftLeft) ? 0.05f : 0.1f;

            Vector2 delta = _lastMousePos - new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            _lastMousePos += delta;

            _camera.AddRotation(delta.X, delta.Y);

            if (Focused)
            {
                Mouse.SetPosition(Width / 2f, Height / 2f);
                CursorVisible = false;
            }

            _lastMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }


        private void DrawAxes()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(-300.0f, 0.0f, 0.0f);
            GL.Vertex3(300.0f, 0.0f, 0.0f);
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0.0f, -300.0f, 0.0f);
            GL.Vertex3(0.0f, 300.0f, 0.0f);
            GL.End();

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.DodgerBlue);
            GL.Vertex3(0.0f, 0.0f, -300f);
            GL.Vertex3(0.0f, 0.0f, 300.0f);
            GL.End();
        }

        protected override void OnUnload(EventArgs e)
        {
            _shader.Dispose();
            base.OnUnload(e);
        }
    }
}