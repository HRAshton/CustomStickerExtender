using System;
using System.Windows;
using System.Windows.Input;
using HRAshton.CustomStickerExtender.Properties;
using Timer = System.Threading.Timer;

namespace HRAshton.CustomStickerExtender
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// ReSharper disable once NotAccessedField.Local
		private Timer Timer;
		private HoverControl SubWindow;

		public MainWindow()
		{
			InitializeComponent();
			Timer = new Timer(SetStickerbarToConnect, null, 0, 250);
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			WinApiHelpers.SetNoActivate(this);
		}  
		
		private void SetStickerbarToConnect(object _ = null)
		{
			var targetWnd = WinApiHelpers.GetForegroundWindow();
			var title = WinApiHelpers.GetWindowText(targetWnd);
			if (title != Settings.Default.TargetWindowTitle)
			{
				if (Visibility == Visibility.Visible)
				{
					Dispatcher.Invoke(new Action(() =>
					{
						Visibility = Visibility.Hidden;
					}));
				}

				return;
			}

			var pos = WinApiHelpers.GetWindowPosition(targetWnd);

			Dispatcher.Invoke(new Action(() =>
			{
				SetControl(pos);
			}));
		}

		private void SetControl(WinPosition pos)
		{
			if (Visibility == Visibility.Hidden)
			{
				Visibility = Visibility.Visible;
				Topmost = true;
			}

			Left = pos.Right - Width - Settings.Default.RightOffsetPx;
			Top = pos.Bottom - Height / 2 - Settings.Default.BottomOffsetPx;
		}

		private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
		{
			if (SubWindow?.IsVisible == true) return;
			
			SubWindow = new HoverControl
			{
				Visibility = Visibility.Visible,
				Top = Top + Height,
			};
			SubWindow.Left = Left - SubWindow.Width / 2;
		}
	}
}