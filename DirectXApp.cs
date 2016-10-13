/**
 * Common class for easy access to everything DirectX-related.
 * 
 * I took a bunch of stuff from :
 * https://github.com/SlimDX/slimdx/tree/master/publish/SlimDX%20Samples%20(Sept%202011)/SampleFramework
 * 
 **/

using System;
using System.Drawing;
using System.Windows.Forms;
using SlimDX.Direct3D9;
using SlimDX.DXGI;
using SlimDX.Windows;
using System.Threading;

namespace SlimDX.Common
{
	/**
	 * This class behaves as a wrapper around the game's window
	 **/
	public class DirectXApp : IDisposable
	{
		#region Public Interface

		public int WindowWidth {
			get { return _width; }
			set { _width = value; }
		}

		public int WindowHeight
		{
			get { return _height; }
			set { _height = value; }
		}

		public UI UserInterface { get { return _userInterface; } }

		// Seconds elapsed since last frame
		public float FrameDelta { get; private set; }

		public float FramesPerSecond { get { return _framesPerSecond.Value; } }

		// Direct2D context, set after calling InitializeDevice()
		public DeviceContext2D Context2D { get; private set; }

		// Unload resources
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Unload resources, managed and unmanaged
		protected virtual void Dispose(bool disposeManagedResources)
		{
			if (disposeManagedResources)
			{
				if (_userInterfaceRenderer != null)
					_userInterfaceRenderer.Dispose();

				_apiContext.Dispose();
				_form.Dispose();
			}
		}

		protected virtual Form CreateForm(DirectXAppConfiguration config)
		{
			return new RenderForm(config.WindowTitle)
			{
				ClientSize = new Size(config.WindowWidth, config.WindowHeight),
				FormBorderStyle = FormBorderStyle.FixedToolWindow
			};
		}

		// Run the app
		public void Run()
		{
			_configuration = OnConfigure();
			_form = CreateForm(_configuration);

			_currentFormWindowState = _form.WindowState;

			bool isFormClosed = false;
			bool formIsResizing = false;

			_form.MouseClick += HandleMouseClick;
			_form.KeyDown += HandleKeyDown;
			_form.KeyUp += HandleKeyUp;

			_form.Resize += (o, args) =>
			{
				if (_form.WindowState != _currentFormWindowState)
					HandleResize(o, args);

				_currentFormWindowState = _form.WindowState;
			};

			_form.Closed += (o, args) =>
			{
				isFormClosed = true;
			};

			_form.ResizeBegin += (o, args) => {
				formIsResizing = true;
			};

			_form.ResizeEnd += (o, args) =>
			{
				formIsResizing = false;
				HandleResize(o, args);
			};

			_userInterface = new UI();
			var stats = new Element();
			stats.SetBinding("Label", _framesPerSecond);
			_userInterface.Container.Add(stats);

			InitializeDevice();

			OnInitialize();
			OnResourceLoad();

			_clock.Start();
			MessagePump.Run(_form, () =>
			{
				if (isFormClosed)
					return;

				Update();
				if (!formIsResizing)
					Render();
			});

			OnResourceUnload();
		}

		// Configure the form.
		protected virtual DirectXAppConfiguration OnConfigure()
		{
			return new DirectXAppConfiguration(WindowWidth, WindowHeight);
		}

		// The following On...() functions should be implemented in the derived app
		protected virtual void OnInitialize() { }

		protected virtual void OnResourceLoad() { }

		protected virtual void OnResourceUnload() { }

		protected virtual void OnUpdate() { }

		protected virtual void OnRender() { }

		protected virtual void OnRenderBegin() { }

		protected virtual void OnRenderEnd() { }

		protected virtual void OnKeyDown(Keys keyCode) { }

		protected virtual void OnKeyUp(Keys keyCode) { }

		// Creates the 2D device context
		protected void InitializeDevice()
		{
			var result = new DeviceContext2D(
				_form.Handle,
				new DeviceSettings2D()
				{
					Width = WindowWidth,
					Height = WindowHeight
				}
			);
			_apiContext = result;
			Context2D = result;
		}

		/**
		 * Quits the app
		 **/
		protected void Quit()
		{
			_form.Close();
		}

		#endregion

		// --------------------------------------------------------------------

		#region Implementation Detail

		private readonly Clock _clock = new Clock();
		private readonly Bindable<float> _framesPerSecond = new Bindable<float>();
		private IDisposable _apiContext;
		private DirectXAppConfiguration _configuration;
		private FormWindowState _currentFormWindowState;
		private Form _form;
		private float _frameAccumulator;
		private int _frameCount;
		private UI _userInterface;
		private UIRenderer _userInterfaceRenderer;

		// Window size by default
		private int _width = 800;
		private int _height = 600;

		/**
		 * Destructor
		 **/
		~DirectXApp()
		{
			Dispose( false );
		}

		/**
		 * Update the app's state
		 **/
		private void Update()
		{
			FrameDelta = _clock.Update();
			_userInterface.Container.Update();
			OnUpdate();
		}

		private void Render()
		{
			_frameAccumulator += FrameDelta;
			_frameCount++;
			if( _frameAccumulator >= 1.0f )
			{
				_framesPerSecond.Value = _frameCount / _frameAccumulator;
				_frameAccumulator = 0.0f;
				_frameCount = 0;
			}

				OnRenderBegin();

				OnRender();

				if( _userInterfaceRenderer != null )
					_userInterfaceRenderer.Render( _userInterface );

				OnRenderEnd();
		}

		/**
		 * on mouse click
		 **/
		private void HandleMouseClick( object sender, MouseEventArgs e )
		{
			// ...
		}

		/**
		 * on Key Down
		 **/
		private void HandleKeyDown(object sender, KeyEventArgs e)
		{
			OnKeyDown(e.KeyCode);
		}

		/**
		 * on Key Up
		 **/
		private void HandleKeyUp( object sender, KeyEventArgs e )
		{
			OnKeyUp(e.KeyCode);
		}

		/**
		 * on Display Resize
		 **/
		private void HandleResize(object sender, EventArgs e)
		{
			if( _form.WindowState == FormWindowState.Minimized )
				return;

			OnResourceUnload();

			// ...

			OnResourceLoad();
		}

		#endregion
	}
}
