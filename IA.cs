using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    private int rows = 100;
    private int cols = 100;
    public bool[,] Matrix ;
    private int previousPosx;
    private int previousPosy;
    

    public void setPositionObject(GameObject bloc,bool isSetPosition)
    {
        Vector3 position = bloc.transform.position;
        int x = (int)position.x;
        int y = (int)position.y;

        Matrix[x,y] = isSetPosition;
        setPreviousPosition();
        previousPosx = x;
        previousPosy = y;
      
    }
    private void setPreviousPosition()
    {

        Matrix[previousPosx, previousPosy] = false;
    }

    
    
    public void findWay(Vector3 pointA ,Vector3 pointB)
    {

    }

    public 
    // Start is called before the first frame update
    void Start()
    {
        Matrix = new bool[rows, cols];
        for(int i =0; i<rows; i++)
        {
            for(int j = 0; j< cols; j++)
            {
                Matrix[i, j] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
