using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Actor
{
    /// <summary>
    /// Uses the description file and moves according to it after spawn. Lifecycle and reposition is also handled by that file.
    /// If it touches any player then-> if player has shield then this asteroid is destroyed. Otherwise do damage of the player.
    /// Modify velocity or force if collided with other asteroid?
    /// </summary>
    public class Asteroid : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}