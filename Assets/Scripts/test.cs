using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    public Button GameStartButton;
    private void Start()
    {
        GameStartButton.onClick.AddListener(GameStart);
    }

    void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
