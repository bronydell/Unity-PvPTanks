using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject TuturialGameObject;

	// Use this for initialization
	void Start ()
	{
	    TuturialGameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Submit"))
            if(TuturialGameObject.active)
            {
                SceneManager.LoadScene(1 , LoadSceneMode.Single);
            }
            else
                TuturialGameObject.SetActive(true);
	}
}
