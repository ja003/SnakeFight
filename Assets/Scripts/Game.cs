using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField]
	private NetworkMessanger networkMessanger;
	[SerializeField]
	private Playground playground;

	[SerializeField]
	private Snake snakePrefab;
	[SerializeField]
	private int snakeCount;
	private List<Snake> snakes = new List<Snake>();

	[SerializeField]
	private PickableObjectGenerator objectGenerator;
	[SerializeField]
	private ObstacleGenerator obstacleGenerator;

	[SerializeField]
	private bool stopAutoEvaluation;

	private float lastActionTime;
	private bool gameStarted;

	void Start()
	{
		playground.Init();
		obstacleGenerator.Init();
		//networkMessanger.Setup();

		for(int i = 1; i <= snakeCount; i++)
		{
			Snake newSnake = Instantiate(snakePrefab);
			PlaygroundField startField = i == 1 ? 
				playground.GetField(3, 8) : playground.GetField(3, 3);
			newSnake.Init(i, networkMessanger, startField);
			snakes.Add(newSnake);
		}

		StartCoroutine(StartDebug());
		gameStarted = true;
	}

	IEnumerator StartDebug()
	{
		while(true)
		{
			yield return new WaitForSeconds(1f);
			float timeDiff = Time.time - lastActionTime;
			//Debug.Log($"Tick {(int)timeDiff}");
		}
	}


	void Update()
	{
		if(!gameStarted)
			return;

		float timeDiff = Time.time - lastActionTime;

		bool allSnakesConfirmed = snakes.TrueForAll(s => s.ActionsConfirmed);

		if((!stopAutoEvaluation && timeDiff > 3) || allSnakesConfirmed)
		{
			EvaluateTurn();
		}

	}

	private void EvaluateTurn()
	{
		lastActionTime = Time.time;
		foreach(Snake snake in snakes)
		{
			snake.Evaluate();
		}
		objectGenerator.GenerateObjects();
	}

	public void OnReceiveConfirmMessage(SnakeConfirmMessage pMessage)
	{
		Snake snake = GetSnake(pMessage.snakeId);
		snake.OnReceiveConfirmMessage(pMessage);
	}

	private Snake GetSnake(int pSnakeId)
	{
		return snakes[pSnakeId - 1];
	}
}

public enum EDirection
{
	None,

	Up,
	Right,
	Down,
	Left,
}