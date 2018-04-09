using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FindAgent : Agent
{
    private FindAcademy academy;

    public GameObject CorrectCarArea;
    public GameObject WrongCarArea;
    public Text txtReward;

    private float distance;
    private GameObject CorrectCar;
    //private GameObject WrongCar;
    private static float WRONG_CAR_DIST = 0.2f;

    public override void InitializeAgent()
    {
        academy = FindObjectOfType(typeof(FindAcademy)) as FindAcademy;
        academy.AcademyReset();
        getNearestCar();

        distance = Vector3.Distance(transform.position, CorrectCar.transform.position);
    }

    public override void CollectObservations()
    {
        GameObject[] wrongCars = GameObject.FindGameObjectsWithTag("wrong");
        for (int i = 0; i < 3; i++)
        {
            if (i < wrongCars.Count())
            {
                AddVectorObs(transform.position.x - wrongCars[i].transform.position.x);
                AddVectorObs(transform.position.z - wrongCars[i].transform.position.z);
            }
            else
            {
                AddVectorObs(transform.position.x - wrongCars[0].transform.position.x);
                AddVectorObs(transform.position.z - wrongCars[0].transform.position.z);
            }
        }
        AddVectorObs(transform.position.x - CorrectCar.transform.position.x);
        AddVectorObs(transform.position.z - CorrectCar.transform.position.z);
        //AddVectorObs(transform.position.x);
        //AddVectorObs(transform.position.z);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        txtReward.text = GetReward().ToString() + " -- " + GetCumulativeReward().ToString();
        int action = Mathf.FloorToInt(vectorAction[0]);

        Vector3 targetPos = transform.position;
        if (action == 0)
        {
            targetPos = transform.position + new Vector3(.02f, 0, 0f);
        }

        if (action == 1)
        {
            targetPos = transform.position + new Vector3(-.02f, 0, 0f);
        }

        if (action == 2)
        {
            targetPos = transform.position + new Vector3(0f, 0, .02f);
        }

        if (action == 3)
        {
            targetPos = transform.position + new Vector3(0f, 0, -.02f);
        }

        Collider[] blockTest = Physics.OverlapSphere(targetPos, GetComponent<SphereCollider>().radius * .2f);
        if (blockTest.Where(col => col.gameObject.tag == "wall").ToArray().Length == 0)
        {
            transform.position = targetPos;

            if (blockTest.Where(col => col.gameObject.tag == "correct").ToArray().Length == 1)
            {
                SetReward(1f);
                DestroyImmediate(blockTest.Where(col => col.gameObject.tag == "correct").ToArray()[0].gameObject);
                if (!getNearestCar())
                {
                    Done();
                    return;
                }
            }
            else if (blockTest.Where(col => col.gameObject.tag == "wrong").ToArray().Length == 1)
            {
                SetReward(-1f);
                Done();
                return;
            }

            if (Vector3.Distance(targetPos, CorrectCar.transform.position) < distance)
            {
                SetReward(0.01f);
                distance = Vector3.Distance(targetPos, CorrectCar.transform.position);
            }
            else
            {
                SetReward(-0.01f);
            }
        }
        else
        {
            SetReward(-0.05f);
        }
        //distance = Vector3.Distance(targetPos, CorrectCar.transform.position);

        /*
        GameObject[] wrongCars = GameObject.FindGameObjectsWithTag("wrong");
        foreach (GameObject wrongCar in wrongCars)
        {
            if (Vector3.Distance(targetPos, wrongCar.transform.position) <= WRONG_CAR_DIST)
            {
                //Debug.Log("too near");
                AddReward(-0.1f);
            }
        }
        */
        //Done();
    }

    public override void AgentReset()
    {
        transform.position = new Vector3(0f, 0.15f, 0f);
        academy.AcademyReset();
        getNearestCar();
        distance = Vector3.Distance(transform.position, CorrectCar.transform.position);
    }

    private bool getNearestCar()
    {
        float shortest = 999;
        CorrectCar = null;
        /*
        foreach (Transform child in CorrectCarArea.transform)
        {
            if (child.tag == "correct")
            {
                if (Vector3.Distance(child.position, transform.position) < shortest)
                {
                    CorrectCar = child.gameObject;
                    shortest = Vector3.Distance(child.position, transform.position);
                }
            }
        }
        */
        GameObject[] correctCars = GameObject.FindGameObjectsWithTag("correct");
        //Debug.Log("cars - " + correctCars.Count());
        foreach (GameObject child in correctCars)
        {
            //Debug.Log("distance = " +  Vector3.Distance(child.transform.position, transform.position));
            if (Vector3.Distance(child.transform.position, transform.position) < shortest)
            {
                CorrectCar = child;
                shortest = Vector3.Distance(child.transform.position, transform.position);
            }
        }
        
        /*
        foreach (Transform child in WrongCarArea.transform)
        {
            if (child.tag == "wrong")
                WrongCar = child.gameObject;
        }
        */

        if (CorrectCar == null)
        {
            //Debug.Log("No more car");
            return false;
        }
        else
        {
            CorrectCar.GetComponent<Renderer>().material.color = Color.black;
            distance = Vector3.Distance(transform.position, CorrectCar.transform.position);
            return true;
        }
    }

}
