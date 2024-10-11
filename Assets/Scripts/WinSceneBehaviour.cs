using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text TimerText;

    private TMP_Text ScoreTextChild;
    private TMP_Text TimerTextChild;

	private string _levelNum = "p_ChoosenLevel";
	private string _playerScore = "p_ScoreCount";
	private string _playerTimer = "p_Timer";
	private string _levelMax = "p_MaxLevel";
	private string _levelMaxAvailable = "p_MaxAvailableLevel";

	private int currentLevel;
    private int score;
    private float timer;
    private int maxLevel;
    private int maxAvailableLevel;

	// Start is called before the first frame update
	void Start()
    {
        currentLevel = PlayerPrefs.GetInt(_levelNum, 1);
		score = PlayerPrefs.GetInt(_playerScore, 0);
		timer = PlayerPrefs.GetFloat(_playerTimer, 0);
		maxLevel = PlayerPrefs.GetInt(_levelMax, 1);
		maxAvailableLevel = PlayerPrefs.GetInt(_levelMaxAvailable, 1);

		ScoreTextChild = ScoreText.transform.GetChild(0).GetComponent<TMP_Text>();
		TimerTextChild = TimerText.transform.GetChild(0).GetComponent<TMP_Text>();

		ScoreText.text = "Points:" + score.ToString();
		ScoreTextChild.text = "Points:" + score.ToString();
		TimerText.text = ((int)timer).ToString() + "s";
		TimerTextChild.text = ((int)timer).ToString() + "s";

		GameControllerBehaviour.onTextUpdateEvent.Invoke();

		if ((currentLevel < maxLevel) && ((currentLevel + 1) >= maxAvailableLevel))
			PlayerPrefs.SetInt(_levelMaxAvailable, currentLevel + 1);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        if (currentLevel < maxLevel)
            PlayerPrefs.SetInt(_levelNum, currentLevel + 1);
		SceneManager.LoadScene("GameScene");
	}
}
