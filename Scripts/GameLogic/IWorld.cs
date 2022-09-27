using UnityEditor;
using UnityEngine;

public interface IWorld
{
    public float WorldWidth { get; }
    public float WorldHeight { get; }
    public float WorldUpperBound { get; }
    public float WorldLowerBound { get; }
    public float WorldLeftBound { get; }
    public float WorldRightBound { get; }
    public Vector2 WorldCenter { get; }

    public PlayerInput.PlayerActions GetPlayerActions();
}