using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grid :MonoBehaviour
{

    private int rows = 10;
    private int cols = 10;
    private float cellSize;
    public static bool[,] Matrix = new bool[4, 4];
    private int previousPosx;
    private int previousPosy;

    private float sizeOfPlane = 5f;
    private GameObject[,] gridplane;


    private int scale = 1;


   
    public int CreateGrid()
    {

        // gridplane = new GameObject[rows, cols];
        //Matrix = new bool[100, 100];

        for (int x = 0; x < Matrix.GetLength(0); x++)
        {

            for (int y = 0; y < Matrix.GetLength(1); y++)
            {
                Matrix[x, y] = false;

            }
        }

        return Matrix.GetLength(0);
    }

    public static int GetLengthRows()
    {
        return Matrix.GetLength(0);
    }
    //changer la position du bloc 
    public void setPositionObject(GameObject bloc, bool isSetPosition)
    {

        Vector3 position = bloc.transform.position;
        int x = (int)position.x / scale;
        int y = (int)position.y / scale;

        Matrix[x, y] = isSetPosition;
        setPreviousPositionToFalse();
        previousPosx = x;
        previousPosy = y;

    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    private void setPreviousPositionToFalse()
    {

        Matrix[previousPosx, previousPosy] = false;
    }

    public static bool GetPositionMatrix(int i, int j)
    {   //je mets à true , parceque si on depasse les limite on considere qu'on ajoute rien dans la liste
        bool isFind = true;
        if (i < Matrix.GetLength(0) && j < Matrix.GetLength(1))
        {
            isFind = Matrix[i, j];
        }
        return isFind;
    }

    public void findWay(Vector3 pointA, Vector3 pointB)
    {

    }


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {


    }
}
