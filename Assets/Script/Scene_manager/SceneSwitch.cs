using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{

	private ChoiceController model;
	private Button button;

	private void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		button = this.GetComponent<Button>();
		button.interactable = false;
		foreach (Objets element in model.Objets)
		{
			if (element.Modification)
			{
				button.interactable = true;
			}
		}
	}

	public void LoadScene()
	{

		SceneManager.LoadScene("Load");
	}

	public void SelectionObjet()
	{
		model.Auto_evaluation = false;
		model.Imaginons_objet = false;
		SceneManager.LoadScene("Selection_objet");
	}

	public void ContextObjet()
	{
		SceneManager.LoadScene("Context_objet");
	}


	public void FormeObjet()
	{
		SceneManager.LoadScene("Forme_objet");
	}

	public void MaterialSwitch()
	{
		SceneManager.LoadScene("Material");
	}

	public void FormeSwitch()
	{
		SceneManager.LoadScene("Forme");
	}

	public void TailleSwitch()
	{
		SceneManager.LoadScene("Taille");
	}

	public void OutilsSwitch()
	{
		SceneManager.LoadScene("Outils");
	}

	public void TechniqueSwitch()
	{
		SceneManager.LoadScene("Technique");
	}

	public void Evaluation()
	{
		SceneManager.LoadScene("Evaluation_choix");
	}

	public void ImaginonsObjet()
	{
		display_resume temp_resume = FindObjectOfType<display_resume>();
		if (temp_resume != null) temp_resume.save_score();
		SceneManager.LoadScene("Imaginons_objet");
	}

	public void ReponseQuestion(Button button)
	{
		Objets temp_objet = model.Objets.Find(r => r.Modification == true);
		foreach (Association_question element in temp_objet.Association_Questions)
		{
			string button_name = button.name;
			if (element.Question.Nom == button.name)
			{
				element.Modification = true;
			}
			else
			{
				element.Modification = false;
			}
		}

		SceneManager.LoadScene("Question_eciture");
	}
}
