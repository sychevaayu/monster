using UnityEditor;
using UnityEngine;

public class Window : EditorWindow
{
    public Color myColor;         // Градиент цвета
    public MeshRenderer GO;      // Ссылка на рендер объекта
    public Material material;
    
    private Transform _camera;
    private PrimitiveType _primitiveType;
    private bool _isExpanded;
    private Vector3 _customPosition;

    [MenuItem("Инструменты/Окна/MyAdvancedGUI 4")]
    public static void ShowMyWindow()
    {
        // Отрисовываем окно для данного класса, не прикреплённое с именем
        GetWindow(typeof(Window), false, "MyAdvancedGUI");
    }
    
    private void OnGUI()
    {
        GO = EditorGUILayout.ObjectField("MeshRenderer", GO, typeof(MeshRenderer), true) as MeshRenderer;
        material = EditorGUILayout.ObjectField("Material", material, typeof(Material), true) as Material;
        
        if (GO && material)
        {
            myColor = RGBSlider(new Rect(10, 60, 200, 20), myColor);  // Отрисовка пользовательского набора слайдеров для получения градиента цвета
            GO.sharedMaterial.color = myColor; // Покраска объекта
        }
        else if (!material)
        {
            GUI.Label(new Rect(10, 60, 100, 30), "Don't material");
        }
        else
        {
            _primitiveType = (PrimitiveType) EditorGUILayout.EnumPopup(_primitiveType);
            _isExpanded = EditorGUILayout.Foldout(_isExpanded, "Custom Position");
            if (_isExpanded)
            {
                _customPosition = EditorGUILayout.Vector3Field("object position", _customPosition);
            }
            
            if (GUILayout.Button("Create"))
            {
                _camera = Camera.main.transform;
                var tempObject = GameObject.CreatePrimitive(_primitiveType);
                var renderer = tempObject.GetComponent<MeshRenderer>();
                renderer.sharedMaterial = material;
                var newPosition = (!_isExpanded) 
                    ? _camera.position + Vector3.forward * 10f 
                    : _customPosition;
                tempObject.transform.position = newPosition;
                GO = renderer;

                Vector3 pos; 
                    switch (_primitiveType)
                {
                    case PrimitiveType.Plane:
                        pos = Vector3.one;
                        break;
                    case PrimitiveType.Cube:
                        pos = Vector3.zero;
                        break;
                    default:
                        pos = Vector3.forward;
                        break;
                };
            }
        }
    }

    // Отрисовка пользовательского слайдера
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMinValue, float sliderMaxValue, string labelText) // ДЗ добавить MinValue
    {
        // создаём прямоугольник с координатами в пространстве и заданой шириной и высотой 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // создаём Label на экране

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // Задаём размеры слайдера
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, sliderMinValue, sliderMaxValue); // Вырисовываем слайдер и считываем его параметр
        return sliderValue; // Возвращаем значение слайдера
    }

    // Отрисовка тройной слайдер группы, каждый слайдер отвечает за свой цвет
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // Используя пользовательский слайдер, создаём его
        rgb.r = LabelSlider(screenRect, rgb.r, 0, 1.0f, "Red");

        // делаем промежуток
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 0, 1.0f, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 0, 1.0f, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 0, 1.0f, "Alpha");

        return rgb; // возвращаем цвет
    }
}
