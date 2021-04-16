using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    void Start()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite handgunSprite;
    public Sprite shotgunSprite;
    public Sprite smgSprite;
    public Sprite pillsSprite;
    public Sprite medkitSprite;
}
