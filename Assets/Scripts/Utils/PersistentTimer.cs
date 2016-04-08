using UnityEngine;
using System.Collections;

/// <summary>
/// Persistent Timer.
/// 
/// Utility class for repeatedtly doing something after a certain period of time.
/// </summary>
public class PersistentTimer : MonoBehaviour
{

    public delegate void TimerCallBack();

    public static PersistentTimer New(GameObject host, float time, TimerCallBack callback)
    {
        var child = new GameObject("Timer");
        child.transform.parent = host.transform;

        PersistentTimer timer = child.AddComponent<PersistentTimer>();

        timer.callback = callback;
        timer.runTime = time;
        

        return timer;
    }

    private float elapsed;
    public float runTime;

    [HideInInspector]
    public TimerCallBack callback;

	
	void Update ()
    {
        if (elapsed < runTime)
        {
            elapsed += Time.deltaTime;
        }
        else
        {
            callback();
            elapsed = 0;
        }

    }
}
