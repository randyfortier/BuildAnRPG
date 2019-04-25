using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private Slider experienceSlider = null;
    [SerializeField] private SoundEffectPlayer soundEffectPlayer = null;

    private Attack lastAttacker = null;

    private void Start() {
        health = maxHealth;
        if (healthSlider != null) {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
    }

    public void TakeDamage(float damageAmount, Attack attacker) {
        // update our health
        health -= damageAmount;
        if (health < 0.0f) {
            health = 0.0f;
        }

        // update our slider (if any)
        if (healthSlider != null) {
            healthSlider.value = health;
        }

        // update the last attacker
        lastAttacker = attacker;
    }

    public void Heal(float healAmount) {
        // update our health
        health += healAmount;
        if (health > maxHealth) {
            health = maxHealth;
        }

        // update our slider (if any)
        if (healthSlider != null) {
            healthSlider.value = health;
        }

        // play a sound effect
        if (soundEffectPlayer != null) {
            soundEffectPlayer.PlayHealthPotionSound();
        }
    }

    public float GetHealth() {
        return health;
    }

    public void Update() {
        if (health <= 0.0f) {
            if (experienceSlider != null) {
                if (experienceSlider.value + maxHealth > experienceSlider.maxValue) {
                    lastAttacker.LevelUp();

                    // play the sound effect
                    soundEffectPlayer.PlayLevelUpSound();
                } else {
                    experienceSlider.value += maxHealth;
                }
            }

            Destroy(transform.gameObject);
        }
    }
}
