using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	public static void DeleteFile()
	{

		string path = path = Application.persistentDataPath + "/";

        // check if file exists
		if (!File.Exists(path+ChoiceController.CURRENT_STATE_FILENAME))
        {
			Debug.LogError("file don't exist");
        }
        else
        {
			File.Delete(path+ChoiceController.CURRENT_STATE_FILENAME);

            RefreshEditorProjectWindow();
        }

		// check if file exists
		if (!File.Exists(path + ChoiceController.PROFILS_FILENAME))
        {
            Debug.LogError("file don't exist");
        }
        else
        {
			File.Delete(path + ChoiceController.PROFILS_FILENAME);

            RefreshEditorProjectWindow();
        }

	}
    


    public static void RefreshEditorProjectWindow()
    {
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif
    }

	public static void SaveProfils(Profils file, string file_path)
	{
		BinaryFormatter formatter = new BinaryFormatter();

		string path = path = Application.persistentDataPath + "/" + file_path;

        if (!Directory.Exists(path))
        {
            string[] temp_path = file_path.Split('/');
            Debug.Log(temp_path[0]);

            string directory = Application.persistentDataPath + "/" + temp_path[0];
            Debug.Log(directory);
            Directory.CreateDirectory(directory);
        }

        Debug.Log(path);

        FileStream stream = File.Create(path);

		Profils data = new Profils();
		data = file;
        formatter.Serialize(stream, data);
        stream.Close();
	}

	public static void SaveObjets(List<Objets> list_objets, string file_path)
	{
		BinaryFormatter formatter = new BinaryFormatter();
        
		string path = "";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
			//Debug.Log("in windows or mac platform");
			//path = @"file://" + Application.streamingAssetsPath + "/" + file_path;
			path = Application.persistentDataPath + "/" + file_path;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            //Debug.Log("in android platform");
            //path = @"jar:file://" + Application.dataPath + "!/assets/" + file_path;
			path = Application.persistentDataPath + "/" + file_path;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //Debug.Log("in iphone plateform");
            //path = @"file://" + Application.streamingAssetsPath + "/" + file_path;
			path = Application.persistentDataPath + "/" + file_path;
        }

		if (!Directory.Exists(path))
		{
			string[] temp_path = file_path.Split('/');
			Debug.Log(temp_path[0]);

			string directory = Application.persistentDataPath + "/"+ temp_path[0];
			Debug.Log(directory);
			Directory.CreateDirectory(directory);
		} 
       

		Debug.Log(path);
        
		FileStream stream = File.Create(path);

		List<Objets> data = new List<Objets>();
		data = list_objets;
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static Profils LoadProfils(string file_path)
	{
		string path = path = Application.persistentDataPath + "/" + file_path;

        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Open);

			Profils data = formatter.Deserialize(stream) as Profils;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
	}

	public static List<Objets> LoadObjets(string file_path)
	{
		string path = "";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            //Debug.Log("in windows or mac platform");
			//path = @"file://" + Application.streamingAssetsPath + "/" + file_path;
			path = Application.persistentDataPath + "/" + file_path;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            //Debug.Log("in android platform");
			//path = @"jar:file://" + Application.dataPath + "!/assets/" + file_path;
			path = Application.persistentDataPath + "/" + file_path;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //Debug.Log("in iphone plateform");
			//path = @"file://" + Application.streamingAssetsPath + "/" + file_path;
			path = Application.persistentDataPath + "/" + file_path;
        }

		Debug.Log(path);

		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = File.Open(path, FileMode.Open);

			List<Objets> data = formatter.Deserialize(stream) as List<Objets>;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}

}
