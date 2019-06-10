using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Donwload_type
{
	Profil, Objet, Coherence_mat_outil, Coherence_mat_tech, Coherence_mat_just,
	Coherence_tech_outils, Taille, Forme, Materiaux, Outils, Technique,
	Justfification_materiel, Question, Current_State
}


[System.Serializable]
public class DownloadItem
{
	public string filePath;
	public bool isLoaded;
	public bool startLoad;
	public Donwload_type type;
	public object data;
}
