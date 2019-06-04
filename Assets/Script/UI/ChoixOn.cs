using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoixOn : MonoBehaviour
{
	private ChoiceController model;
	private Image actual_image;
    
	public Sprite ChoixIsOn;
	public Sprite noChoixIsOn;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		actual_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		if(model.Choix_objet)
		{
			actual_image.sprite = ChoixIsOn;
		}
		else
		{
			actual_image.sprite = noChoixIsOn;
		}
    }
}
