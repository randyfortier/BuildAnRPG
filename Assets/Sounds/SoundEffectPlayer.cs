using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour {
    [SerializeField] private AudioClip spellSound;
    [SerializeField] private AudioClip bowSound;
    [SerializeField] private AudioClip daggerSound;
    [SerializeField] private AudioClip spearSound;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip healthPotionSound;

    private AudioSource audioSource = null;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpellSound() {
        audioSource.clip = spellSound;
        audioSource.Play();
    }

    public void PlayBowSound() {
        audioSource.clip = bowSound;
        audioSource.Play();
    }

    public void PlayDaggerSound() {
        audioSource.clip = daggerSound;
        audioSource.Play();
    }

    public void PlaySpearSound() {
        audioSource.clip = spearSound;
        audioSource.Play();
    }

    public void PlayLevelUpSound() {
        audioSource.clip = levelUpSound;
        audioSource.Play();
    }

    public void PlayHealthPotionSound() {
        audioSource.clip = healthPotionSound;
        audioSource.Play();
    }
}
