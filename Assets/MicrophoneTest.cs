using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

		/*foreach(string dev in Microphone.devices)
		{
			Debug.Log(dev);
		}*/
		StartCoroutine(cor());

		Debug.Log("cor finih");


		

    }

	IEnumerator cor()
	{

		string fp;
		for (int i = 0; i < 10; i++)
		{
			AudioClip clip = Microphone.Start(Microphone.devices[0], false, 5, 16000);
			yield return new WaitForSeconds(6);

			string path = Application.persistentDataPath;

			Debug.Log(Microphone.devices[0]);
			fp = path + "/Audio" + i.ToString() + ".wav";
			SavWav.Save(fp, clip);
			Debug.Log(fp);

		}
		
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
