using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Connection")]
public class LevelConnection : ScriptableObject
{
  public static LevelConnection ActiveConnecttion {  get; set; }
   
    public float fadeDuration = 1.0f;  
    public string loadingScreenSceneName;

}
