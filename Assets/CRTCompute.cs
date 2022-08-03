using UnityEngine;

namespace TestCrt
{
    public class CRTCompute : MonoBehaviour
    {
        [SerializeField] 
        private CustomRenderTexture _texture;

        [SerializeField] 
        private int _iterationPerFrame = 5;
        
        
        private void Start()
        {
            _texture.Initialize();
            _texture.Update();
        }
        
        private void Update()
        {
            _texture.ClearUpdateZones();
            UpdateZones();
            _texture.Update(_iterationPerFrame);
        }
        
        
        private void UpdateZones()
        {
            if (!GetInput(out Vector3 fingerPosition))
            {
                return;
            }
            
            var ray = Camera.main.ScreenPointToRay(fingerPosition);
            if (!Physics.Raycast(ray, out RaycastHit hit))
            {
                return;
            }
            
            var defaultZone = new CustomRenderTextureUpdateZone();
            defaultZone.needSwap = true;
            defaultZone.passIndex = 0;
            defaultZone.rotation = 0f;
            defaultZone.updateZoneCenter = new Vector2(0.5f, 0.5f);
            defaultZone.updateZoneSize = new Vector2(1f, 1f);
            
            var clickZone = new CustomRenderTextureUpdateZone();
            clickZone.needSwap = true;
            clickZone.passIndex = 1;
            clickZone.rotation = 0f;
            clickZone.updateZoneCenter = new Vector2(hit.textureCoord.x, 1f - hit.textureCoord.y);
            clickZone.updateZoneSize = new Vector2(0.01f, 0.01f);
            
            _texture.SetUpdateZones(new[] { defaultZone, clickZone });
        }
        
        private bool GetInput(out Vector3 fingerPosition)
        {
            fingerPosition = Vector3.zero;

            if (Input.touchCount > 0)
            {
                fingerPosition = Input.touches[0].position;
            }
            else
            {
                bool leftClick = Input.GetMouseButton(0);
                bool rightClick = Input.GetMouseButton(1);
                if (!leftClick && !rightClick)
                {
                    return false;
                }

                fingerPosition = Input.mousePosition;
            }

            return true;
        }
        
    }
}
