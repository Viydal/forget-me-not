using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject popUpPrefab;
    private bool canEnter = false;

  private void Update() {
        if (canEnter && Input.GetKeyDown(KeyCode.W)) {
            SceneTransitionManager.Instance.FadeToScene(nextSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            canEnter = true;
            Debug.Log("Press W to enter the door.");
            if (GameManager.Instance.firstDoorInteraction)
            {
                GameManager.Instance.firstDoorInteraction = false;
                GameObject popUpObject = Instantiate(popUpPrefab, new Vector3(0, 0, 0), new Quaternion());
                popUpObject.GetComponent<PopUp>().text_value = "Press W to enter the door.";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canEnter = false;
        }
    }
}