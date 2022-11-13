using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Randomizes groceries in the shop scene and places them on various shelves
/// </summary>
public class RandomiseGroceries : MonoBehaviour
{

    enum Grocery
    {
        Banana,
        Milk,
        Juice,
        Egg,
        TP,
        Apple,
        Cheese,
        Jam,
        PB,
        Meat
    }
    public GameObject GroceryParent; //to find child objects
    /// <summary>
    /// Generates the type of groceries to be displayed on the shelves and places them there at the beginning of the scene
    /// </summary>
    void Start()
    {
        //here, randomly assign yes/no to groceries to be in frame 

        foreach (Grocery item in Grocery.GetValues(typeof(Grocery)))
        {

            int rand = Random.Range(0,2); //int either 0 or 1
            GameObject obj = (GameObject) GroceryParent.transform.Find(item.ToString()).gameObject; // finds child game object
            if (rand >= 0.5) //show
            {
                obj.SetActive(true);
            }
            else //hide
            {
                obj.SetActive(false);
            }
        }

    }
}
