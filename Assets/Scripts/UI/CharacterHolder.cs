using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHolder : MonoBehaviour
{
    public Character character;

    Button button;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(selectChar);
    }
    void selectChar()
    {
        EventBus<SelectCharEvent>.Publish(new SelectCharEvent(character));
    }
}
