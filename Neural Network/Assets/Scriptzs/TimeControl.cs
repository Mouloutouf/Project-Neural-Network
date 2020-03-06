using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
	public  int normalSpeedIndex = 4;
	private int index;
	private int lastIndex;
	private int lastChangedIndex;

	public float[] timeValue = {0f, 0.25f, 0.5f, 1, 2, 4, 8, 10, 20, 30, 64, 128};

	public Text timeSpeedText;

	void Start()
	{
		timeSpeedText.GetComponent<Text>();

		SetNormalSpeed();

		index            = normalSpeedIndex;
		lastIndex        = normalSpeedIndex;
		lastChangedIndex = normalSpeedIndex;


		if (timeSpeedText)
		{
			timeSpeedText.text = "" + Time.timeScale.ToString();
		}

		SetTime();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P) || (Input.GetKeyDown(KeyCode.Keypad0)))
		{
			PauseInput();
		}

		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			SpeedDown();
		}

		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			SetNormalSpeed();
		}

		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			SpeedUp();
		}

		if (Input.GetKeyDown(KeyCode.Keypad9))
		{
			Reload();
		}
	}

	private void SetNormalSpeed()
	{
		index = normalSpeedIndex;
		SetTime();
	}

	public void SpeedUp()
	{
		index = Mathf.Clamp(index + 1, 0, timeValue.Length - 1);
		SetTime();
	}

	public void SpeedDown()
	{
		index = Mathf.Clamp(index - 1, 0, timeValue.Length - 1);
		SetTime();
	}

	public void PauseInput()
	{
		if (Time.timeScale == 0)
		{
			index = lastIndex;
		}
		else
		{
			lastIndex = index;
			index     = 0;
		}

		SetTime();
	}

	void SetTime()
	{
		if (lastChangedIndex != index)
		{
			lastChangedIndex = index;

			Time.timeScale = timeValue[index];

			if (timeSpeedText)
			{
				timeSpeedText.text = Time.timeScale.ToString();
			}
		}
	}

	public void SetSpeed(int thisIndex)
	{
		index = thisIndex;
		SetTime();
	}

	private void Reload()
	{
		Scene loadedLevel = SceneManager.GetActiveScene();
		SceneManager.LoadScene(loadedLevel.buildIndex);
		SceneManager.LoadScene(0);
	}
}