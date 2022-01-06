using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    void Update()
    {
        ShootRay();
    }

    void ShootRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);
            }
        }
    }
}
