using System;
using System.Collections.Generic;
using System.Drawing;
using OpenGLPrimitives.Camera;
using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Maintenance;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenGLPrimitives
{
    public class Window : GameWindow
    {
        private readonly ICamera _camera = new FirstPersonCamera();

        private Vector2 _lastMousePos;

        private Shader _rayTracingShader;
        private Vector3 _oldCameraPosition = Vector3.Zero;
        private Vector3 _oldCameraDirection = Vector3.Zero;

        private Polygon _screen;
        private float _oldFOV = 0.0f;

        private int _accumulationFrames = 0;

        private float _time = 0;


        private Texture _accumulationTexture;

        public Window() : base(1280, 720, OpenTK.Graphics.GraphicsMode.Default,
            "OpenGL RayTracing | Ctrl + H: Open About Window")
        {
            VSync = VSyncMode.On;
            WindowState = WindowState.Maximized;
            var about = new AboutWindow();
            about.ShowDialog();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            _screen = new Polygon(new[]
            {
                new Vertex(new Vector2(-1, -1), new Vector2(-1,-1)),
                new Vertex(new Vector2(1, -1), new Vector2(1,-1)),
                new Vertex(new Vector2(-1, 1), new Vector2(-1,1)),
                new Vertex(new Vector2(-1, 1), new Vector2(-1,1)),
                new Vertex(new Vector2(1, 1), new Vector2(1,1)),
                new Vertex(new Vector2(1, -1), new Vector2(1,-1)),
            });

            _rayTracingShader = new Shader("Shaders/vertexShader.glsl", "Shaders/rayTracing1.glsl");
           // _accumulationTexture = Texture.CreateEmpty(ClientSize.Width, ClientSize.Height);

           _rayTracingShader.SetVec2("viewportDimensions", new Vector2(Width, Height));
           _rayTracingShader.SetVec4("viewport", new Vector4(0, 0, Width, Height));
           
           _lastMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);
            _lastMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
          //  ProcessKeyboard();
          GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
          
          /*
          var perspective =
              Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float) Width / Height, 0.1f, 100);
              */

            /*_rayTracingShader.SetInt("uSamples", 16);
            _rayTracingShader.SetFloat("uTime", _time);
            _rayTracingShader.SetVec2("uViewportSize", new Vector2(Width, Height));
            _rayTracingShader.SetVec3("uPosition", _camera.Position);
            _rayTracingShader.SetVec3("uDirection", _camera.Direction);
            _rayTracingShader.SetVec3("uUp", _camera.Up);
            _rayTracingShader.SetFloat("uFOV", MathHelper.DegreesToRadians(60));
            _rayTracingShader.Use();*/

            _screen.Render();
            
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(0, BlendingFactorSrc.One, BlendingFactorDest.One);
            
            _time += (float) e.Time;
            SwapBuffers();
        }

        private void ProcessKeyboard()
        {
            if (!Focused)
                return;

            var sensivity = 0.05f;
            var kboardState = Keyboard.GetState();

            if (kboardState.IsKeyDown(Key.W))
            {
                _camera.Move(0f, 1, 0f);
            }

            if (kboardState.IsKeyDown(Key.S))
            {
                _camera.Move(0f, -1, 0f);
            }

            if (kboardState.IsKeyDown(Key.A))
            {
                _camera.Move(-1, 0f, 0f);
            }

            if (kboardState.IsKeyDown(Key.D))
            {
                _camera.Move(1, 0f, 0f);
            }

            if (kboardState.IsKeyDown(Key.Space))
            {
                _camera.Move(0f, 0f, 1);
            }

            if (kboardState.IsKeyDown(Key.ShiftLeft))
            {
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

            //  if (Focused) Mouse.SetPosition(Width / 2f, Height / 2f);

            _lastMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        protected override void Dispose(bool manual)
        {
            _rayTracingShader.Dispose();
            base.Dispose(manual);
        }
    }
}