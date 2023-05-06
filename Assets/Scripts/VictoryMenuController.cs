using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CompleteLevel()
    {
       LevelController.Instance.currentLevel++;

        // Перейти на следующий уровень или выполнить другие действия, связанные с завершением уровня
        switch (LevelController.Instance.currentLevel)
        {
            case 2:
                SceneManager.LoadScene("Level_2");
                break;
            case 3:
                SceneManager.LoadScene("Level_3");
                break;
            case 4:
                SceneManager.LoadScene("Level_4");
                break;
            case 5:
                SceneManager.LoadScene("Level_5");
                break;
            // Добавьте другие case-выражения для перехода на следующие уровни
            default:
                SceneManager.LoadScene("GameOverMenu");
                break;
        }
    }
}
