using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    [SerializeField] private string nextSceneName;
    private bool canEnter = false;

  private void Update() {
        if (canEnter && Input.GetKeyDown(KeyCode.W)) {
            SceneTransitionManager.Instance.FadeToScene(nextSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canEnter = true;
            Debug.Log("Press W to enter the door.");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canEnter = false;
        }
    }
}