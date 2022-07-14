namespace HiroEngine.HiroEngine.Graphics.Window
{
    public class AppWindowSettings
    {
        public bool CursorVisible { get; set; }
        public bool LeaveByEscape { get; set; }
        public bool ShowCursorByEscape { get; set; }
        public bool Debug { get; set; }

        public static AppWindowSettings GetDefaultSettings()
        {
            return new AppWindowSettings
            {
                CursorVisible = true,
                LeaveByEscape = true,
                ShowCursorByEscape = true,
                Debug = false
            };
        }
    }
}
