using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Material visible, notVisible;

    private bool isOpen = false;
    // Start is called before the first frame update

    // Update is called once per frame

    public override void onInteract(Character c)
    {
        isOpen = !isOpen;
        if(isOpen)
        {
            gameObject.GetComponent<MeshRenderer>().material = notVisible;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = visible;
        }
        sc.playSound(SoundEffectController.SoundClip.door);
    }

    public override bool isBlocking()
    {
        return !isOpen;
    }


}
