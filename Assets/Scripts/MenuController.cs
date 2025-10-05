using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadSceneAsync("Level0");
    }
}
