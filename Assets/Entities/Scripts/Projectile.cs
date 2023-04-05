using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Character.Direction direction;

    public int dmg = 1;

    public void setDir(Character.Direction dir)
    {
        direction = dir;
        switch(dir)
        {
            case Character.Direction.North:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Character.Direction.East:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Character.Direction.South:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Character.Direction.West:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
        }
        
    }
}
