using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckStateSousMateriaux : MonoBehaviour
{
	private ChoiceController model;
	private Objets objets;
	private Item item;
	private StateSousMateriaux sous_materiaux;
	private List<StateMateriaux> materiauxes;
	private CheckStateJustification checkState;

    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		if (model != null)
		{
			objets = model.Objets.Find(r => r.Modification == true);
			item = model.Categorie.Find(r => r.Nom == this.name);
			sous_materiaux = objets.StateSousMateriauxes.Find(r => r.ID == item.ID);
			materiauxes = objets.StateMateriauxes.FindAll(r => r.Sous_materiaux == sous_materiaux.SousMateriaux);         
		}
    }

	public void CheckSate()
	{		          
		sous_materiaux.Selected = false;
		foreach(StateMateriaux element in materiauxes)
		{
			if(element.Selected)
			{
				sous_materiaux.Selected = true;
			}
		}

		Toggle temp_toggle = this.GetComponent<Toggle>();


        Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
        Image temp_selected = (Image)temp_toggle.graphic;
        

		if (sous_materiaux != null)
        {
			if (sous_materiaux.Modification)
            {
                temp_background.color = Color.white;
            }
			else if (sous_materiaux.Selected)
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
