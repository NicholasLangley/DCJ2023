using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public SoundEffectController sc;

    private bool blocking = true;
    
    public abstract void onInteract(Character c);
    public Vector3 getPos() { return gameObject.transform.localPosition;}
    public void setPos(Vector3 newPos) { gameObject.transform.localPosition = newPos; }
    public virtual bool isBlocking() { return blocking; }

    public void setSc(SoundEffectController s)
    {
        sc = s;
    }
}
