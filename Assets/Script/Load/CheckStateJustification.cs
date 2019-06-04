using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckStateJustification : MonoBehaviour
{   
    private ChoiceController model;
    private Objets objets;
	private CheckStateMateriaux checkState;
	private Toggle toggle;
	private StateMateriaux stateMateriaux;
	private StateJustification stateJustification;
	private Item justification;
	private Materiaux materiaux;

	void Start()
    {
        model = FindObjectOfType<ChoiceController>();
        if (model != null)
        {
            objets = model.Objets.Find(r => r.Modification == true);
			stateMateriaux = objets.StateMateriauxes.Find(r => r.Modification == true);
			justification = model.JustificationMateriel.Find(r => r.Nom == this.gameObject.name);
			stateJustification = objets.StateJustifications.Find(r => r.Justification == justification.ID);
			materiaux = model.Materiaux.Find(r => r.ID == stateMateriaux.Materiaux);

			GameObject choice = GameObject.FindWithTag("Choice");
			Toggle[] temp_list = choice.GetComponentsInChildren<Toggle>();
			foreach (Toggle element in temp_list)
			{
				if(element.name == materiaux.Nom)
				{
					toggle = element;
				}
			}
			checkState = toggle.GetComponent<CheckStateMateriaux>();
        }
    }

	public void CheckState()
	{
		checkState.CheckState();

		Toggle temp_toggle = this.GetComponent<Toggle>();
        
        Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
        Image temp_selected = (Image)temp_toggle.graphic;

		if (stateJustification != null)
        {
			if (stateJustification.Modification)
            {
                temp_background.color = Color.white;
            }
			else if (stateJustification.Selected)
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
