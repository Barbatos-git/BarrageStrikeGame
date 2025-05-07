using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> bgmClips;

    private List<AudioClip> remainingClips;
    private float fadeDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        remainingClips = new List<AudioClip>(bgmClips);
        PlayRandomBGM();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayRandomBGM()
    {
        if (remainingClips.Count == 0)
        {
            remainingClips = new List<AudioClip>(bgmClips);
        }

        int randomIndex = UnityEngine.Random.Range(0, remainingClips.Count);
        AudioClip nextClip = remainingClips[randomIndex];
        remainingClips.RemoveAt(randomIndex);

        StartCoroutine(FadeOutAndPlayNext(nextClip));
    }

    private IEnumerator FadeOutAndPlayNext(AudioClip newClip)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.clip = newClip;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        Invoke(nameof(PlayRandomBGM), newClip.length - fadeDuration);
    }
}
