using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    public bool attacking;

    public override void damageEntity(int dmg)
    {
        setHealth(health - dmg);
    }

    public void setHealth(int hp)
    {
        health = hp;
        if(health <= 0)
        {
            kill();
        }
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    public abstract void advance();
}
