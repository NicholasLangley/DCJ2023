using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{
    public bool pause = false;
    public bool gameOverState = false;
    bool creditsVis = false;

    public MapBuilder mapper;
    public Texture2D levelOne, levelTwo, levelThree, levelFour, levelFive;
    private Texture2D currentLevel;
    public GameObject gameOverUI;
    public EnemyController eController;
    public Cutscene cutscenePlayer;
    public TextMeshProUGUI gameOverText;
    public GameObject creditsUI;

    int advanceCount;

    // Start is called before the first frame update
    void Start()
    {
        //load main menu
        cutscenePlayer.playOpening();
        advanceCount = 0;
        currentLevel = levelOne;
        loadLevel(currentLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverState)
        {
            if (pause)
            {
                gameOverUI.SetActive(false);
                unpause();
            }
            else
            {
                gameOverUI.SetActive(true);
                gameOverText.text = "";
                pauseGame();
            }
        }
        if(creditsVis == true)
        {
            if (Input.anyKeyDown)
            {
                unshowCredits();
            }
        }
    }

    public void loadLevel(Texture2D lvl)
    {
        mapper.clearLevel();
        eController.clearEnemies();
        mapper.BuildLevel(lvl);
    }

    public void levelUp()
    {
        if (currentLevel == levelOne)
        {
            currentLevel = levelTwo;
        }
        else if (currentLevel == levelTwo)
        {
            currentLevel = levelThree;
        }
        else if (currentLevel == levelThree)
        {
            currentLevel = levelFour;
        }
        else if (currentLevel == levelFour)
        {
            currentLevel = levelFive;
        }
        else
        {
            currentLevel = levelOne;
            gameOver(Character.PlayerColor.VICTORY);
            return;
        }
        loadLevel(currentLevel);
    }

    public void gameOver(Character.PlayerColor deadPlayer)
    {
        gameOverUI.SetActive(true);
        gameOverState = true;
        if (deadPlayer == Character.PlayerColor.Blue)
        {
            gameOverText.text = "Game Over: Blue Died";
        }
        else if (deadPlayer == Character.PlayerColor.Red)
        {
            gameOverText.text = "Game Over: Red Died";
        }
        else
        {
            gameOverText.text = "VICTORY!";
        }
        pauseGame();
    }

    public void restart()
    {
        eController.clearEnemies();
        loadLevel(currentLevel);
        gameOverUI.SetActive(false);
        gameOverState = false;
        unpause();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void advance()
    {
        advanceCount += 1;
        if (advanceCount >= 2)
        {
            advanceCount = 0;
            eController.advance();
        }
    }

    public void pauseGame()
    {
        pause = true;
    }

    public void unpause()
    {
        pause = false;
    }

    public void showCredits()
    {
        creditsUI.SetActive(true);
        creditsVis = true;
        Debug.Log("here");
    }

    public void unshowCredits()
    {
        creditsUI.SetActive(false);
        creditsVis = false;
    }
}
