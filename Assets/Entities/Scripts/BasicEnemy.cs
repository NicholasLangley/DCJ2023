using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public Material happy;
    public Material angry;

    bool playAttack = false;
    float attackDur = 100f;



    private void Start()
    {
        health = 3;
    }

    void Update()
    {
        

        if (playAttack)
        {
            attackDur += Time.deltaTime;

            Vector3 nextPos = new Vector3(transform.localPosition.x, -0.3f + 0.1f * Mathf.Sin(8* Mathf.PI * attackDur), transform.localPosition.z);
            transform.localPosition = nextPos;

            if (attackDur >= 0.125)
            {
                nextPos = new Vector3(transform.localPosition.x, -0.3f, transform.localPosition.z);
                transform.localPosition = nextPos;
                playAttack = false;
                gameObject.GetComponent<MeshRenderer>().material = happy;
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
        gameObject.GetComponent<MeshRenderer>().material = angry;
        sc.playSound(SoundEffectController.SoundClip.eAnger);
        attacking = true;
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
                c.damageEntity(2);
            }
        }
        attacking = false;
        playAttack = true;
        sc.playSound(SoundEffectController.SoundClip.eAttack);
        attackDur = 0f;
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
