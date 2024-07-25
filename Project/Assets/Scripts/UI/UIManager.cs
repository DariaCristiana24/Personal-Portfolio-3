using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    List<Character> characters = new List<Character>();

    [SerializeField]
    GameObject charSlotPrefab;

    [SerializeField]
    GameObject charPanelContent;

    [SerializeField]
    GameObject startScreen;

    [SerializeField]
    TextMeshProUGUI activeAttack;
    void Awake()
    {
        EventBus<UpdateActiveAttackEvent>.OnEvent += UpdateActiveAttack;
    }

    void OnDestroy()
    {
        EventBus<UpdateActiveAttackEvent>.OnEvent -= UpdateActiveAttack;
    }
    void Start()
    {
        createCharacterPanel();
        EventBus<ChangeCursorStateEvent>.Publish(new ChangeCursorStateEvent());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            startScreen.SetActive(true);
            EventBus<ChangeCursorStateEvent>.Publish(new ChangeCursorStateEvent());
        }
    }

    void disableStartScreen()
    {
        startScreen.SetActive(false);
        EventBus<ChangeCursorStateEvent>.Publish(new ChangeCursorStateEvent());
    }

    void createCharacterPanel()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            GameObject newChar = Instantiate(charSlotPrefab, charPanelContent.transform);
            var upgradeName = newChar.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var upgradeStrength = newChar.transform.Find("Strength").GetComponent<TextMeshProUGUI>();
            var upgradeDefense = newChar.transform.Find("Defense").GetComponent<TextMeshProUGUI>();
            var upgradeMana = newChar.transform.Find("Mana").GetComponent<TextMeshProUGUI>();
            var upgradeAgility = newChar.transform.Find("Agility").GetComponent<TextMeshProUGUI>();
            var upgradeDexterity = newChar.transform.Find("Dexterity").GetComponent<TextMeshProUGUI>();
            var selectButton = newChar.transform.Find("SelectButton").GetComponent<Button>();

            upgradeName.text = characters[i].name;
            upgradeStrength.text = "Strength: " + characters[i].Strength.ToString();
            upgradeDefense.text = "Defense: " + characters[i].Defense.ToString();
            upgradeMana.text = "Mana: " + characters[i].Mana.ToString();
            upgradeAgility.text = "Agility: " + characters[i].Agility.ToString();
            upgradeDexterity.text = "Dexterity: " + characters[i].Dexterity.ToString();
            selectButton.onClick.AddListener(disableStartScreen);
            selectButton.GetComponent<CharacterHolder>().character = characters[i];
        }
    }

    void UpdateActiveAttack(UpdateActiveAttackEvent updateActiveAttackEvent)
    {
        activeAttack.SetText(updateActiveAttackEvent.attackName);
    }

   

}
