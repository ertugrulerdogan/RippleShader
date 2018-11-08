using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public float Force;
    public PhysicMaterial Mat;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = Camera.main.transform.position;
            sphere.transform.localScale = Vector3.one * 0.25f;
            var r = sphere.AddComponent<Rigidbody>();
            r.AddForce(ray.direction* Force);
            var col = sphere.GetComponent<Collider>();
            col.material = Mat;

        }
    }
}
