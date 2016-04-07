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
}
