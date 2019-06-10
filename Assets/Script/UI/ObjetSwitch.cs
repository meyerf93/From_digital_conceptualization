using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ce script permet d'aller récuper l'image qui correspond aà l'objet sélectionner 
//dans le modèlre de donnée. Il vas ensuite changer ce sprite dans la scène
//Contexte_objet et Forme_objet. Il est lier aux gameobjets qui contiennent
//les images de l'objet au centre de la scène
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
