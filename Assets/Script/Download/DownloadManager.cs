using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DBXSync;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class DownloadManager : MonoBehaviour
{
	public static DownloadManager downloadManager;

	public int Caller;
	private LoadDataBase dataBase;
	private ChoiceController model;

    
    public void Awake()
    {
		if (downloadManager == null)
        {
			downloadManager = this;
            DontDestroyOnLoad(gameObject);

			dataBase = FindObjectOfType<LoadDataBase>();
            model = FindObjectOfType<ChoiceController>();

        }
		else if (downloadManager != this)
        {
            Destroy(gameObject);
        }
    }

	public static byte[] SerializeObject<T>(T serializableObject)
	{
		T obj = serializableObject;

		IFormatter formatter = new BinaryFormatter();
		using (MemoryStream stream = new MemoryStream())
		{
			formatter.Serialize(stream, obj);
			return stream.ToArray();
		}
	}

	public void UploadProfilsOffline(List<DownloadItem> list_item)
    {
        foreach (DownloadItem element in list_item)
        {
            if (!element.startLoad)
            {
                element.startLoad = true;
                Debug.Log(element.filePath);
				SaveSystem.SaveProfils(element.data as Profils, element.filePath);
                element.isLoaded = true;
            }
        }
    }

	public void UploadCurrentStateOffline(List<DownloadItem> list_item)
	{
		foreach(DownloadItem element in list_item)
		{
			if(!element.startLoad)
			{
				element.startLoad = true;
				Debug.Log(element.filePath);
				SaveSystem.SaveObjets(element.data as List<Objets>, element.filePath);
				element.isLoaded = true;
			}
		}
	}

	public void UpodloadCurrentState(List<DownloadItem> liste_item)
	{
		foreach (DownloadItem element in liste_item)
		{
			if (!element.startLoad)
			{
				byte[] temp_data = SerializeObject<List<Objets>>(element.data as List<Objets>);

				Caller++;
				DropboxSync.Main.UploadFile("/StreamingAssets/" + element.filePath, temp_data, (res) =>
				{
					if (res.error != null)
					{
						Debug.LogError("Error uploading file: " + res.error.ErrorDescription);
					}
					else
					{
						Debug.Log("File is uploaded!");

						element.isLoaded = true;

					}
				}, (progress) =>
				{
					Debug.Log("Uploading file... " + Mathf.RoundToInt(progress * 100) + "%");
				});
			}
		}
	}

	public static T DeserializeObject<T>(byte[] serilizedBytes)
	{
		IFormatter formatter = new BinaryFormatter();
		using (MemoryStream stream = new MemoryStream(serilizedBytes))
		{
			return (T)formatter.Deserialize(stream);
		}
	}

	public void DownloadProfilOffline(List<DownloadItem> liste_download)
    {
        foreach (DownloadItem element in liste_download)
        {
            if (!element.startLoad)
            {
                element.startLoad = true;
                Debug.Log(element.filePath);
				Profils temp_liste = SaveSystem.LoadProfils(element.filePath);
				if (temp_liste != null) model.Profils = temp_liste;
                element.isLoaded = true;
            }
        }
    }

	public void DownloadCurrentStateOffline(List<DownloadItem> liste_download)
	{
		foreach(DownloadItem element in liste_download)
		{
			if(!element.startLoad)
			{
				element.startLoad = true;
				Debug.Log(element.filePath);
				List<Objets> temp_liste = SaveSystem.LoadObjets(element.filePath);
				if (temp_liste != null) model.Objets = temp_liste;
				element.isLoaded = true;
			}
		}
	}

	public void DownloadCurrentState(List<DownloadItem> liste_download)
	{
		foreach (DownloadItem element in liste_download)
		{
			if (!element.startLoad)
			{
				element.startLoad = true;
				Caller++;
				//Debug.Log("/StreamingAssets/" + image.item.Image_path_selected);
				DropboxSync.Main.GetFileAsBytes("/StreamingAssets/" + element.filePath, (res) =>
				  {
					  if (res.error != null)
					  {
						  Debug.LogError("Error getting text string: " + res.error.ErrorDescription);
						  Debug.Log("/StreamingAssets/" + element.filePath);
					  }
					  else
					  {
						  element.data = DeserializeObject<List<Objets>>(res.data);
                        
						  dataBase.Load_current_state(element.data as List<Objets>);
                        
						  element.isLoaded = true;
					  }
				  }, (progress) =>
				  {
					  Debug.Log("Downaload progress: " + progress.ToString());
				}, receiveUpdates: true, useCachedIfOffline: true, useCachedFirst: true);

			}
		}
	}

	public void DownloadImageOffline(List<DownloadImageItem> liste_download)
	{
		foreach(DownloadImageItem image in liste_download)
		{
			if(!image.startLoad)
			{
				image.startLoad = true;

				image.item.Image_selected = dataBase.LoadStreamingAssetSprite(image.item.Image_path_selected);
				image.item.Image_unselected = dataBase.LoadStreamingAssetSprite(image.item.Image_path_unselected);

				image.isLoaded = true;
			}
		}
	}

	public void DownloadImage(List<DownloadImageItem> liste_downaload)
	{
		foreach (DownloadImageItem image in liste_downaload)
		{
			if (!image.startLoad)
			{
				Caller++;
				Caller++;
				bool image_unselected = false;
				bool image_selected = false;
				image.startLoad = true;
				//Debug.Log("/StreamingAssets/" + image.item.Image_path_selected);
				DropboxSync.Main.GetFile<Texture2D>("/StreamingAssets/" + image.item.Image_path_selected, (res) =>
				  {
					  if (res.error != null)
					  {
						  Debug.LogError("Error getting text string: " + res.error.ErrorDescription);
					  }
					  else
					  {
						//Debug.Log("Received image from Dropbox!");
						Sprite temp_sprite = Sprite.Create(res.data as Texture2D,
															  new Rect(0, 0, res.data.width, res.data.height), Vector2.zero);

						image.item.Image_selected = temp_sprite;
						image_selected = true;
						if (image_unselected && image_selected) image.isLoaded = true;
					  }
				  }, (progress) =>
				  {
					  Debug.Log("Downaload progress: " + image.item.Image_path_selected + progress.ToString());
				  }, receiveUpdates: true, useCachedIfOffline: true, useCachedFirst: true);

				StartCoroutine(Timer(5));

				//Debug.Log("/StreamingAssets/" + image.item.Image_path_selected);
				DropboxSync.Main.GetFile<Texture2D>("/StreamingAssets/" + image.item.Image_path_unselected, (res) =>
				{
					if (res.error != null)
					{
						Debug.LogError("Error getting text string: " + res.error.ErrorDescription);
						Debug.LogError("path : " + "/StreamingAssets/" + image.item.Image_path_unselected);
						Debug.LogError("item name : " + image.item.Nom);
					}
					else
					{
						//Debug.Log("Received image from Dropbox!");
						Sprite temp_sprite = Sprite.Create(res.data as Texture2D,
															new Rect(0, 0, res.data.width, res.data.height), Vector2.zero);

						image.item.Image_unselected = temp_sprite;
						image_unselected = true;
						if (image_unselected && image_selected) image.isLoaded = true;

					}
				}, (progress) =>
				{
					Debug.Log("Downaload progress: " + image.item.Image_path_selected + progress.ToString());
				}, receiveUpdates: true, useCachedIfOffline: true, useCachedFirst: true);

			}

		}

	}

	IEnumerator Timer(int value)
    {
		yield return new WaitForSeconds(value);
    }

	public void DownloadFileOffline(List<DownloadItem> liste_download)
	{
		DownloadItem objet = liste_download.Find(r => r.type == Donwload_type.Objet);

        foreach (DownloadItem item in liste_download)
        {
            if (item.type == Donwload_type.Objet)
            {
                if (!item.startLoad)
                {
                    item.startLoad = true;

					string temp_file = dataBase.LoadStreamingAssetText(item.filePath);
					temp_file = temp_file.Replace("\r", "");

					string[] file_line = Regex.Split(temp_file, "\n");

                    Load_thing(item, file_line);
     
					item.isLoaded = true;
                }
            }
            else
            {
                if (objet.isLoaded)
                {
                    if (!item.startLoad)
                    {
                        item.startLoad = true;

						string temp_file = dataBase.LoadStreamingAssetText(item.filePath);
                        temp_file = temp_file.Replace("\r", "");

                        string[] file_line = Regex.Split(temp_file, "\n");

                        Load_thing(item, file_line);

						item.isLoaded = true;
                    }
                }
            }
        }
	}

	public void DownloadFile(List<DownloadItem> liste_download)
	{
		DownloadItem objet = liste_download.Find(r => r.type == Donwload_type.Objet);

		foreach (DownloadItem item in liste_download)
		{
			if (item.type == Donwload_type.Objet)
			{
				if (!item.startLoad)
				{
					Caller++;
					item.startLoad = true;
					DropboxSync.Main.GetFile<string>("/StreamingAssets/" + item.filePath, (res) =>
					{
						if (res.error != null)
						{
							Debug.LogError("Error getting text string: " + res.error.ErrorDescription);
						}
						else
						{
							Debug.Log("Received image from Dropbox!");

							res.data = res.data.Replace("\r", "");

							string[] file_line = Regex.Split(res.data, "\n");

							Load_thing(item, file_line);

							item.isLoaded = true;

						}
					}, (progress) =>
					{
						Debug.Log("Downaload progress: " + item.type + progress.ToString());
					}, receiveUpdates: true, useCachedIfOffline: true, useCachedFirst: true);
				}
			}
			else
			{
				if (objet.isLoaded)
				{
					if (!item.startLoad)
					{
						Caller++;
						item.startLoad = true;
						DropboxSync.Main.GetFile<string>("/StreamingAssets/" + item.filePath, (res) =>
						{
							if (res.error != null)
							{
								Debug.LogError("Error getting text string: " + res.error.ErrorDescription);
							}
							else
							{
								Debug.Log("Received file from Dropbox!");

								res.data = res.data.Replace("\r", "");

								string[] file_line = Regex.Split(res.data, "\n");

								Load_thing(item, file_line);

								item.isLoaded = true;

							}
						}, (progress) =>
						{
							Debug.Log("Downaload progress: " + item.type + progress.ToString());
						}, receiveUpdates: true, useCachedIfOffline: true, useCachedFirst: true);
					}
				}
			}
		}
	}

	private void Load_thing(DownloadItem item, string[] file)
	{
		switch (item.type)
		{
			case Donwload_type.Objet:
				item.data = dataBase.load_Objets(file);
				break;
			case Donwload_type.Profil:
				item.data = dataBase.load_Profils(file);
				break;
			case Donwload_type.Coherence_mat_outil:
				dataBase.load_Coherence_Materiel_Outils(file);
				break;
			case Donwload_type.Coherence_mat_tech:
				dataBase.load_Coherence_Materiel_Technique(file);
				break;
			case Donwload_type.Coherence_mat_just:
				dataBase.load_Coherence_Materiel_Justification(file);
				break;
			case Donwload_type.Coherence_tech_outils:
				dataBase.load_Coherence_Technique_Outils(file);
				break;
			case Donwload_type.Taille:
				item.data = dataBase.load_Taille(file);
				break;
			case Donwload_type.Forme:
				dataBase.load_Item(file, model.Forme);
				item.data = model.Forme;
				break;
			case Donwload_type.Materiaux:
				item.data = dataBase.load_Materiaux(file);
				break;
			case Donwload_type.Outils:
				dataBase.load_Item(file, model.Outils);
				item.data = model.Outils;
				break;
			case Donwload_type.Technique:
				dataBase.load_Item(file, model.Technique);
				item.data = model.Technique;
				break;
			case Donwload_type.Justfification_materiel:
				dataBase.load_Item(file, model.JustificationMateriel);
				item.data = model.JustificationMateriel;
				break;
			case Donwload_type.Question:
				item.data = dataBase.load_questions(file);
				break;
			default:
				break;
		}
	}
}
