﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : EndOfHeroesMove
{
    public bool shouldMoveThisFrame = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        shouldMoveThisFrame = false;

        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            shouldMoveThisFrame = true;
        }
    }

    //For Physics Updates
    void FixedUpdate()
    {
        if (shouldMoveThisFrame)
        {
            Move();
        }
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTile(t);

                    }
                }
            }
        }
    }
}
