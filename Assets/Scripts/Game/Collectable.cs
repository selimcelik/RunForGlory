using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    public enum CollectableType
    {
        sphere,
        sphereAfterSphere,
        obstacleOnRightSide,
        obstacleOnLeftSide,
    }
    public CollectableType collectableType;

    public bool canRotate=false;

    private void Start()
    {
        if(collectableType == CollectableType.sphere)
        {
            StartCoroutine(SphereMovement());
        }
        if(collectableType == CollectableType.sphereAfterSphere)
        {
            StartCoroutine(SphereAfterSphereMovement());
        }
        if(collectableType == CollectableType.obstacleOnRightSide)
        {
            StartCoroutine(ObstacleOnRightSideMovement());
        }
        if (collectableType == CollectableType.obstacleOnLeftSide)
        {
            StartCoroutine(ObstacleOnLeftSideMovement());

        }
    }

    private void FixedUpdate()
    {
        if (canRotate)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 250));
        }
    }

    IEnumerator SphereMovement()
    {
        gameObject.transform.DOMoveY(1.5f, 0.5f).OnComplete(() => gameObject.transform.DOMoveY(0.5f, .5f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(SphereMovement());
    }
    IEnumerator SphereAfterSphereMovement()
    {
        gameObject.transform.DOMoveY(.5f, 0.5f).OnComplete(() => gameObject.transform.DOMoveY(1.5f, .5f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(SphereAfterSphereMovement());
    }
    IEnumerator ObstacleOnRightSideMovement()
    {
        gameObject.transform.DOMoveX(gameObject.transform.position.x+2.5f, 0.5f).OnComplete(() => gameObject.transform.DOMoveX(gameObject.transform.position.x - 2.5f, .5f)).
            OnComplete(() => gameObject.transform.DOMoveX(gameObject.transform.position.x - 2.5f, 0.5f).OnComplete(() => gameObject.transform.DOMoveX(gameObject.transform.position.x + 2.5f, .5f)));
        yield return new WaitForSeconds(1f);
        StartCoroutine(ObstacleOnRightSideMovement());
    }
    IEnumerator ObstacleOnLeftSideMovement()
    {
        gameObject.transform.DOMoveX(gameObject.transform.position.x - 2.5f, 0.5f).OnComplete(() => gameObject.transform.DOMoveX(gameObject.transform.position.x + 2.5f, .5f)).
            OnComplete(() => gameObject.transform.DOMoveX(gameObject.transform.position.x + 2.5f, 0.5f).OnComplete(() => gameObject.transform.DOMoveX(gameObject.transform.position.x - 2.5f, .5f)));
        yield return new WaitForSeconds(1f);
        StartCoroutine(ObstacleOnLeftSideMovement());
    }
}
