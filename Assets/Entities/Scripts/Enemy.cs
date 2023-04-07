using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    public bool attacking;
    public SoundEffectController sc;

    public override void damageEntity(int dmg)
    {
        setHealth(health - dmg);
    }

    public void setHealth(int hp)
    {
        health = hp;
        if(health <= 0)
        {
            sc.playSound(SoundEffectController.SoundClip.eDie);
            kill();
        }
        else
        {
            sc.playSound(SoundEffectController.SoundClip.eHurt);
        }
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    public abstract void advance();

    public  void setSc(SoundEffectController s)
    {
        sc = s;
    }
}
