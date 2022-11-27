using Mirror;
using UnityEngine;

public class player2behaviour : NetworkBehaviour
{
    private float borderY = 4.4f; private float borderX1 = 0.8f; private float borderX2 = 8.5f;
    Rigidbody2D rb;
    Vector3 oldMousePos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

            rb.MovePosition(wantedPos);

            oldMousePos = mousePos;
        }
    }

}