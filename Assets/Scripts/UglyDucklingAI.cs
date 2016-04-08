using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityStandardAssets.ImageEffects;
using ADBannerView = UnityEngine.iOS.ADBannerView;

public class UglyDucklingAI : MonoBehaviour
{

    public Animator animator;

    private Vector3 target;

    public float changeDirectionTime = 2.5f;

    private float _changeDirectionTimer;

    private Vector3 start;

    public Rigidbody2D rigidBody;

    public GameObject dragwan;

    public GameObject sprite;

    public Shake spriteShaker;


    /// <summary>
    /// How likley the ugly duckling is to turn into a dragwan.
    /// </summary>
    public float chanceOfTurning = 0.01f;

    /// <summary>
    /// A timer for periodicaly checking if the ugly duckling will
    /// become demented (change into a dragwan).
    /// </summary>
    private PersistentTimer _turningTimer;

    /// <summary>
    /// If it can turn demented. (Into a dragwan)
    /// </summary>
    public bool canTurn;

    // Use this for initialization
    void Start()
    {

        NewDirection();

        //Check once a second if we will go demented.
        _turningTimer = PersistentTimer.New(gameObject, 1.0f, () =>
        {
            if (Random.value < chanceOfTurning && canTurn)
            {
                GoDemented();
            }
        });


    }

    private  void GoDemented()
    {
        spriteShaker.shaking = true;
        
        Timer.New(gameObject, 1.0f, () =>
        {
            Instantiate(dragwan, transform.position, transform.rotation);
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
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

        //transform.position = Utils.QuadEaseInOut(_changeDirectionTimer, start, target, changeDirectionTime);

        rigidBody.velocity = (target - start)*0.1f;
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


    //Debug
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
    }


    private float RandomDistance(float d)
    {
        float raw = Random.Range(-d, d);

        float minimum = d*0.4f;

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