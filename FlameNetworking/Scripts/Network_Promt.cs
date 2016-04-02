using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;
using System;

public class Network_Promt : MonoBehaviour, IQuarrier {

	private Thread networkThread;

	//[SerializeField] private Text debugQuerry;
	
	//[SerializeField] private Gui_QuarryManager guiPromt;
	
	[SerializeField] private Quarrier quarry = null;
	
	//private string textData;
	[HideInInspector] public Network_Networking network_Networking; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//debugQuerry.text = textData;
	}
	
	private void HardCodeLogin()
	{
		// Hard coded
		network_Networking.WriteLn("\"Login\"");
		Debug.Log(network_Networking.ReadLn());
		network_Networking.WriteLn("\"a\"");
		Debug.Log(network_Networking.ReadLn());
		network_Networking.WriteLn("\"a\"");
		
		Debug.Log(network_Networking.ReadLn());
		network_Networking.WriteLn("\"OK\"");
		Debug.Log(network_Networking.ReadLn());
	}
	
	public void Parse (string stringToParse)
	{
		networkThread = new Thread(() => ParseCommand (stringToParse));
		networkThread.Start();

	}
	
	private void ParseCommand (string stringToParse)
	{
		Core_WordReader wr = new Core_WordReader (stringToParse);
		print ("String to parse: "+stringToParse);
		string cmd = wr.readWord();
		
		// TODO: Should be a case.
		if (cmd == "buttonPrompt")
		{
			Debug.Log("Button prompt!");
			string type = wr.readWord();
			
			// Pre coded responses.
			if (type=="login_or_reg")
			{
				
				// LOGIN!
				network_Networking.WriteLn("\"Login\"");
				
			}
			else
			{
				string titel = wr.readString ();
				//guiPromt.DoQuerry(titel);
				
				int nQuerrys = wr.readInt();
				
				QuarryItem [] options = new QuarryItem[nQuerrys];
				Debug.Log ("To add: "+nQuerrys);
				for (int i = 0; i < nQuerrys; i++)
				{
					Debug.Log ("Add to: " + (i));
					options [i] = new QuarryItem (wr.readString(), i.ToString(), QuarryItem.Type.BUTTON);
				
				}
				
				Debug.Log ("Quarry!!!!!!!");
				
				quarry.Close();
				quarry.Create (this ,titel, type, options);
				//guiPromt.AddOption("test", "TEST");
			}
		}
		else if (cmd == "promptString")
			{
				string type = wr.readWord();
				if (type == "game_support")
				{
					network_Networking.WriteLn("\"TankerStrike\"");
				}
				if (type == "enter_player_name")
				{
					network_Networking.WriteLn("\"a\"");
				}
				if (type == "enter_player_pw")
				{
					network_Networking.WriteLn("\"a\"");
				}
				else
				{
					string titel = wr.readString();
					QuarryItem [] textInput = new QuarryItem[2];
					textInput [0] = new QuarryItem ("", "input", QuarryItem.Type.STRING);
					textInput [1] = new QuarryItem ("OK", "read", QuarryItem.Type.BUTTON);
					quarry.Close();
					quarry.Create (this ,titel, type, textInput);
				}
			}
		else
		{
			Debug.LogError ("Unsupported command: "+cmd);
		}
		return;
	}

    public void OnQuarrier(string quarrySlug, string value)
    {
		if (value == "read")
		{
			string v = quarry.GetTextByReturn("input");
			Debug.Log("Text is and was: "+v);
		}
		else
		{
			network_Networking.WriteLn(value);
		}
        
    }
}
