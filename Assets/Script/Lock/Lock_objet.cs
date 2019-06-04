using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Lock_objet
{
	public bool isLocked;
	public string image_path;
	[System.NonSerialized]
	public Sprite image;
}
