using UnityEngine;

public class Level2DeathPrompt : MonoBehaviour
{
    [SerializeField] private GameObject popUpPrefab;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.level2Death)
            {
                GameManager.Instance.level2Death = false;
                GameObject popUpObject = Instantiate(popUpPrefab, new Vector3(0, 0, 0), new Quaternion());
                popUpObject.GetComponent<PopUp>().text_value = "Press R to Respawn.";
            }
        }
    }
}
