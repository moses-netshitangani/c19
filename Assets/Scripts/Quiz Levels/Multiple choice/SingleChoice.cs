using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SingleChoice : MonoBehaviour
{
    //Possible questions and their answers
    public ArrayList question1 = new ArrayList() { "For how long is a person infectious?", "One month", "Up to two weeks", "Two to Three days", "One week" ,"2", "pers"};
    public ArrayList question2 = new ArrayList() { "What areas should your mask cover?", "Your mouth and nose", "Just your mouth", "Your mouth, nose and chin", "You should not wear a mask", "3", "com" };
    public ArrayList question3 = new ArrayList() { "How often should you wash your fabric mask?", "Everyday after use", "Once a week", "Every two to three days", "Bi-weekly","1", "both"};
    public ArrayList question4 = new ArrayList() { "What situation would require you to change your mask throughout the day?", "When you want a different pattern design on the front of your mask", "When your mask is wet or soiled.", "You should not change your mask", "None of the above","2", "pers"};
    public ArrayList question5 = new ArrayList() { "Which of the following is not a symptom of COVID-19? Fever, Cough, shortness of breath, fatigue, vision loss, loss of smell/taste, nausea, diarrhoea, or congestion.", "Nausea", "Vision loss", "Congestion", "Fever", "2", "pers"};
    public ArrayList question6 = new ArrayList() { "What is the minimum alcohol content that a hand sanitizer should have?", "60% if its ethanol,  70% if its isopropyl alcohol.", "50% if its ethanol,  60% if its isopropyl alcohol.", "40% if its ethanol,  30% if its isopropyl alcohol.", "30% if its ethanol,  40% if its isopropyl alcohol." ,"1", "pers"};
    public ArrayList question7 = new ArrayList() { "Once your mask is on, how should you remove or adjust it?", "Only from the mask's strings", "Only touching the front on the mask", "Only touching the edges of the mask", "None of the above" ,"1", "pers"};
    public ArrayList question8 = new ArrayList() { "How many layers should an ideal fabric mask have?", "Three-layers of the same fabric", "Two-layers: an outer layer and an inner layer", "One layer", "Three-layers: an outer layer, inner layer and middle/filter layer" ,"4", "com"};
    public ArrayList question9 = new ArrayList() { "What is the purpose of the outer layer of an ideal fabric mask", "It repels droplets and moisture", "Absorb droplets from your exhaled breath", "To make the mask fashionable", "It absorbs droplets and moisture" ,"1", "both"};
    public ArrayList question10 = new ArrayList() { "What is the purpose of the inner layer of an ideal fabric mask?", "It repels droplets and moisture", "Absorb droplets from your exhaled breath", "To make the mask fashionable", "It absorbs droplets and moisture" ,"2", "both"};
    public ArrayList question11 = new ArrayList() { "What measures should you not take when using public transport?", "Avoid touching surfaces", "Sanitize your hands", "Ensure the windows are open", "Minimize the space between yourself & the passengers" ,"4", "pers"};
    public ArrayList question12 = new ArrayList() { "Who is at higher risk of developing severe illness from COVID-19?", "Toddlers", "Doctors", "Those with underlying medical conditions", "Those who don't wear masks" ,"3", "pers"};


    //The ArrayList thats content will be displayed on the UI
    public ArrayList displayQuestion = new ArrayList();

    //Answer buttons
    public Button ans1, ans2, ans3, ans4;
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
    private Color blue = new Color32(28, 126, 221, 204);

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

    // storage script
    Level levelScript;

    // canned response
    public ArrayList responses = new ArrayList () {"Your avatar has been infected and is currently being kept in quarantine :(", " Your avatar was caught hugging his friends earlier today. ", "Due to proper lack of knowledge, your avatar thought that the reason for wearing a mask was to protect himself."};

    void Start()
    {
        //Button listener method
        ans1.onClick.AddListener(delegate {ChosenAnswer(1); });
        ans2.onClick.AddListener(delegate {ChosenAnswer(2); });
        ans3.onClick.AddListener(delegate {ChosenAnswer(3); });
        ans4.onClick.AddListener(delegate {ChosenAnswer(4); });

        // access score Script
        scoreScript = GameObject.Find("Quiz UI/Scores").GetComponent<Score>();
        infectionB = GameObject.Find("Quiz UI/InfectionBarContainer/HealthBar").GetComponent<InfectionBar>();

        // storage script
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
        Text option1 = GameObject.Find("Quiz UI/Solutions/TopLeft Sol/Text").GetComponent<Text>();
        option1.text = (string)displayQuestion[1];
        Text option2 = GameObject.Find("Quiz UI/Solutions/TopRight Sol/Text").GetComponent<Text>();
        option2.text = (string)displayQuestion[2];
        Text option3 = GameObject.Find("Quiz UI/Solutions/BottomLeft Sol/Text").GetComponent<Text>();
        option3.text = (string)displayQuestion[3];
        Text option4 = GameObject.Find("Quiz UI/Solutions/BottomRight Sol/Text").GetComponent<Text>();
        option4.text = (string)displayQuestion[4];

        //index of the correct answer
        correctAnswer = int.Parse((string)displayQuestion[5]);
        // Debug.Log("num value: "+num);
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
            if(string.Equals(displayQuestion[6], "both"))
            {
              scoreScript.setScore(1,1);
            }
            else if(string.Equals(displayQuestion[6], "pers"))
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

            // increase infection bar by 20
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

        switch (num)
        {
            case 0:
                if (number == 3)
                {
                    score += 1;
                }
                break;
            case 1:
                if (number == 2)
                {
                    score += 1;
                }
                break;
            case 2:
                if (number == 4)
                {
                    score += 1;
                }
                break;
            case 3:
                if (number == 4)
                {
                    score += 1;
                }
                break;
        }

    }

    void backToQuiz(){

        switchBlue();

        if (num < 12)
        {
            num++;
        }

        // End of level. Unlock 2nd level
        if(num>=12)
        {
          levelScript.unlockAndSaveLevel(true, false, false);

          if(scoreScript.getPersonalScore() == 10 && scoreScript.getCommunityScore() == 5)
          {
            PerfectPopUp();
          }else
          {
            levelPassedPopUp();
          }
        }

    }

    void switchRed()
    {
      switch(checkNumber)
      {
        case 1:
          ans1.GetComponent<Image>().color = Color.red;
          break;

        case 2:
          ans2.GetComponent<Image>().color = Color.red;
          break;

        case 3:
          ans3.GetComponent<Image>().color = Color.red;
          break;

        case 4:
          ans4.GetComponent<Image>().color = Color.red;
          break;
      }
    }

    void switchGreen()
    {
      switch(checkNumber)
      {
        case 1:
          ans1.GetComponent<Image>().color = Color.green;
          break;

        case 2:
          ans2.GetComponent<Image>().color = Color.green;
          break;

        case 3:
          ans3.GetComponent<Image>().color = Color.green;
          break;

        case 4:
          ans4.GetComponent<Image>().color = Color.green;
          break;
      }
    }

    // switch button colour to blue
    void switchBlue()
    {
      switch(checkNumber)
      {
        case 1:
          ans1.GetComponent<Image>().color = blue;
          break;

        case 2:
          ans2.GetComponent<Image>().color = blue;
          break;

        case 3:
          ans3.GetComponent<Image>().color = blue;
          break;

        case 4:
          ans4.GetComponent<Image>().color = blue;
          break;
      }
    }

    // show correct UI
    void correctPopUp(){
      popUp = Instantiate(correctUI, transform.position, Quaternion.identity) as GameObject;
      Text ans = GameObject.Find("CorrectUI(Clone)/AnswerText").GetComponent<Text>();
      ans.text = "Answer: \n" + (string)displayQuestion[correctAnswer];
    }

    // show incorrect UI
    void incorrectPopUp(){

      popUp = Instantiate(incorrectUI, transform.position, Quaternion.identity) as GameObject;
      Text ans = GameObject.Find("IncorrectUI(Clone)/AnswerText").GetComponent<Text>();
      ans.text = "Answer: \n" + (string)displayQuestion[correctAnswer];
    }

    // Perfect score UI
    void PerfectPopUp()
    {
      popUp = Instantiate(perfectScoreUI, transform.position, Quaternion.identity) as GameObject;

      continueButton = GameObject.Find("PerfectScoreUI(Clone)/ContinueButton").GetComponent<Button>();
      continueButton.onClick.AddListener(delegate {Exit(); });
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

    // reset all values
    public void resetAll()
    {
      num = 0;
      scoreScript.resetScore();
      infectionB.resetBar();
    }



    // level failed functions

    // Restart method
    void Restart()
    {
      resetAll();
      switchBlue();
      // need to destroy UI
      Destroy(popUp);
    }

    // Exit method
    void Exit()
    {
      SceneManager.LoadScene("MainMenu");
    }

}
