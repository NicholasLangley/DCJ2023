using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pushable : MonoBehaviour
{
    public Vector3 getPos() { return gameObject.transform.localPosition; }
    public void setPos(Vector3 newPos) { gameObject.transform.localPosition = newPos; }

    public abstract bool push(Character.Direction dir, List<Vector3> colliders, List<Vector3> pits, Character.PlayerColor cColor, Character c);
}
