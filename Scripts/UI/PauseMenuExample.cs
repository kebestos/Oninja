using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuExample : MonoBehaviour {

    GameObject panel;
    GameObject aM;

    // Coin save
    int moneyAmount;

    private void Start()
    {    
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount", moneyAmount);
        Debug.Log("moneyAmount (PauseMenuExample / Start): " + moneyAmount);
        aM = GameObject.FindGameObjectWithTag("MusicInGame");
    }

    void Awake () {
        // Get panel object
        panel = transform.Find("PauseMenuPanel").gameObject;
        if (panel == null) {
            Debug.LogError("PauseMenuPanel object not found.");
            return;
        }

        panel.SetActive(false); // Hide menu on start
	}

    // Call from inspector button
    public void ResumeGame () {
        SumPause.Status = false; // Set pause status to false
    }

    public void GoToMenu()
    {
        //SaveLoad.Save();
        SumPause.Status = false;
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        Debug.Log("moneyAmount (PauseMenuExample): " + moneyAmount);
        SceneManager.LoadScene("Menu");
        Destroy(aM);

    }

    // Add/Remove the event listeners
    void OnEnable() {
        SumPause.pauseEvent += OnPause;
    }

    void OnDisable() {
        SumPause.pauseEvent -= OnPause;
    }

    /// <summary>What to do when the pause button is pressed.</summary>
    /// <param name="paused">New pause state</param>
    void OnPause(bool paused) {
        if (paused) {
            // This is what we want do when the game is paused
            panel.SetActive(true); // Show menu
        }
        else {
            // This is what we want to do when the game is resumed
            panel.SetActive(false); // Hide menu
        }
    }

}
