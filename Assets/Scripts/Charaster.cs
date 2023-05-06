using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    Blue,
    Orange
}

public enum CharasterState
{
    OnStart,
    Clicked,
    PointReached,
    Move,
    Pause
}


public class Charaster : MonoBehaviour
{
    public ColorType colorType;
    public float distanceToExit = 0f;
    public CharasterState charasterState=CharasterState.OnStart;


    //уменьшаем колличество координат в списке
    public float minDistance = 0.1f;
    private Vector2 lastMousePosition;

    //рисуем линию
    public Color lineColor = Color.black; // Цвет линии
    public float lineWidth = 0.1f; // Ширина линии
    private LineRenderer lineRenderer; // Компонент LineRenderer для отрисовки линии

    //движение персонажа
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float waypointRadius = 0.1f;
    private int currentWaypoint = 0;
    public Animator animator;

    public List<Vector2> mousePositions = new List<Vector2>();
    void Start()
    {
        LineRendererStats();
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0) && charasterState==CharasterState.Clicked )
        {
            charasterState = CharasterState.OnStart;           
            mousePositions.Clear();
            lineRenderer.positionCount = 0;
            animator.Play("Idle");

        }

        if (charasterState==CharasterState.PointReached && GameController.Instance.checkallLine == true)
        {
            StartCoroutine(Move());
        }


        RegisterMouseMovement();
        CheckMouseOverTarget();

    }

    public void GetClick()
    {
        if (charasterState==CharasterState.OnStart)
        {
            charasterState = CharasterState.Clicked;
            animator.Play("Idle");
        }
    }

    public void RegisterMouseMovement()
    {
        if (charasterState==CharasterState.Clicked)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            if (Vector2.Distance(worldPos, lastMousePosition) > minDistance)
            {
                lastMousePosition = worldPos;
                mousePositions.Add(lastMousePosition);
            }
        }
    }

    public void CheckMouseOverTarget()
    {
        if (charasterState==CharasterState.Clicked)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            DrawLineToPoint(rayPos);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Door") && hit.collider.GetComponent<Door>().colorType == this.colorType && hit.collider.GetComponent<Door>().busyDoor==false)
            {
                CalculateMouseDistance();
                charasterState = CharasterState.PointReached;
                hit.collider.GetComponent<Door>().busyDoor = true;
              
            }

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall"))
            {
                charasterState = CharasterState.OnStart;
                animator.Play("Idle");
                lineRenderer.positionCount = 0;
                mousePositions.Clear();
            }

        }
    }

    public void CalculateMouseDistance()
    {
        for (int i = 0; i < mousePositions.Count - 1; i++)
        {
            distanceToExit += Vector2.Distance(mousePositions[i], mousePositions[i + 1]);           
        }
    }

    public void LineRendererStats()
    {
        lineRenderer = GetComponent<LineRenderer>(); // Получаем компонент LineRenderer у объекта, к которому прикреплен данный скрипт
        lineRenderer.startColor = lineColor; // Задаем цвет начала линии
        lineRenderer.endColor = lineColor; // Задаем цвет конца линии
        lineRenderer.startWidth = lineWidth; // Задаем ширину начала линии
        lineRenderer.endWidth = lineWidth; // Задаем ширину конца линии
        lineRenderer.positionCount = 0; // Изначально количество точек на линии равно 0
    }

    private void DrawLineToPoint(Vector2 point)
    {
        lineRenderer.positionCount++; // Увеличиваем количество точек на линии на 1
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point); // Задаем позицию последней точки на линии равной point      
    }

    IEnumerator Move()
    {
        charasterState = CharasterState.Move;
        animator.Play("Run");
       
        while (currentWaypoint < mousePositions.Count && charasterState==CharasterState.Move) 
        {
            Vector2 waypoint = mousePositions[currentWaypoint]; // Получаем текущую точку пути
            Vector2 direction = (waypoint - (Vector2)transform.position).normalized; // Вычисляем направление к точке пути

            transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime); // Двигаем объект к точке пути

            if (Vector2.Distance(transform.position, waypoint) < waypointRadius) // Если объект достиг точки пути
            {
                currentWaypoint++; // Увеличиваем индекс текущей точки пути на 1

                // Проверяем, является ли направление главным образом горизонтальным, и поворачиваем объект соответствующим образом
                if (direction.x != 0)
                {
                    // Установка угла поворота объекта только по оси Y
                    float angle = Mathf.Sign(direction.x) > 0 ? 180f : 0f;
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }
            }

            yield return null; 
        }

        charasterState = CharasterState.Pause;
        animator.Play("Idle");
    }  
   
}
