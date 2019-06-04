using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_justification : MonoBehaviour
{
	public GameObject prefab_object;
    private ChoiceController model;
	public GameObject materiaux;
    

    void Awake()
    {
        model = FindObjectOfType<ChoiceController>();

		if (model != null)
        {
            foreach (Objets obj in model.Objets)
            {
                if (obj.Modification == true)
                {
					foreach (Item justification in model.JustificationMateriel)
                    {

                        GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
                        objet.name = justification.Nom;

						Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
                        temp_toggle.name = justification.Nom;
                                                
                        Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
                        temp_texte.text = justification.Nom;
                       
                        Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
                        Image temp_selected = (Image)temp_toggle.graphic;

						temp_background.sprite = justification.Image_unselected; 
						temp_selected.sprite = justification.Image_selected;

						StateJustification temp_justification = obj.StateJustifications.Find(r => r.ID == justification.ID);

						if(temp_justification != null)
						{
							if(temp_justification.Modification)
							{
								temp_toggle.isOn = true;
								temp_background.color = Color.white;
							}
							else if (temp_justification.Selected)
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

						objet.AddComponent<CheckStateJustification>();
						CheckStateJustification temp_check = objet.GetComponent<CheckStateJustification>();

                        temp_toggle.onValueChanged.AddListener(delegate {
                            ToggleValueChanged(temp_toggle);
                        });
						temp_toggle.onValueChanged.AddListener(delegate
                        {
							temp_check.CheckState();
                        });
                    }
                }
            }
        }
    }

	void ToggleValueChanged(Toggle change)
    {
        Objets temp = model.Objets.Find(r => r.Modification == true);
		Item item = model.JustificationMateriel.Find(r => r.Nom == change.name);
		Toggle[] temp_list = materiaux.GetComponentsInChildren<Toggle>();

		string temp_name = "";
		foreach(Toggle element in temp_list)
		{
			if(element.isOn)
			{
				temp_name = element.gameObject.name;
			}
		}   

		Materiaux materieux = model.Materiaux.Find(r => r.Nom == temp_name);
		StateMateriaux temp_materiaux = temp.StateMateriauxes.Find(r => (r.ID == materieux.ID));
		StateJustification temp_justification = temp.StateJustifications.Find(r => (r.Justification == item.ID && r.Materiaux == temp_materiaux.ID));

		if (change.isOn)
        {
            temp_justification.Modification = true;
            temp_justification.Selected = true;
        }
        else
        {
            temp_justification.Modification = false;
            temp_justification.Selected = false;
        } 
	}   
}
