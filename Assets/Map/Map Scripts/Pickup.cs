using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public SoundEffectController sc;

    public abstract void onPickup(Character c);
    public Vector3 getPos() { return transform.localPosition; }
    public void setPos(Vector3 p) { transform.localPosition = p; }

    public void setSc(SoundEffectController s)
    {
        sc = s;
    }
}
