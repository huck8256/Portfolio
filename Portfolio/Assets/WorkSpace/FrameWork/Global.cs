using UnityEngine;

public enum SoundType
{
    BGM,
    Effect
}

public class Global
{ 
    public static readonly int ScreenHeight = Screen.height;
    public static readonly int ScreenWidth = Screen.width;
    public static readonly Vector2 Resolution = new Vector2(Screen.width, Screen.height);
    public static readonly int ScreenHalfHeight = Screen.height / 2;
    public static readonly int ScreenHalfWidth = Screen.width / 2;

    public static readonly float BGMVolume = 0.3f;
}
