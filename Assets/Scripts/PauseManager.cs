using UnityEngine;
using System.Collections;

/// <summary>
/// Pause manager.
/// 
/// Allows lots of custom behaviors on pause by centralising
/// pausing and providing pause events to subscribe to.
/// </summary>
public class PauseManager : MonoBehaviour
{
	public delegate void PauseAction();
	public static event PauseAction OnPaused;
	public static event PauseAction OnResumed;
	public static bool Paused { get; private set; }


	public static void Pause()
	{
		Debug.Log ("Pause");


		Time.timeScale = 0.0000f;

		Paused = true;

		if (OnPaused != null)
		{
			OnPaused();
		}
		Cheats.instance.cheatMenuOpen = true;
	}

	public static void Resume()
	{
		Debug.Log ("Resume");


		Time.timeScale = 1.0f;

		Paused = false;

		if (OnResumed != null)
		{
			OnResumed();
		}
		Cheats.instance.cheatMenuOpen = false;
	}


}
