using Scoring;

/* This class stores static variables for access across application. These 
 * variables include game settings, user info, and game info.
 */
public static class GameInfo
{
    // game settings
    public static string gameType = "random";
    public static string speed = "slow";
    public static int length = 60;
    public static bool leftSide = true;
    public static bool rightSide = true;

    // last game score
    public static BaseballGame lastGame;

    // currently logged in user
    public static string currentUser = null;
}