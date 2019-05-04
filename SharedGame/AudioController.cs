using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float delayBetweenClips;

    bool canPlay;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {

        audioSource = GetComponent<AudioSource>();
        canPlay = true;
	}

    public void Play()
    {
        if(!canPlay)
        {
            return;
        }
        GameManager.Instance.Timer.Add(() =>
        {
            canPlay = true;
        }, delayBetweenClips);

        canPlay = false;

        int clipIndex = Random.Range(0, audioClips.Length);
        AudioClip audioClip = audioClips[clipIndex];
        audioSource.PlayOneShot(audioClip);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

}
