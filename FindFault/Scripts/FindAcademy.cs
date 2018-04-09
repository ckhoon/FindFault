using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAcademy : Academy {

    public int CorrectA;
    public int CorrectB;
    public int Wrong;
    public GameObject CorrectPreFab;
    public GameObject CorrectParentA;
    public GameObject CorrectParentB;
    public GameObject WrongPreFab;
    public GameObject WrongParent;
    public GameObject WallTop;
    public GameObject WallBottom;
    public GameObject WallLeft;
    public GameObject WallRight;

    private static int ALL_COLOR = 5;
    private Color[] testColor = new Color[ALL_COLOR];
    private int nCorrectA, nCorrectB, nWrong;


    public override void InitializeAcademy()
    {
        testColor[0] = Color.blue;
        testColor[1] = Color.gray;
        testColor[2] = Color.magenta;
        testColor[3] = Color.red;
        testColor[4] = Color.green;

        //Correct = 2;
        //Wrong = 1;
    }


    public override void AcademyReset()
    {
        nCorrectA = Random.Range(0, CorrectA+1);
        nCorrectB = Random.Range(1, CorrectB+1);
        nWrong = Random.Range(1, Wrong+1);

        //Debug.Log("Academy reset");
        for (int i = CorrectParentA.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(CorrectParentA.transform.GetChild(i).gameObject);
        }
        for (int i = CorrectParentB.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(CorrectParentB.transform.GetChild(i).gameObject);
        }
        for (int i = WrongParent.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(WrongParent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < nCorrectA; i++)
        {
            float x = Random.Range(CorrectParentA.transform.position.x - CorrectParentA.GetComponent<Renderer>().bounds.size.x/2 + CorrectPreFab.GetComponent<Renderer>().bounds.size.x / 2,
                                    CorrectParentA.transform.position.x + CorrectParentA.GetComponent<Renderer>().bounds.size.x / 2 - CorrectPreFab.GetComponent<Renderer>().bounds.size.x / 2);
            float y = Random.Range(CorrectParentA.transform.position.z - CorrectParentA.GetComponent<Renderer>().bounds.size.z / 2 + CorrectPreFab.GetComponent<Renderer>().bounds.size.z / 2,
                                    CorrectParentA.transform.position.z + CorrectParentA.GetComponent<Renderer>().bounds.size.z / 2 - CorrectPreFab.GetComponent<Renderer>().bounds.size.z / 2);

            GameObject objCorrect = Instantiate(
               CorrectPreFab,
               new Vector3(x, 0.1f, y),
               Quaternion.identity);
               
            objCorrect.transform.parent = CorrectParentA.transform;
            objCorrect.GetComponent<Renderer>().material.color = testColor[i % ALL_COLOR];
            objCorrect.tag = "correct";
        }

        for (int i = 0; i < nCorrectB; i++)
        {
            float x = Random.Range(CorrectParentB.transform.position.x - CorrectParentB.GetComponent<Renderer>().bounds.size.x / 2 + CorrectPreFab.GetComponent<Renderer>().bounds.size.x / 2,
                                    CorrectParentB.transform.position.x + CorrectParentB.GetComponent<Renderer>().bounds.size.x / 2 - CorrectPreFab.GetComponent<Renderer>().bounds.size.x / 2);
            float y = Random.Range(CorrectParentB.transform.position.z - CorrectParentB.GetComponent<Renderer>().bounds.size.z / 2 + CorrectPreFab.GetComponent<Renderer>().bounds.size.z / 2,
                                    CorrectParentB.transform.position.z + CorrectParentB.GetComponent<Renderer>().bounds.size.z / 2 - CorrectPreFab.GetComponent<Renderer>().bounds.size.z / 2);

            GameObject objCorrect = Instantiate(
               CorrectPreFab,
               new Vector3(x, 0.1f, y),
               Quaternion.identity);

            objCorrect.transform.parent = CorrectParentB.transform;
            objCorrect.GetComponent<Renderer>().material.color = testColor[i % ALL_COLOR];
            objCorrect.tag = "correct";
        }

        for (int i = 0; i < nWrong; i++)
        {
            float x = Random.Range(WrongParent.transform.position.x - WrongParent.GetComponent<Renderer>().bounds.size.x / 2 + WrongPreFab.GetComponent<Renderer>().bounds.size.x / 2,
                                    WrongParent.transform.position.x + WrongParent.GetComponent<Renderer>().bounds.size.x / 2 - WrongPreFab.GetComponent<Renderer>().bounds.size.x / 2);
            float y = Random.Range(WrongParent.transform.position.z - WrongParent.GetComponent<Renderer>().bounds.size.z / 2 + WrongPreFab.GetComponent<Renderer>().bounds.size.z / 2,
                                    WrongParent.transform.position.z + WrongParent.GetComponent<Renderer>().bounds.size.z / 2 - WrongPreFab.GetComponent<Renderer>().bounds.size.z / 2);
            GameObject objCorrect = Instantiate(
               WrongPreFab,
               new Vector3(x, 0.1f, y),
               Quaternion.identity);
            objCorrect.transform.parent = WrongParent.transform;
            objCorrect.tag = "wrong";
        }
    }

    public override void AcademyStep()
    {
    }
}
