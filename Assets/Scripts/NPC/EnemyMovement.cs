using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : GradientHealth
{
    public enum AIStates
    {
        Patrol,
        Seek,
        Attack,
        Die
    }
    public AIStates state;
    public Transform target;
    public Transform player;
    public Transform WayPointParent;
    protected Transform[] wayPoints;
    public int nextPoint, difficulty;
    public NavMeshAgent agent;
    public float walkSpeed, runSpeed, attackRange, attackSpeed, sightRange, baseDamage;
    public Animator wolfAnim;
    public bool isDead;
    public float distanceToPoint, changePoint;
    public float stopFromPlayer;
    public override void Start()
    {
        //this uses parent's Start. Also can be places anywhere
        base.Start();
        //get waypoints array from waypoint parent
        wayPoints = WayPointParent.GetComponentsInChildren<Transform>();
        //get navMeshAgent from self
        agent = GetComponent<NavMeshAgent>();
        //Set speed of agent
        agent.speed = walkSpeed;
        //Get Animator from self
        wolfAnim = GetComponentInChildren<Animator>();
        //Set target Waypoint
        nextPoint = 1;
        //Set Patrol as Default
        Patrol();
    }
    public override void Update()
    {
        base.Update();
        wolfAnim.SetBool("Walk", false);
        wolfAnim.SetBool("Run", false);
        wolfAnim.SetBool("Attack", false);

        Patrol();
        Seek();
        Attack();
        Die();

    }
    void Patrol()
    {
        //DO NOT CONTINUE IF NO WAYPOINTS, dead, player in range
        if (wayPoints.Length <= 0 || Vector3.Distance(player.position, transform.position) <= sightRange ||isDead)
        {
            return;
        }
        state = AIStates.Patrol;
        wolfAnim.SetBool("Walk", true);
        //Set agent to target
        agent.destination = wayPoints[nextPoint].position;
        agent.speed = walkSpeed;
        distanceToPoint = Vector3.Distance(transform.position, wayPoints[nextPoint].position);
        //are we at the waypoint
        if(distanceToPoint <=changePoint)
        {
            //if so go to next waypoint
            if (nextPoint < wayPoints.Length - 1)
            {
                nextPoint++;
            }
            //if at end of patrol go to start
            else
            {
                nextPoint = 1;
            }
        }
    }
    void Seek()
    {
        //if the player is out of our sight range and inside our attack range
        if (Vector3.Distance(player.position, transform.position) > sightRange || Vector3.Distance(player.position, transform.position) < attackRange || isDead)
        {
            //stop seeking
            return;
        }


        //Set AI state
        state = AIStates.Seek;
        //Set animation
        wolfAnim.SetBool("Run", true);
        //change speed??? up to you
        agent.speed = runSpeed;
        //Target is player
        agent.destination = player.position;
    }
    //This method (function/behaviour) can be overridden by any class that inherits from this class
    public virtual void Attack()
    {
        //if player in attack range attack or we are dead or the player is dead
        if (Vector3.Distance(player.position, transform.position) > attackRange ||isDead || PlayerHandler.isDead)
        {
            //stop attacking
            return;
        }
        //Set AI state
        state = AIStates.Attack;
        //Set animation
        wolfAnim.SetBool("Attack", true);
        agent.stoppingDistance = stopFromPlayer;
    }
    void Die()
    {
        //if we are alive
        if (attributes[0].currentValue > 0 || isDead)
        {
            //don't run this
            return;
        }

        //else we are dead so run this
        //Set AI state
        state = AIStates.Die;
        //Set animation
        wolfAnim.SetTrigger("Die");
        //is dead
        isDead = true;
        //stop moving
        agent.destination = transform.position;
        agent.speed = 0;
        agent.enabled = false;
        //Droploot...not yet

    }
}
