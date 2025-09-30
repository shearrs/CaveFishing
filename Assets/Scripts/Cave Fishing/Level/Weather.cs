using UnityEngine;

namespace CaveFishing.Levels
{
    public class Weather : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private float rotationSpeed;

        private float rotation;

        private void Update()
        {
            rotation += rotationSpeed * Time.deltaTime;

            if (rotation > 360f)
                rotation -= 360f;

            material.SetFloat("_Rotation", rotation);
        }
    }
}
