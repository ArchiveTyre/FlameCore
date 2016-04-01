using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;


public class Network_Networking : MonoBehaviour {
	
	// The client tcp.
	private TcpClient tcpclnt = new TcpClient();
	
	// The network stream.
	private NetworkStream netStm;
	
	// Reader.
	private StreamReader netSr;
	
	// Writer.
	private StreamWriter netSw;
	
	// Network GUI promt.
	public Network_Promt network_Promt;
	
	// The socket of the network.
	private Socket netSo;
	
	public string ReadLn () 
	{	
		// Make sure data is available before read, so we don't time out unecessarly.
		if (netSo.Poll(100, SelectMode.SelectRead))
			// Return the read value.
			return netSr.ReadLine();
		
		// Nothing was found, return null.
		return null;
	}
	
	public void WriteLn (string str)
	{
		// Do some debugggggg.
		Debug.Log ("Write: "+str+"\n");
		netSw.Write(str+"\n");
		netSw.Flush();
	}
	void Connect(string server, int port) 
	{
		try {
			
			// Set up the connection
			tcpclnt.Connect(server, port);
			netStm = tcpclnt.GetStream(); 
			netSo = tcpclnt.Client;
			netSr = new StreamReader(netStm);
			netSw = new StreamWriter(netStm);
			netStm.ReadTimeout = 100;
			CommunicateSetup();
        }
        
        catch (Exception e){
            Debug.LogError("Error... "+e);
        }
	}
	
	public void CommunicateSetup ()
	{
		
		// Tell what type we are.
		WriteLn ("rsb_web_game");
        Debug.Log ( ReadLn() );
			
		// Tell what client we are. 
		WriteLn("www.eit.se/rsb/0.9/client");
		Debug.Log ( ReadLn() );
	}
	
	void Close () 
	{
		// Close and make sure dead.
		tcpclnt.Close();
		tcpclnt = null;
		netStm.Close();
		netStm = null;
		netSr.Close();
		netSr = null;
		netSw.Close();
		netSw = null;
		
		
	}
	
	void Update ()
	{
		// Get the reply
		string replyString = ReadLn();
		
		// Only procced if there was a reply.
		if (replyString!=null)
		{
			// Create new word reader
			Core_WordReader wr = new Core_WordReader (replyString);
			string command = wr.readWord();
			if (command == "qp")
			{
				network_Promt.Parse(wr.readLine());
			}
			else if (command == "playerPreference")
			{
				// DO NOTHING. TODO: Implement.
			}
			else
			{
				Debug.LogError("Unknown command: "+command);
			}
		}
	}
	
	void Start ()
	{
		network_Promt.network_Networking=this;
		Connect ("192.168.42.33", 8080);
	}
}
