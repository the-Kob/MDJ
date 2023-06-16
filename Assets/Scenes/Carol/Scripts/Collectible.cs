
using Scenes.Carol.Scripts;
using UnityEngine;
using System.Collections;

public abstract class Collectible : MonoBehaviour, ICollectible
{
    public abstract void Collect();
}
