using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Image opening1, opening2, opening3, opening4, openingPanel;
    private bool playingOpening;

    public GameController gc;

    private float timePlayedFor = 0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (playingOpening)
        {
            timePlayedFor += Time.deltaTime;
            if (timePlayedFor >= 2f)
            {
                if (opening1.enabled)
                {
                    opening1.enabled = false;
                    opening2.enabled = true;
                }
                else if (opening2.enabled)
                {
                    opening2.enabled = false;
                    opening3.enabled = true;
                }
                else if (opening3.enabled)
                {
                    opening3.enabled = false;
                    opening4.enabled = true;
                }
                else if (opening4.enabled)
                {
                    opening4.enabled = false;
                    openingPanel.enabled = false;
                    gc.unpause();
                    playingOpening = false;
                }
                timePlayedFor = 0f;
            }
        }
    }

    public void playOpening()
    {
        opening1.enabled = true;
        openingPanel.enabled = true;
        playingOpening = true;
        timePlayedFor = 0f;
        gc.pauseGame();
    }
}
