using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock_evaluation : MonoBehaviour
{
	public Button Imaginons;
	private ChoiceController model;
	private Objets objets;

	void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		if(model != null)
		{
			objets = model.Objets.Find(r => r.Modification == true);

		}
    }

    void Update()
    {
		if (model != null)
        {
            bool isFull = false;

			if(objets.FormeActive.Count != 0)
			{
				model.Forme_validation[0] = true;
			}
			else
			{
				model.Forme_validation[0] = false;
			}

			if(objets.OutilsActif.Count != 0)
			{
				model.Forme_validation[1] = true;
			}
            else
            {
                model.Forme_validation[1] = false;
            }

			if(objets.TailleActive.Count != 0)
			{
				model.Forme_validation[2] = true;
			}
            else
            {
                model.Forme_validation[2] = false;
            }

			if(objets.TechniqueActive.Count != 0)
			{
                model.Forme_validation[3] = true;
            }
            else
            {
                model.Forme_validation[3] = false;
            }
            
			List<StateSousMateriaux> temp_liste_state = objets.StateSousMateriauxes.FindAll(r => (r.Modification == true && r.Selected == true));

			if(temp_liste_state.Count != 0)
			{
                model.Forme_validation[4] = true;
            }
            else
            {
                model.Forme_validation[4] = false;
            }

			foreach (bool element in model.Forme_validation)
            {
				isFull = element;
				if(!element){
					isFull = false;
					break;
				}
            }
			model.Forme_objet = isFull;
			Imaginons.interactable = model.Forme_objet && model.Contexte_objet;
        }
    }
}
