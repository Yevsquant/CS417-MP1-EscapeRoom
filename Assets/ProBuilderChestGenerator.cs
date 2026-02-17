using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using System.Linq;

public class ProBuilderChestGenerator : MonoBehaviour
{
    [Header("Chest Dimensions")]
    public float width = 1.0f;
    public float height = 0.6f;
    public float depth = 0.7f;
    public float wallThickness = 0.05f;

    [Header("Lid")]
    public float lidHeight = 0.12f;

    [Header("Materials")]
    public Material baseMaterial;
    public Material lidMaterial;

    [ContextMenu("Generate Chest")]
    public void Generate()
    {
        // Clean up previous children
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        GameObject body = new GameObject("Chest_Body");
        body.transform.SetParent(transform);
        body.transform.localPosition = Vector3.zero;

        BuildHollowBody(body.transform);
        BuildLid();

        Debug.Log("Hollow chest generated! You can place objects inside the body.");
    }

    private void BuildHollowBody(Transform parent)
    {
        float innerW = width - wallThickness * 2f;
        float innerD = depth - wallThickness * 2f;

        // Floor
        CreateSlab(parent, "Floor",
            new Vector3(width, wallThickness, depth),
            new Vector3(0, wallThickness / 2f, 0));

        // Front wall (+Z)
        CreateSlab(parent, "Wall_Front",
            new Vector3(width, height, wallThickness),
            new Vector3(0, height / 2f, (depth - wallThickness) / 2f));

        // Back wall (-Z)
        CreateSlab(parent, "Wall_Back",
            new Vector3(width, height, wallThickness),
            new Vector3(0, height / 2f, -(depth - wallThickness) / 2f));

        // Left wall (-X)
        CreateSlab(parent, "Wall_Left",
            new Vector3(wallThickness, height, innerD),
            new Vector3(-(width - wallThickness) / 2f, height / 2f, 0));

        // Right wall (+X)
        CreateSlab(parent, "Wall_Right",
            new Vector3(wallThickness, height, innerD),
            new Vector3((width - wallThickness) / 2f, height / 2f, 0));
    }

    private void CreateSlab(Transform parent, string name, Vector3 size, Vector3 localPos)
    {
        ProBuilderMesh slab = ShapeGenerator.GenerateCube(PivotLocation.Center, size);
        slab.gameObject.name = name;
        slab.transform.SetParent(parent);
        slab.transform.localPosition = localPos;

        slab.ToMesh();
        slab.Refresh();

        if (baseMaterial != null)
            slab.GetComponent<MeshRenderer>().sharedMaterial = baseMaterial;

        // Add collider so objects rest inside properly
        BoxCollider col = slab.gameObject.AddComponent<BoxCollider>();
        col.size = size;
        col.center = Vector3.zero;
    }

    private void BuildLid()
    {
        ProBuilderMesh lid = ShapeGenerator.GenerateCube(PivotLocation.Center, new Vector3(width, lidHeight, depth));
        lid.gameObject.name = "Chest_Lid";
        lid.transform.SetParent(transform);

        float lidY = height + lidHeight / 2f;
        lid.transform.localPosition = new Vector3(0, lidY, 0);

        // Shift pivot to back edge for hinge rotation
        Vector3 pivotOffset = new Vector3(0, 0, depth / 2f);
        lid.TranslateVertices(Enumerable.Range(0, lid.vertexCount), pivotOffset);
        lid.transform.localPosition -= pivotOffset;

        lid.ToMesh();
        lid.Refresh();

        Material mat = lidMaterial != null ? lidMaterial : baseMaterial;
        if (mat != null)
            lid.GetComponent<MeshRenderer>().sharedMaterial = mat;

        BoxCollider lidCol = lid.gameObject.AddComponent<BoxCollider>();
        lidCol.size = new Vector3(width, lidHeight, depth);
        lidCol.center = new Vector3(0, 0, depth / 2f);
    }
}