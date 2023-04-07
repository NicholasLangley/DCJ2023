using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{
    public bool pause = false;
    public bool gameOverState = false;

    public MapBuilder mapper;
    public Texture2D levelOne, levelTwo, levelThree;
    private Texture2D currentLevel;
    public GameObject gameOverUI;
    public EnemyController eController;
    public Cutscene cutscenePlayer;
    public TextMeshProUGUI gameOverText;

    int advanceCount;

    // Start is called before the first frame update
    void Start()
    {
        //load main menu
        //cutscenePlayer.playOpening();
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
        else
        {
            currentLevel = levelOne;
            gameOver(Character.PlayerColor.Red);
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
        else
        {
            gameOverText.text = "Game Over: Red Died";
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
}
