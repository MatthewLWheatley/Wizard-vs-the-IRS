using UnityEngine;

public class tutorialTexts : MonoBehaviour
{
    //Store the text to be displayed.
    //Put each amount that can be displayed at a time into an array.
    public string[] Tutorial1;
    public string[] Tutorial2;
    public string[] Tutorial3;
    public string[] Tutorial4;
    public string[] Tutorial5;
    public string[] Tutorial6;
    public string[] Tutorial7;

    public string[] Title1;
    public string[] Title2;
    public string[] Title3;
    public string[] Title4;

    // Start is called before the first frame update
    void Start()
    {
        masseageSet();
    }


    //MainWindow
    void TutorialText()
    {
        // Welcoming/Basic Movement tutorial
        Tutorial1 = new string[]
        {
            "Good morning, you were terribly drunk last night, but did you remember that you have to pay taxes to the IRS?",
            "What? You say you have decided to fight the IRS? Are you still intoxicated?",
            "OK then. I know you are an excellent wizard, but IRS agents are highly trained. You will need to be prepared.",
            "Firstly, you have to walk properly. Use the WASD keys to walk."
        };

        //HP Tutorial
        Tutorial2 = new string[]
        {
            "Wonderful. This is the Sobriety Bar. When it reaches 0, you will come back to your senses and then its game over.",
            "Isn't that a good thing? NO. To complete your insane plan of defeating the IRS, you must be drunk!",
            "You are sobering up at a constant rate when enemies are around, and also you sober up when hit by enemies."
        };

        //Basic Attack tutorial
        Tutorial3 = new string[]
        {
            "You can aim with the Mouse. Aim true to defeat your enemies.",
            "You can have 2 weapons on your person at any time. Both weapons will be equipped at once and can be fired via pressing Left-click or Right-click.",
            "Try to hit that target there!"
        };

        //Drink Tutorial
        Tutorial4 = new string[]
        {
            "Great. You can pick up drinks when you defeat enemies. Defeat enemies quickly to keep yourself from sobriety."
        };

        //Dash Tutorial
        Tutorial5 = new string[]
        {
            "You can dash by pressing Left-shift. Dash quickly moves you in the direction that you were moving.",
            "It is useful to dodge the enemies' attack, but be careful, you can still be hit by enemies while you are dashing.",
            "Now try to make use of these to defeat the enemy!"
        };

        //Weapon Tutorial
        Tutorial6 = new string[]
        {
            "Well done! You can take weapons from enemies when you defeat them and they can be",
            "switched out in place of your current weapons. It is shown on the bottom left of the screen."
        };

        //End Message
        Tutorial7 = new string[]
        {
            "That's about all I can tell you at the moment. It will be a tough challenge, but you will get through this with your strong will.",
            " Good luck my Whimsical Wizard! May the booze be in your favor!"
        };
    }

    void TutorialTitle()
    {
        Title1 = new string[]
        {
            "Welcome to the Tutorial!",
        };

        Title2 = new string[]
        {
            "Use WASD to walk",
        };

        Title3 = new string[]
        {
            "Click to Attack",
        };

        Title4 = new string[]
        {
            "Defeat the enemy!",
        };
    }


    void masseageSet()
    {
        TutorialText();
        TutorialTitle();
    }
}
