using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

public static class DungeonArenaBuilder
{
    private const string ScenePath = "Assets/ArcaneTrials/Scenes/Test.unity";
    private const string ModelPath = "Assets/ThirdParty/Dungeon/Free Modular Low Poly Dungeon/low poly dungeon assets.fbx";
    private const string RootName = "Generated_Dungeon_Arena";
    private const string MaterialFolder = "Assets/ArcaneTrials/Generated/ArenaMaterials";
    private const float Radius = 20f;

    [MenuItem("Tools/Arcane Trials/Build Dungeon Arena in Test")]
    public static void Build()
    {
        if (EditorSceneManager.GetActiveScene().path != ScenePath)
        {
            EditorUtility.DisplayDialog("Arena Builder", "Open the Test scene first.", "OK");
            return;
        }

        GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(ModelPath);
        if (model == null)
            throw new InvalidOperationException($"Dungeon model not found: {ModelPath}");

        EnsureFolders();
        Material darkStone = Material("DarkStone.mat", new Color(0.045f, 0.05f, 0.065f), false);
        Material iron = Material("BlackIron.mat", new Color(0.025f, 0.018f, 0.025f), false);
        Material blue = Material("PortalBlue.mat", new Color(0.01f, 0.35f, 0.9f), true);
        Material red = Material("DemonRed.mat", new Color(0.75f, 0.015f, 0.025f), true);

        GameObject oldRoot = GameObject.Find(RootName);
        if (oldRoot != null) Object.DestroyImmediate(oldRoot);

        GameObject root = new GameObject(RootName);
        Undo.RegisterCreatedObjectUndo(root, "Build Dungeon Arena");
        Transform floorGroup = Group("01_Floor", root.transform);
        Transform wallGroup = Group("02_Walls", root.transform);
        Transform portalGroup = Group("03_BossPortal", root.transform);
        Transform propGroup = Group("04_Props", root.transform);
        Transform lightGroup = Group("05_Lighting", root.transform);
        Transform gameplayGroup = Group("06_Gameplay", root.transform);

        GameObject templateRoot = (GameObject)PrefabUtility.InstantiatePrefab(model);
        templateRoot.hideFlags = HideFlags.HideAndDontSave;
        templateRoot.SetActive(true);

        try
        {
            Templates t = new Templates(templateRoot);
            Foundation(floorGroup, darkStone, iron);
            Floor(t, floorGroup);
            Walls(t, wallGroup, propGroup);
            Portal(t, portalGroup, wallGroup, blue, red, iron);
            PropsAndLights(t, propGroup, lightGroup, blue, red);
            Markers(gameplayGroup);
            ConfigureScene();
        }
        finally
        {
            Object.DestroyImmediate(templateRoot);
        }

        Selection.activeGameObject = root;
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        AssetDatabase.SaveAssets();
        Debug.Log("Dungeon arena generated and saved in Test.unity", root);
    }

    private static void Foundation(Transform parent, Material stone, Material iron)
    {
        Primitive("OuterIronBase", PrimitiveType.Cylinder, parent, new Vector3(0, -0.43f, 0),
            new Vector3(46f, 0.28f, 46f), iron, false);
        Primitive("StoneFoundation", PrimitiveType.Cylinder, parent, new Vector3(0, -0.27f, 0),
            new Vector3(43f, 0.32f, 43f), stone, false);
    }

    private static void Floor(Templates t, Transform parent)
    {
        GameObject template = t.Find("floor", "floor.001", "floor.002", "floor.003");
        if (template == null) throw new InvalidOperationException("No floor mesh found in dungeon FBX");

        const float cell = 3.15f;
        int index = 0;
        for (int x = -6; x <= 6; x++)
        for (int z = -6; z <= 6; z++)
        {
            Vector3 p = new Vector3(x * cell, 0, z * cell);
            if (new Vector2(p.x, p.z).magnitude > Radius - 1.5f) continue;
            GameObject tile = Clone(template, $"Floor_{index++:000}", parent);
            Fit(tile, new Vector3(3.28f, 0.18f, 3.28f), p + Vector3.down * 0.03f, Quaternion.identity);
            AddBox(tile, 0.98f);
        }
    }

    private static void Walls(Templates t, Transform walls, Transform props)
    {
        GameObject wall = t.Find("brick wall", "smooth wall", "brick wall.001", "ruined wall");
        GameObject pillar = t.Find("pillar");
        if (wall == null) throw new InvalidOperationException("No wall mesh found in dungeon FBX");

        const int segments = 32;
        for (int i = 0; i < segments; i++)
        {
            float angle = i * 360f / segments;
            if (Opening(angle, 0, 17) || Opening(angle, 180, 17)) continue;
            Vector3 radial = Direction(angle);
            Quaternion rotation = Quaternion.LookRotation(-radial);
            GameObject piece = Clone(wall, $"Wall_{i:00}", walls);
            Fit(piece, new Vector3(4.35f, 3.8f, 0.75f), radial * Radius + Vector3.up * 1.9f, rotation);
            AddBox(piece, 0.94f);

            if (pillar != null && i % 4 == 0)
            {
                GameObject p = Clone(pillar, $"Pillar_{i:00}", props);
                Fit(p, new Vector3(1.35f, 4.8f, 1.35f), radial * (Radius - 0.35f) + Vector3.up * 2.4f, rotation);
                AddBox(p, 0.9f);
            }
        }

        OpeningPillars(pillar, props, 0, "Portal");
        OpeningPillars(pillar, props, 180, "Entrance");
    }

    private static void OpeningPillars(GameObject template, Transform parent, float angle, string prefix)
    {
        if (template == null) return;
        Vector3 radial = Direction(angle);
        Vector3 tangent = new Vector3(radial.z, 0, -radial.x);
        foreach (int side in new[] { -1, 1 })
        {
            GameObject p = Clone(template, $"{prefix}_Pillar_{side}", parent);
            Fit(p, new Vector3(1.6f, 5.6f, 1.6f),
                radial * (Radius - 0.1f) + tangent * side * 3.1f + Vector3.up * 2.8f,
                Quaternion.LookRotation(-radial));
            AddBox(p, 0.9f);
        }
    }

    private static void Portal(Templates t, Transform portal, Transform walls, Material blue, Material red, Material iron)
    {
        GameObject wall = t.Find("brick wall", "smooth wall", "brick wall.001");
        GameObject arch = t.Find("door arc", "door arc.037", "door arc.038", "brick wall with door");
        GameObject stairs = t.Find("stairs", "stairs.001", "stairs.002", "stairs.003");

        for (int row = 0; row < 3; row++)
        foreach (int side in new[] { -1, 1 })
        {
            GameObject piece = Clone(wall, $"PortalTunnel_{row}_{side}", walls);
            Fit(piece, new Vector3(4.2f, 4.2f, 0.75f),
                new Vector3(side * 3.55f, 2.1f, Radius + 2.1f + row * 3.9f),
                Quaternion.Euler(0, 90, 0));
            AddBox(piece, 0.95f);
        }

        if (arch != null)
        {
            GameObject a = Clone(arch, "BossPortal_Arch", portal);
            Fit(a, new Vector3(8.5f, 7.2f, 1.4f), new Vector3(0, 3.6f, Radius + 9f), Quaternion.Euler(0, 180, 0));
        }
        if (stairs != null)
        {
            GameObject s = Clone(stairs, "BossExit_Stairs", portal);
            Fit(s, new Vector3(6.5f, 1.2f, 7.5f), new Vector3(0, 0.55f, Radius + 3.5f), Quaternion.Euler(0, 180, 0));
            AddBox(s, 0.98f);
        }

        Primitive("PortalFrame", PrimitiveType.Cube, portal, new Vector3(0, 3.5f, Radius + 9.25f),
            new Vector3(7.2f, 6.3f, 0.65f), iron, true);
        GameObject energy = Primitive("PortalEnergy_OccludesBossSpawn", PrimitiveType.Quad, portal,
            new Vector3(0, 3.5f, Radius + 8.88f), new Vector3(6.2f, 5.2f, 1), blue, false);
        energy.transform.rotation = Quaternion.Euler(0, 180, 0);
        Rune("PortalRune", portal, new Vector3(0, 0.035f, Radius + 8f), 4.5f, red);
        PointLight("PortalLight", portal, new Vector3(0, 3.6f, Radius + 7.5f), new Color(0.02f, 0.55f, 1), 10, 8);
    }

    private static void PropsAndLights(Templates t, Transform props, Transform lights, Material blue, Material red)
    {
        GameObject torch = t.Find("torch", "torch.001");
        GameObject skull = t.Find("skull", "bone");
        GameObject rock = t.Find("rock", "rock.001", "rock.002", "rock.003");

        for (int i = 0; i < 12; i++)
        {
            float angle = 15 + i * 30;
            if (Opening(angle, 0, 20) || Opening(angle, 180, 20)) continue;
            Vector3 radial = Direction(angle);
            Vector3 p = radial * (Radius - 1.05f);
            Color c = i % 2 == 0 ? new Color(1, 0.025f, 0.015f) : new Color(0.015f, 0.4f, 1);
            if (torch != null)
            {
                GameObject item = Clone(torch, $"Torch_{i:00}", props);
                Fit(item, new Vector3(0.75f, 1.5f, 0.75f), p + Vector3.up * 2.35f, Quaternion.LookRotation(-radial));
            }
            PointLight($"Light_{i:00}", lights, p + Vector3.up * 2.7f, c, 6, 5.5f);
        }

        for (int i = 0; i < 8; i++)
        {
            float angle = 22.5f + i * 45;
            Vector3 radial = Direction(angle);
            Vector3 tangent = new Vector3(radial.z, 0, -radial.x);
            Vector3 p = radial * (Radius - 1.8f);
            if (skull != null)
            {
                GameObject item = Clone(skull, $"Skull_{i:00}", props);
                Fit(item, Vector3.one * 0.8f, p + Vector3.up * 0.42f, Quaternion.LookRotation(-radial));
            }
            if (rock != null && i % 2 == 0)
            {
                GameObject item = Clone(rock, $"Rubble_{i:00}", props);
                Fit(item, new Vector3(1.6f, 0.75f, 1.2f), p - tangent * 0.9f + Vector3.up * 0.35f, Quaternion.Euler(0, angle * 1.7f, 0));
            }
        }

        Rune("BlueRune", props, new Vector3(-8.5f, 0.045f, 8.5f), 2.1f, blue);
        Rune("RedRune", props, new Vector3(8.5f, 0.045f, 8.5f), 2.1f, red);
    }

    private static void Markers(Transform parent)
    {
        Marker("PlayerSpawn", parent, new Vector3(0, 0.2f, -14.2f), Vector3.forward);
        Marker("BossSpawn_HiddenBehindPortal", parent, new Vector3(0, 0.2f, Radius + 10.2f), Vector3.back);
        Marker("BossEntrance", parent, new Vector3(0, 0.2f, Radius - 3.3f), Vector3.back);
        Marker("ArenaCenter", parent, new Vector3(0, 0.2f, 0), Vector3.forward);
    }

    private static void ConfigureScene()
    {
        Camera camera = Camera.main;
        if (camera != null)
        {
            camera.transform.position = new Vector3(0, 38, -33);
            camera.transform.rotation = Quaternion.Euler(48, 0, 0);
            camera.fieldOfView = 52;
            camera.farClipPlane = 180;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.008f, 0.006f, 0.012f);
        }

        Light sun = Object.FindObjectsByType<Light>(FindObjectsSortMode.None).FirstOrDefault(x => x.type == LightType.Directional);
        if (sun != null)
        {
            sun.transform.rotation = Quaternion.Euler(54, -32, 0);
            sun.color = new Color(0.34f, 0.39f, 0.52f);
            sun.intensity = 0.65f;
            sun.shadows = LightShadows.Soft;
        }
        GameObject plane = GameObject.Find("Plane");
        if (plane != null) plane.SetActive(false);

        // Keep user-imported FBX showcases recoverable, but hide them from the finished arena.
        foreach (GameObject sceneRoot in EditorSceneManager.GetActiveScene().GetRootGameObjects())
        {
            string prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(sceneRoot);
            if (sceneRoot.name.Equals("low poly dungeon sample", StringComparison.OrdinalIgnoreCase) ||
                sceneRoot.name.Equals("low poly dungeon assets", StringComparison.OrdinalIgnoreCase) ||
                sceneRoot.name.Equals("window.001", StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(prefabPath) &&
                 prefabPath.StartsWith("Assets/ThirdParty/Dungeon/", StringComparison.OrdinalIgnoreCase)))
            {
                sceneRoot.SetActive(false);
            }
        }

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.0085f;
        RenderSettings.fogColor = new Color(0.018f, 0.012f, 0.025f);
        RenderSettings.ambientMode = AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = new Color(0.06f, 0.075f, 0.11f);
        RenderSettings.ambientEquatorColor = new Color(0.035f, 0.027f, 0.045f);
        RenderSettings.ambientGroundColor = new Color(0.012f, 0.009f, 0.015f);
        RenderSettings.ambientIntensity = 0.72f;
    }

    private static GameObject Clone(GameObject source, string name, Transform parent)
    {
        if (source == null) return null;
        GameObject clone = Object.Instantiate(source);
        clone.name = name;
        clone.hideFlags = HideFlags.None;
        clone.transform.SetParent(parent, false);
        clone.SetActive(true);

        // FBX nodes are nested like a linked list. Keep only the selected node's own mesh;
        // otherwise cloning one module also clones the remainder of the asset showcase.
        foreach (Transform directChild in clone.transform.Cast<Transform>().ToArray())
            Object.DestroyImmediate(directChild.gameObject);

        foreach (Transform child in clone.GetComponentsInChildren<Transform>(true)) child.gameObject.hideFlags = HideFlags.None;
        foreach (Collider c in clone.GetComponentsInChildren<Collider>(true)) Object.DestroyImmediate(c);
        foreach (Camera c in clone.GetComponentsInChildren<Camera>(true)) Object.DestroyImmediate(c);
        foreach (Light l in clone.GetComponentsInChildren<Light>(true)) Object.DestroyImmediate(l);
        return clone;
    }

    private static void Fit(GameObject target, Vector3 desiredSize, Vector3 desiredCenter, Quaternion rotation)
    {
        target.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        Bounds before = Bounds(target);
        Vector3 size = before.size;
        size.x = Mathf.Max(size.x, 0.0001f); size.y = Mathf.Max(size.y, 0.0001f); size.z = Mathf.Max(size.z, 0.0001f);
        target.transform.localScale = Vector3.Scale(target.transform.localScale,
            new Vector3(desiredSize.x / size.x, desiredSize.y / size.y, desiredSize.z / size.z));
        target.transform.rotation = rotation;
        target.transform.position += desiredCenter - Bounds(target).center;
    }

    private static void AddBox(GameObject target, float shrink)
    {
        Bounds world = Bounds(target);
        BoxCollider box = target.AddComponent<BoxCollider>();
        box.center = target.transform.InverseTransformPoint(world.center);
        Vector3 scale = target.transform.lossyScale;
        box.size = new Vector3(world.size.x / Mathf.Max(Mathf.Abs(scale.x), .0001f),
            world.size.y / Mathf.Max(Mathf.Abs(scale.y), .0001f), world.size.z / Mathf.Max(Mathf.Abs(scale.z), .0001f)) * shrink;
    }

    private static Bounds Bounds(GameObject target)
    {
        Renderer[] renderers = target.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0) return new Bounds(target.transform.position, Vector3.one);
        Bounds b = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) b.Encapsulate(renderers[i].bounds);
        return b;
    }

    private static GameObject Primitive(string name, PrimitiveType type, Transform parent, Vector3 pos, Vector3 scale, Material mat, bool collider)
    {
        GameObject go = GameObject.CreatePrimitive(type);
        go.name = name; go.transform.SetParent(parent); go.transform.position = pos; go.transform.localScale = scale;
        go.GetComponent<Renderer>().sharedMaterial = mat;
        if (!collider) Object.DestroyImmediate(go.GetComponent<Collider>());
        return go;
    }

    private static void Rune(string name, Transform parent, Vector3 pos, float diameter, Material mat)
    {
        Primitive(name, PrimitiveType.Cylinder, parent, pos, new Vector3(diameter, .015f, diameter), mat, false);
    }

    private static void PointLight(string name, Transform parent, Vector3 pos, Color color, float range, float intensity)
    {
        GameObject go = new GameObject(name); go.transform.SetParent(parent); go.transform.position = pos;
        Light light = go.AddComponent<Light>(); light.type = LightType.Point; light.color = color;
        light.range = range; light.intensity = intensity; light.shadows = LightShadows.None;
    }

    private static void Marker(string name, Transform parent, Vector3 pos, Vector3 forward)
    {
        GameObject go = new GameObject(name); go.transform.SetParent(parent); go.transform.position = pos;
        go.transform.rotation = Quaternion.LookRotation(forward);
    }

    private static Transform Group(string name, Transform parent)
    {
        GameObject go = new GameObject(name); go.transform.SetParent(parent); return go.transform;
    }

    private static Vector3 Direction(float angle)
    {
        float r = angle * Mathf.Deg2Rad; return new Vector3(Mathf.Sin(r), 0, Mathf.Cos(r));
    }

    private static bool Opening(float angle, float center, float halfWidth) => Mathf.Abs(Mathf.DeltaAngle(angle, center)) <= halfWidth;

    private static void EnsureFolders()
    {
        if (!AssetDatabase.IsValidFolder("Assets/ArcaneTrials/Generated")) AssetDatabase.CreateFolder("Assets/ArcaneTrials", "Generated");
        if (!AssetDatabase.IsValidFolder(MaterialFolder)) AssetDatabase.CreateFolder("Assets/ArcaneTrials/Generated", "ArenaMaterials");
    }

    private static Material Material(string fileName, Color color, bool emission)
    {
        string path = $"{MaterialFolder}/{fileName}";
        Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (mat == null)
        {
            mat = new Material(Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard"));
            AssetDatabase.CreateAsset(mat, path);
        }
        if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", color);
        if (mat.HasProperty("_Color")) mat.SetColor("_Color", color);
        if (mat.HasProperty("_Metallic")) mat.SetFloat("_Metallic", emission ? .15f : .35f);
        if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", emission ? .65f : .32f);
        if (emission && mat.HasProperty("_EmissionColor"))
        {
            mat.EnableKeyword("_EMISSION"); mat.SetColor("_EmissionColor", color * 3.5f);
        }
        EditorUtility.SetDirty(mat); return mat;
    }

    private sealed class Templates
    {
        private readonly List<GameObject> objects;
        public Templates(GameObject root) => objects = root.GetComponentsInChildren<Transform>(true)
            .Where(x => x.GetComponent<Renderer>() != null).Select(x => x.gameObject)
            .OrderBy(x => x.transform.childCount).ToList();

        public GameObject Find(params string[] names)
        {
            foreach (string n in names)
            {
                GameObject exact = objects.FirstOrDefault(x => string.Equals(x.name, n, StringComparison.OrdinalIgnoreCase));
                if (exact != null) return exact;
            }
            foreach (string n in names)
            {
                GameObject partial = objects.FirstOrDefault(x => x.name.IndexOf(n, StringComparison.OrdinalIgnoreCase) >= 0);
                if (partial != null) return partial;
            }
            Debug.LogWarning($"No dungeon template found for: {string.Join(", ", names)}"); return null;
        }
    }
}
