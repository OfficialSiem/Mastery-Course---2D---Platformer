using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    //An array of checkpoints to manage
    private Checkpoint[] checkpoints;

    // Start is called before the first frame update
    private void Start()
    {
        checkpoints = GetComponentsInChildren<Checkpoint>();
    }

    /// <summary>
    /// So this is going to use a LINQ (Language Integrated Query) to find the lastest checkpoint passed
    ///     However, " using LINQ won’t hurt you at all.  It may add a couple nanoseconds here or there,
    ///      but it can also shave some time off if your custom code isn’t completely optimized.
    ///      The one thing you definitely do need to look out for though is Garbage Collection. 
    ///      LINQ statements will generate a little garbage, so avoid using them in something that’s 
    ///      going to be called every frame (don’t put them in your Update() calls).
    ///      For other events though, LINQ can be a huge time saver, make your code easier to read, 
    ///      and having less code always reduces the chance for bugs." -Jason Weimann / July 1, 2017 / "LINQ for Unity Developers"
    /// </summary>
    /// <returns></returns>
    public Checkpoint GetLastCheckpointThatWasPassed()
    {
        //'t' is just a variable name, some like to use 't' in LINQ instead of other variable names.
        //  Regardless, this is going to look through all the Checkpoints and see which one was the last one passed!
        //BUT BE WARNED, in order to avoid handling Exception Errors for when NO checkpoints were passed,
        // I set, at least the first checkpoint, as the spawn point (and oh man I want to do some error handling for LINQ, but it looks like
        // its not even worth trying to try and catch
        return checkpoints.Last(t => t.Passed);
    }
}
