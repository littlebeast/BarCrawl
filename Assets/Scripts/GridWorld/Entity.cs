﻿using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {


	public int x;
	public int y;

	public int health;
	public const int maxRed = 100;
	public int currentRed = 0;
	public bool isPassable = false;
	
	protected SpriteRenderer spriteRenderer;

	//for display & fight logic purposes.
	protected Move facing;

	// Use this for initialization
	// Start MUST be called by superclasses!
	protected virtual void Start () {
		GridManager.instance.entities.Add(this);
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	// Update also MUST be called by superclasses!
	protected virtual void Update() {
		if(currentRed > 0)
			currentRed--;
		
		if(currentRed > 0){
			spriteRenderer.color = Color.red;
			//Debug.Log ("red");
		}else{
			spriteRenderer.color = Color.white;
			//Debug.Log("white");
		}
	}



	/// <summary>
	/// Attempts to execute the specified move.  If there is something in the way, will not change position; will change facing if non-enemy.
	/// In Entity, this does not handle fighting.  Override it to incorporate that behavior.  
	/// </summary>
	/// <returns><c>true</c>, if the move was successfully executed, <c>false</c> otherwise.</returns>
	/// <param name="move">The move to execute.</param>
	protected virtual bool AttemptMove(Move move)
	{
		//does nothing on these cases anyway,
		//but we want to prevent it assigning them as facings.
		switch (move)
		{
		case Move.Fight:
		case Move.None:
			return false;
		}

		//Debug.Log(gameObject.name + " attempts moving " + move);

		//find the destination tile
		Vector2 moveDir = move.getDirection();
		int destX = x + (int)moveDir.x;
		int destY = y + (int)moveDir.y;



		if(GridManager.instance.isPassable(destX, destY))
		{
			//moves the entity if there is no obstacle
			//Debug.Log ("moved");
			x = destX;
			y = destY;
			Vector2 dest = GridManager.getTransformPosition(x, y);
			transform.position = new Vector3(dest.x, dest.y, -1);
			facing = move;

			return true;
		}
		else
		{
			//the player will still turn to face the obstacle
			//enemies, however, will not; they will instead try a different direction.
			if(gameObject.tag != "enemy")
				facing = move;

			return false;
		}
	}

	/// <summary>
	/// Kills the entity.  Call this to make sure its deconstruction logic is done.
	/// </summary>
	public virtual void Die()
	{
		//right now, that consists of removing it from the GridManager's entity list.

		GridManager.instance.entities.Remove(this);
		Debug.Log (gameObject.name + " has been defeated!");
		Destroy (this.gameObject);
	}
}
