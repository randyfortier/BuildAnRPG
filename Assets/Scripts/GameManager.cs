using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    void Update() {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            SceneManager.LoadScene("GameOverWin");
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth.GetHealth() == 0) {
                SceneManager.LoadScene("GameOverLose");
            }
        }
    }
}
