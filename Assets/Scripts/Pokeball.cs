using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    private Rigidbody rb;
    private float lastMouseX, lastMouseY;
    private float speed;
    private float throwSpeed;

    private Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 mousePos = touch.position;
            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform == transform)
                    {
                        transform.SetParent(null);
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (lastMouseY < touch.position.y)
                {
                    ThrowBall(touch.position);
                }
            }
            //if (touch.phase == TouchPhase.Moved)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(touch.position);
            //    RaycastHit hit;
            //    Debug.Log("Hola");
            //    if (Physics.Raycast(ray, out hit, 100f))
            //    {
            //        transform.position = new Vector3(pos.x, pos.y, pos.z);
            //    }
            //}
        }
        if (Input.touchCount ==1)
        {
            lastMouseX = Input.GetTouch(0).position.x;
            lastMouseY = Input.GetTouch(0).position.y;

        }

    }
    void ThrowBall(Vector2 mousePos)
    {
        float differenceY = (mousePos.y - lastMouseY) / Screen.height * 10;
        speed = throwSpeed *differenceY;

        float x = (mousePos.x/Screen.width) - (lastMouseX / Screen.width);
        x = Mathf.Abs(Input.GetTouch(0).position.x - lastMouseX) / Screen.width* 100*x;

        Vector3 direction = new Vector3(x, 0f, 1f);
        direction = Camera.main.transform.TransformDirection(direction);

        rb.AddForce((direction * speed / 2f) + (Vector3.up * speed));
        Debug.Log("Lanzando");

    }
}
