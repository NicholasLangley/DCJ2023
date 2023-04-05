using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Pickup
{
    public override void onPickup(Character c)
    {
        GameController gc = GameObject.FindObjectOfType<GameController>();
        gc.levelUp();
    }

    void Update()
    {
       Vector3 nextPos =  new Vector3(transform.localPosition.x, 0.1f*Mathf.Sin(Mathf.PI * Time.time) - 0.2f , transform.localPosition.z);
       transform.localPosition = nextPos;
        transform.Rotate(0, 70 * Time.deltaTime, 0);
    }
}
