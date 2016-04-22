//Updated During Assessment 4

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour 
{
	public Slider difficultySlider;
	public Slider volumeSlider;

	public void startGame()
	{
		PlayerPrefs.SetInt ("difficulty", (int)difficultySlider.value);
		AudioListener.volume = volumeSlider.value;
		SceneManager.LoadScene ("Cell");
	}
}
