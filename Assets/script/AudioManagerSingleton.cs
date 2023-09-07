using UnityEngine;

public class AudioManagerSingleton : MonoBehaviour
{
    public static AudioManagerSingleton Instance { get; private set; }

    private AudioManager audioManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioManager = GetComponent<AudioManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para reproducir un sonido en el AudioManager
    public void PlaySound(int index)
    {
        if (audioManager != null)
        {
            audioManager.PlaySound(index);
        }
    }
}