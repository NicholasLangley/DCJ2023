using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : Entity
{
    public enum PlayerColor { Red, Blue, VICTORY }
    public enum MoveDirection { Forward, Back, Left, Right}

    public Image BlueTooHeavy;
    public TextMeshProUGUI BlueTooHeavyText;
    float TooHeavyTime = 100f;

    public AudioSource Swing, Shoot, reloadBow, step, hurt, bump;

    [SerializeField]
    public HealthBar playerHealthBar;
    private float lastDamagedAt = 100f;
    public Image damageArt;

    [SerializeField]
    public Image keyIndicator, weaponNormal, Weapon2;

    [SerializeField]
    public PlayerColor playerColor;

    public GameController gameController;
    public MapBuilder map;
    public Arrow arrow;

    bool ammo = true;
    bool key = false;

    float timeSinceLastSwing = 10000f;

    // Start is called before the first frame update
    void Start()
    {
        faceDirection();
        playerHealthBar.setHealth(health);
        damageArt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.pause) { return; }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            move((int)MoveDirection.Forward);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move((int)MoveDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            move((int)MoveDirection.Back);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            move((int)MoveDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotateClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            rotateCounterClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            interact();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }
        else if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            gameController.advance();
        }
        if(timeSinceLastSwing < 0.1f && playerColor == PlayerColor.Red)
        {
            timeSinceLastSwing += Time.deltaTime;
        }
        else if (playerColor == PlayerColor.Red )
        {
            Weapon2.enabled = false;
            weaponNormal.enabled = true;
        }

        if (lastDamagedAt <= 0.05f)
        {
            damageArt.enabled = true;
            lastDamagedAt += Time.deltaTime;
        }
        else
        {
            damageArt.enabled = false;
        }

        if (playerColor == PlayerColor.Blue)
        {
            if (TooHeavyTime <= 1f)
            {
                TooHeavyTime += Time.deltaTime;
                BlueTooHeavy.enabled = true;
                BlueTooHeavyText.enabled = true;
            }
            else
            {
                BlueTooHeavy.enabled = false;
                BlueTooHeavyText.enabled = false;
            }
        }
    }

    public void move(int dir)
    {
        Vector3 nextPos;
        if (dir == (int)MoveDirection.Forward)
        {
            nextPos = getTileInFront();
        }
        else if (dir == (int)MoveDirection.Back)
        {
            nextPos = getTileBehind();
        }
        else if (dir == (int)MoveDirection.Left)
        {
            nextPos = getTileToLeft();
        }
        else
        {
            nextPos = getTileToRight();
        }


        foreach (Vector3 obstacle in map.getWalls())
        {
            if (nextPos == obstacle)
            {
                gameController.advance();
                bump.Play();
                return;
            }
        }
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
        foreach (Interactable i in interactables)
        {
            if (i.getPos() == nextPos && i.isBlocking())
            {
                 gameController.advance();
                bump.Play();
                return;
            }
        }
        Entity[] entities = GameObject.FindObjectsOfType<Entity>();
        foreach (Entity e in entities)
        {
            Vector3 eNormalPos = e.getPos();
            eNormalPos.y = 0;
            if (eNormalPos == nextPos)
            {
                gameController.advance();
                bump.Play();
                return;
            }
        }
        Pushable[] pushables = GameObject.FindObjectsOfType<Pushable>();
        foreach (Pushable p in pushables)
        {
            if (p.getPos() == nextPos)
            {
                if (dir == (int)MoveDirection.Forward)
                {
                    if (!attemptPush(p))
                    {
                        gameController.advance();
                        bump.Play();
                        return;
                    }
                }
                else
                {
                     gameController.advance();
                    bump.Play();
                    return;
                }
            }
        }
        transform.localPosition = nextPos;
        step.Play();
        foreach (Vector3 pit in map.getPits())
        {
            if (nextPos == pit)
            {
                foreach (Pushable p in pushables)
                {
                    Vector3 groundlevel = p.getPos();
                    //compensate for cube falling in pit
                    groundlevel.y += 1;
                    if (pit == groundlevel)
                    {
                        gameController.advance();
                        return;
                    }
                }
                fall();
            }
        }
        Pickup[] pickups = GameObject.FindObjectsOfType<Pickup>();
        foreach (Pickup p in pickups)
        {

            if (nextPos.x == p.getPos().x && nextPos.z == p.getPos().z)
            {
                gameController.advance();
                p.onPickup(this);
                return;
            }
        }
        gameController.advance();
    }

    public void rotateClockwise()
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
        gameController.advance();
    }

    public void rotateCounterClockwise()
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
        gameController.advance();
    }



    public void interact()
    {
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
        foreach (Interactable i in interactables)
        {
            if (i.getPos() == getTileInFront())
            {
                i.onInteract(gameObject.GetComponent<Character>());
                gameController.advance();
                return;
            }
        }
        gameController.advance();
    }

    public void attack()
    {
        if (playerColor == PlayerColor.Blue)
        {
            if (ammo)
            {
                Arrow newArrow = Instantiate(arrow);
                newArrow.setDir(facing);
                newArrow.transform.localPosition = transform.localPosition;
                if (facing == Direction.North)
                {
                    newArrow.transform.localRotation = Quaternion.Euler(90, 0, 00);
                }
                else if (facing == Direction.East)
                {
                    newArrow.transform.localRotation = Quaternion.Euler(0, 0, -90);
                }
                else if (facing == Direction.South)
                {
                    newArrow.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                }
                else
                {
                    newArrow.transform.localRotation = Quaternion.Euler(0, 0, 90);
                }
                ammo = false;
                Weapon2.enabled = true;
                weaponNormal.enabled = false;
                Shoot.Play();
            }
            else { reload(); }
        }
        else
        {
            Entity[] entities = GameObject.FindObjectsOfType<Entity>();
            foreach (Entity e in entities)
            {
                if (e.getPos().x == getTileInFront().x && e.getPos().z == getTileInFront().z)
                {
                    e.damageEntity(1);
                }
            }
            swingSword();
            Swing.Play();
        }
        gameController.advance();
    }






    public override void damageEntity(int dmg)
    {
        lastDamagedAt = 0f;
        health -= dmg;
        hurt.Play();
        playerHealthBar.setHealth(health);
        if (health <= 0)
        {
            gameController.gameOver(playerColor);
        }
    }

    private void fall()
    {
        lastDamagedAt = 0f;
        Vector3 position = transform.localPosition;
        position.y -= 1f;
        transform.localPosition = position;
        hurt.Play();
        setHealth(0);
    }

    private bool attemptPush(Pushable p)
    {
        List<Vector3> allColliders = new List<Vector3>();
        allColliders.AddRange(map.getWalls());
        Entity[] entities = GameObject.FindObjectsOfType<Entity>();
        foreach (Entity e in entities)
        {
            allColliders.Add(e.transform.localPosition);
        }
        Pushable[] pushables = GameObject.FindObjectsOfType<Pushable>();
        foreach (Pushable pushable in pushables)
        {
            allColliders.Add(pushable.transform.localPosition);
        }
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
        foreach (Interactable i in interactables)
        {
            if (i.isBlocking())
            {
                allColliders.Add(i.transform.localPosition);
            }
        }
        return (p.push(facing, allColliders, map.getPits(), playerColor, gameObject.GetComponent<Character>()));
    }

    public void setHealth(int hp)
    {
        health = hp;
        playerHealthBar.setHealth(health);
        if (health <= 0)
        {
            gameController.gameOver(playerColor);
        }
    }



    public void reload()
    {
        ammo = true;
        Weapon2.enabled = false;
        weaponNormal.enabled = true;
        reloadBow.Play();
    }

    private void swingSword()
    {
        timeSinceLastSwing = 0f;
        Weapon2.enabled = true;
        weaponNormal.enabled = false;
    }

    public void giveKey()
    {
        key = true;
        keyIndicator.enabled = true;
    }
    public void useKey()
    {
        key = false;
        keyIndicator.enabled = false;
    }
    public bool haskey()
    {
        return key;
    }
    public void needKey()
    {

    }

    public void tooHeavy()
    {

        TooHeavyTime = 0f;
        BlueTooHeavy.enabled = true;
        BlueTooHeavyText.enabled = true;
    }
}
