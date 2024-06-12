using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    //message box
    public GameObject mainWindow;
    public GameObject subWindow;
    //Windows animation
    public Animator mainAnim;
    public Animator subAnim;
    //Message Box Text
    public Text mainText;
    public Text subText;
    //The actual Text to process
    private Text utilityText;
    //Class for storing text
    public tutorialTexts tutoText;

    //Flags to be used between additive scenes
    private int tipsChecker = 0;
    //Next button for the main box
    public Button nextButton;
    //The Next button, which is actually used
    private Button utilityButton;

    //For tutorial progress
    private bool chapterflag = false;
    //Current message number
    private int textCount;
    //Number which is displaying now
    private int nowTextNum = 0;

    //Whether you have displayed one message
    private bool isOneMessage = false;
    //Whether you have displayed all the messages.
    private bool isEndMessage = true;

    //Temporary UI for tutorial
    [SerializeField] GameObject HPBar;
    [SerializeField] GameObject HealthBarForeground;
    [SerializeField] GameObject WeaponUI;

    //Temporary game objects for tutorial
    [SerializeField] GameObject Target;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject Player;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    //Game objects for switching camera
    [SerializeField] GameObject FixCamera;


    // Start is called before the first frame update
    void Start()
    {
        //If there are any buttons you want to disable in the tutorial, add them to the process.
        StartCoroutine(SendText());
        //store the initial position of the player
        _initialPosition = Player.transform.position;
        _initialRotation = Player.transform.rotation;
        //stop health decay
        Player.GetComponent<Health>().DecayRate = 100.0f;
        //prevent the enemy to disappear
        Enemy.GetComponent<DeathPlanes>().enabled = false;
    }

    //restrict the player movement and swap the camera while main window is shown
    void SwapToFixMode()
    {
        //restrict player's movement
        Player.GetComponent<Slash_Start>().enabled = false;
        Player.GetComponent<Jab_Start>().enabled = false;
        Player.GetComponent<Health>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        HealthBarForeground.GetComponent<HealthBarScaling>().enabled = false;
        
        //stop the player movement then reset the player position and rotation
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = _initialPosition;
        Player.transform.rotation = _initialRotation;
    }


    void SwapToPlayMode()
    {
        Player.GetComponent<Slash_Start>().enabled = true;
        Player.GetComponent<Jab_Start>().enabled = true;
        Player.GetComponent<PlayerMovement>().enabled = true;
        //enable player to move
        Player.GetComponent<CharacterController>().enabled = true;
    }

    public IEnumerator SendText()
    {
        //============================ //
        //                                                         //
        //　Here are the steps of the tutorial.      //
        //                                                         //
        //============================ //

        SwapToFixMode();

        //Welcoming, Basic movement tutorial
        yield return SubTextBox(tutoText.Title1[0], 0);
        yield return new WaitForSeconds(2.0f);
        yield return EndSubTips();
        chapterflag = false;

        while (!chapterflag)
        {
            yield return MainTextBox(tutoText.Tutorial1);
            yield return null;
        }
        chapterflag = false;


        //let the player to walk
        SwapToPlayMode();
        yield return SubTextBox(tutoText.Title2[0], 0);
        yield return new WaitForSeconds(4.0f);
        Player.GetComponent<PlayerMovement>().enabled = false;
        yield return EndSubTips();
        chapterflag = false;
        SwapToFixMode();


        //HP Tutorial
        HPBar.SetActive(true);
        while (!chapterflag)
        {
            yield return MainTextBox(tutoText.Tutorial2);
            yield return null;
        }
        chapterflag = false;


        //Basic Attack tutorial
        while (!chapterflag)
        {
            yield return MainTextBox(tutoText.Tutorial3);
            yield return null;
        }
        chapterflag = false;


        //let the player to attack the target
        SwapToPlayMode();
        Target.SetActive(true);
        yield return SubTextBox(tutoText.Title3[0], 0);

        //when the player hits to the target, wait 1.0f then end sub tips
        yield return EndSubTips();
        //wait the player to complete the task
        while (Target.GetComponent<TargetCollision>().TargetTutComplete == false) 
        {
            yield return null;
        }
        //task chacking
        if (Target.GetComponent<TargetCollision>() != null)
        {
            if(Target.GetComponent<TargetCollision>().TargetTutComplete == true)
            {
                yield return new WaitForSeconds(1.0f);
                chapterflag = false;
                SwapToFixMode();
                Target.SetActive(false);
            }
        }


        //Drink Tutorial
        while (!chapterflag)
        {
            yield return MainTextBox(tutoText.Tutorial4);
            yield return null;
        }
        chapterflag = false;


        //Dash Tutorial
        while (!chapterflag)
        {
            yield return MainTextBox(tutoText.Tutorial5);
            yield return null;
        }
        chapterflag = false;


        //let the player to defeat the enemy
        SwapToPlayMode();
        FixCamera.SetActive(false);
        Enemy.SetActive(true);
        yield return SubTextBox(tutoText.Title4[0], 1);

        //when the player defeat the enemy, wait 1.0f then end sub tips
        yield return EndSubTips();
        //wait the player to complete the task
        while (Enemy.GetComponent<EnemyAI>().m_alive == true) 
        {
            yield return null;
        }
        //task chacking
        if (Target.GetComponent<TargetCollision>() != null)
        {
            if (Enemy.GetComponent<EnemyAI>().m_alive == false)
            {
                yield return new WaitForSeconds(1.0f);
                chapterflag = false;
                Enemy.SetActive(false);
                SwapToFixMode();
                FixCamera.SetActive(true);
            }
        }



        //Weapon Tutorial
        WeaponUI.SetActive(true);
        while (!chapterflag)
        {
            yield return MainTextBox(tutoText.Tutorial6);
            yield return null;
        }
        chapterflag = false;

        //wait for a bit to let the player check where is weapon UI
        yield return new WaitForSeconds(2.0f);

        //End message
        while (!chapterflag)
        {
            yield return MainTextBox(tutoText.Tutorial7);
            yield return null;
        }
        chapterflag = false;

    }

    IEnumerator MainTextBox(string[] sentences)
    {
        while (isEndMessage || sentences == null)
        {
            //If you want to use it in an additive scene, set tipsChecker to 1 and set utilityText or utilityButton to Start() of the loaded scene.
            switch (tipsChecker)
            {
                case 0:
                    if (!mainWindow.activeSelf)
                    {
                        mainWindow.SetActive(true);
                        yield return new WaitForSeconds(0.5f);

                        //For animation
                        mainAnim.SetBool("onAir", true);
                        yield return new WaitForSeconds(0.5f);
                        utilityText = mainText;
                        utilityButton = nextButton;

                        //When tipsChecker is set to 0, isEndMessage is set to false and the main part works.
                        isEndMessage = false;
                    }
                    break;

                case 1:
                    {
                        tipsChecker = 2;
                        isEndMessage = false;
                        break;
                    }

                case 2:
                    break;
            }
            yield return null;
        }

        while (!isEndMessage)
        {
            //No message to be displayed at one time
            if (!isOneMessage)
            {
                //If all messages are displayed, the game objects are hidden
                if (textCount >= sentences.Length)
                {
                    textCount = 0;

                    switch (tipsChecker)
                    {
                        case 0:
                            {
                                //For animation
                                mainAnim.SetBool("onAir", false);
                                yield return new WaitForSeconds(0.5f);
                                mainWindow.SetActive(false);
                                break;
                            }

                        case 1:
                            break;

                        case 2:
                            break;
                    }

                    chapterflag = true;
                    isOneMessage = false;
                    isEndMessage = true;
                    yield break;
                }
                //Otherwise, initialize the text processing related items and display them from the next character.

                //Add one character after the text display time has elapsed.
                utilityText.text += sentences[textCount][nowTextNum];
                nowTextNum++;

                //The full message was displayed, or the maximum number of lines were displayed.
                if (nowTextNum >= sentences[textCount].Length)
                {
                    isOneMessage = true;
                }
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                //Message to be displayed at one time.
                yield return new WaitForSeconds(0.5f);

                //Press the Next button.
                utilityButton.interactable = true;
                isEndMessage = true;
            }
        }
    }

    IEnumerator SubTextBox(string sentence, int tar)
    {
        //Show subboxes.
        subWindow.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        //For animation
        subAnim.SetBool("onAir", true);
        yield return new WaitForSeconds(0.5f);

        isEndMessage = false;

        while (!isEndMessage)
        {
            //No message to be displayed at one time	
            if (!isOneMessage)
            {
                //Add one character after the text display time has elapsed.
                subText.text += sentence[nowTextNum];
                nowTextNum++;

                //The full message was displayed, or the maximum number of lines were displayed.
                if (nowTextNum >= sentence.Length)
                {
                    isOneMessage = true;
                }
                yield return new WaitForSeconds(0.01f);
            }

            //Message to be displayed at one time.
            else
            {
                yield return new WaitForSeconds(0.5f);

                nowTextNum = 0;
                isEndMessage = true;
                yield break;
            }
        }
    }

    IEnumerator EndSubTips()
    {
        subText.text = "";
        isOneMessage = false;

        //For animation
        subAnim.SetBool("onAir", false);
        chapterflag = true;
        yield return new WaitForSeconds(0.5f);
        subWindow.SetActive(false);
    }

    public void NextButton()
    {
        //Initialize the message function when you press the Next button.
        utilityButton.interactable = false;
        utilityText.text = "";
        nowTextNum = 0;

        //When displaying multiple times in a row, the number of Text is counted.
        textCount++;
        isOneMessage = false;
        isEndMessage = false;
    }
}
