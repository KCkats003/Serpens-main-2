using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player2 : MonoBehaviour
{
    /*Keep in mind that Player2's script has not been altered yet, it does not contain the snake and ladder movement code.*/

    public Transform[] waypoints;
    int currentWaypointID = 0;
    public bool player2Turn = false;
    NavMeshAgent navMeshAgent2;
    int diceNum = 1; //test dice value
    GameObject secondPlayer, snakeScript, ladderScript, cableCar, cableCarDestination, cableCarStart;

    SnakeTile snake;
    LadderTile ladder;
    public TMPro.TextMeshProUGUI turnIndicator;
    bool onCableCar = false;
    bool arrived;

    void Start()
    {
        secondPlayer = GameObject.Find("player2");
        navMeshAgent2 = secondPlayer.GetComponent<NavMeshAgent>();

        player2Turn = false;

        print("player2 starting: " + currentWaypointID);
    }

    // Update is called once per frame
    void Update()
    {
        if (player2Turn)
        {
            int startingWaypointID = currentWaypointID;
            int endWaypointID = startingWaypointID + diceNum;

            for (int i = 0; i < diceNum; i++)
            {
                int playerCurrentPosition = currentWaypointID + i;

                if (navMeshAgent2.remainingDistance < 0.04f)
                {
                    playerCurrentPosition = (playerCurrentPosition + 1) % waypoints.Length; //Assuming that the player starts at waypoint 0
                    navMeshAgent2.SetDestination(waypoints[playerCurrentPosition].position);

                }

                if (playerCurrentPosition == endWaypointID)
                {
                    startingWaypointID = playerCurrentPosition;
                    currentWaypointID = playerCurrentPosition;
                    print("new player 2 starting point: " + startingWaypointID);
                    print("player 2 current point: " + currentWaypointID);
                    player2Turn = false;
                }
            }
        }



    }

}
