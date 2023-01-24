using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;

public class ConnexionAsync : MonoBehaviour
{

    TMP_InputField inputtext;
    
    private static byte[] _buffer = new byte[1024];

    private static Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static int idPlayer = 0;
    private int i = 1;
    private int j = 5;
    private int k = 10;
    private Thread _t1;
    private Thread _t2;
    public static Vector2Int startpos= Vector2Int.zero;
    public static Vector2Int endpos=Vector2Int.one;
    public static Vector2Int blocpos=Vector2Int.zero;
    // Start is called before the first frame update
    void Start()
    {

         //_t1 = new Thread(LoopConnect);
        // _t1.Start();
        //_t2 = new Thread(SendLoop);
        // _t2.Start();
        inputtext = GameObject.Find("input").GetComponent<TMP_InputField>();
        //LoopConnect();
        //SendLoop();
        startConnectAsync();

        //  inputtext.text = "start"+ "   "+ Grid.Matrix[10,10].ToString();

    }

    // Update is called once per frame
    void Update()
    {
       // inputtext.text = "update";


        blocpos.x = (int)PlacementPlayersAR.blocObject.transform.position.x;
        blocpos.y = (int)PlacementPlayersAR.blocObject.transform.position.y;
        //StartCoroutine(waiter());
      //  StartCoroutine(secondWaiter(ClientSocket));

    }



    IEnumerator waiter()
    {

        try
        {

            //inputtext.text = "shakal";
            Vector2Int   v = new Vector2Int(i, i);
            Vector2Int v2 = new Vector2Int(k, k);
            Vector2Int bloc = new Vector2Int(j, j);
            //Serialiser 
            var doc = new positions()
            {
                start = startpos,
                end = endpos,
                blocposition = blocpos

            };

           // inputtext.text = "send first";
            var jsondoc = JsonConvert.SerializeObject(doc);
           // inputtext.text = "send first_";
            byte[] buffer = Encoding.ASCII.GetBytes(jsondoc);
            
            ClientSocket.Send(buffer);
            //inputtext.text = "receive noow";
            //reception
            byte[] receivebuf = new byte[1024];
            int receive = ClientSocket.Receive(receivebuf);
            byte[] data = new byte[receive];
            Array.Copy(receivebuf, data, receive);
            //Debug.Log("Received: " + Encoding.ASCII.GetString(data) + "\n");
            string msg = Encoding.ASCII.GetString(data);
            
            jsondeserialize jsonString = JsonConvert.DeserializeObject<jsondeserialize>(msg);

       
            Ennemi1.listnodes = jsonString.listjson;
            List<Vector2Int> path = jsonString.listjson;


            string msgShow = "(";
            foreach (var item in path)
            {

                if (path[path.Count - 1] == item)
                {
                    msgShow += "(" + item.x.ToString() + "," + item.y.ToString() + ")";
                }
                else
                {
                    msgShow += "(" + item.x.ToString() + "," + item.y.ToString() + "),";
                }
            }
            msgShow += ")";
            inputtext.text = msgShow;
            //Debug.Log(msgShow);

            

        }
        catch (SocketException err) { Debug.Log("error:" + err);
            inputtext.text = err.ToString();
        }

        yield return (new WaitForSeconds(10));
        //j += 1;
        //k += 2;

    }


    
    public  void SendLoop()
    {
        //  Thread.Sleep(500);
        
        while (true)
        {

            try
            {


                Vector2Int v = new Vector2Int(1, 1);
                Vector2Int v2 = new Vector2Int(10, 10);
                Vector2Int bloc = new Vector2Int(5, 5);
                //Serialiser 
                /*var doc = new positions()
                {
                    start = v,
                    end = v2,
                    blocposition = bloc

                };*/
                var doc = new positions()
                {
                    start = v,
                    end = v2,
                    blocposition = bloc

                };
                var jsondoc = JsonConvert.SerializeObject(doc);

                byte[] buffer = Encoding.ASCII.GetBytes(jsondoc);

                ClientSocket.Send(buffer);

                //reception
                byte[] receivebuf = new byte[1024];
                int receive = ClientSocket.Receive(receivebuf);
                byte[] data = new byte[receive];
                Array.Copy(receivebuf, data, receive);
                Debug.Log("Received: " + Encoding.ASCII.GetString(data) + "\n");

                //deserialize
                string msg = Encoding.ASCII.GetString(data);
                jsondeserialize jsonString = JsonConvert.DeserializeObject<jsondeserialize>(msg);
                List<Vector2Int> path = jsonString.listjson;


                string msgShow  = "(";
                foreach (var item in path)
                {

                    if (path[path.Count - 1] == item)
                    {
                        msgShow += "(" + item.x.ToString() + "," + item.y.ToString() + ")";
                    }
                    else
                    {
                        msgShow += "(" + item.x.ToString() + "," + item.y.ToString() + "),";
                    }
                }
                msgShow += ")";
                inputtext.text = msgShow;
               
            }
            catch (SocketException err) { Debug.Log("error:" + err); }
        }
    }

    public  void LoopConnect()
    {
        int tentative = 0;
        int port = 100;
        string IPAddress ="192.168.1.51";
        while (!ClientSocket.Connected)
        {
            try
            {
                tentative++;
                ClientSocket.Connect(IPAddress, port);
                Debug.Log("connected");
                inputtext.text = "Connected";

                //reception
                byte[] receivebuf = new byte[1024];
                int receive = ClientSocket.Receive(receivebuf);
                byte[] data = new byte[receive];
                Array.Copy(receivebuf, data, receive);
                inputtext.text = data.ToString();





            }
            catch (SocketException err)
            {
               // Debug.Clear();
                Debug.Log("Connection attempts: " + tentative.ToString());

            }


          

        }


        //StartCoroutine(waiter());
        SendLoop();
        //Console.Clear();


    }
    public void startConnectAsync()
    {
        string IPAddres = "192.168.1.51";
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(IPAddres), 100);

        ClientSocket.BeginConnect(iPEndPoint, OnConnectCallBack, ClientSocket);
    }

    private void OnConnectCallBack(IAsyncResult ar)
    {
        Socket client =(Socket)ar.AsyncState;
        client.EndConnect(ar);
        byte[] buff = new byte[1024];
        ClientSocket.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallback), client);

        var doc = new positions()
        {
            start = startpos,
            end = endpos,
            blocposition = blocpos

        };



        var jsondoc = JsonConvert.SerializeObject(doc);
        byte[] buffer = Encoding.ASCII.GetBytes(jsondoc);
        ClientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);

        //recommencer à recevoir des données de la part du client
        ClientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallback), client);

    }

    private void OnReceiveCallback(IAsyncResult AR)
    {
        Socket socket = (Socket)AR.AsyncState;

        //bytecodes = la taille du tableau de donné envoyé par le client
        int bytecodes = socket.EndReceive(AR);
        //nouveau tableau avec la taille du tableau que le client a envoyé
        byte[] buffersrecept = new byte[bytecodes];
        Array.Copy(_buffer, buffersrecept, bytecodes);
        string msg = Encoding.ASCII.GetString(buffersrecept);
        inputtext.text = msg;
        jsondeserialize jsonString = JsonConvert.DeserializeObject<jsondeserialize>(msg);


        //Ennemi1.listnodes = jsonString.listjson;
        List<Vector2Int> path = jsonString.listjson;

        string msgShow = "(";
        foreach (var item in path)
        {

            if (path[path.Count - 1] == item)
            {
                msgShow += "(" + item.x.ToString() + "," + item.y.ToString() + ")";
            }
            else
            {
                msgShow += "(" + item.x.ToString() + "," + item.y.ToString() + "),";
            }
        }
        msgShow += ")";
        inputtext.text = msgShow;




       //**send**
        var doc = new positions()
        {
            start = startpos,
            end = endpos,
            blocposition = blocpos

        };


     
        var jsondoc = JsonConvert.SerializeObject(doc);
        byte[] buffer = Encoding.ASCII.GetBytes(jsondoc);
        ClientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
       
       
        //recommencer à recevoir des données de la part du client
       socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallback), socket);
        //StartCoroutine(secondWaiter(socket));
        

    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }
        catch (SocketException error)
        {
            Console.WriteLine(error.ToString());
        }
    }
    IEnumerator secondWaiter(Socket client) {


        //**send**
        var doc = new positions()
        {
            start = startpos,
            end = endpos,
            blocposition = blocpos

        };



        var jsondoc = JsonConvert.SerializeObject(doc);
        byte[] buffer = Encoding.ASCII.GetBytes(jsondoc);
        yield return (new WaitForSeconds(2));
        ClientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
        //recommencer à recevoir des données de la part du client
        ClientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(OnReceiveCallback), client);



        

    }
}
