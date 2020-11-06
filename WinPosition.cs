namespace HRAshton.CustomStickerExtender
{
    public class WinPosition
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public WinPosition()
        {
        }

        internal WinPosition(NativeMethods.TITLEBARINFO pti)
        {
            Left = pti.rcTitleBar.left;
            Top = pti.rcTitleBar.top;
            Right = pti.rcTitleBar.right;
            Bottom = pti.rcTitleBar.bottom;
        }
    }
}
