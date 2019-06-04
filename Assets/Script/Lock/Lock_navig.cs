using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock_navig : MonoBehaviour
{
	public Button Go;
	public GameObject Layout;
	private Toggle[] temp_liste;
	private bool isClicked;
	private ChoiceController model;

    void Start()
    {
		model = model = FindObjectOfType<ChoiceController>();
		temp_liste = Layout.GetComponentsInChildren<Toggle>();
		isClicked = false;

    }

    void Update()
    {
		temp_liste = Layout.GetComponentsInChildren<Toggle>();


		foreach(Objets element in model.Objets)
		{
			isClicked = element.Modification;
			if(isClicked)
			{
				break;
			}
		}

		model.Choix_objet = isClicked;
		Go.interactable = model.Choix_objet;
    }
}
