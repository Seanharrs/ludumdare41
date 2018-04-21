using UnityEngine;

public class GUIHealthBar : MonoBehaviour
{
    public enum Orientation { Horizontal, Vertical }
    public Orientation orientation;

    private RectTransform rect;

    private const float FULL_HEALTH_TOP = 40f;
    private const float NO_HEALTH_TOP = 130f;

    private const float FULL_HEALTH_RIGHT = -55f;
    private const float NO_HEALTH_RIGHT = 154f;

    private void Awake()
    {
        rect = GetComponentInChildren<RectTransform>();
    }

    public void UpdateGUI(int curHP, int maxHP)
    {
        float percent = (float)curHP / maxHP;
        switch(orientation)
        {
            case Orientation.Vertical:
                float newRectTop = NO_HEALTH_TOP - ((NO_HEALTH_TOP - FULL_HEALTH_TOP) * percent);
                rect.offsetMax = new Vector2(rect.offsetMax.x, -newRectTop);
                break;
            case Orientation.Horizontal:
                float newRectRight = NO_HEALTH_RIGHT - ((NO_HEALTH_RIGHT - FULL_HEALTH_RIGHT) * percent);
                rect.offsetMax = new Vector2(-newRectRight, rect.offsetMax.y);
                break;
        }
    }
}
