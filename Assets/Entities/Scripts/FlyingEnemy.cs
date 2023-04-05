using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    private void Start()
    {
        health = 2;
    }

    void Update()
    {
        Vector3 nextPos = new Vector3(transform.localPosition.x, 0.1f * Mathf.Sin(Mathf.PI * Time.time) + 0.2f, transform.localPosition.z);
        transform.localPosition = nextPos;
    }

    public override void advance()
    {
        if (attacking)
        {
            finishAttacking();
            return;
        }
        if (!checkForImpact())
        {
            transform.localPosition = getTileInFront();
        }
    }

    public bool checkForImpact()
    {
        Vector3 nextPos = getTileInFront();
        nextPos.y = 0;
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        foreach (Character c in characters)
        {
            Vector3 cPos = c.transform.localPosition;
            if (cPos == nextPos)
            {
                startAttacking();
                return true;
            }
        }
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies)
        {
            Vector3 ePos = e.transform.localPosition;
            ePos.y = 0;
            if (ePos == nextPos)
            {
                flipDirection();
                return true;
            }
        }
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
        foreach (Interactable i in interactables)
        {
            Vector3 iPos = i.transform.localPosition;
            if (i.isBlocking() && iPos == nextPos)
            {
                flipDirection();
                return true;
            }
        }
        Wall[] walls = GameObject.FindObjectsOfType<Wall>();
        foreach (Wall w in walls)
        {
            Vector3 wPos = w.transform.localPosition;
            if (wPos == nextPos)
            {
                flipDirection();
                return true;
            }
        }
        Pushable[] pushers = GameObject.FindObjectsOfType<Pushable>();
        foreach (Pushable p in pushers)
        {
            Vector3 pPos = p.transform.localPosition;
            if (pPos == nextPos)
            {
                flipDirection();
                return true;
            }
        }
        return false;
    }

    void startAttacking()
    {
        attacking = true;
    }

    void finishAttacking()
    {
        Vector3 frontTile = getTileInFront();
        frontTile.y = 0;
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        foreach (Character c in characters)
        {
            Vector3 cPos = c.transform.localPosition;
            if (cPos == frontTile)
            {
                c.damageEntity(1);
            }
        }
        attacking = false;
        Debug.Log("FLY ATTACK!");
    }

    void flipDirection()
    {
        switch (facing)
        {
            case Direction.North:
                facing = Direction.South;
                break;
            case Direction.South:
                facing = Direction.North;
                break;
            case Direction.East:
                facing = Direction.West;
                break;
            case Direction.West:
                facing = Direction.East;
                break;
        }
        faceDirection();
    }
}
