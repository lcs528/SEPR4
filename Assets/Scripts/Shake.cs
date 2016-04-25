using UnityEngine;
using System.Collections;

/// <summary>
/// Assesment 4
/// 
/// Makes a game object shake in local space.
/// 
/// The game object with this script should be the child of the game object
/// that handles world space movement.
/// </summary>
public class Shake : MonoBehaviour
{

    /// <summary>
    /// If the game object should be shaking.
    /// </summary>
    public bool shaking = true;

    /// <summary>
    /// The amount the game object should be shaking.
    /// </summary>
    public float shakeAmount = 10.0f;
	
	// Update is called once per frame
	void Update () {

	    if (shaking)
	    {
	        Vector2 random = Random.insideUnitCircle*shakeAmount;
            transform.localPosition = new Vector3(random.x, random.y, transform.localPosition.z);
	    }
	    else
	    {
	        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
	    }

	}
}
