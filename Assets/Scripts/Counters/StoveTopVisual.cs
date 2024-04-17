using UnityEngine;

public class StoveTopVisual : CounterVisual
{
    private Sprite originalSprite;
    private Sprite alternateSprite;
    private new void Start()
    {
        base.Start();
        GetComponent<StoveTopInteract>().OnStateChanged += StoveTopOnStateChanged;
        originalSprite = sr.sprite;
        alternateSprite = GetAlternateSprite(originalSprite);
    }

    private void StoveTopOnStateChanged(object sender, StoveTopInteract.OnStateChangedEventArgs e)
    {
        if (e.state is StoveTopInteract.States.Cooking or StoveTopInteract.States.Cooked)
        {
            sr.sprite = alternateSprite;
            return;
        }

        sr.sprite = originalSprite;
    }

    private Sprite GetAlternateSprite(Sprite originalSprite)
    {
        var tileSprites = TileSprites.Instance;
        int alternateSpriteNumber = 0;
        switch (tileSprites.GetSpriteNumber(originalSprite))
        {
            case 45:
                alternateSpriteNumber = 38;
                break;
            case 46:
                alternateSpriteNumber = 39;
                break;
            case 47:
                alternateSpriteNumber = 40;
                break;
            case 48:
                alternateSpriteNumber = 41;
                break;
            case 49:
                alternateSpriteNumber = 43;
                break;
            case 50:
                alternateSpriteNumber = 44;
                break;
            case 51:
                alternateSpriteNumber = 42;
                break;
            default:
                Debug.LogError("Sprite Number invalid");
                break;
        }

        return tileSprites.GetSpriteFromNumber(alternateSpriteNumber);
    }
}