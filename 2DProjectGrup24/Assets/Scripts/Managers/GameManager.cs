using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public int health = 100;
    [SerializeField] private int startTime= 100;
    [SerializeField] public bool isTimeStart=false;
    [SerializeField] public int diamondCount=0;
    [SerializeField] public float currentTime;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(Countdown());
        UIManager.Instance.UpdateHealthBar(health);
        UIManager.Instance.UpdateDiamondCount(diamondCount);
        UIManager.Instance.UpdateTimeBar(currentTime);
    }

    /*
    public void stopGame()
    {
        GameState.changeState(state.pause);
        SceneLoader.Instance.LoadCurrentScene();
        Time.timeScale = 0f;
    }*/

    public void UpdateDiamondCount(int count)
    {
        diamondCount += count;
    }

    IEnumerator Countdown()
    {
        currentTime = startTime;
            while (currentTime > 0f)
            {
                yield return new WaitForSeconds(1f);
                currentTime -= 1;

                if (currentTime <= 0f)
                {
                    UIManager.Instance._EndPanel.SetActive(true);
                    UIManager.Instance._LoseObjects.SetActive(true);
                    UIManager.Instance._WinObjects.SetActive(false);
                    Time.timeScale = 0f;
                }
            }
    }


}
