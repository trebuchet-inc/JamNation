using System;
using UnityEngine;

public enum ItemSize
{
	Small,
	Medium,
	Large,
	VeryLarge
} 

public enum ItemType
{
	Electronic,
	Food,
	Misc
}

[CreateAssetMenu]
public class ItemInfo : ScriptableObject 
{
	public string itemName;
	public int scoreValue;

	public ItemSize itemSize;
	public ItemType itemType;
}
