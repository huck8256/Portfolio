using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class AutoResolutionResponseGridLayoutGroup: MonoBehaviour
{
    // ��¥ : 2024.03.26
    // �ۼ��� : JH
    // �� �� �ΰ��ӿ��� �ػ� ���� ��� �߰� ��, ���� �ʿ�
    // 1. ���α׷� ���� ��, SetResolution �ʿ�
    // 2. �ΰ��ӿ��� �ػ� ���� â ����(�������� �ʴ� �ػ� Ȯ�� || Ǯ ��ũ�� ���)
    // 3. �ػ� ���� ��, ResolutionResponse �Լ� ȣ��
    [Tooltip("�⺻ �ػ�")]
    [SerializeField] Vector2 _defaultResolution = new Vector2(1920f, 1080f);

    GridLayoutGroup _gridLayoutGroup;
    RectOffset _padding;
    Vector2 _cellSize;
    Vector2 _spacing;

    void Start()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _padding = _gridLayoutGroup.padding;
        _cellSize = _gridLayoutGroup.cellSize;
        _spacing = _gridLayoutGroup.spacing;

        ResolutionResponse();
    }
    void ResolutionResponse()
    {
        float x = Global.Resolution.x / _defaultResolution.x;
        float y = Global.Resolution.y / _defaultResolution.y;

        _gridLayoutGroup.padding = new RectOffset((int)(_padding.left * x), (int)(_padding.right * x), (int)(_padding.top * y), (int)(_padding.bottom * y));
        _gridLayoutGroup.cellSize = new Vector2(_cellSize.x * x, _cellSize.y * y);
        _gridLayoutGroup.spacing = new Vector2(_spacing.x * x, _spacing.y * y);
    }
}
