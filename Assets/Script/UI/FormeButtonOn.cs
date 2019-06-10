using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ce script est lier au bouttons Taille, Frome, Matériaux, Outils et technique
//de la scène Forme_objet. Il permet de vérifier si une étape bien les étapes
//nécesasire pour chacun de ces boutton à été effectuer. Si c'est le cas
//on change alors la couleurs du boutton qui correspond à l'étape terminer
public class FormeButtonOn : MonoBehaviour
{
	private ChoiceController model;
	private Image actualImage;
	public Sprite FormeButtonIsOn;
	public Sprite noFormeButtonIsOn;

	// Start is called before the first frame update
	void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		actualImage = GetComponent<Image>();
	}

	private void Update()
	{
		if (model.Forme_validation[0] && this.name == "Forme")
        {
            actualImage.sprite = FormeButtonIsOn;
        }
        else if (model.Forme_validation[1] && this.name == "Outils")
        {
            actualImage.sprite = FormeButtonIsOn;
        }
        else if (model.Forme_validation[2] && this.name == "Taille")
        {
            actualImage.sprite = FormeButtonIsOn;
        }
        else if (model.Forme_validation[3] && this.name == "technique")
        {
            actualImage.sprite = FormeButtonIsOn;
        }
        else if (model.Forme_validation[4] && this.name == "matériel")
        {
            actualImage.sprite = FormeButtonIsOn;
        }
        else
        {
            actualImage.sprite = noFormeButtonIsOn;
        }
	}
}
