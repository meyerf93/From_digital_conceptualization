using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StateManager : MonoBehaviour
{
	public GameObject prefab_object;

	public GameObject JusticiationLayout;
	public GameObject MateriauxLayout;
	public GameObject SousMateriauxLayout;

	private ChoiceController model;
    private Objets objets;
	private List<GameObject> justifications;
	private List<GameObject> materiaux;
	private List<GameObject> sousMateriaux;

	public Color modified_color;
	public Color default_color;
	public Color selection_color;
    
    void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		if (model != null)
		{
			objets = model.Objets.Find(r => r.Modification == true);
                        
			justifications = new List<GameObject>();
			materiaux = new List<GameObject>();
			sousMateriaux = new List<GameObject>();

			List<StateSousMateriaux> temp_list_sous_materiaux = objets.StateSousMateriauxes;
			StateSousMateriaux temp_state_sous_materiaux = temp_list_sous_materiaux.Find(r => r.Modification == true);
			Load_sous_materiaux(temp_list_sous_materiaux);

			List<StateMateriaux> temp_list_materiaux = objets.StateMateriauxes;
			StateMateriaux temp_materiaux = temp_list_materiaux.Find(r => (r.Objet == objets.ID && r.Modification == true));

			if (temp_materiaux == null) temp_materiaux = objets.StateMateriauxes.Find(r => (r.Materiaux == 1));

			Load_materiaux(temp_list_materiaux);

			List<StateJustification> temp_list_justification = objets.StateJustifications.FindAll(r => (r.Materiaux == temp_materiaux.Materiaux));
			Load_justification(temp_list_justification,temp_materiaux);

			UpdateView();
		}
	}

	public void UpdateModel(Toggle change)
    {
		Item temp_justification = model.JustificationMateriel.Find(r => r.Nom == change.name);
		Materiaux temp_materiaux = model.Materiaux.Find(r => r.Nom == change.name);
		Item temp_sousMateriaux = model.Categorie.Find(r => r.Nom == change.name);

		if (temp_justification != null)
		{
			StateSousMateriaux temp_state_sous_materiaux = objets.StateSousMateriauxes.Find(r => (r.Modification == true));       
			StateMateriaux temp_state_materiaux = objets.StateMateriauxes.Find(r => (r.Sous_materiaux == temp_state_sous_materiaux.SousMateriaux && r.Modification == true));
			StateJustification temp_state = objets.StateJustifications.Find(r => (r.Materiaux == temp_state_materiaux.Materiaux && r.Justification == temp_justification.ID));
			temp_state.Modification = change.isOn;
		}
		else if (temp_materiaux != null)
		{
			StateMateriaux temp_state = objets.StateMateriauxes.Find(r => (r.Materiaux == temp_materiaux.ID));
			temp_state.Modification = change.isOn;
		}
		else if (temp_sousMateriaux != null)
		{
			StateSousMateriaux temp_state = objets.StateSousMateriauxes.Find(r => (r.SousMateriaux == temp_sousMateriaux.ID));
			temp_state.Modification = change.isOn;
		}
		else
		{
			Debug.Log("error can't found the toggle : " + change.name);
		}

		List<StateSousMateriaux> temp_sous_materiaux_list = objets.StateSousMateriauxes;
		foreach (StateSousMateriaux element_sous_materiaux in temp_sous_materiaux_list)
		{
			bool materiaux_actif = false;
			List<StateMateriaux> temp_materiaux_list = objets.StateMateriauxes.FindAll(r => (r.Sous_materiaux == element_sous_materiaux.SousMateriaux));
			foreach (StateMateriaux element in temp_materiaux_list)
			{
				bool justification_active = false;
				List<StateJustification> temp_justification_list = objets.StateJustifications.FindAll(r => (r.Materiaux == element.Materiaux));
				foreach (StateJustification element_justification in temp_justification_list)
				{
					if (element_justification.Modification) justification_active = true;
				}

				element.Selected = justification_active;

				if (element.Modification || element.Selected) materiaux_actif = true;
			}
			element_sous_materiaux.Selected = materiaux_actif;
		}

		UpdateView();
	}

    void UpdateView()
    {
		List<StateSousMateriaux> temp_sous_materiaux_list = objets.StateSousMateriauxes;
		if(temp_sous_materiaux_list.Count != 0)
		{
			foreach (StateSousMateriaux element in temp_sous_materiaux_list)
            {
                Item item_sous_materiaux = model.Categorie.Find(r => r.ID == element.SousMateriaux);
                GameObject temp_gameobject_sous_materiaux = sousMateriaux.Find(r => r.name == item_sous_materiaux.Nom);
				if(temp_gameobject_sous_materiaux !=null)
				{
					Toggle temp_toggle = temp_gameobject_sous_materiaux.GetComponent<Toggle>();            

					Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
                    Image temp_selected = (Image)temp_toggle.graphic;

                    if (element.Modification)
                    {
						temp_background.color = default_color;
                    }
                    else if (element.Selected)
                    {
						temp_background.color = selection_color;
                    }
                    else
                    {
						temp_background.color = default_color;
						temp_selected.color = modified_color;
                    }
				}
            }
		}


		List<StateMateriaux> temp_materiaux_list = objets.StateMateriauxes;
		foreach (StateMateriaux element in temp_materiaux_list)
        {
			Materiaux item_materiaux = model.Materiaux.Find(r => r.ID == element.Materiaux);
			GameObject temp_gameobject_materiaux = materiaux.Find(r => r.name == item_materiaux.Nom);
			if(temp_gameobject_materiaux != null)
			{
				Toggle temp_toggle = temp_gameobject_materiaux.GetComponent<Toggle>();
            
                Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
                Image temp_selected = (Image)temp_toggle.graphic;

				if (element.Modification)
                {
                    temp_background.color = default_color;
                }
                else if (element.Selected)
                {
                    temp_background.color = selection_color;
                }
                else
                {
                    temp_background.color = default_color;
                    temp_selected.color = modified_color;
                }
			}

        }

		StateSousMateriaux temp_state_sous_materiaux = objets.StateSousMateriauxes.Find(r => (r.Modification == true));
		if(temp_state_sous_materiaux != null)
		{
			StateMateriaux temp_state_materiaux = objets.StateMateriauxes.Find(r => (r.Sous_materiaux == temp_state_sous_materiaux.SousMateriaux && r.Modification == true));
            if (temp_state_materiaux != null)
            {
                foreach (GameObject element in justifications)
                {
                    Destroy(element);
                }

				List<StateJustification> temp_justification_list = objets.StateJustifications.FindAll(r => (r.Materiaux == temp_state_materiaux.Materiaux));
                Load_justification(temp_justification_list, temp_state_materiaux);
            }
		}


      

		StateSousMateriaux temp_sous_materiaux = objets.StateSousMateriauxes.Find(r => (r.Modification == true));
		if(temp_sous_materiaux !=null)
		{
			List<Materiaux> liste_materiaux_to_display = model.Materiaux.FindAll(r => r.SousCategorie == temp_sous_materiaux.SousMateriaux);
            foreach (GameObject toggle_materiaux in materiaux)
            {
                bool foundComaptible = false;
                foreach (Materiaux element in liste_materiaux_to_display)
                {
                    if (element.Nom == toggle_materiaux.name) foundComaptible = true;
                }

                toggle_materiaux.SetActive(foundComaptible);
            }
		}
		else
		{
			foreach(GameObject toggle_materiaux in materiaux)
			{
				toggle_materiaux.SetActive(false);
			}
		}
        
		List<StateMateriaux> temp_list_materiaux = objets.StateMateriauxes.FindAll(r => (r.Modification == true));

		if(temp_list_materiaux.Count == 0)
		{
			foreach (GameObject element in justifications)
            {
                element.SetActive(false);
            }
		}
		foreach(StateMateriaux temp_materiaux in temp_list_materiaux)
		{
			if(temp_materiaux != null && temp_sous_materiaux != null)
            {
                if(temp_materiaux.Sous_materiaux == temp_sous_materiaux.SousMateriaux)
                {
					List<StateJustification> liste_state_justification_to_display = objets.StateJustifications.FindAll(r => (r.Materiaux == temp_materiaux.Materiaux));
                    foreach (StateJustification element in liste_state_justification_to_display)
                    {
						Item justification_name = model.JustificationMateriel.Find(r => r.ID == element.Justification);
                        GameObject gameobjectToggle = justifications.Find(r => r.name == justification_name.Nom);
                        gameobjectToggle.SetActive(true);
                    }
					break;

                }
                else{
                    foreach (GameObject element in justifications)

					{                      
                        element.SetActive(false);
                    }
                }         
            }
            else
            {
                foreach(GameObject element in justifications)
                {
                    element.SetActive(false);
                }
            }  	
		}
       
    }

    void Load_justification(List<StateJustification> list_justification, StateMateriaux temp_mat)
	{
		justifications.Clear();
        foreach (Item justification in model.JustificationMateriel)
        {
			GameObject objet = (GameObject)Instantiate(prefab_object, JusticiationLayout.transform);
            objet.name = justification.Nom;

			StateJustification temp_state = list_justification.Find(r => (r.Objet == objets.ID && r.Materiaux == temp_mat.Materiaux && r.Justification == justification.ID));

            Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
            temp_toggle.name = justification.Nom;
			temp_toggle.isOn = temp_state.Modification;

            Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
            temp_texte.text = justification.Nom;

            Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
			temp_background.color = Color.white;
			temp_background.sprite = justification.Image_unselected;

            Image temp_selected = (Image)temp_toggle.graphic;
			temp_selected.color = Color.grey;
			temp_selected.sprite = justification.Image_selected;

			StateJustification temp_justification = objets.StateJustifications.Find(r => r.ID == justification.ID);
            
            if (temp_justification != null)
            {
                if (temp_justification.Modification)
                {
					temp_background.color = default_color;
                }
                else if (temp_justification.Selected)
                {
					temp_background.color = selection_color;
                }
                else
                {
					temp_selected.color = modified_color;
                }
            }

			temp_toggle.onValueChanged.AddListener(delegate 
			{
				UpdateModel(temp_toggle);
            });

			justifications.Add(objet);
        }

	}

   	void Load_materiaux(List<StateMateriaux> list_materiaux)
	{
        foreach (Materiaux sous_element in model.Materiaux)
        {
			GameObject objet = (GameObject)Instantiate(prefab_object, MateriauxLayout.transform);
            objet.name = sous_element.Nom;

			StateMateriaux temp_state = list_materiaux.Find(r => (r.Objet == objets.ID && r.Materiaux == sous_element.ID));

            Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
            temp_toggle.name = sous_element.Nom;
			temp_toggle.isOn = temp_state.Modification;


			ToggleGroup[] temp_liste_toggle_groupe = SousMateriauxLayout.GetComponentsInChildren<ToggleGroup>();
			Item temp_sous_materiaux = model.Categorie.Find(r => r.ID == sous_element.SousCategorie);
			foreach (ToggleGroup element in temp_liste_toggle_groupe)
			{
				if(element.name == temp_sous_materiaux.Nom)
				{
					element.allowSwitchOff = true;
					temp_toggle.group = element;
					break;
				}
			}   

            Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
            temp_texte.text = sous_element.Nom;
            
            Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
			temp_background.color = Color.white;
			temp_background.sprite = sous_element.Image_unselected;

            Image temp_selected = (Image)temp_toggle.graphic;
            temp_selected.color = Color.grey;
            temp_selected.sprite = sous_element.Image_selected;

			StateMateriaux temp_mat = objets.StateMateriauxes.Find(r => r.ID == sous_element.ID);
            if (temp_mat != null)
            {
                if (temp_mat.Modification)
                {
					temp_background.color = default_color;
                }
                else if (temp_mat.Selected)
                {
					temp_background.color = selection_color;

                }
                else
                {
					temp_selected.color = modified_color;
                }
            }

			temp_toggle.onValueChanged.AddListener(delegate
			{
				UpdateModel(temp_toggle);
            });

			materiaux.Add(objet);
        }
	}

	void Load_sous_materiaux(List<StateSousMateriaux> list_sous_materiaux)
	{
		foreach (Item element in model.Categorie)
		{
			GameObject objet = (GameObject)Instantiate(prefab_object, SousMateriauxLayout.transform);
			objet.name = element.Nom;

			StateSousMateriaux temp_state = list_sous_materiaux.Find(r => (r.Object == objets.ID && r.SousMateriaux == element.ID));

			ToggleGroup temp_toggle_group = objet.AddComponent<ToggleGroup>();

			Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
			temp_toggle.name = element.Nom;
			temp_toggle.group = SousMateriauxLayout.GetComponent<ToggleGroup>();
			temp_toggle.isOn = temp_state.Modification;

			Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
			temp_texte.text = element.Nom;

			Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
			temp_background.color = Color.white;
			temp_background.sprite = element.Image_unselected;

			Image temp_selected = (Image)temp_toggle.graphic;
            temp_selected.color = Color.grey;
			temp_selected.sprite = element.Image_selected;

			StateSousMateriaux temp_sous_materiaux = objets.StateSousMateriauxes.Find(r => r.ID == element.ID);
			if (temp_sous_materiaux != null)
			{
				if (temp_sous_materiaux.Modification)
				{
					temp_background.color = default_color;
				}
				else if (temp_sous_materiaux.Selected)
				{
					temp_background.color = selection_color;
				}
				else
				{
					temp_selected.color = modified_color;
				}


			}

			temp_toggle.onValueChanged.AddListener(delegate 
			{
				UpdateModel(temp_toggle);
			});

			sousMateriaux.Add(objet);
		}
	}
}
