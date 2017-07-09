using UnityEngine;

[CreateAssetMenu]
public class NpcInfo : ScriptableObject
{
	public string Name;
	public GameObject model;
	public float moveSpeed, forceOnPunch;

	public ItemSize favoriteItemSize;
	public int maxQtyInHands;
}
