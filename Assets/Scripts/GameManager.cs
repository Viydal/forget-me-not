using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameObject optionsPanel;
    public GameObject soundPanel;
    public GameObject mainMenu;
    public bool isPaused = false;

    public int soulCount = 0;
    public bool firstLaserDeath = true;
    private void Awake()
    {
        mainMenu = GameObject.Find("Main Menu");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && mainMenu == null) {
            Debug.Log("Options panel active");
            if (!isPaused) {
                optionsPanel.SetActive(true);
                isPaused = true;
                if (mainMenu != null) {
                    mainMenu.SetActive(false);
                }
            } else {
                optionsPanel.SetActive(false);
                isPaused = false;
                if (mainMenu != null) {
                    mainMenu.SetActive(true);
                }
            }
        }
    }

    public void UnPause() {
        optionsPanel.SetActive(false);
        isPaused = false;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
