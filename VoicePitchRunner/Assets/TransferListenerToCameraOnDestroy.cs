using UnityEngine;
public class TransferListenerToCameraOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        if(gameObject != null) {
            var camera = FindObjectOfType<Camera>();
            if (camera != null) {
                camera.GetComponent<AudioListener>().enabled = true;
            }
        }
    }
}
