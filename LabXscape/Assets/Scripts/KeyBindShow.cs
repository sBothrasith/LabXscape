using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindShow : MonoBehaviour
{
    public GameObject keyBindObj;

	private void Start()
	{
		keyBindObj.SetActive(false);
	}
	public void ButtonClicked()
    {
        if(!keyBindObj.activeInHierarchy)
        {
			keyBindObj.SetActive(true);
        }
        else
        {
			keyBindObj.SetActive(false);
        }
    }
}
