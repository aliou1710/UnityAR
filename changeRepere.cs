using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeRepere
{
    private int ratio = 10;
    private int offsetratio = 50;
    public changeRepere()
    {

    }
    public void setRatio(int ratio) { this.ratio = ratio; }

    public Vector2Int changeRepereWord_to_Matrix(Vector2Int v)
    {
        int x = (v.x / ratio) + offsetratio;
        int y = (v.y / ratio) + offsetratio;
        return new Vector2Int(x, y);
    }
    public Vector2Int changeRepereMatrix_to_Word(Vector2Int v)
    {
        int x = (v.x - offsetratio) * ratio;
        int y = (v.y - offsetratio) * ratio;
        return new Vector2Int(x, y);
    }

}
