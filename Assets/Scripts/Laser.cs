using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100f;
    public Transform LaserOrigin;
    public LineRenderer lineRenderer;
    Transform m_transform;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, m_transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(LaserOrigin.position, transform.right);
            Draw2DRay(LaserOrigin.position, hit.point);
            if (hit.collider.CompareTag("Ghost"))
            {
                Ghost ghost = hit.collider.GetComponent<Ghost>();
                ghost.StartFade(); // fade out ghost
            }
        }
        else
        {
            Draw2DRay(LaserOrigin.position, LaserOrigin.transform.right * defDistanceRay);
        }
    }
    
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
