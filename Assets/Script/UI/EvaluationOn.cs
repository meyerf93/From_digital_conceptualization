using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ce script est lier au bouton "Evaluation" du préfabs Navigation
//il permet de voir si tout les éléments nécessaire on été sélectionner pour 
//passer à la phase d'évaluation. Si c'est le cas on change la couleur du boutton
//pour indique au utilisateurs qu'ils peuvent passer à la prochaine étapes
public class EvaluationOn : MonoBehaviour
{
	private ChoiceController model;
	private Image actual_image;

	public Sprite EvaluationIsOn;
	public Sprite noEvaluationIsOn;

	// Start is called before the first frame update
	void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		actual_image = GetComponent<Image>();
	}

	private void Update()
	{
		if (model.Auto_evaluation)
        {
            actual_image.sprite = EvaluationIsOn;
        }
        else
        {
            actual_image.sprite = noEvaluationIsOn;
        }
	}
}
