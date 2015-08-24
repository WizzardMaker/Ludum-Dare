﻿using UnityEngine;using System.Collections;public class LevelGenerator : MonoBehaviour {	public Player player;	public int maxHouses,minHouses;	public int maxGroupMember, minGroupMember, maxGroups, minGroups;	public GameObject[] groupMemberPrefabs = new GameObject[1];	public float[] chanceGroupMember = new float[1];	public int level, prevLevel;	public int timeUntilWave, actTimeUntilWave;	public float xFloorSize, yFloorSize;	public GameObject[] housePrefabs = new GameObject[1];	void Update()	{		if(player.gameObject == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}		level = player.handler.level;		if(level != prevLevel)		{			for(int i = 0; i < chanceGroupMember.Length; i++)			{				chanceGroupMember[i] += 5 * level;            }			        }		prevLevel = level;		if(actTimeUntilWave < Time.time)		{			timeUntilWave += timeUntilWave < 75 ? timeUntilWave / 5 : 0;            actTimeUntilWave = (int)Time.time + timeUntilWave;			CreateGroups(maxGroups, minGroups);        }	}	// Use this for initialization	void Start () {		actTimeUntilWave = (int)Time.time + timeUntilWave;		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();		int temp = Random.Range(minHouses, maxHouses);		GameObject[] houses = new GameObject[temp];		for (int i = 0; i < temp; i++)		{			GameObject tempObject = (GameObject)Instantiate(housePrefabs[Random.Range(0, housePrefabs.Length)], new Vector3(Random.Range(-xFloorSize / 2, xFloorSize /2), 2.25f, Random.Range(-xFloorSize / 2, xFloorSize / 2)), Quaternion.identity);			houses[i] = tempObject;		}		FreeRooms(houses);		CreateGroups(minGroups,maxGroups);    }	int GetChance(float[] chances, float chance)	{		int returnId = -1;		for(int i = 0; i < chances.Length; i++)		{			if(chance < chances[i])			{				returnId = i;            }		}		return returnId;	}	void FreeRooms(GameObject[] houses)	{		for (int roomA = 0; roomA < houses.Length; roomA++)		{			for (int roomB = 0; roomB < houses.Length; roomB++)			{				if (roomA == roomB)					continue;				if (Vector3.Distance(houses[roomA].transform.position, houses[roomB].transform.position) <= 20)				{					houses[roomB].transform.position += (houses[roomA].transform.position - houses[roomB].transform.position) * -3;				}			}		}	}	void CreateGroups(int minGroups, int maxGroups)	{		Vector3[] groups = new Vector3[Random.Range(minGroups, maxGroups)];		for (int i = 0; i < groups.Length; i++)		{			groups[i] = new Vector3(Random.Range(-xFloorSize / 2 + 25, xFloorSize / 2 - 25), 0.5f, Random.Range(-xFloorSize / 2 + 25, xFloorSize / 2 - 25));			if (Vector3.Distance(groups[i], player.transform.position) < 15)				groups[i] += (groups[i] - player.transform.position) * 2;        }		for (int groupA = 0; groupA < groups.Length; groupA++)		{			for (int groupB = 0; groupB < groups.Length; groupB++)			{				if (groupA == groupB)					continue;				if (Vector3.Distance(groups[groupA], groups[groupB]) <= 20)				{					groups[groupB] += (groups[groupA] - groups[groupB]) * -3;				}			}		}				for (int group = 0; group < groups.Length; group++)		{			GameObject[] members = new GameObject[Random.Range(minGroupMember, maxGroupMember)];			for (int i = 0; i < members.Length; i++)			{				members[i] = (GameObject)Instantiate(groupMemberPrefabs[GetChance(chanceGroupMember,Random.Range(0,99))], new Vector3(Random.Range(0, 5), 0.5f, Random.Range(0, 5)) + groups[group], Quaternion.identity);			}			for (int groupMemberA = 0; groupMemberA < members.Length; groupMemberA++)			{				for (int groupMemberB = 0; groupMemberB < members.Length; groupMemberB++)				{					if (groupMemberA == groupMemberB)						continue;					if (members[groupMemberB] == null || members[groupMemberA] == null)						continue;					if (Vector3.Distance(members[groupMemberA].transform.position, members[groupMemberB].transform.position) <= 0.25)					{						members[groupMemberB].transform.position += ((members[groupMemberA].transform.position - members[groupMemberB].transform.position) + Vector3.right) * -2;					}				}			}		}	}}