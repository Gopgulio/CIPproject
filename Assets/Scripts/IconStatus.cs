using UnityEngine;
using UnityEngine.UI;

public class IconStatus : MonoBehaviour
{

    private int health;

    public Sprite stateNormal;
    public Sprite stateLightInjury;
    public Sprite stateWounded;
    public Sprite stateNearDeath;

    private Sprite getIcon()
    {
        switch (health)
        {
            default:
            case var expression when health > 80: return stateNormal;
            case var expression when (health <= 80 && health > 55): return stateLightInjury;
            case var expression when (health <= 55 && health > 30): return stateWounded;
            case var expression when health <= 30: return stateNearDeath;
        }

    }

    private string getText()
    {
        switch (health)
        {
            default:
            case var expression when health > 80: return "You are feeling well.";
            case var expression when (health <= 80 && health > 55): return "You are lightly injured.";
            case var expression when (health <= 55 && health > 30): return "You are severely wounded.";
            case var expression when health <= 30: return "You are almost dead.";
        }

    }

    public void changeState(int health)
    {
        this.health = health;
        GetComponent<Image>().sprite = getIcon();
        GetComponent<DescriptionElement>().setText(getText());
    }
}
