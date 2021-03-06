using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlatformerMenu : MonoBehaviour
{
    // Menu buttons
    public Button resume;
    public Button restart;
    public Button help;
    public Button exit;

    // Level script
    // TrueFalse levelScript;

    // Level's Help UI
    public GameObject helpUI;
    private GameObject helpUIPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.activeInHierarchy)
        {
          gameObject.SetActive(false);
        }

        // button listeners
        resume.onClick.AddListener(delegate {Resume(); });
        restart.onClick.AddListener(delegate {Restart(); });
        help.onClick.AddListener(delegate {Help(); });
        exit.onClick.AddListener(delegate {Exit(); });

        // find the level's main script
        // levelScript = GameObject.Find("Quiz UI").GetComponent<TrueFalse>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Resume method
    void Resume()
    {
      gameObject.SetActive(false);
    }

    // Restart method
    void Restart()
    {
      SceneManager.LoadScene("SampleScene");
    }

    // Help method
    void Help()
    {
      helpUIPrefab = Instantiate(helpUI, transform.position, Quaternion.identity) as GameObject;
      Resume();
    }

    // Exit method
    void Exit()
    {
      SceneManager.LoadScene("MainMenu");
    }

}
