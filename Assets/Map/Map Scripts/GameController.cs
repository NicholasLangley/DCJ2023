using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public bool pause = false;

    public MapBuilder mapper;
    public Texture2D levelOne, levelTwo, levelThree;
    private Texture2D currentLevel;
    public GameObject gameOverUI;
    public EnemyController eController;

    // Start is called before the first frame update
    void Start()
    {
        //load main menu
        currentLevel = levelOne;
        loadLevel(currentLevel);
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
        pauseGame();
    }

    public void restart()
    {
        eController.clearEnemies();
        loadLevel(currentLevel);
        gameOverUI.SetActive(false);
        unpause();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void advance()
    {
        eController.advance();
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
