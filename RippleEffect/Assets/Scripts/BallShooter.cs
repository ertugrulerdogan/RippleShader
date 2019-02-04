#undef DEBUG_RAYCAST
using System.Collections;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public float Force;
    public PhysicMaterial Mat;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
#if DEBUG_RAYCAST
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                var ripple = FindObjectOfType<RippleCollision>();
                if (ripple != null)
                {
                    ripple.CreateWave(hitInfo.point, 25);
                }
            }
#else
            ShootBall();
#endif
        }
    }

    private void ShootBall()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = Camera.main.transform.position;
        sphere.transform.localScale = Vector3.one * 0.25f;

        var r = sphere.AddComponent<Rigidbody>();
        if (r != null)
        {
            r.collisionDetectionMode = CollisionDetectionMode.Continuous;
            r.AddForce(ray.direction * Force);
        }

        var col = sphere.GetComponent<Collider>();
        if (col != null)
        {
            col.material = Mat;
        }

        StartCoroutine(DestroySelf(sphere));
    }

    private IEnumerator DestroySelf(GameObject go)
    {
        yield return new WaitForSeconds(5f);
        if (go != null)
        {
            Destroy(go);
        }
    }
}
