using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameControllerBehaviour : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [Space]
    [SerializeField] private float timer;

	private TMP_Text timerTextChild;
	private TMP_Text scoreTextChild;

	private bool isGame = true;
    private int currentLevel = 1;
    private int maxAvailableLevel = 1;
    private int maxLevel = 1;
    private int requiredScore = 10;

	private string _levelNum = "p_ChoosenLevel";
	private string _levelMax = "p_MaxLevel";
	private string _levelMaxAvailable = "p_MaxAvailableLevel";
	private string _playerScore = "p_ScoreCount";
	private string _playerTimer = "p_Timer";

	public static UnityEvent onScoreUpEvent = new UnityEvent();
    public static UnityEvent onGameOverEvent = new UnityEvent();
    public static UnityEvent onTextUpdateEvent = new UnityEvent();

    public static UnityEvent<int> onExactScoreUpEvent = new UnityEvent<int>();
    public static UnityEvent<int> onLevelLoadEvent = new UnityEvent<int>();

    // Start is called before the first frame update
    void Start()
    {
        onGameOverEvent.AddListener(GameOver);
        onScoreUpEvent.AddListener(ScoreUp);
		onExactScoreUpEvent.AddListener(ExactScoreUpEvent);

		currentLevel = PlayerPrefs.GetInt(_levelNum, 1);
		maxLevel = PlayerPrefs.GetInt(_levelMax, 1);
		maxAvailableLevel = PlayerPrefs.GetInt(_levelMaxAvailable, 1);

        timerTextChild = timerText.transform.GetChild(0).GetComponent<TMP_Text>();
		scoreTextChild = scoreText.transform.GetChild(0).GetComponent<TMP_Text>();

		onLevelLoadEvent.Invoke(currentLevel);
        requiredScore = 50 * currentLevel;
		timer = 10 + requiredScore / 5 * (1f + 0.5f * (maxLevel - currentLevel) / maxLevel);

        UpdateScoreText();
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        TimerUpdate();
	}

    void TimerUpdate()
    {
        if (!isGame)
            return;

        if(timer > 0)
            timer -= Time.deltaTime;

        if(timer <= 0)
			onGameOverEvent.Invoke();

        TimerTextUpdate();
	}

    void TimerTextUpdate()
    {
        timerText.text = ((int)timer).ToString() + "s";
		timerTextChild.text = ((int)timer).ToString() + "s";
		onTextUpdateEvent.Invoke();
	}

    void ScoreUp()
    {
        score += 5;

        UpdateScoreText();

        GameWinCheck();
	}

    void ExactScoreUpEvent(int scoreCount)
    {
        score += scoreCount * 10 * (scoreCount - 1);

		UpdateScoreText();
	}

    void UpdateScoreText()
    {
        scoreText.text = "Points:" + score.ToString() + "/" + requiredScore.ToString();
        scoreTextChild.text = "Points:" + score.ToString() + "/" + requiredScore.ToString();
		onTextUpdateEvent.Invoke();
	}

    void GameWinCheck()
    {
        if (score >= requiredScore)
        {
            onGameOverEvent.Invoke();
		}
    }


	void GameWin()
    {
        if(maxAvailableLevel == currentLevel)
        {
			PlayerPrefs.SetInt(_levelMaxAvailable, maxAvailableLevel + 1);
		}

		SceneManager.LoadScene("WinScene");
	}

	void GameLose()
    {
        SceneManager.LoadScene("LoseScene");
	}

	void GameOver()
    {
		PlayerPrefs.SetInt(_playerScore, score);
		PlayerPrefs.SetFloat(_playerTimer, timer);

		Debug.Log("GameOver");
		isGame = false;

		if (score >= requiredScore)
        {
            GameWin();
		}
        else
        {
            GameLose();
        }
	}
}
