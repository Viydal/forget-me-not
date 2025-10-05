using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameObject optionsPanel;
    public GameObject soundPanel;
    public GameObject mainMenu;
    public bool isPaused = false;
    public int soulCount = 0;
    public bool firstLaserDeath = true;
    public bool gameWin = false;
    public bool musicPlaying = false;


    private void Awake() {
        mainMenu = GameObject.Find("Main Menu");
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (gameWin && !musicPlaying) {
            AudioManager.instance.PlayOutro(AudioManager.instance.gameWin);
            musicPlaying = true;
        }
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

        if (Input.GetKeyDown(KeyCode.L)) {
            NextLevel();
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            PreviousLevel();
        }
    }

    public void UnPause() {
        optionsPanel.SetActive(false);
        isPaused = false;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void NextLevel() {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void PreviousLevel() {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
