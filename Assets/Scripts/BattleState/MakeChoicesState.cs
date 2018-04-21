using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChoicesState : BaseBattleState
{
    protected int CurrentBaseIndex;
    protected Base CurrentBase;

    protected int CurrentOptionIndex;
    protected List<OptionButton> CurrentOptions;

    public MakeChoicesState(BattleController controller)
        : base(controller)
    {

    }


    protected void SetCurrentOption(int index)
    {
        this.CurrentOptionIndex = index;

        if (this.CurrentBaseIndex == 0)
        {
            this.Controller.OptionsPanel.HitterOptionsPanel.SetOptionIndex(this.CurrentOptionIndex);
        }
    }


    protected void SetCurrentBase(int index)
    {
        this.CurrentBaseIndex = index;
        switch (index)
        {
            case 0: this.CurrentBase = this.Controller.HomePlate; break;
            case 1: this.CurrentBase = this.Controller.FirstBase; break;
            case 2: this.CurrentBase = this.Controller.SecondBase; break;
            case 3: this.CurrentBase = this.Controller.ThirdBase; break;
        }

        if (this.CurrentBaseIndex == 0)
        {
            this.Controller.OptionsPanel.HitterOptionsPanel.gameObject.SetActive(true);
            this.Controller.OptionsPanel.RunnerOptionsPanel.gameObject.SetActive(false);
            this.CurrentOptions = this.Controller.OptionsPanel.HitterOptionsPanel.AllOptionButtons;
        }
        else
        {
            this.Controller.OptionsPanel.HitterOptionsPanel.gameObject.SetActive(false);
            this.Controller.OptionsPanel.RunnerOptionsPanel.gameObject.SetActive(true);
            this.CurrentOptions = this.Controller.OptionsPanel.RunnerOptionsPanel.AllOptionButtons;
        }


        this.Controller.CurrentBaseIndicator.transform.SetParent(this.CurrentBase.Canvas.transform, false);
        this.Controller.CurrentBaseIndicator.rectTransform.anchoredPosition = new Vector2(5f, 0f);
    }


    public override void EnterState()
    {
        base.EnterState();

        this.SetCurrentBase(0);
        this.SetCurrentOption(0);
        this.Controller.OptionsPanel.HitterOptionsPanel.SwingOption.Button.onClick.AddListener(this.SwingOptionPressed);
        this.Controller.OptionsPanel.HitterOptionsPanel.PassOption.Button.onClick.AddListener(this.PassOptionPressed);
        this.Controller.OptionsPanel.HitterOptionsPanel.BuntOption.Button.onClick.AddListener(this.BuntOptionPressed);
        this.Controller.OptionsPanel.HitterOptionsPanel.UsePotionOption.Button.onClick.AddListener(this.UsePotionOptionPressed);
        this.Controller.OptionsPanel.HitterOptionsPanel.CastSpellOption.Button.onClick.AddListener(this.CastSpellOptionPressed);

        this.Controller.OptionsPanel.RunnerOptionsPanel.PassOption.Button.onClick.AddListener(this.PassOptionPressed);
        this.Controller.OptionsPanel.RunnerOptionsPanel.LeadOption.Button.onClick.AddListener(this.LeadOptionPressed);
        this.Controller.OptionsPanel.RunnerOptionsPanel.StealOption.Button.onClick.AddListener(this.StealOptionPressed);
        this.Controller.OptionsPanel.RunnerOptionsPanel.UsePotionOption.Button.onClick.AddListener(this.UsePotionOptionPressed);
        this.Controller.OptionsPanel.RunnerOptionsPanel.CastSpellOption.Button.onClick.AddListener(this.CastSpellOptionPressed);
    }


    public override void ExitState()
    {
        base.ExitState();

        this.Controller.OptionsPanel.HitterOptionsPanel.SwingOption.Button.onClick.RemoveListener(this.SwingOptionPressed);
        this.Controller.OptionsPanel.HitterOptionsPanel.PassOption.Button.onClick.RemoveListener(this.PassOptionPressed);
        this.Controller.OptionsPanel.HitterOptionsPanel.BuntOption.Button.onClick.RemoveListener(this.BuntOptionPressed);
        this.Controller.OptionsPanel.HitterOptionsPanel.UsePotionOption.Button.onClick.RemoveListener(this.UsePotionOptionPressed);
        this.Controller.OptionsPanel.HitterOptionsPanel.CastSpellOption.Button.onClick.RemoveListener(this.CastSpellOptionPressed);

        this.Controller.OptionsPanel.RunnerOptionsPanel.PassOption.Button.onClick.RemoveListener(this.PassOptionPressed);
        this.Controller.OptionsPanel.RunnerOptionsPanel.LeadOption.Button.onClick.RemoveListener(this.LeadOptionPressed);
        this.Controller.OptionsPanel.RunnerOptionsPanel.StealOption.Button.onClick.RemoveListener(this.StealOptionPressed);
        this.Controller.OptionsPanel.RunnerOptionsPanel.UsePotionOption.Button.onClick.RemoveListener(this.UsePotionOptionPressed);
        this.Controller.OptionsPanel.RunnerOptionsPanel.CastSpellOption.Button.onClick.RemoveListener(this.CastSpellOptionPressed);
    }


    protected void NextBase()
    {
        ++this.CurrentBaseIndex;
        this.CurrentBase = this.CurrentBase.NextBase;

        if (this.CurrentBaseIndex >= 4 || this.CurrentBase.CurrentPlayer == null)
        {
            // Start the Pitch
            Debug.Log("Pitch!");
        }
        else
        {
            Debug.Log("Next batter: " + this.CurrentBaseIndex);
            this.SetCurrentBase(this.CurrentBaseIndex);
            this.SetCurrentOption(0);
        }
    }


    protected void SwingOptionPressed()
    {
        Debug.Log("SwingOptionPressed");
        this.SetCurrentOption(0);
        this.NextBase();
    }


    protected void PassOptionPressed()
    {
        Debug.Log("PassOptionPressed");
        this.SetCurrentOption(1);
        this.NextBase();
    }


    protected void BuntOptionPressed()
    {
        Debug.Log("BuntOptionPressed");
        this.SetCurrentOption(2);
        this.NextBase();
    }


    protected void LeadOptionPressed()
    {
        Debug.Log("LeadOptionPressed");
        this.SetCurrentOption(1);
        this.NextBase();
    }


    protected void StealOptionPressed()
    {
        Debug.Log("StealOptionPressed");
        this.SetCurrentOption(2);
        this.NextBase();
    }


    protected void UsePotionOptionPressed()
    {
        Debug.Log("UsePotionOptionPressed");
        this.SetCurrentOption(3);
    }


    protected void CastSpellOptionPressed()
    {
        Debug.Log("CastSpellOptionPressed");
        this.SetCurrentOption(4);
    }


    public override void UpdateState()
    {
        base.UpdateState();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (--this.CurrentOptionIndex < 0)
            {
                this.CurrentOptionIndex = this.CurrentOptions.Count - 1;
            }

            this.SetCurrentOption(this.CurrentOptionIndex);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (++this.CurrentOptionIndex >= this.CurrentOptions.Count)
            {
                this.SetCurrentOption(0);
            }

            this.SetCurrentOption(this.CurrentOptionIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            this.CurrentOptions[this.CurrentOptionIndex].Button.onClick.Invoke();
        }
    }

}
