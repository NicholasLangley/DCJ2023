using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    public AudioSource enemyHurt, enemyAnger, enemyAttacks, enemyDies, potion, key, mirror, door, lockedDoor, boulder;
    public enum SoundClip {eHurt, eAnger, eAttack, eDie, potion, key, door, mirror, lockedDoor, boulder}
    

    public void playSound(SoundClip s)
    {
        switch (s)
        {
            case (SoundClip.eHurt):
                enemyHurt.Play();
                break;
            case (SoundClip.eAnger):
                enemyAnger.Play();
                break;
            case (SoundClip.eAttack):
                enemyAttacks.Play();
                break;
            case (SoundClip.eDie):
                enemyDies.Play();
                break;
            case (SoundClip.potion):
                potion.Play();
                break;
            case (SoundClip.key):
                key.Play();
                break;
            case (SoundClip.mirror):
                mirror.Play();
                break;
            case (SoundClip.door):
                door.Play();
                break;
            case (SoundClip.lockedDoor):
                lockedDoor.Play();
                break;
            case (SoundClip.boulder):
                boulder.Play();
                break;
            default:
                break;
        }

    }
}
