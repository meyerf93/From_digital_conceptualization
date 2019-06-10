using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Materiaux
{
	public int ID;
	public string Nom;
	public string Image_path_selected;
	public string Image_path_unselected;
	[System.NonSerialized]
	public Sprite Image_unselected;
	[System.NonSerialized]
	public Sprite Image_selected;
	public int SousCategorie;
}
