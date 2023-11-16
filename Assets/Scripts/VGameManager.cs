using UnityEngine;
using Unity.Netcode;

public class VGameManager : NetworkBehaviour
{
    private NetworkVariable<int> totalButtonPresses = new NetworkVariable<int>(0);

    public static VGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void PressButtonServerRpc()
    {
        if (IsServer)
        {
            totalButtonPresses.Value++;
            UpdateClientUIs();
        }
    }

    private void UpdateClientUIs()
    {
        UpdateUIsClientRpc(totalButtonPresses.Value);
    }

    [ClientRpc]
    private void UpdateUIsClientRpc(int totalPresses)
    {
        Debug.Log("totalClick:"+ totalPresses);
        // ここでクライアント側のUIを更新
    }
}
