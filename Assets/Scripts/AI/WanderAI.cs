using UnityEngine;
using System.Collections;
/// <summary>
/// Assessment 4
/// 
/// Used by the golden goose and ugly duckling to make them
/// wander aimlessley around the map.
/// </summary>
public class WanderAI : MonoBehaviour {

    /// <summary>
    /// The sprite animator.
    /// Should have animations for
    /// walkUp
    /// walkDown
    /// walkLeft
    /// walkRight
    /// </summary>
    public Animator animator;


    /// <summary>
    /// Rigidbody for movement and collisions.
    /// </summary>
    public Rigidbody2D rigidBody;

    /// <summary>
    /// Start position of current linear segment in the wander
    /// </summary>
    private Vector3 start;

    /// <summary>
    /// End position of current linear segment in the wander
    /// </summary>
    private Vector3 target;

    /// <summary>
    /// How often we start a new wander segment.
    /// </summary>
    public float changeDirectionTime = 2.5f;

    /// <summary>
    /// Used to keep track of how long we have been walking along
    /// one segment.
    /// </summary>
    private float _changeDirectionTimer;



    // Use this for initialization
    void Start ()
    {
        NewDirection();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (_changeDirectionTimer < changeDirectionTime)
        {
            _changeDirectionTimer += Time.deltaTime;
        }
        else
        {
            _changeDirectionTimer = 0;
            NewDirection();
        }

        rigidBody.velocity = (target - start) * 0.1f;
    }

    /// <summary>
    /// Start a new walk segment, defining new start and end positions.
    /// </summary>
    private void NewDirection()
    {
        start = transform.position;
        target = GenerateTarget();
        animator.SetTrigger("walk" + Utils.MainDirectionString(target - transform.position));
    }


    /// <summary>
    /// Generate a random point nearby to move towoards.
    /// </summary>
    /// <returns>New target position</returns>
    private Vector3 GenerateTarget()
    {
        float d = 200.0f;

        float xoff = RandomDistance(d);
        float yoff = RandomDistance(d);

        Vector3 target = new Vector3(transform.position.x + xoff, transform.position.y + yoff, transform.position.z);

        return target;
    }

    /// <summary>
    /// Generates a negative or positive distance from a positive distance d
    /// Such that it is also not too close to 0 so as to be too small a distance
    /// to reosonalbly walk in the time.
    /// </summary>
    private float RandomDistance(float d)
    {
        float raw = Random.Range(-d, d);

        float minimum = d * 0.4f;

        if (raw < 0)
        {
            raw = Mathf.Clamp(raw, -d, -minimum);
        }
        else
        {
            raw = Mathf.Clamp(raw, 2, minimum);
        }

        return raw;
    }

    /// <summary>
    /// If we bump into something then we need to be going a different direction.
    /// </summary>
    void OnCollisionEnter2D(Collision2D coll)
    {
        NewDirection();
    }

}
