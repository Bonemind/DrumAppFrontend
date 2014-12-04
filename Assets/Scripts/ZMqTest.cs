using UnityEngine;
using System.Collections;
using NetMQ;
using AsyncIO;

public class ZMqTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ForceDotNet.Force();
        using (NetMQContext ctx = NetMQContext.Create())
        {
            using (var server = ctx.CreateResponseSocket())
            {
                server.Bind("tcp://127.0.0.1:5556");
                using (var client = ctx.CreateRequestSocket())
                {
                    client.Connect("tcp://127.0.0.1:5556");
                    client.Send("Hello");

                    string m1 = server.ReceiveString();
                    Debug.Log("From Client: " + m1);
                    server.Send("Hi Back");

                    string m2 = client.ReceiveString();
                    Debug.Log("From Server: " + m2);
                }
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
