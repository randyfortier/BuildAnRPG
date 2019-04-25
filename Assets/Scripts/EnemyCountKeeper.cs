using UnityEngine;
using UnityEngine.UI;

public class EnemyCountKeeper : MonoBehaviour {
    [SerializeField] Text numEnemiesText = null;
    void Update() {
        if (numEnemiesText != null) {
            int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            numEnemiesText.text = "" + enemies;
        }
    }
}
