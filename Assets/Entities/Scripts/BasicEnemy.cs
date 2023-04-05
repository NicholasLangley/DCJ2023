using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private void Start()
    {
        health = 3;
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
                rotateCounterClockwise();
                return true;
            }
        }
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
        foreach (Interactable i in interactables)
        {
            Vector3 iPos = i.transform.localPosition;
            if (i.isBlocking() && iPos == nextPos)
            {
                rotateCounterClockwise();
                return true;
            }
        }
        Wall[] walls = GameObject.FindObjectsOfType<Wall>();
        foreach (Wall w in walls)
        {
            Vector3 wPos = w.transform.localPosition;
            if (wPos == nextPos)
            {
                rotateCounterClockwise();
                return true;
            }
        }
        HalfWall[] halfWalls = GameObject.FindObjectsOfType<HalfWall>();
        foreach (HalfWall hw in halfWalls)
        {
            Vector3 hwPos = hw.transform.localPosition;
            hwPos.y = 0;
            if (hwPos == nextPos)
            {
                rotateCounterClockwise();
                return true;
            }
        }
        Pit[] pits = GameObject.FindObjectsOfType<Pit>();
        foreach (Pit p in pits)
        {
            Vector3 pPos = p.transform.localPosition;
            pPos.y = 0;
            if (pPos == nextPos)
            {
                rotateCounterClockwise();
                return true;
            }
        }
        Pushable[] pushers = GameObject.FindObjectsOfType<Pushable>();
        foreach (Pushable p in pushers)
        {
            Vector3 pPos = p.transform.localPosition;
            if (pPos == nextPos)
            {
                rotateCounterClockwise();
                return true;
            }
        }
        return false;
    }

    void rotateCounterClockwise()
    {
        if (facing == Direction.North)
        {
            facing = Direction.West;
        }
        else
        {
            facing -= 1;
        }
        faceDirection();
    }

    void startAttacking()
    {
        attacking = true;
    }

    void finishAttacking()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        foreach (Character c in characters)
        {
            Vector3 cPos = c.transform.localPosition;
            if (cPos == getTileInFront())
            {
                c.damageEntity(2);
            }
        }
        attacking = false;
        Debug.Log("basic attack!");
    }
}
