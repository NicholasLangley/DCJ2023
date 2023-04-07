using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform basicEnemy, flyingEnemy, shieldEnemy;
   public enum EnemyType {basic, shield, flying}
    public SoundEffectController sc;

   public void spawnEnemy(EnemyType type, Vector3 pos, Character.Direction dir)
    {
        Transform newEnemy;
        switch (type)
        {
            case EnemyType.basic:
                newEnemy = Instantiate(basicEnemy);
                pos.y -= 0.30f;
                break;
            case EnemyType.shield:
                pos.y -= 0.51f;
                newEnemy = Instantiate(shieldEnemy);
                break;
            case EnemyType.flying:
                newEnemy = Instantiate(flyingEnemy);
                break;
            default:
                return;
        }
            
        newEnemy.localPosition = pos;
        newEnemy.SetParent(transform);
        newEnemy.GetComponent<Entity>().setDirection(dir);
        newEnemy.GetComponent<Enemy>().setSc(sc); 
    }

    public void advance()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies)
        {
            e.advance();
        }
    }

    public void clearEnemies()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies)
        {
            Destroy(e.gameObject);
        }
    }
}
