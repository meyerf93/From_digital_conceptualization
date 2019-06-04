using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginonsUpdate : MonoBehaviour
{
	private ChoiceController model;
    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		model.Imaginons_objet = true;
		int temp_value = model.Profils.Poussiere_actuel / 100;
		model.Profils.Etoiles_actuel += temp_value;
		model.Profils.Poussiere_actuel -= temp_value * 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
