using UnityEngine;

public class Sight : MonoBehaviour
{
    public float distance;
    public float angle;

    public LayerMask targetLayers;
    public LayerMask obstacleLayers;

    public Collider detectedTarget;

    private Collider[] colliders;

    private void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, distance, targetLayers);

        detectedTarget = null;

        foreach (Collider collider in colliders)
        {
            Vector3 directionToCollider = collider.bounds.center - transform.position;
            directionToCollider = Vector3.Normalize(directionToCollider);

            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);

            if (angleToCollider < angle)
            {
                if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstacleLayers))
                {
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    detectedTarget = collider;
                    break;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

        Gizmos.color = Color.magenta;
        Vector3 rightDir = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDir * distance);

        Vector3 leftDir = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDir * distance);
    }
}