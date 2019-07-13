using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{

    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;
    
    public bool moving = false;
    public int moveSpeed = 2;
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    Vector3 forward = new Vector3();
    Vector3 left = new Vector3();
    Vector3 right = new Vector3();
    Vector3 back = new Vector3();

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        forward = gameObject.transform.forward;
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit Hit;
        Tile tile = null;
        if(Physics.Raycast(target.transform.position, -Vector3.up, out Hit, 1))
        {
            tile = Hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists()
    {
        //if map changes
        //tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach(GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors();
        }
    }

    public void ComputeDirections()
    {
        //forward received on init
        forward = gameObject.transform.forward;
        back = -forward;
        left = Quaternion.AngleAxis(-90, Vector3.up) * forward;
        right = Quaternion.AngleAxis(90, Vector3.up) * forward;
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists();
        GetCurrentTile();
        ComputeDirections();
        
        selectableTiles.Add(currentTile.GetAdjacentTile(forward));
        selectableTiles.Add(currentTile.GetAdjacentTile(left));
        selectableTiles.Add(currentTile.GetAdjacentTile(right));
        selectableTiles.Add(currentTile.GetAdjacentTile(back));
        for (int j=0; j<4; j++)
        {
            if (selectableTiles[j] != null)
            {
                selectableTiles[j].distance = 1;
                selectableTiles[j].parent = currentTile;
            }
        }
        if (selectableTiles[0] != null)
        {
            selectableTiles.Add(selectableTiles[0].GetAdjacentTile(forward));
            selectableTiles.Add(selectableTiles[0].GetAdjacentTile(left));
            selectableTiles.Add(selectableTiles[0].GetAdjacentTile(right));
        }
        else
        {
            for(int i = 0; i < 3; i++)
                selectableTiles.Add(null);
        }
        if(selectableTiles[1] != null)
            selectableTiles.Add(selectableTiles[1].GetAdjacentTile(left));
        else
            selectableTiles.Add(null);
        if (selectableTiles[1] != null)
            selectableTiles.Add(selectableTiles[2].GetAdjacentTile(right));
        else
            selectableTiles.Add(null);

        for (int j = 4; j < 7; j++)
        {
            if (selectableTiles[j] != null)
            {
                selectableTiles[j].distance = 2;
                selectableTiles[j].parent = selectableTiles[0];
            }
        }
        if(selectableTiles[7] != null)
            selectableTiles[7].parent = selectableTiles[1];
        if(selectableTiles[8] != null)
            selectableTiles[8].parent = selectableTiles[2];
        if(selectableTiles[4] != null)
            selectableTiles.Add(selectableTiles[4].GetAdjacentTile(forward));
        if (selectableTiles[9] != null)
        {
            selectableTiles[9].distance = 3;
            selectableTiles[9].parent = selectableTiles[4];
        }
        foreach (Tile t in selectableTiles)
        {
            if(t != null)
                t.selectable = true;
        }

        
        /*/////////////////////////////////////DISTANCE CODE//////////////////////////////////
        Queue<Tile> process = new Queue<Tile>();
        process.Enqueue(currentTile);
        currentTile.visited = true;
        //leave currentTile parent as null

        while(process.Count > 0)
        {
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;
            
            //if(t.distance < move)
            foreach(Tile tile in t.adjacencyList)
            {
                if(!tile.visited)
                {
                    tile.parent = t;
                    tile.visited = true;
                    tile.distance = 1 + t.distance;
                    process.Enqueue(tile);
                }
            }
        }
        */
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move()
    {
        int spacesMoved = path.Count;
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;
            
            //calculate unit's position on top of target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if(Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //Tile center reached
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            removeSelectableTiles();
            moving = false;
        }
        RotateObject(spacesMoved);
    }

    protected void removeSelectableTiles()
    {
        if(currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tile in selectableTiles)
        {
            if(tile != null)
                tile.Reset();
        }
        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }
    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }
    void RotateObject(int moved)
    {
        GetCurrentTile();
        List<Tile> tilesToFace = new List<Tile>();
        switch(moved)
        {
            case 0:
            case 1:
                tilesToFace = currentTile.adjacencyList;
                break;
            case 2:
                tilesToFace = currentTile.adjacencyNoBackList;
                break;
        }

        foreach(Tile tile in tilesToFace)
        {
            tile.GetComponent<Renderer>().material.color = Color.cyan;

        }
    }
}
