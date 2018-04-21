using UnityEngine;

public interface ICard
{
    string name { get; }
    int cost { get; }
    string description { get; }
    Sprite image { get; }
}
