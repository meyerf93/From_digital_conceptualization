using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ce script est lier au boutton "Imaginons" du préfabs Navigation. Il va vérifier
//dans le modèle que l'étape du choix de la forme à bien été effectuer et va
//changer la couleur du bouton si c'est le cas
public class ImaginonOn : MonoBehaviour
{
	private ChoiceController model;
	private Image actual_image;

	public Sprite ImaginonIsOn;
	public Sprite noImaginonIsOn;

	// Start is called before the first frame update
	void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		actual_image = GetComponent<Image>();
	}

	private void Update()
	{
		if (model.Imaginons_objet)
        {
            actual_image.sprite = ImaginonIsOn;
        }
        else
        {
            actual_image.sprite = noImaginonIsOn;
        }
	}
}
