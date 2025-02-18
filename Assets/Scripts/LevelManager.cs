using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    public AdsManager ads;
    private int maxTaskCount;
    private int minTaskCount;
    private int maxTaskCountForLevel;
    private int minTaskCountForLevel;
    private int maxObjectsCount;
    private int taskCount;
    public ParticleSystem fireworkParticle;
    public Button pauseButton;
    public Button resumeButton;

    public GameObject diskIcon;
    public GameObject cookieIcon;
    public GameObject squareIcon;
    public GameObject starIcon;
    public GameObject triangleIcon;

    public GameObject diskTick;
    public GameObject cookieTick;
    public GameObject squareTick;
    public GameObject starTick;
    public GameObject triangleTick;

    [HideInInspector] public int diskTaskCount;
    [HideInInspector] public int squareTaskCount;
    [HideInInspector] public int starTaskCount;
    [HideInInspector] public int cookieTaskCount;
    [HideInInspector] public int triangleTaskCount;

    [HideInInspector] public int maxDiskCount;
    [HideInInspector] public int maxSquareCount;
    [HideInInspector] public int maxStarCount;
    [HideInInspector] public int maxCookieCount;
    [HideInInspector] public int maxTriangleCount;


    public Text coinText;
    public Text diskText;
    public Text cookieText;
    public Text squareText;
    public Text triangleText;
    public Text starText;

    public TextMeshProUGUI levelText;
    public Text levelTextUI;

    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;
    [HideInInspector] public int levelCompleteNum = 0;

    private bool isCubeOver;
    private bool isDiskOver;
    private bool isStarOver;
    private bool isCookieOver;
    private bool isTriangleOver;

    [HideInInspector] public bool isGameOver;
    // Start is called before the first frame update
    void Awake()
    {
        if (levelManager == null)
        {
            levelManager = this;
        }
        else
            Destroy(gameObject);

        TasksForLevels();
        DataManager.instance.LoadGameData();
        CoinCounter();
        isGameOver = false;
        fireworkParticle.Stop();
    }
    private void Start()
    {
        DiskTask();
        SquareTask();
        CookieTask();
        StarTask();
        TriangleTask();

        levelText.text = "Level " + DataManager.instance.levelCount;
        levelTextUI.text = "Level  " + DataManager.instance.levelCount;
    }
    private void Update()
    {
        if (levelCompleteNum >= 5)
        {
            SucceddGame();
            DataManager.instance.levelCount++;
            DataManager.instance.SaveGameData();
        }
    }

    public void CoinCounter()
    {
        coinText.text = "x " + DataManager.instance.gameCoin;
    }

    public void TaskGeneretor()
    {
        maxObjectsCount = Random.Range(minTaskCountForLevel, maxTaskCountForLevel);
        taskCount = Random.Range(minTaskCount, maxTaskCount);
    }
    public void DiskTask()
    {
        TaskGeneretor();
        diskTaskCount = taskCount;
        maxDiskCount = maxObjectsCount;
        WriteDisks();
    }
    public void WriteDisks()
    {
        if (diskTaskCount == 0)
        {
            levelCompleteNum++;
            diskTaskCount = -1;
            GameManager.gameManager.GetComponent<ObjectPooling>().isDiskDone = true;
            isDiskOver = true;
            diskIcon.SetActive(false);
            diskTick.SetActive(true);
            // disk u� panel change green tick.
        }
        if (maxDiskCount < diskTaskCount && !isDiskOver && !isGameOver)
        {
            maxDiskCount = 0;
            GameOver();
        }
        diskText.text = diskTaskCount + "/" + maxDiskCount;
    }
    public void SquareTask()
    {
        TaskGeneretor();
        squareTaskCount = taskCount;
        maxSquareCount = maxObjectsCount;
        WriteSquares();
    }
    public void WriteSquares()
    {
        if (squareTaskCount == 0)
        {
            levelCompleteNum++;
            squareTaskCount = -1;
            GameManager.gameManager.GetComponent<ObjectPooling>().isCubeDone = true;
            isCubeOver = true;
            squareIcon.SetActive(false);
            squareTick.SetActive(true);
        }
        if (maxSquareCount < squareTaskCount && !isCubeOver && !isGameOver)
        {
            maxSquareCount = 0;
            GameOver();
        }
        squareText.text = squareTaskCount + "/" + maxSquareCount;
    }
    public void StarTask()
    {
        TaskGeneretor();
        starTaskCount = taskCount;
        maxStarCount = maxObjectsCount;
        WriteStars();
    }
    public void WriteStars()
    {
        if (starTaskCount == 0)
        {
            levelCompleteNum++;
            starTaskCount = -1;
            GameManager.gameManager.GetComponent<ObjectPooling>().isStarDone = true;
            isStarOver = true;
            starIcon.SetActive(false);
            starTick.SetActive(true);
        }
        if (maxStarCount < starTaskCount && !isStarOver && !isGameOver)
        {
            maxStarCount = 0;
            GameOver();
        }
        starText.text = starTaskCount + "/" + maxStarCount;
    }
    public void CookieTask()
    {
        TaskGeneretor();
        cookieTaskCount = taskCount;
        maxCookieCount = maxObjectsCount;
        WriteCookies();
    }
    public void WriteCookies()
    {
        if (cookieTaskCount == 0)
        {
            levelCompleteNum++;
            cookieTaskCount = -1;
            GameManager.gameManager.GetComponent<ObjectPooling>().isCookieDone = true;
            isCookieOver = true;
            cookieIcon.SetActive(false);
            cookieTick.SetActive(true);
        }
        if (maxCookieCount < cookieTaskCount && !isCookieOver && !isGameOver)
        {
            maxCookieCount = 0;
            GameOver();
        }
        cookieText.text = cookieTaskCount + "/" + maxCookieCount;
    }
    public void TriangleTask()
    {
        TaskGeneretor();
        triangleTaskCount = taskCount;
        maxTriangleCount = maxObjectsCount;
        WriteTriangles();
    }
    public void WriteTriangles()
    {
        if (triangleTaskCount == 0)
        {
            levelCompleteNum++;
            triangleTaskCount = -1;
            GameManager.gameManager.GetComponent<ObjectPooling>().isTriangelDone = true;
            isTriangleOver = true;
            triangleIcon.SetActive(false);
            triangleTick.SetActive(true);
        }
        if (maxTriangleCount < triangleTaskCount && !isTriangleOver && !isGameOver)
        {
            maxTriangleCount = 0;
            GameOver();
        }
        triangleText.text = triangleTaskCount + "/" + maxTriangleCount;
    }
    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.gameObject.SetActive(true);
        SoundManager.instance.GameOverSound();
        DataManager.instance.gameOverCountsForAds++;
        Debug.Log(DataManager.instance.gameOverCountsForAds);
        DataManager.instance.SaveGameData();
        if(DataManager.instance.gameOverCountsForAds >= 3)
        {
            DataManager.instance.gameOverCountsForAds = 0;
            DataManager.instance.SaveGameData();
            ads.PlayAd();
        }
    }

    void SucceddGame()
    {
        fireworkParticle.Play();
        SoundManager.instance.FireworkSound();
        isGameOver = true;
        DataManager.instance.gameOverCountsForAds++;
        DataManager.instance.SaveGameData();
        Debug.Log(DataManager.instance.gameOverCountsForAds);
        if (DataManager.instance.gameOverCountsForAds >= 3)
        {
            DataManager.instance.gameOverCountsForAds = 0;
            DataManager.instance.SaveGameData();
            ads.PlayAd();
        }
        levelCompletePanel.gameObject.SetActive(true);
        levelCompleteNum = 0;
    }

    public void AddingGolds()
    {
        ads.PlayRewardedVideo(OnRewardedAdSuccess);
    }
    void OnRewardedAdSuccess()
    {
        DataManager.instance.gameCoin += 10;
        DataManager.instance.SaveGameData();
    }
    public void TasksForLevels()
    {
        if(DataManager.instance.levelCount <= 3)
        {
            maxTaskCount = 2;
            minTaskCount = 1;
            maxTaskCountForLevel = 3;
            minTaskCountForLevel = 2;
            DataManager.instance.SaveGameData();
        }else if(DataManager.instance.levelCount > 3 && DataManager.instance.levelCount <= 9)
        {
            maxTaskCount = 5;
            minTaskCount = 1;
            minTaskCountForLevel = 6;
            maxTaskCountForLevel = 7;
            DataManager.instance.SaveGameData();
        }
        else if(DataManager.instance.levelCount > 9)
        {
            maxTaskCount = 8;
            minTaskCount = 3;
            minTaskCountForLevel = 9;
            maxTaskCountForLevel = 10;
            DataManager.instance.SaveGameData();
        }
    }

}
