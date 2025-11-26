using UnityEngine;
using TMPro;

public class NumberTextIncrease : MonoBehaviour
{

    public TextMeshProUGUI numberText;
    public int counter = 0;

    public void ButtonPressed()
    {
        counter++;
        numberText.text = counter.ToString();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
