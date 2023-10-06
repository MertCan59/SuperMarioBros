using UnityEngine;
public class SideScrolling : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float height = 7.9f;
    public float undergroundHeight = -104f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition=transform.position;
        cameraPosition.x=Mathf.Max(cameraPosition.x,player.position.x);
        transform.position=cameraPosition;
    }
    public void SetUnderground(bool underground)
    {
        Vector3 cameraPosition=transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position=cameraPosition;
    }
}
