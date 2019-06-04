using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteFile : MonoBehaviour
{
	private ChoiceController model;
	private void Start()
	{
		model = FindObjectOfType<ChoiceController>();
	}
	public void deleteSave()
	{
		model.reset = false;
		model.InitModel();
		SaveSystem.DeleteFile();
	}
}
