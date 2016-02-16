namespace eLight
{
    public sealed class Tools
    {
        public static void PrevenScreenLook()
        {
            // disable automatic screen lock
            var displayRequest = new Windows.System.Display.DisplayRequest();
            displayRequest.RequestActive();
        }
    }
}