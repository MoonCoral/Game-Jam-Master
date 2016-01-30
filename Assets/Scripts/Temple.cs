﻿using UnityEngine;
using System.Collections;

public class Temple : MonoBehaviour {

	private int state;
	private float time;
	private bool elapsing;
	private bool paused;

	private Player player;
	private Overlay overlay;
	
	void Awake()
	{
		player = FindObjectOfType<Player>();
	    overlay = FindObjectOfType<Overlay>();
		
	}

    void Start()
    {
        Restart();
    }

	void Update ()
	{
		if (Time.realtimeSinceStartup >= time && elapsing)
		{
			StopTime();
			time = 0.0f;
			Failure();
		}

        /*
		if ( win)
	    {
	        Success();
	    }
        */
	}

	public void StartTime()
	{
		time += Time.realtimeSinceStartup;
		this.elapsing = true;
	}

	public void StopTime()
	{
		time -= Time.realtimeSinceStartup;
		this.elapsing = false;
	}

	public float SecondsLeft()
	{
		if ( !elapsing )
		{
			return time;
		}
		else if (state < 3)
		{
			return time - Time.realtimeSinceStartup;
		}
		else
		{
			return 0.0f;
		}
	}

	public void Init()
	{
		state = 0;
		time = 120.0f;
		elapsing = false;
		paused = false;
        player.SetPlayerControl(false);
	}

	public void Intro()
	{
		Debug.Log ("Intro");
		overlay.Introduction();
		state = 1;
	}

	public void MainGame()
	{
		Debug.Log("Start");
		state = 2;
		overlay.MainGame();
        player.SetPlayerControl(true);
		StartTime();
	}

	public void Failure()
	{
		Debug.Log ("Game Over");
        player.SetPlayerControl(false);
		overlay.GameOver();
		state = 3;
	}

	public void Succes()
	{
		Debug.Log("Succes");
        player.SetPlayerControl(false);
		overlay.Success();
		state = 3;
	}

	public void ExitGame()
	{
		Debug.Log("Exit");
		Application.Quit();
	}

	public void Restart()
	{
		Debug.Log("Restart");
		Init();
		Intro();
	}

	public void Pause()
	{
		if (paused)
		{
			Debug.Log("Unpause");
			overlay.Pause(false);
            player.SetPlayerControl(true);
			StartTime();
			paused = false;
		}
		else
		{
			Debug.Log("Pause");
			StopTime();
            player.SetPlayerControl(false);
			overlay.Pause(true);
			paused = true;
		}
	}

}
