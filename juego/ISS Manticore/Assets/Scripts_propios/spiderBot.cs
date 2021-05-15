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
    public bool backing = false;

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
        float dis = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log("Distance is: " + dis);
        timeToAttack -= Time.deltaTime;
        if (dis < 1.5)
        {
            if (timeToAttack <= 0)
            {
                animt.SetBool("Attack", false);
                animt.SetBool("Chasing",false);
                animt.SetBool("Back", true);
                timeToAttack = attackTimer;
                backing = true;
            }
            else
            {
                if (!backing)
                {
                    chaseEnemy();
                    animt.SetBool("Chasing",false);
                    animt.SetBool("Back", false);
                }
                else
                {
                    transform.Translate(Vector3.forward * -Time.deltaTime);
                }
            }
        }
        else
        {
            if (dis > 4.0f)
            {
                animt.SetBool("Back", false);
                backing = false;
            }
            else
            {
                if (backing)
                {
                    transform.Translate(Vector3.forward * -Time.deltaTime);
                }
                else
                {
                    chaseEnemy();
                }
            }
        }
        /*chaseEnemy();
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
        }*/
        //animt.SetBool("Died", true);
    }
}
