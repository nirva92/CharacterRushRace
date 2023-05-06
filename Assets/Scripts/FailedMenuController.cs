using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedMenuController : MonoBehaviour
{
    
    public void FailedLevel()
    {
        switch (LevelController.Instance.currentLevel)
        {
            case 1:
                SceneManager.LoadScene("Level_1");
                break;
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
        }
    }
}
