using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock_imaginons : MonoBehaviour
{
	public Button Imaginons;
	private ChoiceController model;

    void Start()
    {
		model = FindObjectOfType<ChoiceController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (model != null)
        {
			Imaginons.interactable = model.Auto_evaluation;
        }
    }
}
