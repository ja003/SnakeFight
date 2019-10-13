using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

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

	private Snake localSnake;

	[SerializeField]
	private PickableObjectGenerator objectGenerator;
	[SerializeField]
	private ObstacleGenerator obstacleGenerator;

	[SerializeField]
	private bool stopAutoEvaluation;

	private float lastActionTime;
	private bool gameStarted;

	[SerializeField]
	private GameUI ui;

	[SerializeField]
	private ItemManager itemManager;

	[SerializeField]
	private PowerUpManager powerUpManager;


	[SerializeField]
	private InputController input;

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
			newSnake.Init(i, i == 1, networkMessanger, startField, ui);

			//newSnake.AddItem(itemManager.GenerateItem(EItemId.Gun, 2));
			//newSnake.AddItem(itemManager.GenerateItem(EItemId.Knife, 1));

			//newSnake.AddPowerUp(powerUpManager.GeneratePowerUp(EPowerUpId.Specal, 1));
			//newSnake.AddPowerUp(powerUpManager.GeneratePowerUp(EPowerUpId.Specal2, 1));
			snakes.Add(newSnake);
		}
		
		localSnake = GetSnake(1);

		snakes[1].AddItem(itemManager.GenerateItem(EItemId.Gun, 1));
		snakes[1].AddItem(itemManager.GenerateItem(EItemId.Knife, 1));
		snakes[1].AddPowerUp(powerUpManager.GeneratePowerUp(EPowerUpId.Specal2, 1));

		snakes[0].AddPowerUp(powerUpManager.GeneratePowerUp(EPowerUpId.Specal, 1));
		snakes[0].AddItem(itemManager.GenerateItem(EItemId.Gun, 2));
		snakes[0].AddItem(itemManager.GenerateItem(EItemId.Gun, 2));


		StartCoroutine(StartDebug());
		gameStarted = true;

		input.Init(
			SetDirectionToLocalSnake,
			SetConfirmToLocalSnake,
			SetTargetToLocalSnake,
			SwapLocalSnake,
			CancelAction);

		SetDirectionToLocalSnake(EDirection.Right);

		ui.Init();
	}

	private void SetDirectionToLocalSnake(EDirection pDirection)
	{
		localSnake.SetDirection(pDirection);
		ui.ActionPanel.OnSetDirection(pDirection);
	}

	private void CancelAction(EAction pAction)
	{
		localSnake.CancelAction(pAction);
		ui.ActionPanel.OnCancelAction(pAction);
	}

	private void SetTargetToLocalSnake(PlaygroundField pTargetField)
	{
		localSnake.SetTarget(pTargetField);
	}

	private void SetConfirmToLocalSnake()
	{
		localSnake.ActionsConfirmed = true;
		if(Debugger.LocalDebug)
		{
			foreach(Snake snake in snakes)
			{
				snake.ActionsConfirmed = true;
			}
		}
	}

	private void SwapLocalSnake()
	{
		int index = snakes.IndexOf(localSnake);
		localSnake = snakes[(index + 1) % snakes.Count];
		Debug.Log("Local snake = " + localSnake);
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

		bool allSnakesConfirmed = snakes.Count > 0 && snakes.TrueForAll(s => s.ActionsConfirmed);

		if((!stopAutoEvaluation && timeDiff > 3) || allSnakesConfirmed)
		{
			StartCoroutine(EvaluateTurn());
		}

	}

	private IEnumerator EvaluateTurn()
	{
		lastActionTime = Time.time;
		input.IsEvaluating = true;
		ui.OnEvaluationStart();

		foreach(Snake snake in snakes)
		{
			snake.OnEvaluationStart();
		}

		Debug.Log("Evaluate - MOVE");
		foreach(Snake snake in snakes)
		{
			snake.EvaluateMove();
		}
		yield return new WaitForSeconds(1);

		Debug.Log("Evaluate - ITEM");
		foreach(Snake snake in snakes)
		{
			snake.EvaluateItem();
		}
		yield return new WaitForSeconds(1);

		Debug.Log("Evaluate - POWERUP");
		foreach(Snake snake in snakes)
		{
			snake.EvaluatePowerUp();
		}
		yield return new WaitForSeconds(1);

		Debug.Log("Evaluation - FINISHED");
		foreach(Snake snake in snakes)
		{
			snake.OnEvaluationFinished();
		}

		objectGenerator.GenerateObjects();
		ui.ActionPanel.OnEvaluateTurn();
		input.IsEvaluating = false;
		ui.OnEvaluationFinished();
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