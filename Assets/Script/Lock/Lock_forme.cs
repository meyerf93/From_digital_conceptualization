using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Lock_forme : MonoBehaviour
{
	public Button Forme;
	private ChoiceController model;
	// Start is called before the first frame update
	void Start()
	{
		model = FindObjectOfType<ChoiceController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (model != null)
		{
			bool isFull = false;

			Objets temp_objet = model.Objets.Find(r => r.Modification == true);
			foreach (Association_question element in temp_objet.Association_Questions)
			{
				isFull = !(element.Reponse.CompareTo("") == 0);
				if (!isFull)
				{
					break;
				}
			}

			model.Contexte_objet = isFull;
			Forme.interactable = model.Contexte_objet && model.Choix_objet;
		}

	}
}
