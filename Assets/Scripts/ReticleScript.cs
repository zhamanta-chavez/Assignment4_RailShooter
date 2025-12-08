using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void OnMouseEnter()
    {
        Debug.Log("Entered");
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseExit()
    {
        Debug.Log("Exited");
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
