using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{


    public bool canShoot = true;
    public float cooldown = 3f;
    public float ammo = 5f;

    void Start()
    {
        EventDispatcher.RegisterFunction<RaycastHit>("Shoot", Shoot);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void Shoot(RaycastHit hit)
    {
        if (canShoot && ammo > 0)
        {
            if (hit.transform.tag == "Customer")
            {
                GameObject hit_object = hit.transform.root.gameObject;
                Rigidbody[] rigidbodies = hit_object.GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rigidbody in rigidbodies)
                {
                    rigidbody.isKinematic = false;
                }
                Debug.Log(hit_object);
                hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(Camera.main.transform.forward * 500f, hit.point, ForceMode.Impulse);
            }
            canShoot = false;
            ammo -= 1;
            //shoot
            StartCoroutine(ShootCooldown());
        }
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
