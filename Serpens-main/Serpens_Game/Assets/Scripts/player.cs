using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypointID = 0;
    public bool player1Turn = false;
    bool stopMovement = false;
    NavMeshAgent navMeshAgent1, cableCarNavMesh;
    public int diceNum = 4; //test dice value
    GameObject firstPlayer, snakeScript, ladderScript, cableCar, cableCarDestination, cableCarStart;
    SnakeTile snake;
    LadderTile ladder;
    public TMPro.TextMeshProUGUI turnIndicator;
    bool onCableCar = false;
    bool arrived;

    public bool playWalkAnimation;

    Rigidbody rBody;

    int startingWaypointID;
    int endWaypointID;


    //Apoline
    Vector3 DicePosStop;

    void Start()
    {
        print("dice: " + diceNum);
        firstPlayer = GameObject.Find("player");
        navMeshAgent1 = firstPlayer.GetComponent<NavMeshAgent>();

        //snakeScript = GameObject.Find("tile5");
        //snake = snakeScript.GetComponent<SnakeTile>();
        //ladderScript = GameObject.Find("tile2");
        //ladder = ladderScript.GetComponent<LadderTile>();

        cableCar = GameObject.Find("CableCarTest");

        cableCarNavMesh = GameObject.Find("CarNavMesh").GetComponent<NavMeshAgent>();
        cableCarDestination = GameObject.Find("CableCarDestination");
        cableCarStart = GameObject.Find("CableCarStart");

        rBody = GetComponent<Rigidbody>();

        player1Turn = true;

        print("player starting: " + currentWaypointID);


    }

    void MovePlayer()
    {
        navMeshAgent1.isStopped = false;
        rBody.isKinematic = false;

        startingWaypointID = currentWaypointID; //Player's start waypoint
        endWaypointID = startingWaypointID + diceNum; //Player's end waypoint

        if (stopMovement == false)
        {
            for (int i = 0; i < diceNum; i++)
            {
                int playerCurrentPosition = currentWaypointID + i;

                //Get distance between the player and the end waypoint, move the player towards it as long as the distance between
                // them is more than 0.03
                if (Vector3.Distance(navMeshAgent1.transform.position, waypoints[endWaypointID].position) > 0.03f)
                {
                    playWalkAnimation = true;
                    playerCurrentPosition = (playerCurrentPosition + 1) % waypoints.Length; //Assuming that the player starts at waypoint 0
                    navMeshAgent1.SetDestination(waypoints[playerCurrentPosition].position);
                }

                //If endpoint reached, set the new start point for next turn
                if (playerCurrentPosition == endWaypointID)
                {
                    startingWaypointID = playerCurrentPosition;
                    currentWaypointID = playerCurrentPosition;
                    
                    print("new player 1 starting point: " + startingWaypointID);
                    print("player 1 current point: " + currentWaypointID);
                    print("endWaypointID: " + endWaypointID);
                    print("playerCurrentPosition: " + playerCurrentPosition);
                    stopMovement = true;
                }
            }
        }

    }

    //Teleports the player to a tile further back if they land on a snake tile (numSpacesBack determined by snakeTile script)
    void MovePlayerBack()
    {
        snake = waypoints[currentWaypointID].GetComponent<SnakeTile>();
        //Set the new start point for next turn
        currentWaypointID = currentWaypointID - snake.numSpacesBack;
        startingWaypointID = currentWaypointID;
        endWaypointID = currentWaypointID;

        navMeshAgent1.transform.position = waypoints[currentWaypointID].transform.position;

        print("new player 1 starting point: " + startingWaypointID);
        print("player 1 current point: " + currentWaypointID);
        print("endWaypointID: " + endWaypointID);
    }

    //Move the player to the Cable Car
    void MovePlayerToCableCar()
    {
        if (Vector3.Distance(navMeshAgent1.transform.position, cableCar.transform.position) > 0.03f) 
        {
           playWalkAnimation = true;
            navMeshAgent1.SetDestination(cableCar.transform.position); 
        }
    }


    //If player collides with the Cable Car, player is parented to the car (still a work in progress)
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "CableCarTest")//
        {
            onCableCar = true;
        }
    }

    //If player leaves the Cable Car, player is unparented from the car (still a work in progress)
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "CableCarTest") //
        {
            onCableCar = false; 
        }
    }

    //Move the CableCar to end destination once player has collided and parented with car
    void MoveCableCar()
    {
        if (onCableCar)
        {
            if (Vector3.Distance(cableCarNavMesh.transform.position, cableCarDestination.transform.position) > 0.03f)
            {
                cableCarNavMesh.SetDestination(cableCarDestination.transform.position);
            }
                
        }
    }


    //Move player from the Cable Car to the end waypoint
    void MovePlayerOffCableCar()
    {
        transform.parent = null;
        ladder = waypoints[currentWaypointID].GetComponent<LadderTile>();

        if (Vector3.Distance(navMeshAgent1.transform.position, waypoints[currentWaypointID+ladder.numSpacesForward].position) > 4f)
        {
            playWalkAnimation = true;
            navMeshAgent1.transform.position = Vector3.MoveTowards(navMeshAgent1.transform.position, waypoints[currentWaypointID + ladder.numSpacesForward].position, Time.deltaTime * 4);
            //navMeshAgent1.SetDestination(waypoints[currentWaypointID + ladder.numSpacesForward].position);
        }
        else
        {
            //if (GetComponent<Rigidbody>().velocity.sqrMagnitude < 5 && GetComponent<Rigidbody>().angularVelocity.sqrMagnitude < 0.1)
            {
                playWalkAnimation = false;
                currentWaypointID = currentWaypointID + ladder.numSpacesForward;
                startingWaypointID = currentWaypointID;
                endWaypointID = currentWaypointID;

                //Set new start point for next turn
                print("new player 1 starting point: " + startingWaypointID);
                print("player 1 current point: " + currentWaypointID);
                print("endWaypointID: " + endWaypointID);
                arrived = true;
            }
        }
    }

    //Move cable car back to original position once it drops off the player
    void MoveCableCarBack()
    {
        cableCarNavMesh.SetDestination(cableCarStart.transform.position);
    }


    IEnumerator Move()
    {
        MovePlayer();

        switch (diceNum)
        {
            case 1:
                yield return new WaitForSeconds(10);
                break;
            case 2:
                yield return new WaitForSeconds(15);
                break;
            case 3:
                yield return new WaitForSeconds(16);
                break;
            case 4:
                yield return new WaitForSeconds(22);
                break;
            case 5:
                yield return new WaitForSeconds(25);
                break;
            case 6:
                yield return new WaitForSeconds(30);
                break;
        }

        if (waypoints[currentWaypointID].tag == "Snake")
        {
            MovePlayerBack();
        }

        if (waypoints[currentWaypointID].tag == "Ladder")
        {
            MovePlayerToCableCar();
            yield return new WaitForSeconds(5);
            MoveCableCar();
            yield return new WaitForSeconds(22);
            if (!arrived)
            {
                MovePlayerOffCableCar();
            }
            yield return new WaitForSeconds(2);
            MoveCableCarBack();
        }

        player1Turn = false;
        stopMovement = false;
        rBody.isKinematic = true;
        navMeshAgent1.isStopped = true;
    }

    // Update is called once per frame
    void Update() 
    {
        //If player1's turn, start sequence of actions (coroutines)
        if (player1Turn) 
        {
            /*
             * Rolling die code might go here
             */
            StartCoroutine(Move());
            turnIndicator.text = "Player1 Turn | Ladder: Tile2, Move forward 4 | Snake: Tile5, Move back 3";
        }
        else
        {
            turnIndicator.text = "";
        }

        //If player has stopped moving and has reached end waypoint, walk animation stops playing (more details in testAnimateCode Script)
        if (!navMeshAgent1.pathPending)
        {
            if (navMeshAgent1.remainingDistance <= 1 || navMeshAgent1.isStopped)
            {
                if (!navMeshAgent1.hasPath || navMeshAgent1.velocity.sqrMagnitude < 5)
                {
                    playWalkAnimation = false;
                    
                }
            }
        }
    }

    //Apoline Die Check Code

    private void OnTriggerEnter(Collider other)
    {
        GameObject go;
        go = GameObject.FindWithTag("Dice");
        go.GetComponent<Dice>().CheckPos();
        go.GetComponent<Dice>().DieStopPosition(DicePosStop);


        if (other.name == "Side1")
        {
            Debug.Log("Player hit Side 1, therefor 3");
            //if it is true, turn position slowly until its at that fixed position.....check which posion x is closest to camera....that is the number set

        }
        else if (other.name == "Side2")
        {
            Debug.Log("Player hit Side 2, therefor 4");

        }
        else if (other.name == "Side3")
        {
            Debug.Log("Player hit Side 3, therefor 1");

        }
        else if (other.name == "Side4")
        {
            Debug.Log("Player hit Side 4, therefor 2");

        }
        else if (other.name == "Side5")
        {
            Debug.Log("Player hit Side 5, therefor 6");
        }
        else if (other.name == "Side6")
        {
            Debug.Log("Player hit Side 6, therefor 5");

        }
    }











}
