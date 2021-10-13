using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

namespace AsteroidGame.Actor
{
    /// <summary>
    /// Uses the description file and moves according to it after spawn. Lifecycle and reposition is also handled by that file.
    /// If it touches any player then-> if player has shield then this asteroid is destroyed. Otherwise do damage of the player.
    /// Modify velocity or force if collided with other asteroid?
    /// </summary>
    public class Asteroid : AIActor
    {
        
    }
}