using UnityEngine;


public class Utils
{

	public static string MainDirectionString(Vector3 vec)
	{

		if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
		{
			if (vec.x > 0)
			{
				return "Right";
			} 
			else
			{
				return "Left";
			}
		}
		else
		{
			if (vec.y > 0)
			{
				return "Up";
			} 
			else
			{
				return "Down";
			}
		}
	}


    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <param name="b">Starting value.</param>
    /// <param name="c">Final value.</param>
    /// <param name="d">Duration of animation.</param>
    /// <returns>The correct value.</returns>
    public static float QuadEaseInOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2) < 1)
            return c / 2 * t * t + b;

        return -c / 2 * ((--t) * (t - 2) - 1) + b;
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing in/out: 
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="time">Current time in seconds.</param>
    /// <param name="start">Start Vector3.</param>
    /// <param name="finish">Finish Vector3.</param>
    /// <param name="duration">Duration.</param>
    /// <returns>The tweened Vector3</returns>
    public static Vector3 QuadEaseInOut(float time, Vector3 start, Vector3 finish, float duration)
    {
        Vector3 result = new Vector3
        {
            x = QuadEaseInOut(time, start.x, finish.x, duration),
            y = QuadEaseInOut(time, start.y, finish.y, duration),
            z = QuadEaseInOut(time, start.z, finish.z, duration)
        };

        return result;
    }
}
