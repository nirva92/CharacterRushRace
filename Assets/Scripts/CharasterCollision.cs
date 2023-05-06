using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharasterCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            gameObject.GetComponent<Charaster>().charasterState = CharasterState.Pause;
            StartCoroutine(RestartLevel());
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            StartCoroutine(RestartLevelDelayed());
        }

        if (collision.gameObject.CompareTag("Fence"))
        {
            gameObject.GetComponent<Charaster>().charasterState = CharasterState.Pause;
            StartCoroutine(RestartLevel());
        }
    }


    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("FailedMenu");
    }

    private IEnumerator RestartLevelDelayed()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("VictoryMenu");
    }
}
