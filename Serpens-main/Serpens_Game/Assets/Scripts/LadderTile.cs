using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTile : MonoBehaviour
{
    GameObject ladder;
    player firstPlayer;
    player2 secondPlayer;
    GameObject player1Script, player2Script;
    public int numSpacesForward = 4; //Number of spaces to move the player forward

    // Start is called before the first frame update
    void Start()
    {
        ladder = GameObject.FindGameObjectWithTag("Ladder");
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
