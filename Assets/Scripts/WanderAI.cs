using UnityEngine;
using System.Collections;

public class WanderAI : MonoBehaviour {


    public Animator animator;

    public Rigidbody2D rigidBody;

    private Vector3 start;

    private Vector3 target;

    public float changeDirectionTime = 2.5f;

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

    private void NewDirection()
    {
        start = transform.position;
        target = GenerateTarget();
        animator.SetTrigger("walk" + Utils.MainDirectionString(target - transform.position));
    }


    private Vector3 GenerateTarget()
    {
        float d = 200.0f;

        float xoff = RandomDistance(d);
        float yoff = RandomDistance(d);

        Vector3 target = new Vector3(transform.position.x + xoff, transform.position.y + yoff, transform.position.z);

        return target;
    }


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

    void OnCollisionEnter2D(Collision2D coll)
    {
        NewDirection();
    }

}
