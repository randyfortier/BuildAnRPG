using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour {
    [SerializeField] Slider experienceSlider = null;

    public virtual void StartAttack() {
        Debug.Log("Attack()");
    }

    public virtual void LevelUp() {
        Debug.Log("LevelUp()");
    }

    public void ResetExperience() {
        if (experienceSlider != null) {
            experienceSlider.value = 0.0f;
        }
    }
}
