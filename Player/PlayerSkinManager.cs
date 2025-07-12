using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    [SerializeField] private GameObject[] skinPrefabs;
    private GameObject currentSkin;

    private void Start()
    {
        // Debug.Log("PlayerSkinManager started.");
        ChangePlayerSkin();
    }

    void ChangePlayerSkin()
    {
        int selectedSkin = GameDataManager.GetSelectedCharacterIndex();

        if (selectedSkin < 0 || selectedSkin >= skinPrefabs.Length || skinPrefabs[selectedSkin] == null)
        {
            // Debug.LogWarning("Invalid skin index or missing prefab!");
            return;
        }

        // Xóa skin cũ 
        if (currentSkin != null)
            Destroy(currentSkin);

        // Instantiate skin tại đúng localPosition = Vector3.zero dưới object "Player"
        currentSkin = Instantiate(skinPrefabs[selectedSkin], transform);
        currentSkin.transform.localPosition = Vector3.zero;
        currentSkin.transform.localRotation = Quaternion.identity;
        currentSkin.transform.localScale = Vector3.one;

        currentSkin.SetActive(true);

        // Debug.Log("Instantiated skin: " + currentSkin.name);

        SpriteRenderer sr = currentSkin.GetComponentInChildren<SpriteRenderer>();
        // if (sr == null)
        // {
        //     Debug.LogError("❌ Không tìm thấy SpriteRenderer trong skin prefab!");
        // }
        // else
        // {
        //     Debug.Log("✅ SpriteRenderer FOUND! Sprite: " + sr.sprite?.name);
        // }
    }
}
