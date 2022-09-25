using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().name == "WinScreen")

        {
            Destroy(GameObject.Find("GameManager"));
        }
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
