using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeName : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public TMP_InputField inputField;
   

    void Start()
    {
        inputField = inputField.GetComponent<TMP_InputField>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputText()
    {
      
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangeName(inputField.text);

    }
}
