using GameplayFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Manager
{
    public class CutSceneManager : GameSystem
    {
        bool hasCutSceneEnded = false;
        public bool HasCutSceneEnded { get { return hasCutSceneEnded || true; } }
    }
}