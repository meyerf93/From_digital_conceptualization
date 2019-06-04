using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetSwitch : MonoBehaviour
{
	private ChoiceController model;

    void Start()
    {
		model = FindObjectOfType<ChoiceController>();

		Image temp = this.GetComponent<Image>();
        foreach (Objets element in model.Objets)
        {
            if (element.Modification)
            {
				temp.sprite = element.Image_unselected;
            }
        }
        
    }
}
