using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : Entity
{
    public enum PlayerColor { Red, Blue}
    

    [SerializeField]
    public HealthBar playerHealthBar;

    [SerializeField]
    public Image keyIndicator;

    [SerializeField]
    public PlayerColor playerColor;

    public GameController gameController;
    public MapBuilder map;
    public Arrow arrow;

    bool ammo = true;
    bool key = false;

    // Start is called before the first frame update
    void Start()
    {
        faceDirection();
        playerHealthBar.setHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.pause) {return; }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move();
            if (playerColor == PlayerColor.Blue) { gameController.advance(); }
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
        else if  (Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }
        else if (Input.GetKeyDown(KeyCode.Semicolon) &&  (playerColor == PlayerColor.Blue))
        {
            gameController.advance();
        }

    }

    void move()
    {
        Vector3 nextPos;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            nextPos = getTileInFront();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nextPos = getTileToLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            nextPos = getTileBehind();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextPos = getTileToRight();
        }
        else { return; }

        foreach (Vector3 obstacle in map.getWalls())
        {
            if (nextPos == obstacle)
            {
                return;
            }
        }
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
        foreach (Interactable i in interactables)
        {
            if (i.getPos() == nextPos && i.isBlocking())
            {
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
                return;
            }
        }
        Pushable[] pushables = GameObject.FindObjectsOfType<Pushable>();
        foreach (Pushable p in pushables)
        {
            if (p.getPos() == nextPos)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    if(!attemptPush(p))
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        transform.localPosition = nextPos;
        foreach (Vector3 pit in map.getPits())
        {
            if (nextPos == pit)
            {
                foreach (Pushable p in pushables)
                {
                    Vector3 groundlevel = p.getPos();
                    //compensate for cube falling in pit
                    groundlevel.y += 1;
                    if(pit == groundlevel)
                    {
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
                p.onPickup(this);
            }
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
        if (playerColor == PlayerColor.Blue) { gameController.advance(); }
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
        if (playerColor == PlayerColor.Blue) { gameController.advance(); }
    }

    

    void interact()
    {
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
        foreach(Interactable i in interactables)
        {
           if(i.getPos() == getTileInFront())
            {
                i.onInteract(gameObject.GetComponent<Character>());
                if (playerColor == PlayerColor.Blue) { gameController.advance(); }
            }
        }
    }

    void attack()
    {
        if (playerColor == PlayerColor.Blue)
        {
            if (ammo)
            {
                Arrow newArrow = Instantiate(arrow);
                newArrow.setDir(facing);
                newArrow.transform.localPosition = transform.localPosition;
                ammo = false;
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
        }
        if (playerColor == PlayerColor.Blue) { gameController.advance(); }
    }




   

    public override void damageEntity(int dmg)
    {
        health -= dmg;
        playerHealthBar.setHealth(health);
        if (health <= 0)
        {
            gameController.gameOver(playerColor);
        }
    }

    private void fall()
    {
        Vector3 position = transform.localPosition;
        position.y -= 1f;
        transform.localPosition = position;
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

    }
}
