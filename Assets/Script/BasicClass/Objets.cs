using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objets
{
	public int ID;
	public string Nom;
	public string Image_path_selected;
	public string Image_path_unselected;

	[System.NonSerialized]
	public Sprite Image_unselected;
	[System.NonSerialized]
	public Sprite Image_selected;

	public int min_etoiles;

	public Lock_objet Lock;

	public List<int> FormeActive;
	public List<int> FormeInactive;
	public List<int> TailleActive;
	public List<int> TailleInactive;
	public List<int> OutilsActif;
	public List<int> OutilsInactif;
	public List<int> TechniqueActive;
	public List<int> TechniqueInactive;

	public bool Modification;

	public List<Association_question> Association_Questions;

	public List<CoherenceMaterielJustification> CoherencesMaterielJustification;
	public List<CoherenceMaterielTechnique> CoherenceMaterielTechnique;
	public List<CoherenceMaterielOutils> CoherenceMaterielOutils;
	public List<CoherenceTechniqueOutils> CoherenceTechniqueOutils;

	public List<StateSousMateriaux> StateSousMateriauxes;
	public List<StateMateriaux> StateMateriauxes;
	public List<StateJustification> StateJustifications;

	public Objets()
	{

	}

	public Objets(Objets data)
	{
		ID = data.ID;
		Nom = data.Nom;
		Image_path_selected = data.Image_path_selected;
		Image_path_unselected = data.Image_path_unselected;

		min_etoiles = data.min_etoiles;

		Lock = new Lock_objet();
		if (min_etoiles != 0) Lock.isLocked = true;
		else Lock.isLocked = false;

		Lock.image_path = data.Lock.image_path;
		Lock.image = data.Lock.image;

		FormeActive = new List<int>();
		FormeActive = data.FormeActive;

		FormeInactive = new List<int>();
		FormeInactive = data.FormeInactive;

		TailleActive = new List<int>();
		TailleActive = data.TailleActive;

		TailleInactive = new List<int>();
		TailleInactive = data.TailleInactive;

		OutilsActif = new List<int>();
		OutilsActif = data.OutilsActif;

		OutilsInactif = new List<int>();
		OutilsInactif = data.OutilsInactif;

		TechniqueActive = new List<int>();
		TechniqueActive = data.TechniqueActive;

		TechniqueInactive = new List<int>();
		TechniqueInactive = data.TechniqueInactive;

		Modification = data.Modification;

		Association_Questions = new List<Association_question>();
		Association_Questions = data.Association_Questions;

		CoherencesMaterielJustification = new List<CoherenceMaterielJustification>();
		CoherencesMaterielJustification = data.CoherencesMaterielJustification;

		CoherenceMaterielTechnique = new List<CoherenceMaterielTechnique>();
		CoherenceMaterielTechnique = data.CoherenceMaterielTechnique;

		CoherenceMaterielOutils = new List<CoherenceMaterielOutils>();
		CoherenceMaterielOutils = data.CoherenceMaterielOutils;

		CoherenceTechniqueOutils = new List<CoherenceTechniqueOutils>();
		CoherenceTechniqueOutils = data.CoherenceTechniqueOutils;

		StateSousMateriauxes = new List<StateSousMateriaux>();
		StateSousMateriauxes = data.StateSousMateriauxes;

		StateMateriauxes = new List<StateMateriaux>();
		StateMateriauxes = data.StateMateriauxes;

		StateJustifications = new List<StateJustification>();
		StateJustifications = data.StateJustifications;
	}

}