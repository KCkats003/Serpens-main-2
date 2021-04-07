using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public bool win;
    public bool lose;
    public GameObject winscreen;
    public GameObject losescreen;

    public AudioSource winsound;

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        lose = false; 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space")) {
            if (win == false)
            {
                win = true;
                winsound.Play();
            }
            else if (win != false) {
                win = false; 
            }
              
        }
            //set win on or off 
            if (win == false)
        {
            winscreen.SetActive(false);
        }
        else {
            winscreen.SetActive(true);
        }

        if (Input.GetKeyDown("return"))
        {
            if (lose == false)
            {
                lose = true;
            }
            else if (lose != false)
            {
                lose = false;
            }

        }
        //set win on or off 
        if (win == false)
        {
            winscreen.SetActive(false);
        }
        else
        {
            winscreen.SetActive(true);
        }



        // set lose on or off 
        if (lose == false)
        {
            losescreen.SetActive(false);
        }
        else {
            losescreen.SetActive(true);
        }
    }
}
