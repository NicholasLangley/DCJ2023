using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Pickup
{
    public override void onPickup(Character c)
    {
        GameController gc = GameObject.FindObjectOfType<GameController>();
        sc.playSound(SoundEffectController.SoundClip.mirror);
        gc.levelUp();
        Destroy(gameObject);
    }

    void Update()
    {
       Vector3 nextPos =  new Vector3(transform.localPosition.x, 0.1f*Mathf.Sin(Mathf.PI * Time.time), transform.localPosition.z);
       transform.localPosition = nextPos;
        transform.Rotate(0, 70 * Time.deltaTime, 0);
    }
}
