using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ce script doit être utiliser avec le "Contexte de l'objet" du préfab "Navigation"
//Il va controler dans le modèle que le contexte est bien réondu
//il change la couleur du bouton si t'elle est le cas 
public class ContexteCompleted : MonoBehaviour
{

	private ChoiceController model;
	private Image actual_image;

	public Sprite ContexteSprite;
	public Sprite noContexteSprite;

	// Start is called before the first frame update
	void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		actual_image = GetComponent<Image>();
	}

	private void Update()
	{
		if (model.Contexte_objet)
        {
            actual_image.sprite = ContexteSprite;
        }
        else
        {
            actual_image.sprite = noContexteSprite;
        }
	}
}
