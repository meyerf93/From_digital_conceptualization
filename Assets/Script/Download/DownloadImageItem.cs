using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImageType { Objets, Taille, Forme, Justification, Outils, Technique, Materiaux, Categorie }

[System.Serializable]
public class DownloadImageItem
{
	public bool isLoaded;
	public bool startLoad;
	public Item item;
}
