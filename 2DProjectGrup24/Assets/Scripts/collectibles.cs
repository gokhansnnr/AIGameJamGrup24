using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.UpdateDiamondCount(10);
            this.gameObject.SetActive(false);
        }
    }
}
