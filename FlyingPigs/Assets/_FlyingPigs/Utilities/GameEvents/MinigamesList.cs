using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="MinigamesList", menuName ="MinigameListScriptableObj")]
public class MinigamesList : SingletonScriptableObject<MinigamesList>
{
   public List<string> MinigamesNames;

   public List<string> PlayedGamesOfTheDay;
}
