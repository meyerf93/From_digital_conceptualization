using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadDataBase : MonoBehaviour
{
	public static LoadDataBase loadManager;

	public const string LOCK_FILEPATH = "Images/Cadenas.png";

	private ChoiceController model;

	public void Start()
	{
		if (loadManager == null)
		{
			loadManager = this;
			DontDestroyOnLoad(gameObject);

			model = FindObjectOfType<ChoiceController>();

		}
		else if (loadManager != this)
        {
            Destroy(gameObject);
        }
	}
    
	public string LoadStreamingAssetText(string absoluteFilePath)
	{
		string path = "";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor
		    || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            //Debug.Log("in windows or mac platform");
			path = "file://" + Application.streamingAssetsPath + "/" + absoluteFilePath;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            //Debug.Log("in android platform");
			path = "jar:file://" + Application.dataPath + "!/assets/" + absoluteFilePath;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //Debug.Log("in iphone plateform");
			path = "file://" + Application.streamingAssetsPath + "/" + absoluteFilePath;
        }

		WWW www = new WWW(path);

		while(!www.isDone)  { }

        if (!string.IsNullOrEmpty(www.error))
        {

            Debug.LogError(www.error);
            return null;
        }
        else
        {
			return www.text;
        }

	}

	public Sprite LoadStreamingAssetSprite(string absoluteImagePath)
    {

		string path = "";
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor
            || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            //Debug.Log("in windows or mac platform");
			path = "file://" + Application.streamingAssetsPath + "/" + absoluteImagePath;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            //Debug.Log("in android platform");
			path = "jar:file://" + Application.dataPath + "!/assets/" + absoluteImagePath;
        }
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //Debug.Log("in iphone plateform");
			path = "file://" + Application.streamingAssetsPath + "/" + absoluteImagePath;
        }
                               
		WWW www = new WWW(path);

		while (!www.isDone) { }
             
        if (!string.IsNullOrEmpty(www.error))
        {
			Debug.LogError(path);
			Debug.LogError(www.error);
			return null;
        }
        else
        {
			Sprite temp_sprite = Sprite.Create(www.texture,
			                                   new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);

			return temp_sprite;
        }
    }
   
	public object load_Objets(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}

			if (Header.CompareTo("Objets") == 0 && i > marker)
			{

				Objets temp = new Objets();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				temp.Image_path_selected = elements[3];
				temp.Image_path_unselected = elements[4];
				temp.min_etoiles = int.Parse(elements[5]);

				temp.Lock = new Lock_objet();
				if (temp.min_etoiles != 0) temp.Lock.isLocked = true;
				else temp.Lock.isLocked = false;

				temp.Lock.image_path = LOCK_FILEPATH;
				temp.Lock.image =  LoadStreamingAssetSprite(temp.Lock.image_path);

				temp.Association_Questions = new List<Association_question>();

				temp.CoherenceMaterielOutils = new List<CoherenceMaterielOutils>();
				temp.CoherenceTechniqueOutils = new List<CoherenceTechniqueOutils>();
				temp.CoherenceMaterielTechnique = new List<CoherenceMaterielTechnique>();
				temp.CoherencesMaterielJustification = new List<CoherenceMaterielJustification>();

				temp.StateSousMateriauxes = new List<StateSousMateriaux>();
				temp.StateMateriauxes = new List<StateMateriaux>();
				temp.StateJustifications = new List<StateJustification>();

				model.Objets.Add(temp);
			}
		}
		return model.Objets;
	}


	public object load_Profils(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}
			if (Header.CompareTo("Historique") == 0 && i > marker)
			{
				/*//Debug.Log("in historique");

                Historique temp = new Historique();
                temp.ID = int.Parse(elements[1]);
                temp.Objet = int.Parse(elements[2]);

                string[] forme;
                for (int j = 3; j < elements.Length - 1; j++)
                {
                    List<int> liste = new List<int>();

                    if (elements[j].CompareTo("") != 0)
                    {
                        forme = elements[j].Split(',');
                        foreach (string thing in forme)
                        {
                            liste.Add(int.Parse(thing));
                        }
                    }
                    switch (j)
                    {
                        case 3:
                            temp.FormeActive = liste;
                            break;
                        case 4:
                            temp.FormeInactive = liste;
                            break;
                        case 5:
                            temp.TailleActive = liste;
                            break;
                        case 6:
                            temp.TailleInactive = liste;
                            break;
                        case 7:
                            temp.MateriauxActif = liste;
                            break;
                        case 8:
                            temp.MateriauxInactif = liste;
                            break;
                        case 9:
                            temp.SousMateriauxActif = liste;
                            break;
                        case 10:
                            temp.SousMateriauxInactif = liste;
                            break;
                        case 11:
                            temp.OutilsActif = liste;
                            break;
                        case 12:
                            temp.OutilsInactif = liste;
                            break;
                        case 13:
                            temp.TechniqueActive = liste;
                            break;
                        case 14:
                            temp.TechniqueInactive = liste;
                            break;
                        case 15:
                            temp.AssociationJustification = liste;
                            break;
                        default:
                            break;
                    }
                }
                Historique.Add(temp);*/
			}
			else if (Header.CompareTo("Profils") == 0 && i > marker)
			{
				model.Profils.ID = int.Parse(elements[1]);
				model.Profils.Etoiles_actuel = int.Parse(elements[2]);
				model.Profils.Etoiles_depenser = int.Parse(elements[3]);
				model.Profils.Poussiere_actuel = int.Parse(elements[4]);
			}
		}

		return model.Profils;
	}

	public object load_questions(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}
			else if (Header.CompareTo("Question") == 0 && i > marker)
			{
				Question temp = new Question();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				temp.Description = elements[3];
				model.Questions.Add(temp);
			}
		}

		return model.Questions;
	}

	public object load_Materiaux(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}

			if (Header.CompareTo("Sous_categorie_Materiaux") == 0 && i > marker)
			{
				Item temp_item = new Item();
				temp_item.ID = int.Parse(elements[1]);
				temp_item.Nom = elements[2];
				temp_item.Image_path_selected = elements[3];
				temp_item.Image_path_unselected = elements[4];
				model.Categorie.Add(temp_item);
			}
			else if (Header.CompareTo("Materiaux") == 0 && i > marker)
			{
				Materiaux temp_materiaux = new Materiaux();
				temp_materiaux.ID = int.Parse(elements[1]);
				temp_materiaux.Nom = elements[2];

				temp_materiaux.Image_path_selected = elements[3];
				temp_materiaux.Image_path_unselected = elements[4];
				temp_materiaux.SousCategorie = int.Parse(elements[5]);

				model.Materiaux.Add(temp_materiaux);
			}
		}

		return model.Materiaux;
	}

	public object load_Taille(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}

			if (Header.CompareTo("Taille_objet") == 0 && i > marker)
			{
				Taille temp = new Taille();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				temp.Dimension = int.Parse(elements[3]);
				temp.Image_path_selected = elements[4];
				temp.Image_path_unselected = elements[5];

				model.TailleObjet.Add(temp);
			}
		}

		return model.TailleObjet;
	}


	public void load_Item(string[] file_line, List<Item> list_item)
	{
		string Header = null;

		int size = file_line.Length;
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}

			if (i > marker)
			{
				Item temp = new Item();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				temp.Image_path_selected = elements[3];
				temp.Image_path_unselected = elements[4];

				list_item.Add(temp);
			}
		}
	}

	public void load_Coherence_Materiel_Justification(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');

			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}

			if (Header.CompareTo("Coherence_materiel_justification") == 0 && i > marker)
			{
				CoherenceMaterielJustification temp = new CoherenceMaterielJustification();
				temp.ID = int.Parse(elements[1]);
				temp.Objet = int.Parse(elements[2]);

				temp.Materiels = int.Parse(elements[3]);
				temp.Justification = int.Parse(elements[4]);
				temp.Score = int.Parse(elements[5]);

				Objets temp_objet = model.Objets.Find(r => r.ID == temp.Objet);
				temp_objet.CoherencesMaterielJustification.Add(temp);
			}
		}
	}

	public void load_Coherence_Materiel_Outils(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}

			if (Header.CompareTo("Coherence materiel technique") == 0 && i > marker)
			{
				CoherenceMaterielOutils temp = new CoherenceMaterielOutils();
				temp.ID = int.Parse(elements[1]);
				temp.Objet = int.Parse(elements[2]);

				temp.Materiels = int.Parse(elements[3]);
				temp.Outils = int.Parse(elements[4]);
				temp.Score = int.Parse(elements[5]);

				Objets temp_objet = model.Objets.Find(r => r.ID == temp.Objet);
				temp_objet.CoherenceMaterielOutils.Add(temp);
			}
		}
	}

	public void load_Coherence_Technique_Outils(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}

			if (Header.CompareTo("Coherence materiel technique") == 0 && i > marker)
			{
				CoherenceTechniqueOutils temp = new CoherenceTechniqueOutils();
				temp.ID = int.Parse(elements[1]);
				temp.Objet = int.Parse(elements[2]);

				temp.Technique = int.Parse(elements[3]);
				temp.Outils = int.Parse(elements[4]);
				temp.Score = int.Parse(elements[5]);

				Objets temp_objet = model.Objets.Find(r => r.ID == temp.Objet);
				temp_objet.CoherenceTechniqueOutils.Add(temp);
			}
		}
	}

	public void load_Coherence_Materiel_Technique(string[] file_line)
	{
		string Header = null;

		int size = file_line.Length;

		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}

			if (Header.CompareTo("Coherence materiel technique") == 0 && i > marker)
			{
				CoherenceMaterielTechnique temp = new CoherenceMaterielTechnique();
				temp.ID = int.Parse(elements[1]);
				temp.Objet = int.Parse(elements[2]);

				temp.Materiels = int.Parse(elements[3]);
				temp.Technique = int.Parse(elements[4]);
				temp.Score = int.Parse(elements[5]);

				Objets temp_objet = model.Objets.Find(r => r.ID == temp.Objet);
				temp_objet.CoherenceMaterielTechnique.Add(temp);
			}
		}
	}

	public void Load_current_state(List<Objets> liste_objet)
	{
		if (liste_objet != null)
		{
			foreach (Objets element in liste_objet)
			{
				Objets temp_element = model.Objets.Find(r => r.ID == element.ID);

				if (temp_element != null)
				{
					temp_element.Image_path_selected = element.Image_path_selected;
					temp_element.Image_path_unselected = element.Image_path_unselected;
					temp_element.Modification = element.Modification;
					temp_element.Nom = element.Nom;
					temp_element.min_etoiles = element.min_etoiles;

					foreach (int item in element.FormeActive)
					{
						temp_element.FormeActive.Add(item);
						temp_element.FormeInactive.Remove(item);
					}

					foreach (int item in element.OutilsActif)
					{
						temp_element.OutilsActif.Add(item);
						temp_element.OutilsInactif.Remove(item);
					}

					foreach (int item in element.TailleActive)
					{
						temp_element.TailleActive.Add(item);
						temp_element.TailleInactive.Remove(item);
					}
					foreach (int item in element.TechniqueActive)
					{
						temp_element.TechniqueActive.Add(item);
						temp_element.TechniqueInactive.Remove(item);
					}

					foreach (Association_question association in element.Association_Questions)
					{
						Association_question temp = temp_element.Association_Questions.Find(r => r.ID == association.ID);
						if (temp != null)
						{
							temp.Question = model.Questions.Find(r => r.ID == association.Question.ID);
							if (association.Reponse != null)
							{
								temp.Reponse = association.Reponse;
							}
							else
							{
								temp.Reponse = "";
							}
							temp.Modification = association.Modification;
						}
					}

					foreach (StateSousMateriaux state in element.StateSousMateriauxes)
					{
						StateSousMateriaux temp = temp_element.StateSousMateriauxes.Find(r => r.SousMateriaux == state.SousMateriaux);
						if (temp != null)
						{
							temp.Selected = state.Selected;
							temp.Modification = state.Modification;
						}

					}

					foreach (StateMateriaux state in element.StateMateriauxes)
					{
						StateMateriaux temp = temp_element.StateMateriauxes.Find(r => (r.Sous_materiaux == state.Sous_materiaux && r.Materiaux == state.Materiaux));
						if (temp != null)
						{
							temp.Selected = state.Selected;
							temp.Modification = state.Modification;
						}
					}

					foreach (StateJustification state in element.StateJustifications)
					{
						StateJustification temp = temp_element.StateJustifications.Find(r => (r.Materiaux == state.Materiaux && r.Justification == state.Justification));
						if (temp != null)
						{
							temp.Selected = state.Selected;
							temp.Modification = state.Modification;
						}
					}

				}
			}
		}

	}
}