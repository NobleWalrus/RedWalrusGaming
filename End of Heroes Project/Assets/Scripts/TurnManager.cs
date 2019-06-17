using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<EndOfHeroesMove>> units = new Dictionary<string, List<EndOfHeroesMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<EndOfHeroesMove> turnTeam = new Queue<EndOfHeroesMove>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue()
    {
        List<EndOfHeroesMove> teamList = units[turnKey.Peek()];

        foreach (EndOfHeroesMove unit in teamList)
        {
            turnTeam.Enqueue(unit);

        }

        StartTurn();
    }

    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        EndOfHeroesMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(EndOfHeroesMove unit)
    {
        List<EndOfHeroesMove> list;
        
        if (!units.ContainsKey(unit.tag))
        {
            list = new List<EndOfHeroesMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }
}
