using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignManager : MonoBehaviour
{
    [SerializeField] private TMP_Text headline;
    [SerializeField] private TMP_Text amount1;
    [SerializeField] private TMP_Text amount2;
    [SerializeField] private TMP_Text amount3;
    [SerializeField] private TMP_Text holeSign;
    [SerializeField] private Image imageRes1;
    [SerializeField] private Image imageRes2;
    [SerializeField] private Image imageRes3;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    private bool has3Items;

    private void Start()
    {
        has3Items = false;
        this.gameObject.SetActive(false);
        DeaktivateAllText();
    }

    public void ActivateSignEveryThingIsFine(string text)
    {
        holeSign.text = text;
        holeSign.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void ActivateResourceSign()
    {


        amount1.gameObject.SetActive(true);
        amount2.gameObject.SetActive(true);
        amount3.gameObject.SetActive(true);


        imageRes1.gameObject.SetActive(true);
        imageRes2.gameObject.SetActive(true);
        
        if(has3Items)
            imageRes3.gameObject.SetActive(true);


        headline.gameObject.SetActive(true);

        gameObject.SetActive(true);
    }
    public void DeactivateSign()
    {
        gameObject.SetActive(false);

        DeaktivateAllText();
    }

    private void DeaktivateAllText()
    {
        holeSign.gameObject.SetActive(false);
        amount1.gameObject.SetActive(false);
        amount2.gameObject.SetActive(false);
        amount3.gameObject.SetActive(false);
        imageRes1.gameObject.SetActive(false);
        imageRes2.gameObject.SetActive(false);
        imageRes3.gameObject.SetActive(false);
        headline.gameObject.SetActive(false);
    }

    private Sprite SetIcon(string res)
    {
        Debug.Log(res);
        Sprite returnSprite = null;
        if(res == "Iron")
        {
            Debug.Log("InIron");
            returnSprite = sprites[0];
        }
        if (res == "Water")
        {
            returnSprite = sprites[1];
        }
        if (res == "Organic")
        {
            returnSprite = sprites[2];
        }
        if(res == "Cable")
        {
            returnSprite = sprites[3];
        }
        Debug.Log(returnSprite.name);
        return returnSprite;
    }

    public void SetRow(int index, int amount, string res)
    {
        Debug.Log("SetRow: " + index + "/" + amount.ToString() + "/" + res);
        if(index == 0)
        {
            Debug.Log(res);
            amount1.text = amount.ToString();
            imageRes1.sprite = SetIcon(res);
        }
        if (index == 1)
        {
            amount2.text = amount.ToString();
            imageRes2.sprite = SetIcon(res);
        }
        if (index == 2 && amount != 0)
        {
            amount3.text = amount.ToString();
            imageRes3.sprite = SetIcon(res);
            has3Items = true;
        }
        else
        {
            amount3.text = "";
            imageRes3.sprite = null;
            has3Items = false;
        }
    }



}
