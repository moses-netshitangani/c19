using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrueFalse : MonoBehaviour
{
    //Possible questions and their answers
    public ArrayList question1 = new ArrayList() { "Your mask should be loose on your face.", "True", "False" , "1", "com"};
  	public ArrayList question2 = new ArrayList() { "Wearing a mask prevents you from getting infected.", "True", "False" , "1", "pers"};
  	public ArrayList question3 = new ArrayList() { "Thermal scanners cannot detect COVID-19.", "True", "False" ,"0", "pers"};
  	public ArrayList question4 = new ArrayList() { "Exposing yourself to the sun decreases your chances of getting COVID-19.", "True", "False" ,"1", "pers"};
    public ArrayList question5 = new ArrayList() { "UV lamps should be used to disinfect your hands or other areas of your body.", "True", "False" ,"1", "pers"};
    public ArrayList question6 = new ArrayList() { "You no longer pose a threat to others once you have completed quarantine and you are symptom free for more than 72 hours.", "True", "False" ,"0", "both"};
    public ArrayList question7 = new ArrayList() { "You can always tell if someone has COVID-19.", "True", "False" ,"1", "pers"};
    public ArrayList question8 = new ArrayList() { "COVID-19 and SARS are the same virus.", "True", "False" ,"1", "pers"};
    public ArrayList question9 = new ArrayList() { "There is no vaccine or cure currently for COVID-19.", "True", "False" ,"0", "both"};
    public ArrayList question10 = new ArrayList() { "There is a difference between being isolated and being quarantined.", "True", "False" ,"0", "pers"};
    public ArrayList question11 = new ArrayList() { "If you are displaying any symptoms or have come into contact with an infected person, then you should definitely go to school or work.", "True", "False" ,"1", "com"};
    public ArrayList question12 = new ArrayList() { "The Corona virus is passed on through respiratory droplets i.e. a cough or a sneeze. ", "True", "False" ,"0", "both"};

    // canned response
    public ArrayList responses = new ArrayList () {"Your avatar has been infected and is currently being kept in quarantine :(", " Your avatar was caught hugging his friends earlier today. ", "Due to proper lack of knowledge, your avatar thought that the reason for wearing a mask was to protect himself."};


    //The ArrayList thats content will be displayed on the UI
    public ArrayList displayQuestion = new ArrayList();

    //Answer buttons
    public Button ans1, ans2;
    private int checkNumber;

    // level failed UI buttons
    private Button restart;
    private Button exit;

    // level passed UI button
    private Button continueButton;

    //Correct and Incorrect Canvas
    public GameObject correctUI;
    public GameObject incorrectUI;
    public GameObject levelFailedUI;
    public GameObject levelCompleteUI;
    public GameObject perfectScoreUI;
    private GameObject popUp;

    // default colour for the possible answers
    private Color purple = new Color32(188, 54, 255, 204);

    // Score and InfectionBar scripts
    Score scoreScript;
    InfectionBar infectionB;

    // Infection bar
    public int currentHealth;

    //counter to check the question number
    private int num = 0;

    //score tally
    public int score = 1;

    private int correctAnswer;

    // storage Script
    Level levelScript;


    void Start()
    {
        //Button listener method
        // 0 - True, 1 - False
        ans1.onClick.AddListener(delegate {ChosenAnswer(0); });
        ans2.onClick.AddListener(delegate {ChosenAnswer(1); });

        // access score Script
        scoreScript = GameObject.Find("Quiz UI/Scores").GetComponent<Score>();
        infectionB = GameObject.Find("Quiz UI/InfectionBarContainer/HealthBar").GetComponent<InfectionBar>();

        // access storage script
        levelScript = GameObject.Find("Quiz UI").GetComponent<Level>();

    }

    void Update(){
        //changes the possible question based on the 'num' value
        switch(num){
            case 0: displayQuestion = question1;
                    break;
            case 1: displayQuestion = question2;
                    break;
            case 2: displayQuestion = question3;
                    break;
            case 3: displayQuestion = question4;
                    break;
            case 4: displayQuestion = question5;
                    break;
            case 5: displayQuestion = question6;
                    break;
            case 6: displayQuestion = question7;
                    break;
            case 7: displayQuestion = question8;
                    break;
            case 8: displayQuestion = question9;
                    break;
            case 9: displayQuestion = question10;
                    break;
            case 10: displayQuestion = question11;
                    break;
            case 11: displayQuestion = question12;
                    break;
        }

        //Displays the question
        Text questionObj = GameObject.Find("Quiz UI/Question Box/Text").GetComponent<Text>();
        questionObj.text = (string)displayQuestion[0];

        //Displays the possible answers
        Text option1 = GameObject.Find("Quiz UI/Solutions/True/Text").GetComponent<Text>();
        option1.text = (string)displayQuestion[1];
        Text option2 = GameObject.Find("Quiz UI/Solutions/False/Text").GetComponent<Text>();
        option2.text = (string)displayQuestion[2];

        correctAnswer = int.Parse((string)displayQuestion[3]);
    }

    void ChosenAnswer(int number)
    {
        currentHealth = infectionB.getHealth();//get the current value of the health bar

        checkNumber = number;
        //switch checks if answer was correct and displays the corresponding screen
        if(number == correctAnswer)
        {
            // change colour of selected option
            switchGreen();

            // display correct UI
            Invoke("correctPopUp",0.5f);

            // add to score. 1st argument is personal 2nd is community
            if(string.Equals(displayQuestion[4], "both"))
            {
              scoreScript.setScore(1,1);
            }
            else if(string.Equals(displayQuestion[4], "pers"))
            {
              scoreScript.setScore(1,0);
            }else
            {
              scoreScript.setScore(0,1);
            }

            // display quiz
            Invoke("backToQuiz", 2);
        }
        else
        {
          // change colour of selected option
          switchRed();

          // increase infection bar by a third
          currentHealth += 33;
          infectionB.SetHealth(currentHealth);

          // display incorrect pop
          Invoke("incorrectPopUp", 0.5f);

          if(currentHealth < 90)
          {
            // display next quiz question
            Invoke("backToQuiz", 2);

          }else
          {
            Invoke("levelFailedPopUp", 2);
          }
        }

        //Output this to console when Button1 or Button3 is clicked
        Debug.Log($"You have clicked the button {number}!");
    }

    void backToQuiz(){

        switchPurple();

        if (num < 12)
        {
            num++;
        }

        // unlock 3rd level (Jeopardy)
        if(num>=12)
        {
            levelScript.unlockAndSaveLevel(true, true, false);

            if(scoreScript.getPersonalScore() == 10 && scoreScript.getCommunityScore() == 5)
            {
              PerfectPopUp();
            }else
            {
              levelPassedPopUp();
            }
        }

        //Four Questions will be displayed in then there will be a transition to the next scene

    }

    void switchRed()
    {
      switch(checkNumber)
      {
        case 0:
          ans1.GetComponent<Image>().color = Color.red;
          break;

        case 1:
          ans2.GetComponent<Image>().color = Color.red;
          break;

      }
    }

    void switchGreen()
    {
      switch(checkNumber)
      {
        case 0:
          ans1.GetComponent<Image>().color = Color.green;
          break;

        case 1:
          ans2.GetComponent<Image>().color = Color.green;
          break;

      }
    }

    // switch button color to purple
    void switchPurple()
    {
      switch(checkNumber)
      {
        case 0:
          ans1.GetComponent<Image>().color = purple;
          break;

        case 1:
          ans2.GetComponent<Image>().color = purple;
          break;

      }
    }

    // show correct UI
    void correctPopUp(){
      popUp = Instantiate(correctUI, transform.position, Quaternion.identity) as GameObject;
      Text ans = GameObject.Find("CorrectUI(Clone)/AnswerText").GetComponent<Text>();
      ans.text = "Answer: \n" + (string)displayQuestion[correctAnswer+1];
    }

    // show incorrect UI
    void incorrectPopUp(){
      popUp = Instantiate(incorrectUI, transform.position, Quaternion.identity) as GameObject;
      Text ans = GameObject.Find("IncorrectUI(Clone)/AnswerText").GetComponent<Text>();
      ans.text = "Answer: \n" + (string)displayQuestion[correctAnswer+1];
    }

    // reset all values
    public void resetAll()
    {
      num = 0;
      scoreScript.resetScore();
      infectionB.resetBar();
    }

    // level passed UI
    void levelPassedPopUp()
    {
      popUp = Instantiate(levelCompleteUI, transform.position, Quaternion.identity) as GameObject;
      Text ans = GameObject.Find("LevelCompleteUI(Clone)/Text").GetComponent<Text>();
      ans.text = "Congratulations,\nLevel Passed!\nPersonal Score: " + scoreScript.getPersonalScore() + "/10\nCommunity Score: "+scoreScript.getCommunityScore() + "/5";

      continueButton = GameObject.Find("LevelCompleteUI(Clone)/ContinueButton").GetComponent<Button>();
      continueButton.onClick.AddListener(delegate {Exit(); });
    }

    // Perfect score UI
    void PerfectPopUp()
    {
      popUp = Instantiate(perfectScoreUI, transform.position, Quaternion.identity) as GameObject;

      continueButton = GameObject.Find("PerfectScoreUI(Clone)/ContinueButton").GetComponent<Button>();
      continueButton.onClick.AddListener(delegate {Exit(); });
    }

    // level failed popUp
    void levelFailedPopUp()
    {
      popUp = Instantiate(levelFailedUI, transform.position, Quaternion.identity) as GameObject;
      Text ans = GameObject.Find("levelFailedUI(Clone)/Canvas/Text").GetComponent<Text>();
      System.Random rnd = new System.Random();
      ans.text = "Level Failed!\n"+responses[rnd.Next(0,3)];

      // Buttons
      restart = GameObject.Find("levelFailedUI(Clone)/Buttons/Restart").GetComponent<Button>();
      exit = GameObject.Find("levelFailedUI(Clone)/Buttons/Exit").GetComponent<Button>();
      restart.onClick.AddListener(delegate {Restart(); });
      exit.onClick.AddListener(delegate {Exit(); });
    }

    // level failed functions

    // Restart method
    void Restart()
    {
      resetAll();
      switchPurple();
      // need to destroy UI
      Destroy(popUp);
    }

    // Exit method
    void Exit()
    {
      SceneManager.LoadScene("MainMenu");
    }

}
