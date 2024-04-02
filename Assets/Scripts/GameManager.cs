using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pokeball pokeball; // Referencia al GameObject de la Pokebola en la escena
    public Transform throwPoint; // Punto de lanzamiento de la Pokebola

    private Vector2 touchStartPos; // Posición de inicio del toque
    private Vector2 touchEndPos; // Posición de fin del toque
    public float minThrowForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            if(touch.phase == TouchPhase.Began)
            {
                touchStartPos = pos;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;

                // Calcular la dirección y la fuerza del lanzamiento
                Vector3 throwDirection = CalculateThrowDirection(touchEndPos - touchStartPos);
                float throwForce = (touchEndPos - touchStartPos).magnitude;

                // Lanzar la Pokebola si la fuerza del lanzamiento es suficiente
                if (throwForce >= minThrowForce)
                {
                    ThrowPokeball(throwDirection, throwForce);
                }
            }
        }

    }
    private Vector3 CalculateThrowDirection(Vector2 screenDirection)
    {
        // Convertir la dirección del toque en una dirección en el espacio 3D de la pantalla
        Vector3 worldDirection = new Vector3(screenDirection.x, screenDirection.y, throwPoint.position.z) - throwPoint.position;
        return worldDirection.normalized;
    }
    private void ThrowPokeball(Vector3 direction, float force)
    {

        // Aplicar fuerza a la Pokebola para lanzarla en la dirección y con la fuerza calculadas
        pokeball.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }
}
