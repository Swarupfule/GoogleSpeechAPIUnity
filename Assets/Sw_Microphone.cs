using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sw_Microphone : MonoBehaviour
{


	public string api_key;
	public string code;

	public AudioSource source;
	public Text text;
	public Text response;
	public Text logs;

	Sw_STT stt = new Sw_STT();
	private string resp;
	
	void Start()
    {

		StartCoroutine(Recorder());
		
	

	}

   IEnumerator Recorder()
	{

		for (int i=0;i<1;i++)
		{
			Debug.Log(Time.time);
			logs.text = Time.time.ToString();
			AudioClip ac = Microphone.Start(null, false, 4, 16000);
			
			if (Microphone.IsRecording(null))
			{
				Debug.Log("channel " + ac.channels);
				logs.text += "\n" + "channels" + ac.channels;
				logs.text += "\n" + "freq" + ac.frequency;
		
				Debug.Log("recording");
				text.text = "recording";
				response.text = "wait";
			}
			yield return new WaitForSeconds(5); //make it 10
			Microphone.End(null);
			source.clip = ac;
			yield return new WaitForSeconds(1);
			source.Play();
			if (source.isPlaying)
			{
				text.text = "playing";
				try
				{
					resp = stt.GetTranscript(api_key, ac, code);
					Debug.Log(resp);
					if (resp != null)
					{
						response.text = resp;
					}
					else
					{
						response.text = "i dont understand,sorry";
					}

				}catch(Exception e)
				{
					logs.text = e.Message;
				}
			}
			yield return new WaitForSeconds(5); //make it 10
			Debug.Log("play done");
			
			source.clip = null;
			Debug.Log(Time.time);

		}

		Application.Quit();
	}
}
