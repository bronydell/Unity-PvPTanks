using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretManager : MonoBehaviour
{

    public List<string> Secrets;
    public List<Sprite> ImageList;
    public float EffectTime = 3.0f;

    public Image SecretImage;
    private string _last30;
	// Use this for initialization
	void Start ()
	{
	    _last30 = "";
	    SecretImage.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	    if (!string.IsNullOrEmpty(Input.inputString))
	    {
	        if (_last30.Length >= 30)
	        {
	            _last30 = _last30.Remove(0, Input.inputString.Length);
            }
	        _last30 += Input.inputString.ToLower();
	    }
	    for(int i = 0; i < Secrets.Count; i++)
	    {
	        if (_last30.EndsWith(Secrets[i]))
	        {
	            SecretImage.sprite = ImageList[i];
	            SecretImage.enabled = true;
	            _last30 = "";
                StartCoroutine(Reset());
	        }
        }
	}

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(EffectTime);
        SecretImage.enabled = false;

    }
}
