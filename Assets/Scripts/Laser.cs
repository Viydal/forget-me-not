using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour {
    [SerializeField] private float defDistanceRay = 20f;

    [SerializeField] private string laserDirection;

    [SerializeField] private GameObject popUpPrefab;
    
    public Transform LaserOrigin;
    public LineRenderer lineRenderer;
    Transform m_transform;

    private void Awake() {
        m_transform = GetComponent<Transform>();
    }

    private void Update() {
        ShootLaser();
    }

    void ShootLaser() {
        Vector2 origin = LaserOrigin.position;
        Vector2 direction = Vector2.right;
        if (laserDirection == "Right") {
            direction = Vector2.right;
        } else if (laserDirection == "Left") {
            direction = Vector2.left;
        } else if (laserDirection == "Up") {
            direction = Vector2.up;
        } else if (laserDirection == "Down") {
            direction = Vector2.down;
        } else {
            Debug.Log("Invalid laser direction");
        }
        
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, defDistanceRay);

        if (hit.collider != null) {
            Draw2DRay(LaserOrigin.position, hit.point);

            if (hit.collider.CompareTag("Ghost"))
            {
                Ghost ghost = hit.collider.GetComponent<Ghost>();
                ghost.StartFade(); // fade out ghost
                if (GameManager.Instance.firstLaserDeath)
                {
                    GameManager.Instance.firstLaserDeath = false;
                    Debug.Log("First laser death");
                    GameObject popUpObject = Instantiate(popUpPrefab, hit.collider.transform.position + new Vector3(0, 2, 0), new Quaternion());
                    popUpObject.GetComponent<PopUp>().text_value = "Ghosts can't touch the light.";
                }
            }
        } else {
            Draw2DRay(origin, origin + direction * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos) {
        lineRenderer.SetPosition(0, new Vector3(startPos.x, startPos.y, 0));
        lineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y, 0));
    }
}
