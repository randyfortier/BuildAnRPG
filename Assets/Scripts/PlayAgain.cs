using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayAgain : MonoBehaviour {
    public void RestartGame() {
        SceneManager.LoadScene("PlayGame", LoadSceneMode.Single);
    }
}
