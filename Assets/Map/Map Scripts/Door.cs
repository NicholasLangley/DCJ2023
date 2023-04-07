using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool isOpen = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().enabled = !isOpen;
    }

    public override void onInteract(Character c)
    {
        isOpen = !isOpen;
        sc.playSound(SoundEffectController.SoundClip.door);
    }

    public override bool isBlocking()
    {
        return !isOpen;
    }


}
