using UnityEngine;

[CreateAssetMenu]
public class NpcInfo : ScriptableObject
{
	public GameObject model;
	public float moveSpeed;
	public ItemSize favoriteItemSize;
	public int maxQtyInHands;
}
