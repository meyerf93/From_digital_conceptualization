using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_choice : MonoBehaviour
{
	public GameObject prefab_object;
	private bool load;
	private ChoiceController model;

	void Start()
	{
		load = false;
		model = FindObjectOfType<ChoiceController>();
		if (model != null && !load)
		{
			load = true;
			foreach (Objets element in model.Objets)
			{
				Create_toggle(element);
			}
		}

	}
	void Update()
	{
		if (model != null && !load)
		{
			load = true;
			foreach (Objets element in model.Objets)
			{
				Create_toggle(element);
			}
		}
	}

	void Create_toggle(Objets element)
	{
		GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
		objet.name = element.Nom;

		Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
		temp_toggle.name = element.Nom;
		temp_toggle.group = this.GetComponent<ToggleGroup>();
		temp_toggle.isOn = model.Objets.Find(r => r.Nom == objet.name).Modification;

		Image temp_background = temp_toggle.transform.Find("Background").GetComponent<Image>();
		Image temp_overlay = temp_toggle.transform.Find("Overlay").GetComponent<Image>();
		Image temp_selected = (Image)temp_toggle.graphic;

		temp_background.sprite = element.Image_unselected;
		temp_selected.sprite = element.Image_selected;
		temp_overlay.sprite = element.Lock.image;

		Text Temp_text = temp_toggle.GetComponentInChildren<Text>();
		Temp_text.text = "Etoiles : " + element.min_etoiles;

		if (element.Lock.isLocked)
		{
			Color temp_color = temp_overlay.color;
			temp_color.a = 0.5f;
			temp_overlay.color = temp_color;
			Color temp_text_color = Temp_text.color;
			temp_text_color.a = 0.5f;
			Temp_text.color = temp_text_color;
		}
		else
		{
			Color temp_color = temp_overlay.color;
			temp_color.a = 0f;
			temp_overlay.color = temp_color;
			Color temp_text_color = Temp_text.color;
			temp_text_color.a = 0f;
			Temp_text.color = temp_text_color;

		}


		temp_toggle.onValueChanged.AddListener(delegate
		{
			if (!element.Lock.isLocked)
			{
				ToggleValueChanged(temp_toggle);
			}
			else
			{
				TryToUnlock(element, temp_toggle);
			}
		});

	}

	void TryToUnlock(Objets obj, Toggle temp_toggle)
	{
		if (model.Profils.Etoiles_actuel >= obj.min_etoiles)
		{
			model.Profils.Etoiles_depenser += obj.min_etoiles;
			model.Profils.Etoiles_actuel -= obj.min_etoiles;
			obj.Lock.isLocked = false;
		}

		Image temp_background = temp_toggle.transform.Find("Background").GetComponent<Image>();
		Image temp_overlay = temp_toggle.transform.Find("Overlay").GetComponent<Image>();
		Image temp_selected = (Image)temp_toggle.graphic;

		Text Temp_text = temp_toggle.GetComponentInChildren<Text>();

		Objets temp = model.Objets.Find(r => r.Nom == temp_toggle.name);

		if (obj.Lock.isLocked)
		{
			Color temp_color = temp_overlay.color;
			temp_color.a = 0.5f;
			temp_overlay.color = temp_color;
			Color temp_text_color = Temp_text.color;
			temp_text_color.a = 0.5f;
			Temp_text.color = temp_text_color;
		}
		else
		{
			Color temp_color = temp_overlay.color;
			temp_color.a = 0f;
			temp_overlay.color = temp_color;
			Color temp_text_color = Temp_text.color;
			temp_text_color.a = 0f;
			Temp_text.color = temp_text_color;
			temp.Modification = true;
		}

	}

	void ToggleValueChanged(Toggle change)
	{
		//Debug.Log("game object name : " + change.name);
		Objets temp = model.Objets.Find(r => r.Nom == change.name);
		temp.Modification = change.isOn;
	}
}
