using UnityEngine;
using UnityEngine.UI; 
using TMPro;  

public class PopUp : MonoBehaviour
{
    [SerializeField] TMP_Text popUpText;
    public string text_value;
    void Start()
    {
        popUpText.text = text_value;
        Destroy(gameObject, 5f);
    }

}
