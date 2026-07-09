using UnityEngine;

namespace MyLittleBoat
{
    public static class MaterialUtility
    {
        public static Material CreateColoredMaterial(string materialName, Color color)
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null)
            {
                shader = Shader.Find("Standard");
            }

            Material material = new Material(shader);
            material.name = materialName;
            material.color = color;
            return material;
        }
    }
}
