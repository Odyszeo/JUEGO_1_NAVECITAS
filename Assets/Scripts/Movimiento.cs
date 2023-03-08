// estamos usando .NET
// aquí "importamos" namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// OJO 
// con esta directiva obligamos la presencia de un componente en el gameobject
// (todos tienen transform así que este ejemplo es redundante)
[RequireComponent(typeof(Transform))]
public class Movimiento : MonoBehaviour
{

    // va a haber situaciones en donde deba accedr a otro componente
    // voy a necesitar una referencia
    private Transform _transform;

    [SerializeField]
    private float _speed = 10;

    [SerializeField]
    private Proyectil _disparoOriginal;

    [SerializeField]
    private Enemigo _enemigo;

    private float xMin, xMax;
    private float yMin, yMax;

    // ciclo de vida / lifecycle
    // - existen métodos que se invocan en momentos específicos de la vida del script
    
    // Se invoca una vez al inicio de la vida del componente 
    // otra diferencia - awake se invoca aunque objeto esté deshabilitado
    void Awake()
    {
        print("AWAKE");
    }

    // Se invoca una vez después que fueron invocados todos los awakes
    void Start()
    {
        // Get the size of the camera
        var distance = transform.position.z - Camera.main.transform.position.z;
        var leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        var rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        var topMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        var bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));

        xMin = leftMost.x;
        xMax = rightMost.x;
        yMin = bottomMost.y;
        yMax = topMost.y;

        Debug.Log("START");

        // como obtener referencia a otro componente

        // NOTAS:
        // - getcomponent es lento, hazlo la menor cantidad de veces posible
        // - con transform ya tenemos referencia (ahorita lo vemos)
        // - esta operación puede regresar nulo
        _transform = GetComponent<Transform>();
        
        // si tienes require esto ya no es necesario
        Assert.IsNotNull(_transform, "ES NECESARIO PARA MOVIMIENTO TENER UN TRANSFORM");
        Assert.IsNotNull(_disparoOriginal, "DISPARO NO PUEDE SER NULO");
        Assert.IsNotNull(_enemigo, "Enemigo NO PUEDE SER NULO");
        Assert.AreNotEqual(0, _speed, "VELOCIDAD DEBE SER MAYOR A 0");


        
    }


    void Update(){
        
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if (transform.position.x > xMax) {
            // Stop the player from going off the right side of the screen
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        } else if (transform.position.x < xMin) {
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
        } else {
            transform.Translate(
                        horizontal * _speed * Time.deltaTime, 
                        vertical * _speed * Time.deltaTime, 
                        0,
                        Space.World
                    );
        }

        // se pueden usar ejes como botones
        if(Input.GetButtonDown("Jump")){
            
            print("JUMP");
            
            Instantiate(
                _disparoOriginal, 
                transform.position, 
                transform.rotation
            );
        }

        if(Input.GetKeyDown(KeyCode.E)){
            print("Enemigo creado");
            
            Instantiate(_enemigo);
        }
    }


    IEnumerator CorrutinaDummy(){
        yield return new WaitForSeconds(2);

        print("HOLA");
    }

    void FixedUpdate()
    {
        //Debug.LogError("FIXED UPDATE");
    }

    void LateUpdate()
    {
        //print("LATE UPDATE");
    }


}
