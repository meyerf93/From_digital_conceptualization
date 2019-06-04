using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_outils : MonoBehaviour
{
	public GameObject prefab_object;
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
                    foreach (Item element in model.Outils)
                    {

                        GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
                        objet.name = element.Nom;

                        Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
                        temp_toggle.name = element.Nom;
                        if (obj.OutilsActif.Contains(element.ID))
                        {
                            temp_toggle.isOn = true;
                        }
						else if (obj.OutilsInactif.Contains(element.ID))
                        {
                            temp_toggle.isOn = false;
                        }
                        Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
                        temp_texte.text = element.Nom;

						foreach (int temp_outilsActive in obj.OutilsActif)
                        {
							if (temp_outilsActive == element.ID)
                            {
                                temp_toggle.isOn = true;
                                break;
                            }
                            else
                            {
								temp_toggle.isOn = false;
                            }
                        }

                        Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
                        Image temp_selected = (Image)temp_toggle.graphic;

						temp_background.sprite = element.Image_unselected;
						temp_selected.sprite = element.Image_selected;
                        temp_selected.color = Color.grey;

                        temp_toggle.onValueChanged.AddListener(delegate {
                            ToggleValueChanged(temp_toggle);
                        });
                    }
                }
            }


        }

    }


    void ToggleValueChanged(Toggle change)
    {
        Objets temp = model.Objets.Find(r => r.Modification == true);
		Item item = model.Outils.Find(r => r.Nom == change.name);
        
        if (change.isOn)
        {
			if (temp.OutilsInactif.Contains(item.ID))
            {
				temp.OutilsInactif.Remove(item.ID);
            }
			if (!temp.OutilsActif.Contains(item.ID))
            {
				temp.OutilsActif.Add(item.ID);
            }
        }
        else
        {
			if (temp.OutilsActif.Contains(item.ID))
            {
				temp.OutilsActif.Remove(item.ID);
            }
			if (!temp.OutilsInactif.Contains(item.ID))
            {
				temp.OutilsInactif.Add(item.ID);
            }
        }
    }
}
