using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    private Player player;
    private Spawner spawner;
    Animator anim;

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button replay;
    [SerializeField] private Button exit;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;

    private float score;
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
               
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        anim = player.gameObject.GetComponent<Animator>();
        NewGame();
        

        

    }

    public void NewGame()
    {
        score = 0;
        anim.Play("walk");
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameSpeed = initialGameSpeed;
        enabled = true;

        UpdateHiScore();

        gameOverText.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        replay.gameObject.SetActive(false);
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    public void GameOver()
    {
        gameSpeed = 0;
        enabled = false;
        anim.Play("death");
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        replay.gameObject.SetActive(true);
        UpdateHiScore();
    }
    public void UpdateHiScore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore",0);
        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }
        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
