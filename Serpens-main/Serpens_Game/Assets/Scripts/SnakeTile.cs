using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTile : MonoBehaviour
{
    GameObject snake;
    player firstPlayer;
    player2 secondPlayer;
    GameObject player1Script, player2Script;
    public int numSpacesBack = 3; //Number of spaces to move the player back

    // Start is called before the first frame update
    void Start()
    {
        snake = GameObject.FindGameObjectWithTag("Snake");
        player1Script = GameObject.Find("player");
        firstPlayer = player1Script.GetComponent<player>();

        player2Script = GameObject.Find("player2");
        secondPlayer = player2Script.GetComponent<player2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
