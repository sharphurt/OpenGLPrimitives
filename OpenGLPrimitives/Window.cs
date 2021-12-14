using System;
using System.Collections.Generic;
using System.Drawing;
using OpenGLPrimitives.Camera;
using OpenGLPrimitives.Primitives.ThreeD;
using OpenGLPrimitives.Primitives.TwoD;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGLPrimitives
{
    public class Window : GameWindow
    {
        private List<GameObject> _entities;

        private readonly ICamera _camera = new FirstPersonCamera();

        private Vector2 _lastMousePos;

        private Shader _shader;

        private LightSource _light;

        private float _time;

        public Window() : base(1280, 720, OpenTK.Graphics.GraphicsMode.Default, "OpenGL Primitives")
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _shader = new Shader("Shaders/ThreeD/vertexShader.glsl", "Shaders/ThreeD/fragmentShader.glsl");
            _light = new LightSource(new Vector4(0, 3, -4, 1), new Vector4(1, 1, 1, 1));

            _entities = new List<GameObject>
            {
                GameObjectFactory.CreateTetrahedron(new Vector4(0, 0, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1)),
                GameObjectFactory.CreateCube(new Vector4(3, 0, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1)),
                GameObjectFactory.CreateOctahedron(new Vector4(6, 0, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1)),
                GameObjectFactory.CreateDodecahedron(new Vector4(9, 0, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1)),
                GameObjectFactory.CreateIcosahedron(new Vector4(12, 0, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 1, 1)),
            };

            SetupPerspective();

            MouseDown += ProcessMouse;

            _lastMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            CursorVisible = false;
        }

        private void ProcessMouse(object sender, MouseButtonEventArgs args)
        {
            if (args.Button != MouseButton.Left) return;
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
            _time += 0.01f;
        }

        private void ProcessKeyboard()
        {
            if (!Focused)
                return;

            var sensivity = 0.05f;

            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                if (Keyboard.GetState().IsKeyDown(Key.LControl))
                    _light.Position += Vector4.UnitZ * sensivity;
                else
                    _camera.Move(0f, 1, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                if (Keyboard.GetState().IsKeyDown(Key.LControl))
                    _light.Position -= Vector4.UnitZ * sensivity;
                else
                    _camera.Move(0f, -1, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Key.A))
            {
                if (Keyboard.GetState().IsKeyDown(Key.LControl))
                    _light.Position += Vector4.UnitX * sensivity;
                else
                    _camera.Move(-1, 0f, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Key.D))
            {
                if (Keyboard.GetState().IsKeyDown(Key.LControl))
                    _light.Position -= Vector4.UnitX * sensivity;
                else
                    _camera.Move(1, 0f, 0f);
            }

            if (Keyboard.GetState().IsKeyDown(Key.Space))
            {
                if (Keyboard.GetState().IsKeyDown(Key.LControl))
                    _light.Position += Vector4.UnitY * sensivity;
                else
                    _camera.Move(0f, 0f, 1);
            }

            if (Keyboard.GetState().IsKeyDown(Key.ShiftLeft))
            {
                if (Keyboard.GetState().IsKeyDown(Key.LControl))
                    _light.Position -= Vector4.UnitY * sensivity;
                else
                    _camera.Move(0f, 0f, -1);
            }

            if (Keyboard.GetState().IsKeyDown(Key.Escape)) Exit();

            _camera.MoveSpeed = Keyboard.GetState().IsKeyDown(Key.ShiftLeft) ? 0.05f : 0.1f;


            Vector2 delta = _lastMousePos - new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            _lastMousePos += delta;
            
            _camera.AddRotation(delta.X, delta.Y);
            
            if (Focused)
                Mouse.SetPosition(Width / 2f, Height / 2f);
            
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