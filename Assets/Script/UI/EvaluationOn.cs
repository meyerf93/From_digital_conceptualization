using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
		if(model.Auto_evaluation)
		{
			actual_image.sprite = EvaluationIsOn;
		}
		else
		{
			actual_image.sprite = noEvaluationIsOn;
		}
    }
}
