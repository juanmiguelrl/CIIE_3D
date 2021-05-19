using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderBot : MonoBehaviour
{
    // Start is called before the first frame update
    //public Animation anim;
    public Animator animt;
    public float attackTimer = 10.0f;
    private float timeToAttack;
    public UnityEngine.AI.NavMeshAgent navigator;
    public GameObject player;
    public bool attacking = false;
    private float attackDuration;
    private float attackLength = 2.0f;

    void Start()
    {
        animt = GetComponent<Animator>();
        timeToAttack = attackTimer;
        navigator = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animt.SetBool("Chasing", true);
        player = GameObject.Find("Character");
    }

    void chaseEnemy()
    {
        navigator.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(player.transform.position, transform.position);
        timeToAttack -= Time.deltaTime;
        if (attacking)
        {
            attackDuration -= Time.deltaTime;
        }
        if (attackDuration < 0)
        {
            animt.SetBool("Attack", false);
            attacking = false;
        }
        if (dis < 1.5)
        {
            if (timeToAttack <= 0)
            {
                animt.SetBool("Attack", true);
                animt.SetBool("Chasing", false);
                animt.SetBool("Back", true);
                timeToAttack = attackTimer;
                attacking = true;
                attackDuration = attackLength;
                PlayerStats.Instance.TakeDamage(5.0f);
            } else
            {
                navigator.ResetPath();
                navigator.isStopped = true;
            }
        }
        else
        {
            if (!attacking)
            {
                navigator.isStopped = false;
                chaseEnemy();
            }
            else
            {
                navigator.ResetPath();
                navigator.isStopped = true;
            }
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character" && attacking)
        {
            PlayerStats.Instance.TakeDamage(1.0f);
            Debug.Log("Collided with player");
        }
        Destroy(gameObject);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Character" && attacking)
        {
            PlayerStats.Instance.TakeDamage(0.25f);
            Debug.Log("Collided with player");
        }
        Destroy(gameObject);
    }*/
}
