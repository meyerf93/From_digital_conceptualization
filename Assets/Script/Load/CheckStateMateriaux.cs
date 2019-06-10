using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckStateMateriaux : MonoBehaviour
{
	private ChoiceController model;
	private Objets objets;
	private Materiaux item;
	private CheckStateSousMateriaux checkState;
	private Toggle toggle;
	private StateMateriaux materiaux;
	private List<StateJustification> justifications;
	private Item SousMateriaux;

	void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		if (model != null)
		{
			objets = model.Objets.Find(r => r.Modification == true);
			item = model.Materiaux.Find(r => r.Nom == this.name);
			materiaux = objets.StateMateriauxes.Find(r => r.ID == item.ID);
			justifications = objets.StateJustifications.FindAll(r => r.Materiaux == materiaux.Materiaux);
			SousMateriaux = model.Categorie.Find(r => r.ID == materiaux.Sous_materiaux);

			GameObject choice = GameObject.FindWithTag("Choice");
			Toggle[] temp_list = choice.GetComponentsInChildren<Toggle>();

			foreach (Toggle element in temp_list)
			{
				if (element.name == SousMateriaux.Nom)
				{
					toggle = element;
				}
			}
			checkState = toggle.GetComponent<CheckStateSousMateriaux>();
		}
	}

	public void CheckState()
	{
		checkState.CheckSate();

		justifications = objets.StateJustifications.FindAll(r => r.Materiaux == materiaux.Materiaux);
		materiaux.Selected = false;
		foreach (StateJustification element in justifications)
		{
			if (element.Selected)
			{
				materiaux.Selected = true;
			}
		}

		Toggle temp_toggle = this.GetComponent<Toggle>();

		Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
		Image temp_selected = (Image)temp_toggle.graphic;

		if (materiaux != null)
		{
			if (materiaux.Modification)
			{
				temp_background.color = Color.white;
			}
			else if (materiaux.Selected)
			{
				temp_background.color = Color.black;

			}
			else
			{
				temp_selected.color = Color.grey;
			}
		}
	}
}
