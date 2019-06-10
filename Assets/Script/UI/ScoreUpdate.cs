using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ce script est lier au préfab du score et permet de mettre a jour l'UI
//du score pour que l'information de la vue corresponde à l'information du modèle
public class ScoreUpdate : MonoBehaviour
{
	public Text Poussiere_text;
	public Text Etoile_text;

	private ChoiceController model;

	void Start()
	{
		model = FindObjectOfType<ChoiceController>();

		if (model != null)
		{
			Poussiere_text.text = model.Profils.Poussiere_actuel.ToString();
			Etoile_text.text = model.Profils.Etoiles_actuel.ToString();
		}
	}

	void Update()
	{
		Poussiere_text.text = model.Profils.Poussiere_actuel.ToString();
		Etoile_text.text = model.Profils.Etoiles_actuel.ToString();
	}
}
