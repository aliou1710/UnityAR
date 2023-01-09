using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : MonoBehaviour
{
    // Start is called before the first frame update
    private int bestLength;
    //je cree une paire de liste pour stocker chaque valeur et le chemin parcouru
    Queue<KeyValuePair<List<Vector2>, Vector2>> integer_stack = null;

    private List<Vector2> listcheckoutLength = null;
    bool[,] tarray;
    Vector2 endvalue;
    Vector2 beginvalue;
    public static BFS dfs;
    int[] dx = { -1, 1, 0, 0, -1, -1, 1, 1 };
    int[] dy = { 0, 0, -1, 1, -1, 1, -1, 1 };
    List<(int x, int y)> listcheckoutLength_;

    int rows = 100;
    int cols = 100;


    Vector2 v = new Vector2(1, 1);
    Vector2 v2 = new Vector2(50, 50);
    bool isCheckifDFS = false;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }
    public string getNameBFS()
    {
        return "bfs";
    }
    public List<Vector2> getPathsDfs()
    {
        return this.listcheckoutLength;
    }

    public void checkupDfs(Vector2 _begin, Vector2 endvalue)
    {

        // path = new List<Vector2>();
        beginvalue = _begin;
        //Vector2 endvalue = _end;

        listcheckoutLength = new List<Vector2>();
        List<Vector2> lists = new List<Vector2>();
        lists.Add(_begin);
        integer_stack = null;
        integer_stack = new Queue<KeyValuePair<List<Vector2>, Vector2>>();
        integer_stack.Enqueue(new KeyValuePair<List<Vector2>, Vector2>(lists, _begin));

        tarray = new bool[rows, cols];
        //create array for checking if node has been visited
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                tarray[i, j] = false;
            }

        }

        DFSmethods(endvalue);

        //*********INSERT to _path (pNodes)***********

        //si cette liste qui contient le chemin le plus court n'est pas vide
        if (listcheckoutLength != null)
        {
            bestLength = listcheckoutLength.Count;

        }
        else
        {
            bestLength = int.MaxValue;
        }



        lists.Clear();




    }

    bool valid(int x, int y)
    {
        return (x >= 0 && x < rows && y >= 0 && y < cols);
    }


    public void DFSmethods(Vector2 endvalue)
    {
        while (integer_stack.Count != 0 && tarray[(int)endvalue.x, (int)endvalue.y] == false)
        {
            //je cree une paire  temporaire 
            //KeyValuePair<List<Vector2>, Vector2> tmp = integer_stack.Peek();//on recupere la derniere paire d'elements du stack (qui est le Top)
            //top element in the stack (warning: the top element is the last element added in stack)

            KeyValuePair<List<Vector2>, Vector2> tmp = integer_stack.Dequeue(); // remove the top element
                                                                                //top element in the stack (warning: the top element is the last element added in stack)
            Vector2 v = tmp.Value;
            Debug.Log(integer_stack.Count);                   //Debug.Log(v.x.ToString() + ' ' + v.y.ToString());


            bool find = tarray[(int)v.x, (int)v.y];
            if (find == false)
            {
                //si la valeur v correspond au sommet _end, 
                tarray[(int)v.x, (int)v.y] = true;
                if ((int)v.x == (int)endvalue.x && (int)v.y == (int)endvalue.y)
                {

                    //si la liste n'est pas vide
                    if (listcheckoutLength.Count != 0)
                    {

                        //on compare la longueur du chemin actuel à la longueur du chemin se trouvant dans la list listcheckoutLength

                        if (tmp.Key.Count < listcheckoutLength.Count)
                        {
                            //on fait un copy constructeur
                            listcheckoutLength = new List<Vector2>(tmp.Key);
                            bestLength = tmp.Key.Count;

                        }
                    }
                    else
                    {//si la liste listcheckout est vide

                        listcheckoutLength = new List<Vector2>(tmp.Key);


                    }
                    //la valeur du end dans le tableau booleen  est mis  à false
                    //tarray[(int)endvalue.x, (int)endvalue.y] = false;
                }
                else
                {
                    int valuex = (int)v.x;
                    int valuey = (int)v.y;
                    // on ajoute tous les somments suivants de v dans le stack

                    for (int j = 0; j < 8; ++j)
                    {

                        int next_x = valuex + dx[j];
                        int next_y = valuey + dy[j];
                        //if (valid(next_x, next_y) )
                        if (valid(next_x, next_y) && (Grid.GetPositionMatrix(next_x, next_y) == false && (tarray[next_x, next_y] == false)))
                        {


                            //copie de la liste
                            //listcheckoutLength = new List<Vector2>(tmp.Key);
                            List<Vector2> tmpCopie = new List<Vector2>(tmp.Key);
                            //List<Vector2> tmpCopie = new List<Vector2>();
                            //tmpCopie = tmp.Key;

                            //on ajoute l'element j dans la liste
                            Vector2 vq = new Vector2(next_x, next_y);

                            tmpCopie.Add(vq);
                            //KeyValuePair<List<Vector2>, Vector2> tmp2 = new KeyValuePair<List<Vector2>, Vector2>(tmpCopie, vq);
                            // on ajoute cette paire dans le stack
                            //integer_stack.Push(tmp2);
                            integer_stack.Enqueue(new KeyValuePair<List<Vector2>, Vector2>(tmpCopie, vq));
                            tmpCopie = null;





                        }
                    }


                }

            }
            tmp.Key.Clear();

            /* if (integer_stack.Count != 0 && tarray[(int)endvalue.x, (int)endvalue.y] ==false)
             {
               return  DFSmethods(endvalue);
             }
             else
             {
                 Debug.Log("stack vide");
                 return 0;
             }*/
        }
    }
}

