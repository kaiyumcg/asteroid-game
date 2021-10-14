using GameplayFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidGame.Actor;
using UnityEngine.Events;

namespace AsteroidGame.Manager
{
    //Timeline clip?[custom timeline script] or DoTween? 
    public class CutSceneManager : GameSystem
    {
        bool hasCutSceneEnded = false;
        public bool HasCutSceneEnded { get { return hasCutSceneEnded; } }
        const float camSizeInitial = 7.18f;
        const float camSizeFinal = 20f;
        [SerializeField] Transform camStartPos, camFinalPos, shipStartPos, shipFinalPos, asteroidInitPos, asteroidFinalPos;
        [SerializeField] float durationShipCome = 3f, durationCam = 1.5f, durationAsteroid = 2f;
        [SerializeField] Rigidbody asteroidRgd;
        [SerializeField] Transform asteroidTr;
        public UnityEvent OnWaitForInput;

        protected override IEnumerator InitSystemAsync()
        {
            asteroidRgd.velocity = Vector3.zero;
            asteroidRgd.isKinematic = true;
            asteroidTr.position = asteroidInitPos.position;
            yield return base.InitSystemAsync();
            var cam = Camera.main;
            var ship = FindObjectOfType<SpaceShip>();
            cam.transform.position = camStartPos.position;
            cam.orthographicSize = camSizeInitial;
            ship._Transform.position = shipStartPos.position;

            var shipSpeed = Vector3.Distance(shipStartPos.position, shipFinalPos.position) / durationShipCome;
            var camSizeSpeed = Mathf.Abs(camSizeFinal - camSizeInitial) / durationCam;
            var camMoveSpeed = Vector3.Distance(camStartPos.position, camFinalPos.position) / durationCam;
            var asteroidMoveSpeed = Vector3.Distance(asteroidInitPos.position, asteroidFinalPos.position) / durationAsteroid;

            while (true)
            {
                ship._Transform.position = Vector3.MoveTowards(ship._Transform.position, shipFinalPos.position, shipSpeed * Time.deltaTime);
                if (Vector3.Distance(ship._Transform.position, shipFinalPos.position) < 0.02f)
                {
                    ship._Transform.position = shipFinalPos.position;
                    break;
                }
                yield return null;
            }

            bool sizeDone = false;
            StartCoroutine(CamSizeCOR(cam, camSizeSpeed, () =>
            {
                sizeDone = true;
            }));

            bool camPosDone = false;
            StartCoroutine(CamPosCOR(cam, camFinalPos.position, camMoveSpeed, () =>
            {
                camPosDone = true;
            }));

            while (sizeDone == false || camPosDone == false)
            {
                yield return null;
            }

            while (true)
            {
                asteroidTr.position = Vector3.MoveTowards(asteroidTr.position, asteroidFinalPos.position, asteroidMoveSpeed * Time.deltaTime);
                if (Vector3.Distance(asteroidTr.position, asteroidFinalPos.position) < 0.02f)
                {
                    asteroidTr.position = asteroidFinalPos.position;
                    break;
                }
                yield return null;
            }

            asteroidRgd.isKinematic = false;
            while (true)
            {
                if (Input.anyKey)
                {
                    break;
                }
                OnWaitForInput?.Invoke();
                yield return null;
            }

            hasCutSceneEnded = true;
        }

        IEnumerator CamSizeCOR(Camera cam, float speed, System.Action OnComplete)
        {
            while (true)
            {
                cam.orthographicSize += speed * Time.deltaTime;
                if (Mathf.Abs(cam.orthographicSize - camSizeFinal) < 0.1f)
                {
                    cam.orthographicSize = camSizeFinal;
                    break;
                }
                yield return null;
            }
            OnComplete?.Invoke();
        }

        IEnumerator CamPosCOR(Camera cam, Vector3 finalPos, float speed, System.Action OnComplete)
        {
            while (true)
            {
                cam.transform.position = Vector3.MoveTowards(cam.transform.position, finalPos, speed * Time.deltaTime);
                if (Vector3.Distance(cam.transform.position, finalPos) < 0.02f)
                {
                    cam.transform.position = finalPos;
                    break;
                }
                yield return null;
            }
            OnComplete?.Invoke();
        }
    }
}