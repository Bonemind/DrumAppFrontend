using UnityEngine;
using System.Collections;
using AsyncIO;
using NetMQ;
using System;

[RequireComponent (typeof(INoteHandler))]
public class ZmqSub : MonoBehaviour {

	// Use this for initialization
    NetMQContext context;
    NetMQ.Sockets.SubscriberSocket consumer;
    private INoteHandler handler;
    public int portNumber;
    public NoteInputType inputType;

	void Start () {
        ForceDotNet.Force();
        handler = gameObject.GetComponent(typeof(INoteHandler)) as INoteHandler;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            context = NetMQContext.Create();
            consumer = context.CreateSubscriberSocket();
            consumer.Connect("tcp://127.0.0.1:" + portNumber);
            consumer.Subscribe("");
            Debug.Log("Client connected");
        }
        if (consumer != null)
        {
            consumer.Poll(new TimeSpan(30));
            while (consumer.HasIn)
            {
                OnMessageHandler(consumer.ReceiveString(new TimeSpan(1)));
            }
        }
	}

    void OnApplicationQuit()
    {
        if (consumer != null)
        {
            consumer.Close();
        }
        context.Terminate();
    }

    private void OnMessageHandler(string message)
    {
        string[] split = message.Split(',');
        if (split.Length != 2)
        {
            Debug.LogError("Recieved malformed note packet: " + message);
        }
        int noteNumber = int.Parse(split[0]);
        int velocity = int.Parse(split[1]);
        handler.PlayNote(noteNumber, velocity, inputType);
    }
}
