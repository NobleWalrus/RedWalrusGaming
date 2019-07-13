using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<Tile> adjacencyList = new List<Tile>();
    public List<Tile> adjacencyNoBackList = new List<Tile>();

    //BFS vars
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;
    public Vector3 parentDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        walkable = true;
        current = false;
        target = false;
        selectable = false;

        adjacencyList.Clear();
        adjacencyNoBackList.Clear();
        //BFS vars
        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeighbors()
    {
        Reset();

        CheckTile(Vector3.forward, adjacencyList);
        CheckTile(Vector3.back, adjacencyList);
        CheckTile(Vector3.right, adjacencyList);
        CheckTile(Vector3.left, adjacencyList);

        CheckTile(Vector3.forward, adjacencyNoBackList);
        CheckTile(Vector3.right, adjacencyNoBackList);
        CheckTile(Vector3.left, adjacencyNoBackList);

    }

    public Tile GetAdjacentTile(Vector3 direction)
    {
        Tile t = null;
        Vector3 halfExtents = new Vector3(0.25f, 0.25f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            t = item.GetComponent<Tile>();
        }
        return t;
    }

    public void CheckTile(Vector3 direction, List<Tile> list)
    {
        Vector3 halfExtents = new Vector3(0.25f, 0.25f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach(Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    tile.parentDirection = direction;
                    list.Add(tile);
                }
            }
        }
    }
}
