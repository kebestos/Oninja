using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour
{
    GameObject panel;

    void Awake()
    {
        // Get panel object
        panel = transform.Find("DefeatMenuPanel").gameObject;
        if (panel == null)
        {
            Debug.LogError("DefeatMenuPanel object not found.");
            return;
        }

        panel.SetActive(false); // Hide menu on start
    }
}
