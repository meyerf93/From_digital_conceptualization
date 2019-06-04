using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContexteCompleted : MonoBehaviour
{

	private ChoiceController model;
	private Objets Actual_Objets;
	private Image actual_image;

	public Sprite ContexteSprite;
	public Sprite noContexteSprite;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		Actual_Objets = model.Objets.Find(r => r.Modification == true);
		actual_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		bool temp_is_on = true;

		foreach(Association_question temp_question in Actual_Objets.Association_Questions)
		{
			temp_is_on = temp_question.Reponse.CompareTo("") != 0;
			if(!temp_is_on)
			{
				break;
			}
		}

		if (temp_is_on) 
		{
			actual_image.sprite = ContexteSprite;
		}
		else
		{
			actual_image.sprite = noContexteSprite;
		}
    }
}
