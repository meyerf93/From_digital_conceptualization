using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Load_sousMateriaux : MonoBehaviour
{
	public GameObject prefab_object;
	public GameObject materiaux;
	public GameObject justification;
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

					foreach (Item element in model.Categorie)
					{
						List<GameObject> temp_list_toggle = new List<GameObject>();

						GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
						objet.name = element.Nom;

						Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
						temp_toggle.name = element.Nom;

						Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
						temp_texte.text = element.Nom;
						temp_toggle.group = this.GetComponent<ToggleGroup>();

						Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
						Image temp_selected = (Image)temp_toggle.graphic;

						temp_background.sprite = element.Image_unselected;
						temp_selected.sprite = element.Image_selected;

						StateSousMateriaux temp_sous_materiaux = obj.StateSousMateriauxes.Find(r => r.ID == element.ID);
						if (temp_sous_materiaux != null)
						{
							if (temp_sous_materiaux.Modification)
							{
								temp_toggle.isOn = true;
								temp_background.color = Color.white;
							}
							else if (temp_sous_materiaux.Selected)
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

						objet.AddComponent<CheckStateSousMateriaux>();
						CheckStateSousMateriaux temp_check = objet.GetComponent<CheckStateSousMateriaux>();

						InitiateToggleMateriaux(temp_toggle, temp_list_toggle);

						temp_toggle.onValueChanged.AddListener(delegate
						{
							ToggleSousMateriauxValueChanged(temp_toggle);
						});

						temp_toggle.onValueChanged.AddListener(delegate
						{
							DisableMateriaux(temp_toggle, temp_list_toggle);

							StateMateriaux temp_state_materiaux = obj.StateMateriauxes.Find(r => (r.Sous_materiaux == temp_sous_materiaux.ID && r.Modification == true));

							int temp_id = 0;
							if (temp_state_materiaux != null)
							{
								temp_id = temp_state_materiaux.ID;
							}

							Materiaux temp_mat = model.Materiaux.Find(r => r.ID == temp_id);

							Toggle[] temp_toggles_with_id = materiaux.GetComponentsInChildren<Toggle>();
							bool toggle_actif = false;

							foreach (Toggle temp_materiaux_toggle in temp_toggles_with_id)
							{
								if (temp_mat != null)
								{
									toggle_actif = true;
									if (temp_materiaux_toggle.name == temp_mat.Nom)
									{
										InitiateJustification(temp_materiaux_toggle, temp_list_justification);
									}
								}
							}
							if (!toggle_actif)
							{
								DisableJustification(false, temp_list_justification);
							}
						});

						temp_toggle.onValueChanged.AddListener(delegate
						{
							temp_check.CheckSate();
						});

					}
				}
			}
		}
	}

	void InitiateJustification(Toggle toggle, List<GameObject> list)
	{
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

	void DisableJustification(bool enable, List<GameObject> temp_list)
	{
		foreach (GameObject temp in temp_list)
		{
			temp.SetActive(enable);
		}
	}

	void InitiateToggleMateriaux(Toggle change, List<GameObject> temp_list)
	{
		Objets objets = model.Objets.Find(r => r.Modification == true);
		Item Sous_materiaux = model.Categorie.Find(r => r.Nom == change.name);

		Toggle[] toggles_with_id = materiaux.GetComponentsInChildren<Toggle>();
		foreach (Materiaux element in model.Materiaux)
		{
			if (element.SousCategorie == Sous_materiaux.ID)
			{
				foreach (Toggle toggle in toggles_with_id)
				{
					if (toggle.name == element.Nom)
					{
						toggle.gameObject.SetActive(change.isOn);
						temp_list.Add(toggle.gameObject);
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

	void ToggleSousMateriauxValueChanged(Toggle change)
	{
		Objets temp = model.Objets.Find(r => r.Modification == true);
		Item item = model.Categorie.Find(r => r.Nom == change.name);

		StateSousMateriaux temp_sous_materiaux = temp.StateSousMateriauxes.Find(r => r.ID == item.ID);

		if (change.isOn)
		{
			temp_sous_materiaux.Modification = true;
		}
		else
		{
			temp_sous_materiaux.Modification = false;
		}
	}
}
