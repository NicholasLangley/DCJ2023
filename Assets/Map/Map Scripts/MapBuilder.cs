using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    
    public Transform wallTile, halfWallTile, doorTile, lockedDoorTile, floorTile, ceilingTile, pitTile, redPlayer, bluePlayer, boulderTile, heavyBoulderTile, potionTile, keyTile, mirrorTile;

    public Character redPlayerScript, bluePlayerScript;

    public EnemyController eController;

    static Color
        wall = Color.white, halfWall = new Color(174f / 255f, 1, 1, 1), clear = Color.clear,
        door = new Color(127f / 255f, 127f / 255f, 127f / 255f, 1), lockedDoor = new Color(135f / 255f, 104f / 255f, 101f / 255f, 1),
        redSpawnN = Color.red, redSpawnE = new Color(180f / 255f, 0, 0, 1), redSpawnS = new Color(130f / 255f, 0, 0, 1), redSpawnW = new Color(90f / 255f, 0, 0, 1),
        blueSpawnN = Color.blue, blueSpawnE = new Color(0, 0, 170f / 255f, 1), blueSpawnS = new Color(0, 0, 120f / 255f, 1), blueSpawnW = new Color(0, 0, 60f / 255f, 1),
        boulder = new Color(1, 1, 0, 1), heavyBoulder = new Color(1, 127f / 255, 0, 1),
        pit = Color.magenta,
        basicEnemyN = new Color(127f / 255f, 0, 1, 1), basicEnemyE = new Color(90f / 255f, 0, 190f / 255f, 1), basicEnemyS = new Color(60f / 255f, 0, 170f / 255f, 1), basicEnemyW = new Color(30f / 255f, 0, 100f / 255f, 1),
        flyingEnemyN = Color.cyan, flyingEnemyE = new Color(0, 200f / 255f, 200f / 255f, 1),
        shieldEnemyN = new Color(255f / 255f, 127f / 255f, 127f / 255f, 1), shieldEnemyE = new Color(220f / 255f, 100f / 255f, 100f / 255f, 1), shieldEnemyS = new Color(200f / 255f, 70f / 255f, 70f / 255f, 1), shieldEnemyW = new Color(170f / 255f, 50f / 255f, 50f / 255f, 1),
        potion = Color.green, key = new Color(120f / 255f, 120f / 255f, 0, 1),
        mirror = new Color(96f/255, 115f/255f, 250f/255f, 1);


    [SerializeField]
    Texture2D map;

    private List<Vector3> walls;
    private List<Vector3> pits;
    // Start is called before the first frame update
    void Awake()
    {
        walls = new List<Vector3>();
        pits = new List<Vector3>();
    }

    public void BuildLevel(Texture2D mapImage)
    {
        Color[] pixels = mapImage.GetPixels();

        for (int x = 0; x < mapImage.height; x++)
        {
            for (int z = 0; z < mapImage.width; z++)
            {
                Color pix = pixels[(x * mapImage.width) + z];

                if (pix != clear)
                {
                    Transform newCeiling = Instantiate(ceilingTile);
                    newCeiling.localPosition = new Vector3(z, +1f, x);
                    newCeiling.rotation = Quaternion.Euler(180, 0, 0);
                    newCeiling.SetParent(transform);
                }

                if (pix != pit && pix != clear)
                {
                    Transform newFloor = Instantiate(floorTile);
                    newFloor.localPosition = new Vector3(z, -1f, x);
                    newFloor.SetParent(transform);
                }
                else
                {
                    pits.Add(new Vector3(z, 0, x));
                    Vector3 position = new Vector3(z, 0, x);
                    Transform newPit = Instantiate(pitTile);
                    newPit.localPosition = position;
                    newPit.SetParent(transform);
                }

                if (pix == wall)
                {
                    Vector3 position = new Vector3(z, 0, x);
                    Transform newWall = Instantiate(wallTile);
                    newWall.localPosition = position;
                    newWall.SetParent(transform);
                    walls.Add(position);
                }
                if (pix == halfWall)
                {
                    Vector3 position = new Vector3(z, 0, x);
                    walls.Add(position);
                    position.y = -0.35f;
                    Transform newHalfWall = Instantiate(halfWallTile);
                    newHalfWall.localPosition = position;
                    newHalfWall.SetParent(transform);
                }
                else if (pix == door)
                {
                    Transform newDoor = Instantiate(doorTile);
                    Vector3 position = new Vector3(z, 0, x);
                    newDoor.localPosition = position;
                    newDoor.SetParent(transform);
                }
                else if (pix == lockedDoor)
                {
                    Transform newLockedDoor = Instantiate(lockedDoorTile);
                    Vector3 position = new Vector3(z, 0, x);
                    newLockedDoor.localPosition = position;
                    newLockedDoor.SetParent(transform);
                }
                else if(pix == boulder)
                {
                    Transform newBoulder = Instantiate(boulderTile);
                    Vector3 position = new Vector3(z, 0, x);
                    newBoulder.localPosition = position;
                    newBoulder.SetParent(transform);
                }
                else if (pix == heavyBoulder)
                {
                    Transform newBoulder = Instantiate(heavyBoulderTile);
                    Vector3 position = new Vector3(z, 0, x);
                    newBoulder.localPosition = position;
                    newBoulder.SetParent(transform);
                }
                else if (pix == blueSpawnN)
                {
                    bluePlayer.localPosition = new Vector3(z, 0, x);
                    bluePlayerScript.setHealth(10);
                    bluePlayerScript.setDirection(Character.Direction.North);
                }
                else if (pix == redSpawnN)
                {
                    redPlayer.localPosition = new Vector3(z, 0, x);
                    redPlayerScript.setHealth(10);
                    redPlayerScript.setDirection(Character.Direction.North);
                }
                else if (pix == blueSpawnE)
                {
                    bluePlayer.localPosition = new Vector3(z, 0, x);
                    bluePlayerScript.setHealth(10);
                    bluePlayerScript.setDirection(Character.Direction.East);
                }
                else if (pix == redSpawnE)
                {
                    redPlayer.localPosition = new Vector3(z, 0, x);
                    redPlayerScript.setHealth(10);
                    redPlayerScript.setDirection(Character.Direction.East);
                }
                else if (pix == blueSpawnS)
                {
                    bluePlayer.localPosition = new Vector3(z, 0, x);
                    bluePlayerScript.setHealth(10);
                    bluePlayerScript.setDirection(Character.Direction.South);
                }
                else if (pix == redSpawnS)
                {
                    redPlayer.localPosition = new Vector3(z, 0, x);
                    redPlayerScript.setHealth(10);
                    redPlayerScript.setDirection(Character.Direction.South);
                }
                else if (pix == blueSpawnW)
                {
                    bluePlayer.localPosition = new Vector3(z, 0, x);
                    bluePlayerScript.setHealth(10);
                    bluePlayerScript.setDirection(Character.Direction.West);
                }
                else if (pix == redSpawnW)
                {
                    redPlayer.localPosition = new Vector3(z, 0, x);
                    redPlayerScript.setHealth(10);
                    redPlayerScript.setDirection(Character.Direction.West);
                }
                else if (pix == basicEnemyN)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.basic, new Vector3(z, 0, x), Character.Direction.North);
                }
                else if (pix == basicEnemyE)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.basic, new Vector3(z, 0, x), Character.Direction.East);
                }
                else if (pix == basicEnemyS)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.basic, new Vector3(z, 0, x), Character.Direction.South);
                }
                else if (pix == basicEnemyW)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.basic, new Vector3(z, 0, x), Character.Direction.West);
                }
                else if (pix == flyingEnemyN)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.flying, new Vector3(z, 0, x), Character.Direction.North);
                }
                else if (pix == flyingEnemyE)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.flying, new Vector3(z, 0, x), Character.Direction.East);
                }
                else if (pix == shieldEnemyN)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.shield, new Vector3(z, 0, x), Character.Direction.North);
                }
                else if (pix == shieldEnemyE)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.shield, new Vector3(z, 0, x), Character.Direction.East);
                }
                else if (pix == shieldEnemyS)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.shield, new Vector3(z, 0, x), Character.Direction.South);
                }
                else if (pix == shieldEnemyW)
                {
                    eController.spawnEnemy(EnemyController.EnemyType.shield, new Vector3(z, 0, x), Character.Direction.West);
                }
                else if (pix == potion)
                {
                    Transform newPotion = Instantiate(potionTile);
                    Vector3 position = new Vector3(z, 0, x);
                    newPotion.localPosition = position;
                    newPotion.SetParent(transform);
                }
                else if (pix == key)
                {
                    Transform newKey = Instantiate(keyTile);
                    Vector3 position = new Vector3(z, 0, x);
                    newKey.localPosition = position;
                    newKey.SetParent(transform);
                }
                else if (pix == mirror)
                {
                    Transform newMirror = Instantiate(mirrorTile);
                    Vector3 position = new Vector3(z, 0, x);
                    newMirror.localPosition = position;
                    newMirror.SetParent(transform);
                }

            }
        }
        
    }

    public void clearLevel()
    {

            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        walls.Clear();
        pits.Clear();
        bluePlayerScript.reload();
        bluePlayerScript.useKey();
        redPlayerScript.useKey();
    }

    public List<Vector3> getWalls()
    {
        List<Vector3> fakeWalls = walls;
        return fakeWalls;
    }

    public List<Vector3> getPits()
    {
        return pits;
    }
}
