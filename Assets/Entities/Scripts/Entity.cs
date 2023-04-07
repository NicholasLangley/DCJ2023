using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public enum Direction { North, East, South, West }

    [SerializeField]
    public Direction facing = Direction.North;

    [SerializeField]
    public int health;

    public abstract void damageEntity(int dmg);
    public Vector3 getPos() { return gameObject.transform.localPosition; }
    public void setPos(Vector3 newPos) { gameObject.transform.localPosition = newPos; }

    public void setDirection(Direction d)
    {
        facing = d;
        faceDirection();
    }

    public Direction getDirection()
    {
        return facing;
    }

    public Vector3 getTileInFront()
    {
        Vector3 position = transform.localPosition;
        if (facing == Direction.North)
        {
            position.z = position.z + 1;
        }
        else if (facing == Direction.South)
        {
            position.z = position.z - 1;
        }
        else if (facing == Direction.East)
        {
            position.x = position.x + 1;
        }
        else if (facing == Direction.West)
        {
            position.x = position.x - 1;
        }
        return position;
    }

    public Vector3 getTileBehind()
    {
        Vector3 position = transform.localPosition;
        if (facing == Direction.North)
        {
            position.z = position.z - 1;
        }
        else if (facing == Direction.South)
        {
            position.z = position.z + 1;
        }
        else if (facing == Direction.East)
        {
            position.x = position.x - 1;
        }
        else if (facing == Direction.West)
        {
            position.x = position.x + 1;
        }
        return position;
    }

    public Vector3 getTileToRight()
    {
        Vector3 position = transform.localPosition;
        if (facing == Direction.North)
        {
            position.x = position.x + 1;
        }
        else if (facing == Direction.South)
        {
            position.x = position.x - 1;
        }
        else if (facing == Direction.East)
        {
            position.z = position.z - 1;
        }
        else if (facing == Direction.West)
        {
            position.z = position.z + 1;
        }
        return position;
    }

    public Vector3 getTileToLeft()
    {
        Vector3 position = transform.localPosition;
        if (facing == Direction.North)
        {
            position.x = position.x - 1;
        }
        else if (facing == Direction.South)
        {
            position.x = position.x + 1;
        }
        else if (facing == Direction.East)
        {
            position.z = position.z + 1;
        }
        else if (facing == Direction.West)
        {
            position.z = position.z - 1;
        }
        return position;
    }

    public virtual void faceDirection()
    {
        if (facing == Direction.North)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (facing == Direction.South)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (facing == Direction.East)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }
}
