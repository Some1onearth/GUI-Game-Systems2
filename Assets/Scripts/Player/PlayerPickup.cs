using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public static Vector3 dropPos;

    private void Update()
    {
        dropPos = transform.position + transform.forward * 2;
    }
    private void FixedUpdate()
    {

        Collider[] items = Physics.OverlapSphere(transform.position, 1f, 1 << 8);
        foreach (Collider collider in items)
        {
            collider.GetComponent<PickUpItem>().PickedUp();
        }
    }
}
