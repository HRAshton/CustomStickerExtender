using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Clipboard = System.Windows.Clipboard;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace HRAshton.CustomStickerExtender
{
    /// <summary>
    /// Interaction logic for HoverControl.xaml
    /// </summary>
    public partial class HoverControl : Window
    {
        public HoverControl()
        {
            InitializeComponent();
            DataContext = new StickersViewModel();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            Helpers.SetNoActivate(this);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            Close();
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var buf = Clipboard.GetDataObject();

            var bitmapImage = (sender as Image)?.Source as BitmapImage;
            if (bitmapImage != null)
            {
                Clipboard.SetFileDropList(new StringCollection { bitmapImage.UriSource.LocalPath });
            }
            
            SendKeys.SendWait("^v");

            if (buf != null)
            {
                Clipboard.SetDataObject(buf);
            }
        }
    }
}
