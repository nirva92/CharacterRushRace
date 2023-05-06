using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    static LevelController instance = null; // Экземпляр объекта
    public static LevelController Instance { get => instance; }


    public int currentLevel = 1;
    public AudioSource audioSource;

    private void Awake()
    {
        OpenLevelController();
    }

    public void PlayAudio(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    public void OpenLevelController()
    {
        if (instance == null)  // Теперь, проверяем существование экземпляра
        { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта
        }
        else if (instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }

           DontDestroyOnLoad(gameObject); // Теперь нам нужно указать, чтобы объект не уничтожался  // при переходе на другую сцену игры
    }
}
