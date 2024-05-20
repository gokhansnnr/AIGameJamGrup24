using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    [SerializeField] public int damages;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            getDamage(damages);
        }
    }

    public void getDamage(int damage)
    {
        if (GameManager.Instance.health > 0)
        {
            GameManager.Instance.health -= damage;
            GameManager.Instance.health = Mathf.Clamp(GameManager.Instance.health, 0, 100);
        }
        else
        {
            UIManager.Instance._EndPanel.SetActive(true);
            UIManager.Instance._LoseObjects.SetActive(true);
            UIManager.Instance._WinObjects.SetActive(false);
            Time.timeScale = 0f;
        }


    }
}
