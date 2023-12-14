using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ControlableWasp : MonoBehaviour
{
    [SerializeField] private float moveAbleXGap = 3; 
    [SerializeField] private float sensivity = 3;
    [SerializeField] private float cutoffValue = 3;
    [SerializeField] public float waspSpeed = 6;
    private bool a;
    private bool mouseFlag;
    private Vector3 currentMousePos;
    private Vector3 oldMousePos;
    private Vector2 normalizeVector;
    private static Vector3 destinationPoint = new Vector3(1.12f, 3, 204);
    private bool endFlag;
    void Start()
    {
        endFlag = false;
        normalizeVector = new Vector2(Screen.width, Screen.height);
    }

    void FixedUpdate()
    {
        if (endFlag)
        {
            return;
        }
        var position = this.transform.position;
        var z = position.z + (waspSpeed * Time.deltaTime);
        var x = position.x;
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition / normalizeVector;
            if (!mouseFlag)
            {
                oldMousePos = mousePos;
                mouseFlag = true;
            }
            currentMousePos = mousePos;
            var diff = currentMousePos - oldMousePos;
            if (Mathf.Abs(diff.x)>cutoffValue)
            {
                x += diff.x * sensivity*Time.deltaTime;
            }
            x = Mathf.Clamp(x, -moveAbleXGap, moveAbleXGap);
            oldMousePos = currentMousePos;
        }
        else
        {
            mouseFlag = false;
        }

        this.transform.position = new Vector3(x, position.y, z);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EndGate"))
        {
            waspSpeed = 0;
            StartCoroutine(Anim());
        }
    }

    IEnumerator Anim()
    {
        var startPosition = this.transform.position;
        var lerpValue = 0f;
        endFlag = true;
        while (lerpValue<1)
        {
            var pos = Vector3.Lerp(startPosition, destinationPoint, lerpValue);
            this.transform.position = pos;
            lerpValue += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        this.transform.position = destinationPoint;
        GameManager.Instance.StartFight();
        yield return 0;
    }
}
