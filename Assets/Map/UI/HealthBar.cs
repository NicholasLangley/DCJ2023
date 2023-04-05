using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject heart;
    public int playerHealth;
    List<HealthHeart> hearts = new List<HealthHeart>();

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }

    public void CreateFullHeart()
    {
        GameObject newHeart = Instantiate(heart);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.setHeartImage(HealthHeart.HeartStatus.Full);
        hearts.Add(heartComponent);
     }

    public void DrawHearts()
    {
        ClearHearts();
        for(int i = 0; i<5; i++)
        {
            CreateFullHeart();
        }
        int healthIterator = playerHealth;
        for(int i = 0; i<hearts.Count; i++)
        {
            if (healthIterator >= 2)
            {
                hearts[i].setHeartImage(HealthHeart.HeartStatus.Full);
                healthIterator -= 2;
            }
            else if (healthIterator == 1)
            {
                hearts[i].setHeartImage(HealthHeart.HeartStatus.Half);
                healthIterator--;
            }
            else
            {
                hearts[i].setHeartImage(HealthHeart.HeartStatus.Empty);
            }
        }
    }

    public void Awake()
    {
        DrawHearts();
    }

    public void setHealth(int amount)
    {
        playerHealth = amount;
        DrawHearts();
    }
}
