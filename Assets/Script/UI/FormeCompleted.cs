using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ce script est lier au boutton "Forme" du préfabs Navigation. Il va vérifier
//dans le modèle que l'étape du choix de la forme à bien été effectuer et va
//changer la couleur du bouton si c'est le cas
public class FormeCompleted : MonoBehaviour
{
	private ChoiceController model;
	private Image actual_image;

	public Sprite FormeOn;
	public Sprite noFormeOn;

	// Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		actual_image = GetComponent<Image>();
    }

	private void Update()
	{
        if (model.Forme_objet)
        {
            actual_image.sprite = FormeOn;
        }
        else
        {
            actual_image.sprite = noFormeOn;
        }
	}
}
