using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldEnemy : Enemy
{

    public Mesh standing, angry, attackMesh;

    bool playAttack = false;
    float attackDur = 100f;

    private void Start()
    {
        health = 5;
    }

    private void Update()
    {
        if (playAttack)
        {
            attackDur += Time.deltaTime;

            Vector3 nextPos = new Vector3(transform.localPosition.x, -0.51f + 0.1f * Mathf.Sin(8 * Mathf.PI * attackDur), transform.localPosition.z);
            transform.localPosition = nextPos;

            if (attackDur >= 0.125)
            {
                nextPos = new Vector3(transform.localPosition.x, -0.51f, transform.localPosition.z);
                transform.localPosition = nextPos;
                playAttack = false;
                gameObject.GetComponent<MeshFilter>().sharedMesh = standing;
            }
        }
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
            pos.y = 0;
            cPos.y = 0;
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
        gameObject.GetComponent<MeshFilter>().sharedMesh = angry;
        sc.playSound(SoundEffectController.SoundClip.eAnger);
    }

    void finishAttacking()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();
        foreach (Character c in characters)
        {
            Vector3 cPos = c.transform.localPosition;
            cPos.y = 0f;
            Vector3 frontTile = getTileInFront();
            frontTile.y = 0;
            if (cPos == frontTile)
            {
                c.damageEntity(4);
            }
        }
        gameObject.GetComponent<MeshFilter>().sharedMesh = attackMesh;
        attacking = false;
        playAttack = true;
        attackDur = 0f;
        Vector3 nextPos = getTileInFront();
        nextPos.y = 0;
        if (!checkForPlayer(nextPos))
        {
            nextPos = getTileInFront();
            transform.localPosition = getTileInFront();
        }
        sc.playSound(SoundEffectController.SoundClip.eAttack);
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

    public override void faceDirection()
    {
        if (facing == Direction.North)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (facing == Direction.South)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (facing == Direction.East)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
