using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private float _speed = -1;

    [SerializeField]
    private float _tiempoDeAutodestruccion = 5;

    private float _posicionEnX;
    private float _posicionEnY;

    // Start is called before the first frame update
        void Start()
    {
        _posicionEnX = Random.Range(-8, 8);
        transform.Translate(
            _posicionEnX,
            0,
            0
        );
        Destroy(gameObject, _tiempoDeAutodestruccion);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            0,
            _speed * Time.deltaTime,
            0
        );
    }
}
