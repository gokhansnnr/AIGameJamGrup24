using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishGame : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            UIManager.Instance._EndPanel.SetActive(true);
            UIManager.Instance._LoseObjects.SetActive(false);
            UIManager.Instance._WinObjects.SetActive(true);
            Time.timeScale = 0f;
        }

    }
}
