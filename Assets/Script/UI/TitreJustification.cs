using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitreJustification : MonoBehaviour
{
	private Text text_pourquoi;
    private Toggle[] temp_child;

    void Start()
    {
        text_pourquoi = this.GetComponent<Text>();

		temp_child = this.GetComponentsInChildren<Toggle>();
    }

    void Update()
    {
		temp_child = this.GetComponentsInChildren<Toggle>();

		bool isEnanble = false;

		foreach(Toggle element in temp_child)
		{
			if(element.enabled == true)
			{
				isEnanble = true;
			}
		}

		text_pourquoi.enabled = isEnanble;
    }
}
