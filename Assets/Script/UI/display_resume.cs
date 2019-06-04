using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class display_resume : MonoBehaviour
{
	private ChoiceController model;
	private Objets objets;
	public GameObject Resume_contexte;
	public GameObject Resume_choix;
	public Text Value_score;
	public Color color_config;
    

	private List<int> temp_id_forme;
	private List<int> temp_id_taille;
	private List<int> temp_id_outils;
	private List<int> temp_id_technique;

	private List<StateMateriaux> temp_state_materiaux;

	private List<Item> temp_list_materiaux;
	private List<Item> temp_list_forme;
	private List<Item> temp_list_taile;
	private List<Item> temp_list_outils;
	private List<Item> temp_list_tehcnique;

	private int temp_poussiere_prof = 0;
	private int temp_poussiere_eleve = 0;
	private int poussiere_coherence = 0;

    void Start()
    {
		model = FindObjectOfType<ChoiceController>();

		if(model != null)
		{
			objets = model.Objets.Find(r => r.Modification == true);

			displayContexte();
           
			temp_id_forme = objets.FormeActive;
			temp_id_taille = objets.TailleActive;
			temp_id_outils = objets.OutilsActif;
			temp_id_technique = objets.TechniqueActive; 

			temp_state_materiaux = objets.StateMateriauxes.FindAll(r => ((r.Modification == true || r.Selected == true)));

			temp_list_materiaux = new List<Item>();
			temp_list_forme = new List<Item>();
			temp_list_taile = new List<Item>();
			temp_list_outils = new List<Item>();
			temp_list_tehcnique = new List<Item>();

			displayForme();

			calculateScore();
			displayScore();

			model.Auto_evaluation = true;
		}      
    }

	void displayContexte()
	{
		Text titre_contexte = Resume_contexte.AddComponent<Text>();
        titre_contexte.font = (Font)Resources.Load("Futura-Bold");
        titre_contexte.fontSize = 60;
        titre_contexte.color = color_config;
        titre_contexte.text = "Résumé du contexte : ";

        List<Association_question> temp_list_association = objets.Association_Questions;
        foreach (Association_question element in temp_list_association)
        {
            GameObject temp_description = new GameObject();
            temp_description.name = element.Question.Description;
			temp_description.transform.SetParent(Resume_contexte.transform);

            LayoutElement temp_layout_descritption = temp_description.AddComponent<LayoutElement>();
            temp_layout_descritption.minWidth = 5000;
            temp_layout_descritption.minHeight = 100;

            Text question = temp_description.AddComponent<Text>();
            question.font = (Font)Resources.Load("Futura-Bold");
            question.fontSize = 50;
            question.text = element.Question.Description;
            question.color = color_config;

            GameObject temp_reponse = new GameObject();
            temp_reponse.name = element.Reponse;
			temp_reponse.transform.SetParent(Resume_contexte.transform);

            LayoutElement temp_layout_reponse = temp_reponse.AddComponent<LayoutElement>();
            temp_layout_reponse.minWidth = 5000;
            temp_layout_reponse.minHeight = 100;

            Text reponse = temp_reponse.AddComponent<Text>();
            reponse.font = (Font)Resources.Load("Futura-Bold");
            reponse.fontSize = 40;
            reponse.text = element.Reponse;
            reponse.color = color_config;
        }
	}
	void displayForme()
	{
		Text titre_forme = Resume_choix.AddComponent<Text>();
        titre_forme.font = (Font)Resources.Load("Futura-Bold");
        titre_forme.fontSize = 60;
        titre_forme.color = color_config;
        titre_forme.text = "Résumé de la forme : ";

		foreach (StateMateriaux element in temp_state_materiaux)
        {
            Materiaux temp_materiaux = model.Materiaux.Find(r => r.ID == element.Materiaux);

            Item new_item = new Item();
            {
                new_item.ID = temp_materiaux.ID;
                new_item.Image_path_selected = temp_materiaux.Image_path_selected;
                new_item.Image_selected = temp_materiaux.Image_selected;
                new_item.Image_unselected = temp_materiaux.Image_unselected;
				new_item.Image_path_unselected = temp_materiaux.Image_path_unselected;
                new_item.Nom = temp_materiaux.Nom;

                temp_list_materiaux.Add(new_item);
            }
        }

        foreach (int element in temp_id_forme)
        {
            Item temp_item = model.Forme.Find(r => r.ID == element);
            if (temp_item != null)
            {
                temp_list_forme.Add(temp_item);
            }
        }

        foreach (int element in temp_id_taille)
        {
            Taille temp_item = model.TailleObjet.Find(r => r.ID == element);

            Item new_item = new Item();
            if (temp_item != null)
            {
                new_item.ID = temp_item.ID;
                new_item.Image_path_selected = temp_item.Image_path_selected;
                new_item.Image_selected = temp_item.Image_selected;
                new_item.Image_unselected = temp_item.Image_unselected;
				new_item.Image_path_unselected = temp_item.Image_path_unselected;
                new_item.Nom = temp_item.Nom;

                temp_list_taile.Add(new_item);
            }
        }

        foreach (int element in temp_id_outils)
        {
            Item temp_item = model.Outils.Find(r => r.ID == element);
            if (temp_item != null)
            {
                temp_list_outils.Add(temp_item);
            }
        }

        foreach (int element in temp_id_technique)
        {
            Item temp_item = model.Technique.Find(r => r.ID == element);
            if (temp_item != null)
            {
                temp_list_tehcnique.Add(temp_item);
            }
        }

        List<List<Item>> List_of_selection = new List<List<Item>>();
        List_of_selection.Add(temp_list_forme);
        List_of_selection.Add(temp_list_taile);
        List_of_selection.Add(temp_list_outils);
        List_of_selection.Add(temp_list_tehcnique);
        List_of_selection.Add(temp_list_materiaux);

        int i = 5;
        foreach (List<Item> liste in List_of_selection)
        {
            GameObject temp_titre = new GameObject();
			temp_titre.transform.SetParent(Resume_choix.transform);

            LayoutElement temp_layout_titre = temp_titre.AddComponent<LayoutElement>();
            temp_layout_titre.minWidth = 1750;

            GridLayoutGroup temp_grid_layout = temp_titre.AddComponent<GridLayoutGroup>();
            temp_grid_layout.cellSize = new Vector2(300, 400);
            temp_grid_layout.padding.top = 100;
            temp_grid_layout.spacing = new Vector2(25, 25);
           
			Text temp_text = temp_titre.AddComponent<Text>();
            temp_text.font = (Font)Resources.Load("Futura-Bold");
            temp_text.fontSize = 50;
            temp_text.color = color_config;

            if (i == 5)
            {
                temp_titre.name = "Forme";
                temp_text.text = "Forme : ";
            }
            else if (i == 4)
            {
                temp_titre.name = "Taille";
                temp_text.text = "Taille : ";
            }
            else if (i == 3)
            {
                temp_titre.name = "Outils";
                temp_text.text = "Outils : ";
            }
            else if (i == 2)
            {
                temp_titre.name = "Technique";
                temp_text.text = "Technique : ";
            }
            else if (i == 1)
            {
                temp_titre.name = "Materiaux";
                temp_text.text = "Materiaux : ";

            }

            foreach (Item element in liste)
            {
                GameObject temp_item = new GameObject();
                temp_item.name = element.Nom;
                temp_item.transform.SetParent(temp_titre.transform);

                LayoutElement temp_layout_item = temp_item.AddComponent<LayoutElement>();
                temp_layout_item.minWidth = 300;
                temp_layout_item.minHeight = 200;

                VerticalLayoutGroup temp_vertical_item = temp_item.AddComponent<VerticalLayoutGroup>();
                temp_vertical_item.childForceExpandWidth = false;
                temp_vertical_item.childForceExpandHeight = false;
                temp_vertical_item.childControlWidth = true;
                temp_vertical_item.childControlHeight = true;
                temp_vertical_item.childAlignment = TextAnchor.MiddleCenter;

                GameObject temp_image = new GameObject();
                temp_image.transform.SetParent(temp_item.transform);
                temp_image.name = "image";

                LayoutElement temp_layout_image = temp_image.AddComponent<LayoutElement>();
                temp_layout_image.minWidth = 300;
                temp_layout_image.minHeight = 200;

                Image image = temp_image.AddComponent<Image>();
                image.sprite = element.Image_unselected;

                GameObject temp_nom = new GameObject();
                temp_nom.name = "Nom";
                temp_nom.transform.SetParent(temp_item.transform);

                LayoutElement temp_layout_descritption = temp_nom.AddComponent<LayoutElement>();
                temp_layout_descritption.minWidth = 300;
                temp_layout_descritption.minHeight = 200;

                Text Nom = temp_nom.AddComponent<Text>();
                Nom.alignment = TextAnchor.MiddleCenter;
                Nom.font = (Font)Resources.Load("Futura-Bold");
                Nom.fontSize = 40;
                Nom.text = element.Nom;
                Nom.color = color_config;
               
				if(i == 1)
				{
					List<StateJustification> list_justification_mat = objets.StateJustifications.FindAll
                                                                  (r => (r.Materiaux == element.ID && (r.Modification == true || r.Selected == true)));
                    foreach (StateJustification justif in list_justification_mat)
                    {
                        Item temp_justification_item = model.JustificationMateriel.Find(r => r.ID == justif.Justification);

                        GameObject temp_justification = new GameObject();
                        temp_justification.name = temp_justification_item.Nom;
                        temp_justification.transform.SetParent(temp_titre.transform);

                        LayoutElement temp_justi_layout_item = temp_justification.AddComponent<LayoutElement>();
                        temp_justi_layout_item.preferredWidth = 200;
                        temp_justi_layout_item.preferredHeight = 150;

                        VerticalLayoutGroup temp_justi_vertical_item = temp_justification.AddComponent<VerticalLayoutGroup>();
                        temp_justi_vertical_item.childForceExpandWidth = false;
                        temp_justi_vertical_item.childForceExpandHeight = false;
                        temp_justi_vertical_item.childControlWidth = true;
                        temp_justi_vertical_item.childControlHeight = true;
                        temp_justi_vertical_item.childAlignment = TextAnchor.UpperCenter;

                        GameObject temp_justi_image = new GameObject();
                        temp_justi_image.transform.SetParent(temp_justification.transform);
                        temp_justi_image.name = "image";

                        LayoutElement temp_justi_layout_image = temp_justi_image.AddComponent<LayoutElement>();
                        temp_justi_layout_image.preferredWidth = 200;
                        temp_justi_layout_image.preferredHeight = 150;

                        Image justi_image = temp_justi_image.AddComponent<Image>();
                        justi_image.sprite = temp_justification_item.Image_unselected;

                        GameObject temp_justi_nom = new GameObject();
                        temp_justi_nom.name = "Nom";
                        temp_justi_nom.transform.SetParent(temp_justification.transform);

                        LayoutElement temp_justi_layout_descritption = temp_justi_nom.AddComponent<LayoutElement>();
                        temp_justi_layout_descritption.preferredWidth = 200;
                        temp_justi_layout_descritption.preferredHeight = 150;

                        Text Nom_justi = temp_justi_nom.AddComponent<Text>();
                        Nom_justi.alignment = TextAnchor.MiddleCenter;
                        Nom_justi.font = (Font)Resources.Load("Futura-Bold");
                        Nom_justi.fontSize = 35;
                        Nom_justi.text = temp_justification_item.Nom;
                        Nom_justi.color = color_config;
                    }
				}
            }
            i--;
        }
	}
	void calculateScore()
	{
        
		List<CoherenceMaterielTechnique> list_coherenceMaterielTechnique = new List<CoherenceMaterielTechnique>();
		List<CoherenceMaterielJustification> list_coherenceMaterielJustification = new List<CoherenceMaterielJustification>();
		List<CoherenceMaterielOutils> list_coherenceMaterielOutils = new List<CoherenceMaterielOutils>();
		List<CoherenceTechniqueOutils> list_coherenceTechniqueOutils = new List<CoherenceTechniqueOutils>();

		foreach(StateMateriaux element in temp_state_materiaux)
		{
			List<StateJustification> temp_id_justification = objets.StateJustifications.FindAll(r => (r.Materiaux == element.Materiaux && r.Modification == true));

            list_coherenceMaterielOutils = objets.CoherenceMaterielOutils.FindAll(r => (r.Materiels == element.Materiaux && temp_id_outils.Contains(r.Outils)));
			list_coherenceMaterielTechnique = objets.CoherenceMaterielTechnique.FindAll(r => (r.Materiels == element.Materiaux && temp_id_technique.Contains(r.Technique)));
                       
			foreach (StateJustification id in temp_id_justification) 
			{
				CoherenceMaterielJustification temp_coherence = objets.CoherencesMaterielJustification.Find(r => (r.Materiels == element.Materiaux && r.Justification == id.Justification));
				if(temp_coherence != null){
					list_coherenceMaterielJustification.Add(temp_coherence);
				}
			}

		}

		foreach(int element in temp_id_technique)
		{
			list_coherenceTechniqueOutils = objets.CoherenceTechniqueOutils.FindAll(r => (r.Technique == element && temp_id_outils.Contains(r.Outils)));
		}

		foreach(CoherenceMaterielTechnique coherence in list_coherenceMaterielTechnique)
		{
			poussiere_coherence += coherence.Score;
		}

		foreach(CoherenceMaterielOutils coherence in list_coherenceMaterielOutils)
		{
			poussiere_coherence += coherence.Score;
		}

		foreach(CoherenceMaterielJustification coherence in list_coherenceMaterielJustification)
		{
			poussiere_coherence += coherence.Score;
		}

		foreach(CoherenceTechniqueOutils coherence in list_coherenceTechniqueOutils)
		{
			poussiere_coherence += coherence.Score;
		}

	}

	void displayScore()
	{
		if(temp_poussiere_prof+temp_poussiere_eleve+poussiere_coherence > 0)
		{
			Value_score.text = temp_poussiere_prof+temp_poussiere_eleve+poussiere_coherence + " poussières";
		}
		else 
		{
			Value_score.text = " Aucune pousières";
		}
	}

	public void save_score()
	{
		model.Profils.Poussiere_actuel += temp_poussiere_prof+temp_poussiere_eleve+poussiere_coherence;
	}

	public void onValueChangeProf(float value)
	{
		temp_poussiere_prof = (int)value;
	}
	public void onValueChangeEleve(float value)
	{
		temp_poussiere_eleve = (int)value;
	}
}
