using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject blood;
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
            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1<<6 | 1<<0))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);

                Health health = hitInfo.collider.GetComponent<Health>();

                if(health!=null)
                {
                    if(hitInfo.collider.name=="Enemy")
                    {
                        GameObject bloodSplatter= Instantiate(blood, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        bloodSplatter.transform.SetParent(hitInfo.transform);
                        Destroy(bloodSplatter, 1.5f);
                    }
                    health.Damage(10);
                }
            }
        }
    }
}
