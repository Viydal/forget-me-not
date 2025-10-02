// using System.Collections.Generic;
// using UnityEngine;

// class GameManager : MonoBehaviour
// {
//     PlayerRecorder playerRecorder;
//     GameObject shadowPrefab;
//     Transform spawnPoint;

//     void Start()
//     {
//         StartNewLife();
//     }

//     void StartNewLife()
//     {
//         playerRecorder.StartRecording();
//         RespawnPlayer();
//     }

//     void OnPlayerDeath()
//     {
//         // Stop recording and create a shadow
//         List<InputFrame> data = playerRecorder.StopRecording();
//         SpawnShadow(data);

//         // Start new life
//         StartNewLife();
//     }

//     void SpawnShadow(List<InputFrame> data)
//     {
//         GameObject shadow = Instantiate(shadowPrefab, spawnPoint.position, Quaternion.identity);
//         shadow.GetComponent<ShadowReplayer>().Init(data);
//     }

//     void RespawnPlayer()
//     {
//         // Instantiate player at spawn point
//         Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
//     }
// }
