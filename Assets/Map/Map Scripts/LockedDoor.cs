using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable
{
    private bool isOpen = false;
    private bool isLocked = true;

    public Material visible, notVisible;


    public override void onInteract(Character c)
    {
        if (!isLocked)
        {
            isOpen = !isOpen;
            sc.playSound(SoundEffectController.SoundClip.door);
        }
        else
        {
            if (c.haskey())
            {
                isLocked = false;
                isOpen = !isOpen;
                c.useKey();
                sc.playSound(SoundEffectController.SoundClip.door);
            }
            else
            {
                c.needKey();
                sc.playSound(SoundEffectController.SoundClip.lockedDoor);
            }
        }
        if (isOpen)
        {
            gameObject.GetComponent<MeshRenderer>().material = notVisible;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = visible;
        }
    }

    public override bool isBlocking()
    {
        return !isOpen;
    }


}
