using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ce script permet de changer le titre de la scène en haut à gauche en fonction
//de l'objet qui à été sélectionner. Il est actif dans les scène Contexte_objet
//Forme_objet
public class SwitchName : MonoBehaviour
{
	private ChoiceController model;

	void Start()
	{
		model = FindObjectOfType<ChoiceController>();

		Text temp = this.GetComponent<Text>();
		foreach (Objets element in model.Objets)
		{
			if (element.Modification)
			{
				temp.text = element.Nom;
			}
		}
	}
}
