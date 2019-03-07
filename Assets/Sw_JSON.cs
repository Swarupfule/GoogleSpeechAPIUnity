using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class config
{

	public string encoding;
	public int sampleRateHertz;
	public string languageCode;

	public config(string en,int hz,string lc)
	{
		encoding = en;
		sampleRateHertz = hz;
		languageCode = lc;
		//
		// add model="default" or "command" here
		//
	}

}

[System.Serializable]
class audio
{
	public string content;
	public audio(string c)
	{
		content = c;
	}
}

[System.Serializable]
class Request
{
	public config config;
	public audio audio;

	public Request(config c,audio aud)
	{
		config = c;
		audio = aud;
	}
}
public class Sw_JSON
{
	config conf = new config("LINEAR16",16000,"en-US");
	
	

	public string Get_Json(string content,string lang)
	{
		audio aud = new audio(content);
		conf.languageCode = lang;
		Request rc = new Request(conf, aud);
		return JsonUtility.ToJson(rc);

	}
}
