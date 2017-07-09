﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class Objective
{
	public ItemInfo itemToGet;
	public int qtyTaken;
	public int qtyToGet;
	public bool isComplete = false;
}

public class ObjectivesManager : MonoBehaviour 
{
	public static ObjectivesManager Instance;
	
	public int numberOfObjectives;
	public List<Objective> objectivesToComplete = new List<Objective>();

	public List<Item> itemsTaken = new List<Item>();
	
	private void Awake()
	{
		Instance = this;
	}

	public void CreateObjectives()
	{
		for (int i = 0; i < numberOfObjectives; i++)
		{
			objectivesToComplete.Add(RollObjective());
		}
	}

	public Objective RollObjective()
	{
		Objective newObjective = new Objective();
		GameObject[] itemsInStore = ItemDatabase.Instance.items;
		List<ItemInfo> availableItems = new List<ItemInfo>();

		foreach (var item in itemsInStore)
		{
			availableItems.Add(item.GetComponent<Item>().item);
		}

		newObjective.itemToGet = availableItems.OrderBy(i => Random.value).FirstOrDefault();
		availableItems.Remove(newObjective.itemToGet);

		newObjective.qtyToGet = Random.Range(5,15);

		return newObjective;
	}

	public int CheckObjectives()
	{
		int completedObjectives = 0;
		
		foreach (Objective obj in objectivesToComplete)
		{
			foreach (Item item in itemsTaken)
			{
				if(item.item.itemName == obj.itemToGet.itemName)
				{
					obj.qtyTaken++;
				}
			}

			if(obj.qtyTaken >= obj.qtyToGet)
			{
				obj.isComplete = true;
				completedObjectives++;
			}
		}

		return completedObjectives;
	}	
}
