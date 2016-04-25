using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityStandardAssets.ImageEffects;
using ADBannerView = UnityEngine.iOS.ADBannerView;

public class UglyDucklingAI : MonoBehaviour
{


    public GameObject dragwan;

    public GameObject transformEffect;

    public Shake spriteShaker;

    public GameObject player;

    /// <summary>
    /// How close the player needs to be for the Ugly Duckling to
    /// check for a random transform, so that it is always visible
    /// on screen if it transforms.
    /// </summary>
    public float range = 150.0f;

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

        player = GameObject.Find("Player");

        //Check once a second if we will go demented.
        _turningTimer = PersistentTimer.New(gameObject, 1.0f, () =>
        {
            if (Random.value < chanceOfTurning && canTurn && PlayerIsCloseEnough() )
            {
                GoDemented();
            }
        });


    }

    private bool PlayerIsCloseEnough()
    {
        Vector3 joiningLine = player.transform.position - transform.position;
        return joiningLine.magnitude <= range;
    }

    private  void GoDemented()
    {
        spriteShaker.shaking = true;
        
        Timer.New(gameObject, 1.0f, () =>
        {
            Instantiate(transformEffect, transform.position, transform.rotation);
            Instantiate(dragwan, transform.position, transform.rotation);
            Destroy(gameObject);
        });
    }


    //Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue.MultiplyAlpha(0.2f);
        Gizmos.DrawWireSphere(transform.position, range);
    }


}