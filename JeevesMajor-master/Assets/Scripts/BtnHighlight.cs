﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BtnHighlight : MonoBehaviour
{

	private Image img;
	private GameManager GM;
	private GameDataStore dataRef;
	[SerializeField] int Num;
	[SerializeField] int cost;
	private float timeStep;
	private float count;
	private bool hover;
	private Vector3 origin;
	// Use this for initialization


	void Start()
	{
		img = gameObject.GetComponent<Image>();
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
		dataRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GameDataStore>();
		origin = transform.position;
		timeStep = 5;
		count = 0;
		hover = false;

		if (Num != 0)
		{
			if (Num == 1)
			{
				cost = (int)GM.speedUp.z;
			}
			else if (Num == 2)
			{
				cost = (int)GM.taskSpeedUp.z;
			}
			else if (Num == 3)
			{
				cost = (int)GM.power3.x;
			}
			else if (Num == 4)
			{
				cost = (int)GM.power4.x;
			}
			else
			{
				cost = 0;
			}
		}


		De();
	}

	void OnAwake()
	{
		hover = false;

		De();
	}



	public void HiLight()
	{
		if (dataRef.Stamina > cost)
		{
			hover = true;
		}
	}


	private void FixedUpdate()
	{
		De();


	}


	public void DeHiLight()
	{
		hover = false;
	}

	private void De()
	{
		if (hover == false && cost != 0)
		{
			transform.localScale = new Vector3(2, 2, 1);
			if (dataRef.Stamina < cost)
			{
				img.color = Color.Lerp(img.color, Color.grey, 0.5f);
			}
			else
			{
				img.color = Color.Lerp(img.color, Color.yellow, 0.5f);
				Wobble();
			}
		}
		else
		{
			if (dataRef.Stamina >= cost)
			{


				img.color = Color.Lerp(img.color, Color.green, 0.5f);
				Wobble();
				transform.localScale = new Vector3(2 + 0.25f * Mathf.Sin(Time.time * 1), 2 + 0.25f * Mathf.Sin(Time.time * 1) + (0.25f * Mathf.Cos(Time.time * 10)), 0);
			}
		}
	}

	//https://forum.unity.com/threads/shake-an-object-from-script.138382/
	void Wobble()
	{

		transform.position += new Vector3(0.25f * Mathf.Sin(Time.time * 10), 0.25f * Mathf.Sin(Time.time * 10) + (0.25f * Mathf.Cos(Time.time * 10)), 0);
	}
}