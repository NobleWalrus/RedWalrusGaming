using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDKeys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // below is commented out because I beleive that I do not need it, as I am not showing anything from it in the UI
        //Key = new int[3];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Key;
    public void MoveThePlayer() //in example, RollTheDice() is where MoveThePlayer() is
    {        //the intent of this function is to create a value that will move the player avatar down the game track

        //DiceTotal = 0;
        //for (int i = 0; DiceValues.Length; i++)
        //{
        //  DiceValues[i] = Random.Range( 0, 2 );
        //  DiceTotal += DiceValues[i];
        //}
        // this is the formula used to create random numbers. I have commented it out because we are not creating random numbers
        
        //if (Input.GetKey(KeyCode.W))
           //int Key = 1;
    }



}
