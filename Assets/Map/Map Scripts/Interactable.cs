using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool blocking = true;
    
    public abstract void onInteract(Character c);
    public Vector3 getPos() { return gameObject.transform.localPosition;}
    public void setPos(Vector3 newPos) { gameObject.transform.localPosition = newPos; }
    public virtual bool isBlocking() { return blocking; }
}
