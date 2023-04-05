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
        }
        else
        {
            if (c.haskey())
            {
                isLocked = false;
                isOpen = !isOpen;
                c.useKey();
            }
            else
            {
                c.needKey();
            }
        }
    }

    public override bool isBlocking()
    {
        return !isOpen;
    }


}
