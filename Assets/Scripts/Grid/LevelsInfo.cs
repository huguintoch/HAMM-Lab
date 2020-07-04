using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelsInfo {

    public static ushort currentLevel = 0;

    ///<summary>
    ///To create a grid for a new level you must create a new multidimensional array.
    ///The digit you type is the height of the floor you will be creating.
    ///Type '0' to create a hole.
    ///To set the initial place where the player will start add 50 to your desired height; for example, 51 will place the player at height 1.
    ///To set the goal type the desired height of it, but add a negative sign; for example, -1 will place the goal at height 1.
    ///</summary>
    public static sbyte[][,] grids = {

        //Level 0 -- FOR TESTING?
        new sbyte[,] { { 51, 1, 1, 1, 1, 1, 1 },
                       { 1, 1, 1, 1, 1, 1, 1 },
                       { 1, 1, 1, 0, 1, 1, 1 },
                       { 1, 1, 1, 1, 1, 1, 1 },
                       { 1, 1, 1, 0, 1, 1, 1 },
                       { 1, 1, 1, 1, 1, 1, 1 },
                       { 1, 1, 1, 1, 1, 1, -1 } },
        
        //Level 1
        new sbyte[,]{ { 51, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1 },
                      { 1, 1, -1, 1, 1 } },

        //Level 2
        new sbyte[,]{ { 3, 3, 3, 3, 3 },
                      { 51, 1, 3, 1, -1 },
                      { 1, 1, 3, 1, 1 },
                      { 1, 1, 3, 1, 1 },
                      { 1, 1, 1, 1, 1 } },

        //Level 3
        new sbyte[,]{ { 1, 1, 2, 0, 1, 1 },
                      { 51, 1, 2, 0, 1, -1 },
                      { 1, 1, 2, 0, 1, 1 } },

        //Level 4
        new sbyte[,]{ { 51, 1, 1, 1, 1, 1, 1, 1, 1},
                      { 1, 1, 1, 1, 1, 1, 1, 1, 1},
                      { 1, 1, 1, 1, 0, 1, 1, 1, 1},
                      { 1, 1, 1, 1, 0, 1, 1, 1, 1},
                      { 1, 1, 1, 1, 1, 1, 1, 1, 1},
                      { 1, 1, 1, 1, 1, 1, 1, 1, -1} },

        //Level 5
        new sbyte[,]{ { 51, 1, 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1, 1, -1 } },

        //Level 6
        new sbyte[,]{ { 51, 1, 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1, 1, 1 },
                      { 1, 1, 1, 1, 1, 1, -1 } },
    };
}
