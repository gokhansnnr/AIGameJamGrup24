using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Main Menu")]
    [SerializeField] bool mainMenu;
    [SerializeField] Button _Play;

    [Space]
    [Header("GamePlayUI")]
    [SerializeField] bool InGame;
    [SerializeField] TMP_Text _HealthSlider;
    [SerializeField] TMP_Text _TimeSlider;
    [SerializeField] TMP_Text _DiamondCount;
    [SerializeField] Button _Pause;
    [SerializeField] Button _SoundOnOff;

    [Space]
    [Header("Settings Menu")]
    [SerializeField] Button _Continue;
    [SerializeField] Button _Exit;

    [Space]
    [Header("EndPanel")]
    [SerializeField] public GameObject _EndPanel;
    [SerializeField] public GameObject _WinObjects;
    [SerializeField] public GameObject _LoseObjects;
    [SerializeField] TMP_Text _NickText;
    [SerializeField] TMP_Text _LastDiamondCount;
    [SerializeField] TMP_Text _TimeText;
    [SerializeField] Button _NewGame;
    [SerializeField] Button _ExitGame;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (InGame)
        {
            _Continue.onClick.AddListener(btn_Continue);
            _Pause.onClick.AddListener(btn_Pause);
            _SoundOnOff.onClick.AddListener(SoundOnOff);
        }

        if (mainMenu)
        {
            _Play.onClick.AddListener(btn_Play);
        }
        _Exit.onClick.AddListener(btn_Exit);
    }

    private void Update()
    {
        UpdateDiamondCount(GameManager.Instance.diamondCount);
        UpdateTimeBar(GameManager.Instance.currentTime);
        UpdateHealthBar(GameManager.Instance.health);
    }

    private void btn_Play()
    {
        SceneLoader.Instance.LoadLevel(SceneEnum.GameScene1);
    }

    private void btn_Exit()
    {
        Application.Quit();
    }

    private void btn_Pause()
    {
        GameState.changeState(state.pause);
        Time.timeScale = 0f;
    }

    private void btn_Continue()
    {
        GameState.changeState(state.play);
        Time.timeScale = 1f;
    }

    public void UpdateHealthBar(float health)
    {
        _HealthSlider.text = health.ToString();
    }

    public void UpdateTimeBar(float time)
    {
        //time = Mathf.Clamp(time, 0, 100);
        _TimeSlider.text = time.ToString();
        _TimeText.text = time.ToString();
    }

    public void UpdateDiamondCount(int value)
    {
        _DiamondCount.text = value.ToString();
        _LastDiamondCount.text = value.ToString();
    }

    private void SoundOnOff()
    {
        
    }
}
