using UnityEngine;
using System.Collections;

/*
 * Flame_CustomSpawner
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This class contains enums that are standradised of Flame
 * - Please follow this standard: EnumTo<TYPE> all FlameCore devs.
 * - Also please make all moste commenly used enums the first once you check. And use cases.
 * History:
 * - Created on April 2 2016 By Alexander Björkman
 */

public class Flame_Enums {

    // This is only neccesary if using inspector. Other wise use Nullable.
    // Polar means yes -- no question.
    public enum UndefinedPolar
    {
        UNDEFINED,
        YES,
        NO
    }

    // Mono Means "one". So this enum can only go in 1D.
    public enum MonoDirection
    {
        // Going forward
        FORWARD,

        // Has stopped. Probably has completed.
        STOP,

        // Going back
        BACKWARD,
    }

	public enum Direction3D
	{
		// Forward
		FORWARD,

		// Back
		BACK,

		// Left 
		LEFT,

		// Right
		RIGHT,

		// Up
		UP,

		// Down
		DOWN
	}

	public enum Direction2D
	{
		// Up
		UP,

		// Down
		DOWN,

		// Left
		LEFT,

		// Right
		RIGHT
	}

    static public int EnumToInt(MonoDirection d)
    {
        switch (d)
        {
            case MonoDirection.FORWARD:
                return 1;
            case MonoDirection.BACKWARD:
                return -1;
            default:
                return 0;
        }
    }

    static public bool EnumToBool(UndefinedPolar q)
    {
        switch (q)
        {
            case UndefinedPolar.YES:
                return true;

            case UndefinedPolar.NO:
                return false;

            default:
                Debug.LogError("Error enum is type UNDEFINED!");
                Debug.LogError(System.Environment.StackTrace);
                return false;
        }
    }

    // Returns -2 On failure
    static public int EnumToInt(UndefinedPolar q)
    {
        switch (q)
        {
            case UndefinedPolar.YES:
                return 1;
            case UndefinedPolar.NO:
                return 0;
            case UndefinedPolar.UNDEFINED:
                return -1;
            default:
                Debug.LogError("Error enum is undefined!");
                Debug.LogError(System.Environment.StackTrace);
                return -2;

        }
    }
	
	static public Vector3 EnumToVector (Direction3D d)
	{
		switch (d)
		{
			case Direction3D.UP:
				return Vector3.up;

			case Direction3D.DOWN:
				return Vector3.down;

			case Direction3D.LEFT:
				return Vector3.left;

			case Direction3D.RIGHT:
				return Vector3.right;

			case Direction3D.FORWARD:
				return Vector3.forward;

			case Direction3D.BACK:
				return Vector3.back;

			default:
				Debug.LogError("Error enum is undefined!");
				Debug.LogError(System.Environment.StackTrace);
				return Vector3.zero;

		}
	}
}
