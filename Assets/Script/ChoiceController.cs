using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using DBXSync;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
using System;

public class ChoiceController : MonoBehaviour
{
	public static ChoiceController controller;
	private DownloadManager downloadManager;

	public Profils Profils;
	public List<Historique> Historique;
	public List<Objets> Objets;
	public List<Item> Forme;
	public List<Taille> TailleObjet;
	public List<Item> JustificationMateriel;
	public List<Item> Outils;
	public List<Item> Technique;
	public List<Item> Categorie;
	public List<Materiaux> Materiaux;
	public List<Question> Questions;

	public bool[] Forme_validation;

	public bool Choix_objet;
	public bool Choix_isLoaded;
	public bool Contexte_objet;
	public bool Contexte_isLoaded;
	public bool Forme_objet;
	public bool Forme_isLoaded;
	public bool Auto_evaluation;
	public bool Auto_isLoaded;
	public bool Imaginons_objet;
	public bool Imaginons_isLoaded;

	public const string SAVE_FOLDER = "/Sauvegarde";
	public const string CONF_FOLDER = "/Configuration";
	public const string RULSE_FOLDER = "/Regles";

	public const string PROFILS_FILENAME = "Sauvegarde/Profils_utilisateur.csv";
	public const string CURRENT_STATE_FILENAME = "Sauvegarde/Current_Satate.object";

	public const string OBJETS_FILENAME = "Configuration/Objets.csv";
	public const string TAILLE_FILENAME = "Configuration/Taile_objet.csv";
	public const string FORME_FILENAME = "Configuration/Forme.csv";
	public const string MATERIEL_FILENAME = "Configuration/Materiel.csv";
	public const string JUSTIFICATION_FILENAME = "Configuration/Justification_materiel.csv";
	public const string OUTILS_FILENAME = "Configuration/Outils.csv";
	public const string TECHNIQUE_FILENAME = "Configuration/Technique.csv";
	public const string QUESTION_FILENAME = "Configuration/Questions.csv";

	public const string COHERENCE_MATERIEL_OUTILS_FILENAME = "Regles/Coherence_materiel_outils.csv";
	public const string COHERENCE_MATERIEL_TECHNIQUE_FILENAME = "Regles/Coherence_materiel_techniques.csv";
	public const string COHERENCE_MATERIEL_JUSTIFICATION_FILENAME = "Regles/Coherence_materiel_justificatif.csv";
	public const string COHERENCE_TECHNIQUE_OUTILS_FILENAME = "Regles/Coherence_techniques_outils.csv";

	private LoadDataBase dataBase;

	private bool model_loaded = false;
	public bool reset = true;

	private List<DownloadItem> list_to_download;
	private List<Item> download_liste;
	private List<DownloadImageItem> download_image_liste;

	bool download_conf = false;
	bool download_image = false;
	bool load_specific = false;
	bool base_is_loaded = false;
	bool download_current_state = false;
	bool upload_current_state = false;
	bool download_profils = false;

	private void Awake()
	{
		Debug.Log(" rest at awake : " + reset);
		if (controller == null)
		{
			DontDestroyOnLoad(gameObject);
			InitModel();
		}
		else if (controller != this)
		{
			reset = true;
			Destroy(gameObject);
		}
	}

	public void InitModel()
	{
		reset = false;
		model_loaded = false;
		download_conf = false;
		download_image = false;
		load_specific = false;
		base_is_loaded = false;
		download_current_state = false;
		upload_current_state = false;
		download_profils = false;
		Choix_objet = false;
		Choix_isLoaded = false;
		Contexte_objet = false;
		Contexte_isLoaded = false;
		Forme_objet = false;
		Forme_isLoaded = false;
		Auto_evaluation = false;
		Auto_isLoaded = false;
		Imaginons_objet = false;
		Imaginons_isLoaded = false;

		Debug.Log("reset at init : " + reset);
		dataBase = FindObjectOfType<LoadDataBase>();

		controller = this;
		downloadManager = FindObjectOfType<DownloadManager>();

		Profils = new Profils();
		Historique = new List<Historique>();
		Objets = new List<Objets>();

		Forme_validation = new bool[5];
		for (int i = 0; i < Forme_validation.Length; i++)
		{
			Forme_validation[i] = false;
		}


		Forme = new List<Item>();
		TailleObjet = new List<Taille>();
		JustificationMateriel = new List<Item>();
		Outils = new List<Item>();
		Technique = new List<Item>();
		Categorie = new List<Item>();
		Materiaux = new List<Materiaux>();
		Questions = new List<Question>();

		list_to_download = new List<DownloadItem>();

		InitDownload();
	}

	void Update()
	{
		if (controller != null && !model_loaded)
		{
			bool base_loaded = false;

			download_conf = true;
			//downloadManager.DownloadFile(list_to_download);
			downloadManager.DownloadFileOffline(list_to_download);


			foreach (DownloadItem item in list_to_download)
			{
				base_loaded = item.isLoaded;
				if (base_loaded == false) break;
			}


			if (base_loaded && !base_is_loaded)
			{
				base_is_loaded = true;
				init_StateSousMateriaux();
				init_StateMateriaux();
				init_StateJustification();
				init_associationQuestion();
				init_currentState();


				/*if (File.Exists(Application.persistentDataPath +"/current_state.object"))
				{
					Objets = SaveSystem.LoadObjets();
					dataBase.Load_current_state(Objets);
				}*/
				if (!download_current_state)
				{
					download_current_state = true;
					DownloadItem load_objet = new DownloadItem();
					load_objet.filePath = CURRENT_STATE_FILENAME;
					load_objet.type = Donwload_type.Objet;

					List<DownloadItem> temp_liste = new List<DownloadItem>();
					temp_liste.Add(load_objet);

					//downloadManager.DownloadCurrentState(temp_liste);
					downloadManager.DownloadCurrentStateOffline(temp_liste);
				}

				if (!download_profils)
				{
					download_profils = true;
					DownloadItem load_objet = new DownloadItem();
					load_objet.filePath = PROFILS_FILENAME;
					load_objet.type = Donwload_type.Profil;

					List<DownloadItem> temp_liste = new List<DownloadItem>();
					temp_liste.Add(load_objet);

					//downloadManager.DownloadCurrentState(temp_liste);
					downloadManager.DownloadProfilOffline(temp_liste);
				}

				bool current_state_is_loaded = false;
				foreach (DownloadItem thing in list_to_download)
				{
					current_state_is_loaded = thing.isLoaded;
					if (!current_state_is_loaded) break;
				}



				if (!download_image)
				{
					download_liste = new List<Item>();

					InitDownloadImage();

					download_image = true;
					//downloadManager.DownloadImage(download_image_liste);
					downloadManager.DownloadImageOffline(download_image_liste);
				}


				bool isModelLoaded = false;

				foreach (DownloadImageItem image in download_image_liste)
				{
					isModelLoaded = image.isLoaded;
					if (isModelLoaded == false) break;
				}

				if (!load_specific)
				{
					foreach (Objets obj in Objets)
					{
						DownloadImageItem temp_item = download_image_liste.Find(r => r.item.Nom == obj.Nom);
						obj.Image_selected = temp_item.item.Image_selected;
						obj.Image_unselected = temp_item.item.Image_unselected;
						Sprite temp_sprite = dataBase.LoadStreamingAssetSprite(obj.Lock.image_path);
						obj.Lock.image = temp_sprite;
					}

					foreach (Taille obj in TailleObjet)
					{
						DownloadImageItem temp_item = download_image_liste.Find(r => r.item.Nom == obj.Nom);
						obj.Image_selected = temp_item.item.Image_selected;
						obj.Image_unselected = temp_item.item.Image_unselected;
					}

					foreach (Materiaux obj in Materiaux)
					{
						DownloadImageItem temp_item = download_image_liste.Find(r => r.item.Nom == obj.Nom);
						obj.Image_selected = temp_item.item.Image_selected;
						obj.Image_unselected = temp_item.item.Image_unselected;
					}
					load_specific = true;

				}

				if (load_specific)
				{
					model_loaded = true;
					SceneManager.LoadScene("Selection_objet");
				}
			}
		}

	}


	private void OnDestroy()
	{
		Debug.Log("reset : " + reset);
		//SaveSystem.SaveObjets(Objets);
		if (!reset)
		{
			download_current_state = true;
			DownloadItem load_objet = new DownloadItem();
			load_objet.filePath = CURRENT_STATE_FILENAME;
			load_objet.type = Donwload_type.Objet;
			load_objet.data = Objets;

			List<DownloadItem> temp_liste = new List<DownloadItem>();
			temp_liste.Add(load_objet);

			//downloadManager.UpodloadCurrentState(temp_liste);
			downloadManager.UploadCurrentStateOffline(temp_liste);

			download_profils = true;
			DownloadItem load_profil = new DownloadItem();
			load_profil.filePath = PROFILS_FILENAME;
			load_profil.type = Donwload_type.Objet;
			load_profil.data = Profils;

			List<DownloadItem> temp_liste_profil = new List<DownloadItem>();
			temp_liste_profil.Add(load_profil);

			//downloadManager.UpodloadCurrentState(temp_liste);
			downloadManager.UploadProfilsOffline(temp_liste_profil);
		}
	}

	void init_associationQuestion()
	{
		int i = 1;
		foreach (Objets id in Objets)
		{
			foreach (Question item in Questions)
			{
				Association_question temp_question = new Association_question();
				temp_question.ID = i;
				temp_question.Objet = id.ID;
				temp_question.Question = item;
				temp_question.Reponse = "";
				temp_question.Modification = false;
				id.Association_Questions.Add(temp_question);
				i++;
			}
		}
	}

	void init_currentState()
	{
		foreach (Objets id in Objets)
		{
			id.FormeActive = new List<int>();
			id.FormeInactive = new List<int>();
			id.TailleActive = new List<int>();
			id.TailleInactive = new List<int>();
			id.OutilsActif = new List<int>();
			id.OutilsInactif = new List<int>();
			id.TechniqueActive = new List<int>();
			id.TechniqueInactive = new List<int>();
			foreach (Item temp_forme in Forme)
			{
				id.FormeInactive.Add(temp_forme.ID);
			}

			foreach (Taille temp_Taille in TailleObjet)
			{
				id.TailleInactive.Add(temp_Taille.ID);
			}

			foreach (Item temp_outils in Outils)
			{
				id.OutilsInactif.Add(temp_outils.ID);
			}

			foreach (Item temp_technique in Technique)
			{
				id.TechniqueInactive.Add(temp_technique.ID);
			}
		}
	}

	public void init_StateSousMateriaux()
	{
		int i = 1;
		foreach (Objets id in Objets)
		{
			foreach (Item item in Categorie)
			{
				StateSousMateriaux temp_sous_materiaux = new StateSousMateriaux();
				temp_sous_materiaux.ID = i;
				temp_sous_materiaux.Modification = false;
				temp_sous_materiaux.Selected = false;
				temp_sous_materiaux.Object = id.ID;
				temp_sous_materiaux.SousMateriaux = item.ID;
				id.StateSousMateriauxes.Add(temp_sous_materiaux);

				i++;
			}
		}
	}

	public void init_StateMateriaux()
	{
		int i = 1;
		foreach (Objets id in Objets)
		{

			foreach (Materiaux item in Materiaux)
			{
				StateMateriaux temp_state_materiaux = new StateMateriaux();
				temp_state_materiaux.ID = i;

				temp_state_materiaux.Modification = false;
				temp_state_materiaux.Selected = false;

				temp_state_materiaux.Objet = id.ID;
				temp_state_materiaux.Sous_materiaux = item.SousCategorie;
				temp_state_materiaux.Materiaux = item.ID;

				id.StateMateriauxes.Add(temp_state_materiaux);


				i++;
			}
		}
	}

	public void init_StateJustification()
	{
		int i = 1;
		foreach (Objets id in Objets)
		{
			foreach (Materiaux element in Materiaux)
			{
				foreach (Item item in JustificationMateriel)
				{
					StateJustification temp_state_justification = new StateJustification();
					temp_state_justification.ID = i;

					temp_state_justification.Modification = false;
					temp_state_justification.Selected = false;
					temp_state_justification.Objet = id.ID;
					temp_state_justification.Materiaux = element.ID;
					temp_state_justification.Justification = item.ID;

					id.StateJustifications.Add(temp_state_justification);

					i++;
				}
			}
		}
	}

	private void InitDownload()
	{
		DownloadItem temp_Objet = new DownloadItem();
		temp_Objet.filePath = OBJETS_FILENAME;
		temp_Objet.type = Donwload_type.Objet;

		list_to_download.Add(temp_Objet);

		DownloadItem temp_taille = new DownloadItem();
		temp_taille.filePath = TAILLE_FILENAME;
		temp_taille.type = Donwload_type.Taille;

		list_to_download.Add(temp_taille);

		DownloadItem temp_forme = new DownloadItem();
		temp_forme.filePath = FORME_FILENAME;
		temp_forme.type = Donwload_type.Forme;

		list_to_download.Add(temp_forme);

		DownloadItem temp_materiel = new DownloadItem();
		temp_materiel.filePath = MATERIEL_FILENAME;
		temp_materiel.type = Donwload_type.Materiaux;

		list_to_download.Add(temp_materiel);

		DownloadItem temp_justification = new DownloadItem();
		temp_justification.filePath = JUSTIFICATION_FILENAME;
		temp_justification.type = Donwload_type.Justfification_materiel;

		list_to_download.Add(temp_justification);

		DownloadItem temp_outils = new DownloadItem();
		temp_outils.filePath = OUTILS_FILENAME;
		temp_outils.type = Donwload_type.Outils;

		list_to_download.Add(temp_outils);

		DownloadItem temp_technique = new DownloadItem();
		temp_technique.filePath = TECHNIQUE_FILENAME;
		temp_technique.type = Donwload_type.Technique;

		list_to_download.Add(temp_technique);

		DownloadItem temp_question = new DownloadItem();
		temp_question.filePath = QUESTION_FILENAME;
		temp_question.type = Donwload_type.Question;

		list_to_download.Add(temp_question);

		DownloadItem temp_coherence_mat_outils = new DownloadItem();
		temp_coherence_mat_outils.filePath = COHERENCE_MATERIEL_OUTILS_FILENAME;
		temp_coherence_mat_outils.type = Donwload_type.Coherence_mat_outil;

		list_to_download.Add(temp_coherence_mat_outils);

		DownloadItem temp_coherence_mat_tech = new DownloadItem();
		temp_coherence_mat_tech.filePath = COHERENCE_MATERIEL_TECHNIQUE_FILENAME;
		temp_coherence_mat_tech.type = Donwload_type.Coherence_mat_tech;

		list_to_download.Add(temp_coherence_mat_tech);

		DownloadItem temp_coherence_mat_just = new DownloadItem();
		temp_coherence_mat_just.filePath = COHERENCE_MATERIEL_JUSTIFICATION_FILENAME;
		temp_coherence_mat_just.type = Donwload_type.Coherence_mat_just;

		list_to_download.Add(temp_coherence_mat_just);

		DownloadItem temp_coherence_tech_outils = new DownloadItem();
		temp_coherence_tech_outils.filePath = COHERENCE_TECHNIQUE_OUTILS_FILENAME;
		temp_coherence_tech_outils.type = Donwload_type.Coherence_tech_outils;
		list_to_download.Add(temp_coherence_tech_outils);
	}

	private void InitDownloadImage()
	{
		List<Item> temp_list_objet = new List<Item>();
		foreach (Objets element in Objets)
		{
			Item temp_item = new Item();
			temp_item.ID = element.ID;
			temp_item.Image_path_selected = element.Image_path_selected;
			temp_item.Image_path_unselected = element.Image_path_unselected;
			temp_item.Nom = element.Nom;
			temp_item.Image_selected = element.Image_selected;
			temp_item.Image_unselected = element.Image_unselected;
			element.Image_selected = temp_item.Image_selected;
			element.Image_unselected = temp_item.Image_unselected;
			temp_list_objet.Add(temp_item);
		}

		download_liste.AddRange(temp_list_objet);

		download_liste.AddRange(Forme);
		download_liste.AddRange(JustificationMateriel);
		download_liste.AddRange(Outils);
		download_liste.AddRange(Technique);

		List<Item> temp_list_taille = new List<Item>();
		foreach (Taille element in TailleObjet)
		{
			Item temp_item = new Item();
			temp_item.ID = element.ID;
			temp_item.Image_path_selected = element.Image_path_selected;
			temp_item.Image_path_unselected = element.Image_path_unselected;
			temp_item.Nom = element.Nom;
			temp_item.Image_selected = element.Image_selected;
			temp_item.Image_unselected = element.Image_unselected;
			element.Image_selected = temp_item.Image_selected;
			element.Image_unselected = temp_item.Image_unselected;
			temp_list_taille.Add(temp_item);
		}

		download_liste.AddRange(temp_list_taille);


		List<Item> temp_list_materiaux = new List<Item>();
		foreach (Materiaux element in Materiaux)
		{
			Item temp_item = new Item();
			temp_item.ID = element.ID;
			temp_item.Image_path_selected = element.Image_path_selected;
			temp_item.Image_path_unselected = element.Image_path_unselected;
			temp_item.Nom = element.Nom;
			temp_item.Image_selected = element.Image_selected;
			temp_item.Image_unselected = element.Image_unselected;
			element.Image_selected = temp_item.Image_selected;
			element.Image_unselected = temp_item.Image_unselected;
			temp_list_materiaux.Add(temp_item);
		}

		download_liste.AddRange(temp_list_materiaux);

		download_liste.AddRange(Categorie);

		download_image_liste = new List<DownloadImageItem>();
		foreach (Item item in download_liste)
		{
			DownloadImageItem imageItem = new DownloadImageItem();
			imageItem.isLoaded = false;
			imageItem.startLoad = false;
			imageItem.item = item;
			download_image_liste.Add(imageItem);
		}
	}
}