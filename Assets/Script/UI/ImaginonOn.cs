using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImaginonOn : MonoBehaviour
{
	private ChoiceController model;
	private Image actual_image;

	public Sprite ImaginonIsOn;
	public Sprite noImaginonIsOn;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		actual_image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		if(model.Imaginons_objet)
		{
			actual_image.sprite = ImaginonIsOn;
		}
		else
		{
			actual_image.sprite = noImaginonIsOn;
		}
    }
}
