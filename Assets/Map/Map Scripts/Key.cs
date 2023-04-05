using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Pickup
{
    public override void onPickup(Character c)
    {
        c.giveKey();
        Destroy(gameObject);
    }

    void Update()
    {
       Vector3 nextPos =  new Vector3(transform.localPosition.x, 0.1f*Mathf.Sin(Mathf.PI * Time.time) - 0.2f , transform.localPosition.z);
       transform.localPosition = nextPos;
        transform.Rotate(0, 60 * Time.deltaTime, 0);
    }
}
