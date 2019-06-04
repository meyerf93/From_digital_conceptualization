using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_taille : MonoBehaviour
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
					foreach (Taille element in model.TailleObjet)
                    {

                        GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
                        objet.name = element.Nom;

                        Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
                        temp_toggle.name = element.Nom;

						if (obj.TailleActive.Contains(element.ID))
                        {
                            temp_toggle.isOn = true;
                        }
						else if (obj.TailleInactive.Contains(element.ID))
                        {
                            temp_toggle.isOn = false;
                        }

                        Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
                        temp_texte.text = element.Nom;
                        
						foreach (int temp_tailleActive in obj.TailleActive)
                        {
							if (temp_tailleActive == element.ID)
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

                        temp_toggle.onValueChanged.AddListener(delegate 
						{
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
		Taille item = model.TailleObjet.Find(r => r.Nom == change.name);

        if (change.isOn)
        {
			if (temp.TailleInactive.Contains(item.ID))
            {
				temp.TailleInactive.Remove(item.ID);
            }
			if (!temp.TailleActive.Contains(item.ID))
            {
				temp.TailleActive.Add(item.ID);
            }
        }
        else
        {
			if (temp.TailleActive.Contains(item.ID))
            {
				temp.TailleActive.Remove(item.ID);
            }
			if (!temp.TailleInactive.Contains(item.ID))
            {
				temp.TailleInactive.Add(item.ID);
            }
        }
    }
}
