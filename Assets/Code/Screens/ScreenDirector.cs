using System.Collections.Generic;
public class ScreenDirector
{
    private Dictionary<ScreenID,Screen> loadedScreens;
    public ScreenDirector()
    {
        loadedScreens = new Dictionary<ScreenID,Screen>();
    }

    public void LoadScreen(Screen screen)
    {
        loadedScreens.Add(screen.GetID(), screen);
        screen.Hide();
    }

    public void ShowScreen(ScreenID screenID)
    {
        try
        {
            loadedScreens[screenID].Show();
        }catch
        {
            //this screen is not loaded
            return;
        }
    }

    public void Hidecreen(ScreenID screenID)
    {
        try
        {
            loadedScreens[screenID].Hide();
        }catch
        {
            //this screen is not loaded
            return;
        }
    }
}