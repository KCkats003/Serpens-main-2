using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnimateCode : MonoBehaviour
{
    player firstPlayer;
    player2 secondPlayer;
    GameObject player1Script, player2Script;

    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        player1Script = GameObject.Find("player");
        firstPlayer = player1Script.GetComponent<player>();

        player2Script = GameObject.Find("player2");
        secondPlayer = player2Script.GetComponent<player2>();

        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstPlayer.playWalkAnimation)
        {
            anim.Play("Armature.001|Jump");
        }
        else
        {
            anim.Play("Armature.001|Idle");
        }
    }
}
