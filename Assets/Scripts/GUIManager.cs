using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text _texto;
    public int contadorEnemigos = 0;


    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_texto, "TEXTO NO PUEDE SER NULO");
        _texto.text = "Enemigos muertos: " + contadorEnemigos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
