using UnityEngine;
using System.Collections;
using AsyncIO;
using NetMQ;

public class ZmqPub : MonoBehaviour {

	// Use this for initialization
    NetMQ.NetMQContext context;
    NetMQ.Sockets.PublisherSocket pubSocket;
    
	void Start () {
        AsyncIO.ForceDotNet.Force();
        context = NetMQContext.Create();
        pubSocket = context.CreatePublisherSocket();
        pubSocket.Bind("tcp://127.0.0.1:7284");
        Debug.Log("Server started");
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pubSocket.Send("Space pressed");
        }
        if (pubSocket != null)
        {
            pubSocket.Poll(new System.TimeSpan(30));
            if (pubSocket.HasIn)
            {
                Debug.Log(pubSocket.ReceiveMessage(true));
            }

        }
	}
    void OnApplicationQuit()
    {
        pubSocket.Close();
    }
}
