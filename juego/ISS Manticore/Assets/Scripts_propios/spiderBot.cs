using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderBot : MonoBehaviour
{
    // Start is called before the first frame update
    //public Animation anim;
    public Animator animt;
    public float attackTimer = 5.0f;
    private float timeToAttack;
    public UnityEngine.AI.NavMeshAgent navigator;
    public GameObject player;

    void Start()
    {
        //anim = GetComponent<Animation>();
        animt = GetComponent<Animator>();
        timeToAttack = attackTimer;
        navigator = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void chaseEnemy()
    {
        navigator.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //anim.Play("PA_Warrior Idle_Clip");
        /*anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
        {
            state.speed = 0.5F;
        }*/
        chaseEnemy();
        animt.SetBool("Chasing", true);
        //animt.SetBool("Attack", false);
        if (timeToAttack <= 0)
        {
            animt.SetBool("Attack", true);
            animt.SetBool("Back", true);
            //animt.SetBool("Attack", false);
            timeToAttack = attackTimer;
        }
        else
        {
            timeToAttack -= Time.deltaTime;
        }
        //animt.SetBool("Died", true);
    }
}
