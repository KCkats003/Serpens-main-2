using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    int numSides = 6;
    float RollTimer = 3;
    int dieRoll;
    string dieCheck;
    int dieNum;
    public Rigidbody DiceRB;
    public bool Rolling = true;

    GameObject go;
    GameObject Player;
    GameObject Camera;
    Vector3 DicePosStop;
    int finalNum;
    private float timeCount = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

        DiceRB = GetComponent<Rigidbody>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        Player = GameObject.FindGameObjectWithTag("Player");
        

    }

    // Update is called once per frame
    void Update()
    {




        if (Rolling == true)
        {

            float dirX = Random.Range(-500, 500);
            float dirY = Random.Range(-500, 500);
            float dirZ = Random.Range(-500, 500);


            DiceRB.MoveRotation(DiceRB.rotation * (Quaternion.Euler(dirX, 0f, 0f * Time.deltaTime)));
            DiceRB.MoveRotation(DiceRB.rotation * (Quaternion.Euler(0f, dirY, 0f * Time.deltaTime)));
            DiceRB.MoveRotation(DiceRB.rotation * (Quaternion.Euler(0f, 0f, dirZ * Time.deltaTime)));

        }
        else if (Rolling == true)
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z), timeCount);
            timeCount = timeCount + Time.deltaTime;


         
        }

    }

    public void DieStopPosition(Vector3 DiceChosen)
    {

        //DicePosStop = DiceChosen;


    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rolling = false;
        }
    }


    public Vector3 CheckPos()
    {

        go = GameObject.Find("Side1");
        GameObject Final;
        Final = go;
        Vector3 endPos = go.transform.position;


        for (int i = 1; i <= 6; i++)
        {
            string Side = "Side" + i;

            go = GameObject.Find(Side);
            //Camera = GameObject.FindGameObjectWithTag("MainCamera");
            Vector3 comparePos = go.transform.position;
            Vector3 cameraPos = Camera.transform.position;
            //   Vector3 prevPos = comparePos - cameraPos;
            // Vector3 currPos = endPos - cameraPos;


            float prevPos = Vector3.Distance(endPos, cameraPos); // Calculating Distance

            float currPos = Vector3.Distance(comparePos, cameraPos); // Calculating Distance


            if (currPos <= prevPos)
            {

                endPos = comparePos;
                finalNum = i;
                Final = go;
            }
            DicePosStop = new Vector3(Final.transform.position.x, Final.transform.position.y, Final.transform.position.z);
        }
        Debug.Log("Final Roll is" + finalNum);

        DiceNumRoller(finalNum);

        DicePosStop = new Vector3(Final.transform.position.x, Final.transform.position.y, Final.transform.position.z);
        return DicePosStop;
    }
    public void DiceNumRoller (int finalNum)
    {
        Player.GetComponent<player>().diceNum = finalNum;
    }



}




