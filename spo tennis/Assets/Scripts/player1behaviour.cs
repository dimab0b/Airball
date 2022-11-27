using Mirror;
using UnityEngine;

public class player1behaviour : NetworkBehaviour // Сетевой объект
{
    private float borderY = 4.4f; private float borderX1 = -8.5f; private float borderX2 = -0.8f;
    Rigidbody2D rb;
    CircleCollider2D cc;
    Vector3 oldMousePos; // оптимизация
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }
   
    void FixedUpdate()
    {
        if (hasAuthority && isLocalPlayer && Input.GetMouseButton(0)) // Есть ли права изменять объект
        {
            Vector3 mousePos = Input.mousePosition;
            if (mousePos == oldMousePos) // не делать лишних движений, если ничего не поменялось
                return;

            Vector3 wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

            wantedPos.x = Mathf.Clamp(wantedPos.x, borderX1, borderX2);
            wantedPos.y = Mathf.Clamp(wantedPos.y, -borderY, borderY);

            //Debug.Log(mousePos);
            //Debug.Log(cc.transform.position);

            //if (cc.OverlapPoint(mousePos))
            rb.MovePosition(wantedPos);

            oldMousePos = mousePos;
        }
    }

}
