using UnityEngine;
using TMPro;

public class CreditsScroll : MonoBehaviour {
    [SerializeField] private RectTransform creditsText;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float endPosition = 2000f; // Adjust based on text height

    private void Start() {
        string credits = "";
        credits += "FORGET ME NOT\n\n\n\n\n\n\n\n\n\n";
        credits += "Developed By\n";
        credits += "Viydal\n";
        credits += "Longy\n\n";
        credits += "Programming\n";
        credits += "Viydal\n";
        credits += "Longy\n\n";
        credits += "Art & Design\n";
        credits += "Viydal\n";
        credits += "Noodle\n";
        credits += "Some other people\n\n";
        credits += "Audio\n";
        credits += "Viydal\n";
        credits += "Some other people\n\n";
        credits += "Writing & Narrative\n";
        credits += "Viydal\n";
        credits += "Longy\n\n";
        credits += "QA & Testing\n";
        credits += "Whtbrd\n";
        credits += "Viydal\n";
        credits += "Longy\n";
        credits += "Asad\n";
        credits += "Longy's Mum\n\n";
        credits += "Special Thanks\n";
        credits += "Thank you to ourselves for pushing through\n the cold nights and hot days, without\n you this game wouldn't be here...\n\n";
        credits += "Thank you for playing\n\n";
        credits += "Made with love\n";
        credits += "Using Unity\n\n\n";
        credits += "Souls Collected - " + GameManager.Instance.soulCount + "/5";

        text.text = credits;
    }

    private void Update() {
        // Scroll credits upward
        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // When credits finish, return to main menu
        if (creditsText.anchoredPosition.y >= endPosition) {
            ReturnToMenu();
        }

        GameManager.Instance.gameWin = true;
    }

    private void ReturnToMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Main menu
    }
}