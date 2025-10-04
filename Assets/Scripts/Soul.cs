using UnityEngine;
using System.Collections;

public class Soul : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;
    [SerializeField] private BoxCollider2D box;

    // AudioManager audioManager;

    private bool entered = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && entered == false) {
            entered = true;
            Debug.Log("Player collected soul");

            AudioManager.instance.PlaySFX(AudioManager.instance.collectSoul);
            
            GameManager.Instance.soulCount++;
            animator.SetBool("isConsumed", true);
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation() {
        // Wait for the current animation state to update
        yield return null;

        // Get the current animation state info
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Wait for the length of the animation
        yield return new WaitForSeconds(stateInfo.length);

        Destroy(gameObject); // Destroy soul after animation finishes
    }
}
