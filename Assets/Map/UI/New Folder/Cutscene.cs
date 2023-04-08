using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Image opening1, opening2, opening3, opening4, openingPanel;
    public Image closing1, closing2, closing3, closing4;
    private bool playingOpening, playingClosing;

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

        if (playingClosing)
        {
            timePlayedFor += Time.deltaTime;
            if (timePlayedFor >= 2f)
            {
                if (closing1.enabled)
                {
                    closing1.enabled = false;
                    closing2.enabled = true;
                }
                else if (closing2.enabled)
                {
                    closing2.enabled = false;
                    closing3.enabled = true;
                }
                else if (closing3.enabled)
                {
                    closing3.enabled = false;
                    closing4.enabled = true;
                }
                else if (closing4.enabled)
                {
                    closing4.enabled = false;
                    openingPanel.enabled = false;
                    //gc.unpause();
                    playingClosing = false;
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

    public void playClosing()
    {
        closing1.enabled = true;
        openingPanel.enabled = true;
        playingClosing = true;
        timePlayedFor = 0f;
        gc.pauseGame();
    }
}
