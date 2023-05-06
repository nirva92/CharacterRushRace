using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController instance = null; // Экземпляр объекта
    public static GameController Instance { get => instance; }

    public List<GameObject> Charasters = new List<GameObject>();
    public List<GameObject> Doors = new List<GameObject>();
   

    public bool checkallLine = false;

    private void Awake()
    {
        OpenGameController();
    }

    void Update()
    {
        CheckAllLine();
        CalculateSpeedCharaster();
        
    }
    public void OpenGameController()
    {
        if (instance == null)  // Теперь, проверяем существование экземпляра
        { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта
        }
        else if (instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }

     //   DontDestroyOnLoad(gameObject); // Теперь нам нужно указать, чтобы объект не уничтожался  // при переходе на другую сцену игры
    }
 

    public void CheckAllLine()
    {
        bool allCharactersReachedPoint = true;
        for (int i = 0; i < Charasters.Count; i++)
        {
            GameObject character = Charasters[i];
            if (character.GetComponent<Charaster>().charasterState != CharasterState.PointReached)
            {
                allCharactersReachedPoint = false;
                break;
            }
        }

        if (allCharactersReachedPoint)
        {
            checkallLine = true;
        }

    }

    public void CalculateSpeedCharaster()
    {
        float travelTime=2;
        for (int i = 0; i < Charasters.Count; i++)
        {
            Charasters[i].GetComponent<Charaster>().speed = Charasters[i].GetComponent<Charaster>().distanceToExit / travelTime;
        }      
    }

}
