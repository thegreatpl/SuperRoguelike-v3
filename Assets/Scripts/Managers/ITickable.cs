using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface ITickable
{
    int Cooldown { get; }

    /// <summary>
    /// Whether this ITickable is waiting for player input before running its tick. 
    /// </summary>
    bool WaitingForPlayerInput { get; }

    /// <summary>
    /// This runs the tick for this ITickable. 
    /// </summary>
    void RunTick();

    /// <summary>
    /// Called at the end of the current Tick. 
    /// </summary>
    void EndTick(); 
}

