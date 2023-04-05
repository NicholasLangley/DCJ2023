using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : Pushable
{
    public override bool push(Character.Direction dir, List<Vector3> colliders, List<Vector3> pits, Character.PlayerColor cColor, Character c)
    {
        Vector3 nextPos = transform.localPosition;
        if (dir == Character.Direction.North)
        {
            nextPos.z = nextPos.z + 1;
        }
        else if (dir == Character.Direction.South)
        {
            nextPos.z = nextPos.z - 1;
        }
        else if (dir == Character.Direction.East)
        {
            nextPos.x = nextPos.x + 1;
        }
        else if (dir == Character.Direction.West)
        {
            nextPos.x = nextPos.x - 1;
        }
        foreach(Vector3 possibleCollision in colliders)
        {
            if (nextPos == possibleCollision)
            {
                return false;
            }
        }
        transform.localPosition = nextPos;
        foreach(Vector3 p in pits)
        {
            if (transform.localPosition == p)
            {
                Vector3 fallPos = nextPos;
                fallPos.y -= 1;
                foreach(Vector3 possibleCollision in colliders)
                {
                    if (fallPos == possibleCollision)
                    {
                        return true;
                    }
                }
                fall();
            }
        }
        return true;
    }

    public void fall()
    {
        Vector3 nextPos = transform.localPosition;
        nextPos.y -= 1;
        transform.localPosition = nextPos;
    }
    
}
