namespace HiroEngine.HiroEngine.Graphics.Window
{
    public struct AppWindowSettings
    {
        public bool CursorVisible { get; set; }
        public bool LeaveByEscape { get; set; }
        public bool ShowCursorByEscape { get; set; }

        public static AppWindowSettings GetDefaultSettings()
        {
            return new AppWindowSettings
            {
                CursorVisible = false,
                LeaveByEscape = false,
                ShowCursorByEscape = true,
            };
        }
    }
}
