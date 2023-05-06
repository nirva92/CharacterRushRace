using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip sound;
    private Button button { get { return GetComponent<Button>(); } }

    
    private void OnEnable()
    {
        button.onClick.AddListener(() => PlaySound());
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    void PlaySound()
    {
        LevelController.Instance.PlayAudio(sound);
    }
}