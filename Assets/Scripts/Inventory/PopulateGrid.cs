using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGrid : MonoBehaviour
{
    public GameObject item1; //this si the object to add into the scroll view
    public int numberToCreate; //how many of the things you want to add

    void Start()
    {
        Populate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Populate()
    {


        GameObject newObj; //create item

        for (int i = 0; i < numberToCreate; i++)
        {
            newObj = (GameObject)Instantiate(item1, transform); //create new instances of prefab until number to create is done
        }
    }
}
