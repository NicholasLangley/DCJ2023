using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    float duration;
    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;
        if (duration >= 0.01f)
        {
            duration = 0f;


            Vector3 nextPos = transform.localPosition;
            if (direction == Character.Direction.North)
            {
                nextPos.z = nextPos.z + 0.25f;
            }
            else if (direction == Character.Direction.South)
            {
                nextPos.z = nextPos.z - 0.25f;
            }
            else if (direction == Character.Direction.East)
            {
                nextPos.x = nextPos.x + 0.25f;
            }
            else if (direction == Character.Direction.West)
            {
                nextPos.x = nextPos.x - 0.25f;
            }
            transform.localPosition = nextPos;

            checkForImpact();
        }

        void checkForImpact()
        {
            Vector3 pos = transform.localPosition;
            Entity[] entities = GameObject.FindObjectsOfType<Entity>();
            foreach (Entity e in entities)
            {
                Vector3 ePos = e.transform.localPosition;
                ePos.y = 0;
                if (ePos == pos)
                {
                    e.damageEntity(dmg);
                    Destroy(gameObject);
                }
            }
            Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();
            foreach (Interactable i in interactables)
            {
                Vector3 iPos = i.transform.localPosition;
                if (i.isBlocking() && iPos == pos)
                {
                   Destroy(gameObject);
                }
            }
            Wall[] walls = GameObject.FindObjectsOfType<Wall>();
            foreach (Wall w in walls)
            {
                Vector3 wPos = w.transform.localPosition;
                if (wPos == pos)
                {
                    Destroy(gameObject);
                }
            }
            Pushable[] pushers = GameObject.FindObjectsOfType<Pushable>();
            foreach (Pushable p in pushers)
            {
                Vector3 pPos = p.transform.localPosition;
                if (pPos == pos)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
