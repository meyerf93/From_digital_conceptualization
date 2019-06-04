using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_materiaux : MonoBehaviour
{
	public GameObject prefab_object;
	public GameObject justification;
	public GameObject sousmateriaux;

    private ChoiceController model;

    void Start()
    {
        model = FindObjectOfType<ChoiceController>();

        if (model != null)
        {
            foreach (Objets obj in model.Objets)
            {
                if (obj.Modification == true)
				{
					List<GameObject> temp_list_justification = new List<GameObject>();
					Toggle[] toggles_with_id = justification.GetComponentsInChildren<Toggle>();
                    foreach (Toggle temp_toggle_with_id in toggles_with_id)
                    {
                        temp_list_justification.Add(temp_toggle_with_id.gameObject);
                    }

					foreach (Materiaux sous_element in model.Materiaux)
                    {
                        GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
                        objet.name = sous_element.Nom;

						Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
                        temp_toggle.name = sous_element.Nom;
						temp_toggle.group = this.GetComponent<ToggleGroup>();

                        Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
                        temp_texte.text = sous_element.Nom;
                        
                        Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
                        Image temp_selected = (Image)temp_toggle.graphic;

						temp_background.sprite = sous_element.Image_unselected;
						temp_selected.sprite = sous_element.Image_selected;

						StateMateriaux temp_mat = obj.StateMateriauxes.Find(r => r.ID == sous_element.ID);
                        if (temp_mat != null)
                        {
                            if (temp_mat.Modification)
                            {
                                temp_toggle.isOn = true;
								temp_background.color = Color.white;
                            }
                            else if (temp_mat.Selected)
                            {
                                temp_toggle.isOn = false;
								temp_background.color = Color.black;

                            }
                            else
                            {
                                temp_toggle.isOn = false;
								temp_selected.color = Color.grey;
                            }
                        }

						InitiateJustification(temp_toggle, temp_list_justification);
 
						objet.AddComponent<CheckStateMateriaux>();
						CheckStateMateriaux check = objet.GetComponent<CheckStateMateriaux>();


						temp_toggle.onValueChanged.AddListener(delegate 
						{
                            ToggleValueChanged(temp_toggle);
                        });
						temp_toggle.onValueChanged.AddListener(delegate
                        {
							InitiateJustification(temp_toggle, temp_list_justification);
							DisableMateriaux(temp_toggle, temp_list_justification);
                        });
						temp_toggle.onValueChanged.AddListener(delegate
						{
							check.CheckState();
						});
                    }
                }
            }       
        }

    }

	void InitiateJustification(Toggle toggle, List<GameObject> list){

		Objets objets = model.Objets.Find(r => r.Modification == true);
		Materiaux Materiaux = model.Materiaux.Find(r => r.Nom == toggle.name);

		List<StateJustification> temp_list = objets.StateJustifications.FindAll(r => (r.Materiaux == Materiaux.ID));
		foreach (StateJustification element in temp_list)
        {
			if (element.Materiaux == Materiaux.ID)
            {
				foreach (GameObject temp_toggle in list)
                {
					Item temp_just = model.JustificationMateriel.Find(r => r.ID == element.Justification);

					if (temp_toggle.name == temp_just.Nom)
                    {
						Toggle temp_value = temp_toggle.GetComponent<Toggle>();
                        temp_value.isOn = element.Modification;
						temp_toggle.SetActive(toggle.isOn);
                    }
                }
			}
        }
	}

	void DisableMateriaux(Toggle change, List<GameObject> temp_list)
	{
		foreach (GameObject temp in temp_list)
		{
			temp.SetActive(change.isOn);
		}
	}

	void ToggleValueChanged(Toggle change)
	{
		Objets temp = model.Objets.Find(r => r.Modification == true);
		Materiaux item = model.Materiaux.Find(r => r.Nom == change.name);
		StateMateriaux temp_mat = temp.StateMateriauxes.Find(r => r.ID == item.ID);

		if (change.isOn)
		{
			temp_mat.Modification = true;
		}
		else
		{
			temp_mat.Modification = false;
		}
        
	}   
}
