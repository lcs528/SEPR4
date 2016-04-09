using UnityEngine;
using System.Collections;

public class DragwanAI : MonoBehaviour
{
    private FireProjectile _projectileFirer;

    private Transform _target;

    public GameObject sprite;

    public GameObject curedVersion;

    public GameObject teleportEffect;

    public AudioSource fireSound;

    private PersistentTimer _fireTimer;

    private PersistentTimer _randomTeleportTimer;

    public float chanceOfTeleporting = 0.1f;

    private Shake _spriteShaker;


    /// <summary>
    /// The time between each projectile firing.
    /// </summary>
    public float rechargeTime = 1.5f;

    /// <summary>
    /// The amount of time before firing the Dragwan shakes
    /// so as to warn the player it will soon fire.
    /// </summary>
    public float shakeToWarnTime = 0.4f;

    // Use this for initialization
    void Start ()
	{
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _spriteShaker = sprite.GetComponent<Shake>();

        //Regularly fire projectiles.
        _fireTimer = PersistentTimer.New(gameObject, rechargeTime - shakeToWarnTime, () =>
        {
            //Start shaking to warn the player.
            _spriteShaker.shaking = true;

            //Start a timer for firing.
            Timer.New(gameObject, shakeToWarnTime, () =>
            {
                _spriteShaker.shaking = false;
                FireProjectile();
            });
        });

        _randomTeleportTimer = PersistentTimer.New(gameObject, 1.0f, () =>
        {
            if (Random.value < chanceOfTeleporting)
            {
                Teleport();
                Instantiate(teleportEffect, transform.position, transform.rotation);
            }
        });
	}

    private void Teleport()
    {
        Debug.Log("Teleported");
        Vector2 randomDisplacement = Random.insideUnitCircle*100.0f;
        transform.position = new Vector3(transform.position.x + randomDisplacement.x,
            transform.position.y + randomDisplacement.y,
            transform.position.z);
    }

    private void FireProjectile()
    {
        _projectileFirer = GetComponent<FireProjectile>();

        _projectileFirer.target = _target.position;

        _projectileFirer.Fire();

        fireSound.Play();
    }

    public void Cure()
    {
        Instantiate(curedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
