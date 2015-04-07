using UnityEngine;
using System.Collections;

public static class CharStateBhv {
	// char states
	public const int ST_IDLE = 0;
	public const int ST_WALKING_F = 1; // walking forward
	public const int ST_WALKING_B = 2; // walking backwards
	public const int ST_WALKING_UP = 3; // walking up on the street plane
	public const int ST_WALKING_DOWN = 4;
	public const int ST_JUMPING = 5;
	public const int ST_CROUCHING = 6;

	// attack states
	public const int ST_ATK_NORMAL = -1;
	public const int ST_ATK_EX = -2;
	public const int ST_ATK_SPECIAL = -3;
}
