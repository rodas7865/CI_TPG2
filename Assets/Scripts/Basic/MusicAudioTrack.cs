using UnityEngine;
using UnityEngine.UI;

public class MusicAudioTrack : MonoBehaviour
{
    [SerializeField] 
    private AudioSource audioSource;

    [SerializeField] 
    private AudioClip[] playlist;

    [SerializeField] 
    private float maxVolume = 1f;

    [SerializeField] 
    private float fadeTime = 2f;

    private int currentTrack = 0;

    void Start()
    {
        PlayTrack(0);
    }

    void Update()
    {

        
            if (audioSource.clip == null)
                return;

            float timeLeft = audioSource.clip.length - audioSource.time;

            if (timeLeft <= fadeTime)
            {
                audioSource.volume = Mathf.Lerp(0f, maxVolume, timeLeft / fadeTime);
            }
            else if (audioSource.time <= fadeTime)
            {
                audioSource.volume = Mathf.Lerp(0f, maxVolume, audioSource.time / fadeTime);
            }
            else
            {
                audioSource.volume = maxVolume;
            }

            if (!audioSource.isPlaying)
            {
                PlayNextTrack();
           }
        
    }

    private void PlayTrack(int index)
    {
        if (playlist.Length == 0)
            return;

        currentTrack = index;

        audioSource.clip = playlist[currentTrack];
        audioSource.volume = 0f;
        audioSource.Play();
    }

    private void PlayNextTrack()
    {
        currentTrack++;

        if (currentTrack >= playlist.Length)
        {
            currentTrack = 0;
        }

        PlayTrack(currentTrack);
    }


}