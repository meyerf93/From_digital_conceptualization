using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormeButtonOn : MonoBehaviour
{
	private ChoiceController model;
	private Image actualImage;
	public Sprite FormeButtonIsOn;
	public Sprite noFormeButtonIsOn;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		actualImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
		if (model.Forme_validation[0] && this.name == "Forme")
		{
			actualImage.sprite = FormeButtonIsOn;
		}
		else if (model.Forme_validation[1] && this.name == "Outils")
		{
			actualImage.sprite = FormeButtonIsOn;
		}
		else if (model.Forme_validation[2] && this.name == "Taille") 
		{
			actualImage.sprite = FormeButtonIsOn;
		}
		else if( model.Forme_validation[3] && this.name == "technique") 
		{
			actualImage.sprite = FormeButtonIsOn;
		}
		else if ( model.Forme_validation[4] && this.name == "matériel") 
		{
			actualImage.sprite = FormeButtonIsOn;
		}
		else
		{
			actualImage.sprite = noFormeButtonIsOn;
		}
    }
}
