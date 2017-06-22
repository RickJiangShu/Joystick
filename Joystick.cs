using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Joystick : MonoBehaviour {

    //事件
    public Action OnTouchDown;
    public Action<float,float> OnTouchMove;//p1:angle360 0~360 p2:power 0~1
    public Action OnTouchUp;

    //Untiy 指定参数
    public GameObject control;//移动对象
    public float controlRadius = 200.0f;//移动半径（UGUI像素）
    
    private Vector3 touchOrigin;//按下原点（转换到Canvas的点）
    private float scaleFactor;//Screen 2 Canvas 的转换因子
    private Vector3 controlOrigin;//移动对象的原点

	// Use this for initialization
	void Start () {
        if (control != null)
        {
            //获取转换系数
            Canvas canvas = control.GetComponentInParent<Canvas>();
            scaleFactor = canvas.scaleFactor;

            controlOrigin = control.transform.localPosition;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //按下
        if (Input.GetMouseButtonDown(0))
        {
            TouchDown();
        }

        //拖动
        if (Input.GetMouseButton(0))
        {
            TouchMove();
        }

        //放开
        if (Input.GetMouseButtonUp(0))
        {
            TouchUp();
        }
	}

    private void TouchDown()
    {
        touchOrigin = Input.mousePosition / scaleFactor;

        if (OnTouchDown != null)
            OnTouchDown();
    }
    private void TouchMove()
    {
        Vector3 now = Input.mousePosition / scaleFactor;

        float distance = Vector3.Distance(now, touchOrigin);
        Vector3 direction = now - touchOrigin;
        float radians = Mathf.Atan2(direction.y, direction.x);

        //移动摇杆
        if (control != null)
        {
            if (distance > controlRadius)
                distance = controlRadius;

            float mx = Mathf.Cos(radians) * distance;
            float my = Mathf.Sin(radians) * distance;
            Vector3 uiPos = controlOrigin;
            uiPos.x += mx;
            uiPos.y += my;
            control.transform.localPosition = uiPos;
        }

        //派发事件
        if (OnTouchMove != null)
        {
            float power = distance / controlRadius;
            float angle = radians * Mathf.Rad2Deg;
            float angle360 = angle < 0 ? 360 + angle : angle;

            OnTouchMove(angle360, power);
        }
    }
    private void TouchUp()
    {
        control.transform.localPosition = controlOrigin;

        if (OnTouchUp != null)
            OnTouchUp();
    }
}
