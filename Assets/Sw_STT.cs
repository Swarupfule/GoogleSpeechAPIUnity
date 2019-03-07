using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class Sw_STT
{

	string op;
   public string GetTranscript(string api_key,AudioClip ac,string code)
	{



		Debug.Log("GetTranscipt called");
		JObject jst = new JObject();


		string filename = "Thiseis.wav";

		string p = Application.persistentDataPath + filename;

		SavWav.Save(p, ac);
		byte[] conv = File.ReadAllBytes(p);
		string base64 = Convert.ToBase64String(conv);

		string url = "https://speech.googleapis.com/v1/speech:recognize?key=" + api_key;


		var req = (HttpWebRequest)WebRequest.Create(url);
		req.ContentType = "application/json";
		req.Method = "POST";

		var StreamW = new StreamWriter(req.GetRequestStream());




		Sw_JSON js = new Sw_JSON();
		string mystr = base64;
		string str = js.Get_Json(mystr, code);




		Debug.Log(str);
		File.WriteAllText("C:/Users/Swarup/Desktop/MYFILE.txt", str);


		try
		{
			StreamW.WriteLine(str);
			StreamW.Flush();
			StreamW.Close();


			var httpres = (HttpWebResponse)req.GetResponse();
			var reader = new StreamReader(httpres.GetResponseStream());
			string result = reader.ReadToEnd();

			jst = JObject.Parse(result);
			//string[] resp = result.Split("{".ToCharArray());
			//Debug.Log(resp[3]);
		     op = jst.SelectToken("results[0]").SelectToken("alternatives[0]").SelectToken("transcript").ToString();
			return op;
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}

		File.Delete(p);
		return op;


	}
}
