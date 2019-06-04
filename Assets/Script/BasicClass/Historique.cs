using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Historique
{
	public int ID;
	public int Objet;
	public List<int> FormeActive;
	public List<int> FormeInactive;
	public List<int> TailleActive;
	public List<int> TailleInactive;
	public List<int> MateriauxActif;
	public List<int> MateriauxInactif;
	public List<int> SousMateriauxActif;
	public List<int> SousMateriauxInactif;
	public List<int> OutilsActif;
	public List<int> OutilsInactif;
	public List<int> TechniqueActive;
	public List<int> TechniqueInactive;
	public List<int> AssociationJustification;    
}
