using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldEnemy : Enemy
{
    private void Start()
    {
        health = 5;
    }

    public override void advance()
    {
        if (attacking)
        {
            finishAttacking();
            return;
        }
        if (checkForPlayer(getTileInFront()))
        {
            startAttacking();
        }
        else if (checkForPlayer(getTileToLeft()))
        {
            rotateCounterClockwise();
            startAttacking();
        }
        else if (checkForPlayer(getTileToRight()))
        {
            rotateClockwise();
            startAttacking();
        }
    }

    public bool checkForPlayer(Vector3 pos)
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        foreach (Character c in characters)
        {
            Vector3 cPos = c.transform.localPosition;
            if (cPos == pos)
            {
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
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        foreach (Character c in characters)
        {
            Vector3 cPos = c.transform.localPosition;
            if (cPos == getTileInFront())
            {
                c.damageEntity(4);
            }
        }
        attacking = false;
        Debug.Log("SHIELD SMASH");
        if(!checkForPlayer(getTileInFront()))
        {
            transform.localPosition = getTileInFront();
        }
    }

    void rotateClockwise()
    {
        if (facing == Direction.West)
        {
            facing = Direction.North;
        }
        else
        {
            facing += 1;
        }
        faceDirection();
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
}
