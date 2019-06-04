using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_forme : MonoBehaviour
{
	public GameObject prefab_object;
    private ChoiceController model;

    void Start()
    {
        model = FindObjectOfType<ChoiceController>();
        if (model != null)
        {
			foreach(Objets obj in model.Objets){
				
				if (obj.Modification == true){
					
					foreach (Item element in model.Forme)
                    {
						
                        GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
                        objet.name = element.Nom;

                        Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
                        temp_toggle.name = element.Nom;

						if(obj.FormeActive.Contains(element.ID)){
							temp_toggle.isOn = true;
						}
						else if (obj.FormeInactive.Contains(element.ID)){
							temp_toggle.isOn = false;
						}

						Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
						temp_texte.text = element.Nom;
                        
						foreach(int temp_formeActive in obj.FormeActive){
							
							if(temp_formeActive == element.ID){
								temp_toggle.isOn = true;
								break;
							}
							else{
								//Debug.Log("toggle is off");
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
		Item item = model.Forme.Find(r => r.Nom == change.name);

		if (change.isOn)
		{
			if (temp.FormeInactive.Contains(item.ID))
			{
				temp.FormeInactive.Remove(item.ID);
			}
			if (!temp.FormeActive.Contains(item.ID)){
				temp.FormeActive.Add(item.ID);
			}
		}
		else{
			if(temp.FormeActive.Contains(item.ID)){
				temp.FormeActive.Remove(item.ID);
			}
			if(!temp.FormeInactive.Contains(item.ID)){
				temp.FormeInactive.Add(item.ID);
			}
		}
    }
}
