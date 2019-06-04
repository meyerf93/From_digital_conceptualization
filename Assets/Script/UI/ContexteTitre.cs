using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContexteTitre : MonoBehaviour
{
    private ChoiceController model;

    void Start()
    {
		model = FindObjectOfType<ChoiceController>();

		if (model != null)
		{
			Objets temp_objet = model.Objets.Find(r => r.Modification == true);
			Association_question temp_question = temp_objet.Association_Questions.Find(r => r.Modification == true);
			Text temp_text = GetComponent<Text>();
            temp_text.text = temp_question.Question.Description; 
		}
    }
}
