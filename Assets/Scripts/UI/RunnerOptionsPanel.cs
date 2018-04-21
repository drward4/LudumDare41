using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerOptionsPanel : MonoBehaviour
{
    public List<OptionButton> AllOptionButtons;
    public OptionButton PassOption;
    public OptionButton LeadOption;
    public OptionButton StealOption;
    public OptionButton UsePotionOption;
    public OptionButton CastSpellOption;


    public Image CurrentOptionImage;

    public void SetOptionIndex(int index)
    {
        this.CurrentOptionImage.transform.SetParent(this.AllOptionButtons[index].transform, false);
        this.CurrentOptionImage.rectTransform.anchoredPosition = new Vector2(-5f, 0f);
    }

}
