using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public Material happy;
    public Material angry;

    bool playAttack = false;
    float attackDur = 100f;

    private void Start()
    {
        health = 2;
    }

    void Update()
    {
        Vector3 nextPos = new Vector3(transform.localPosition.x, 0.05f * Mathf.Sin(Mathf.PI * Time.time) - 0.3f, transform.localPosition.z);
        transform.localPosition = nextPos;

        if (playAttack)
        {
            attackDur += Time.deltaTime;

            nextPos = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.2f * Mathf.Sin(8 * Mathf.PI * attackDur), transform.localPosition.z);
            transform.localPosition = nextPos;

            if (attackDur >= 0.125)
            {
                nextPos = new Vector3(transform.localPosition.x, 0.05f * Mathf.Sin(Mathf.PI * Time.time) - 0.3f, transform.localPosition.z);
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
        gameObject.GetComponent<MeshRenderer>().material = angry;
        attacking = true;
        sc.playSound(SoundEffectController.SoundClip.eAnger);
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
        playAttack = true;
        sc.playSound(SoundEffectController.SoundClip.eAttack);
        attackDur = 0f;
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

    public override void faceDirection()
    {
        if (facing == Direction.North)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (facing == Direction.South)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (facing == Direction.East)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
