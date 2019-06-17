using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{

   
    public Tile StartingTile;
    Tile currentTile;
    Grid grid;
    int positionX;
    int positionY;

    private CapsuleCollider CC;

    private Rigidbody RB;

    public GameObject player;



    void Start()
    {
        player = GameObject.Find("Player");
        positionX = 0;
        positionY = 0;
        grid = new Grid();
        CC = GetComponent<CapsuleCollider>();
        RB = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
            // The following is the first rendition of movement. It is commented out becasue we dont want it for the momement. 
       // if(Input.GetKeyDown(KeyCode.W))
       // {
       //
       //     Debug.Log("W");
       //
       //     positionY++;
       //
       //     Debug.Log("position Y: " + positionY);
       //
       //     if(positionY > grid.getRows())
       //     {
       //         positionY = grid.getRows();
       //     }
       //     Debug.Log(grid.getRows());
       //
       //     Vector3 start = transform.position;
       //     Vector3 end = start + new Vector3(positionY * grid.getSize(), positionX * grid.getSize(), 1f);
       //
       //     transform.position = end;
         
  
        //}
    }
    public void OnMouseUp()
    //is the mouse over UI? then ignore click
    {
        Debug.Log("Attacks with ");


    }

}
