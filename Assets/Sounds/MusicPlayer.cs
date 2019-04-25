using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private float interSongDelay = 2.0f;

    private AudioSource audioSource;
    private int clipIndex = 0;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        // for now
        PlayMusic();
    }

    public void PlayMusic() {
        if (songs.Length > clipIndex) {
            audioSource.clip = songs[clipIndex];
            audioSource.Play();
        }
    }

    private bool delaying = false;
    private float songEndTime = 0.0f;

    private void Update() {
        if (delaying && Time.time > (songEndTime + interSongDelay)) {
            // we don't need to wait any more
            delaying = false;
            songEndTime = 0.0f;

            // play the next song
            audioSource.Play();
        }

        if (!audioSource.isPlaying && !delaying) {
            // get prepared to play the next song
            clipIndex++;
            if (clipIndex >= songs.Length) {
                clipIndex = 0;
            }
            audioSource.clip = songs[clipIndex];

            // remember when the song stopped, so we can delay correctly
            songEndTime = Time.time;
            delaying = true;
        }
    }
}
