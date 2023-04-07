using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Interactable
{
    private bool isOpen = false;
    private bool isLocked = true;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().enabled = !isOpen;
    }

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
    }

    public override bool isBlocking()
    {
        return !isOpen;
    }


}
