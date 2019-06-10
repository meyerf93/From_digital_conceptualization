using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ce script est lier au boutton de séllection des question dans la scène 
//Contexte_objet. Il va controler si la question correspondant au bouton
//à été rempli correctement. Il va alors changer la couleur du boutton si c'est 
//le cas.
public class questionExplain : MonoBehaviour
{
	private ChoiceController model;
	private Objets actual_objet;
	private Association_question actual_question;

	public Sprite noResponseSprite;
	public Sprite ResponseSprite;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		actual_objet = model.Objets.Find(r => r.Modification == true);
		actual_question = actual_objet.Association_Questions.Find(r => r.Question.Nom == this.name);      
    }

	private void Update()
	{
		bool isFull = !(actual_question.Reponse.CompareTo("") == 0);
        Image temp_image = this.GetComponent<Image>();
        if (isFull)
        {
            temp_image.sprite = ResponseSprite;
        }
        else
        {
            temp_image.sprite = noResponseSprite;
        }
	}
}
