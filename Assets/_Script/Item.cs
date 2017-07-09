using UnityEngine;

public class Item : MonoBehaviour 
{
	public ItemInfo item;
	
	[HideInInspector]
	public Rigidbody rb;

	public void Init()
	{
		rb = GetComponent<Rigidbody>();
	}
}
