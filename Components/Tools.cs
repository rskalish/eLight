namespace eLight.Components
{
    public sealed class Tools
    {
        public static void PreventScreenLock()
        {
            // disable automatic screen lock
            var displayRequest = new Windows.System.Display.DisplayRequest();
            displayRequest.RequestActive();
        }
    }
}