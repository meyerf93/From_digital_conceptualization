using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTexte : MonoBehaviour
{
	private ChoiceController model;
	private Association_question temp_question;

    void Start()
    {
		model = FindObjectOfType<ChoiceController>();

		if(model != null)
		{
			Objets tmep_objet = model.Objets.Find(r => r.Modification == true);

			temp_question = tmep_objet.Association_Questions.Find(r => r.Modification == true);
            Text temp_text = this.GetComponentInChildren<Text>();
            if (temp_question.Reponse != null && temp_question.Reponse != "")
            {
                temp_text.text = temp_question.Reponse;
            }
		}
    }

	public void Save_content(string contenu)
	{
		if(temp_question != null)
		{
			if(contenu != ""){
				temp_question.Reponse = contenu;
			}
		}
	}
}
