using UnityEngine;
using System.Collections;

public static class CharStateBhv {
	// char states
	public const int ST_STANDING = 0;
	public const int ST_WALKING_F = 1; // walking forward
	public const int ST_WALKING_B = 2; // walking backwards
	public const int ST_WALKING_UP = 3; // walking up on the street plane
	public const int ST_WALKING_DOWN = 4;
	public const int ST_JUMPING = 5;
	public const int ST_CROUCHING = 6;
	public const int ST_JUMPING_ANGLE = 7;
	public const int ST_THROWING = 8;

	// state modifiers
	public const int ST_DISTANCE_FAR = 20;
	public const int ST_DISTANCE_CLOSE = 21;

	// attack states
	public const int ST_ATK_NONE = -1;

	public const int ST_ATK_LP = -2;
	public const int ST_ATK_MP = -3;
	public const int ST_ATK_HP = -4;

	public const int ST_ATK_LK = -5;
	public const int ST_ATK_MK = -6;
	public const int ST_ATK_HK = -7;

	public const int ST_ATK_EX = -10;

	public const int ST_ATK_SPECIAL_1 = -20;
	public const int ST_ATK_SPECIAL_2 = -21;
	public const int ST_ATK_SPECIAL_3 = -22;

	public const int ST_ATK_SUPER_ART = -30;
}
