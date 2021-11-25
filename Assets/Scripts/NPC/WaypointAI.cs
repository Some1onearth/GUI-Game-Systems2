using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointAI : MonoBehaviour
{
    [SerializeField] public float speed = 1f; //camelCasing
    [SerializeField] private GameObject[] goal;
    private int goalIndex = 0;

    private GameObject currentGoal;

    public bool isAIMoving = true;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        currentGoal = goal[goalIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (isAIMoving == false)
        {
            return; //exit the method early
        }
        else
        {
            if (target == null)
            {
                Wander(currentGoal, speed);
            }
            else
            {
                Chase(target, speed);
            }
        }
    }
    void Chase(GameObject goal, float currentSpeed) //placing the variables makes it transferable.
    {
        //finds the direction to goal (to the circle)
        Vector3 direction = (goal.transform.position - transform.position).normalized;

        Vector3 position = transform.position;
        //moves ai towards the direction set (which was the goal)
        position += (direction * currentSpeed * Time.deltaTime);
        transform.position = position;
    }
    void Wander(GameObject goal, float currentSpeed)
    {
        //this gets the distance to the goal
        float distance = Vector3.Distance(transform.position, goal.transform.position);

        if (distance > 0.05f)
        {
            Chase(goal, currentSpeed); //passing along the floats
        }
        else
        {
            NextGoal();
        }
    }
    void NextGoal()
    {
        //Increase goalIndex by 1 (all 3 work the same)
        //goalIndex = goalIndex + 1;
        //goalIndex += 1;
        goalIndex++;


        //goal.Length = 3
        //goalIndex >= goal.Length)
        if (goalIndex > goal.Length - 1)
        {
            goalIndex = 0;
        }

        currentGoal = goal[goalIndex];
    }
}
