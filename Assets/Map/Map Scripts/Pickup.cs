using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public abstract void onPickup(Character c);
    public Vector3 getPos() { return transform.localPosition; }
    public void setPos(Vector3 p) { transform.localPosition = p; }
}
