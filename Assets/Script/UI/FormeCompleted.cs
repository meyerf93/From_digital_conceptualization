using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormeCompleted : MonoBehaviour
{

	private ChoiceController model;
	private Image actual_image;

	public Sprite FormeOn;
	public Sprite noFormeOn;

	// Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		actual_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		if(model.Forme_objet) 
		{
			actual_image.sprite = FormeOn;
		}
		else 
		{
			actual_image.sprite = noFormeOn;
		}
    }
}
