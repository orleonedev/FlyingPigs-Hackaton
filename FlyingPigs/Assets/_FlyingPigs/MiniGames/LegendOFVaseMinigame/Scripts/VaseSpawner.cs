using System.Collections.Generic;
using UnityEngine;

public class VaseSpawner : MonoBehaviour
{
    public GameObject blueVasePrefab;
    public GameObject redVasePrefab;
    public Transform playArea;
    public int totalNumberOfVases;   
    public float blueVaseProbability; // Valore compreso tra 0 e 1, ad es. 0.7 per il 70% di probabilità di un vaso blu.
    public int numberOfBlueVases = 0;
   
    // Lista per tenere traccia delle posizioni degli oggetti generati
    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        for (int i = 0; i < totalNumberOfVases; i++)        {
            // Genera una posizione casuale
            Vector3 spawnPosition = GetRandomSpawnPosition(playArea.position, playArea.localScale / 2.5f);
            // Verifica che la posizione soddisfi i criteri (distanza minima)
            while (!IsPositionValid(spawnPosition))
            {
                spawnPosition = GetRandomSpawnPosition(playArea.position, playArea.localScale / 2.5f);            }

            // Aggiungi la posizione alla lista delle posizioni generata
            spawnedPositions.Add(spawnPosition);

            // Scegli casualmente tra il prefab "BlueVase" e "RedVase" tenendo conto della blue vase probability
            GameObject selectedPrefab = Random.Range(0f, 1f) < blueVaseProbability ? blueVasePrefab : redVasePrefab;
            if (selectedPrefab == blueVasePrefab)
            {
                numberOfBlueVases++;
            }

            // Istanzia il prefab scelto alla posizione generata
            GameObject vase = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            // Imposta il "playArea" come genitore dell'oggetto
            vase.transform.parent = playArea;
        }
    }

    // Restituisce una posizione casuale nell'intervallo desiderato
    Vector3 GetRandomSpawnPosition(Vector3 center, Vector3 size)    {
        float x = Random.Range(center.x - size.x, center.x + size.x);        
        float y = Random.Range(center.y - size.y, center.y + size.y);        
        return new Vector3(x, y, 0f);
    }

    // Verifica se una posizione è valida (distanza minima da altre posizioni)
    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 otherPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, otherPosition) < 0.1f)
            {
                return false;
            }
        }
        return true;
    }
}

