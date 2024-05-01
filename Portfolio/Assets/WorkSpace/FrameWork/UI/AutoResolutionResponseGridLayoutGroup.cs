using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class AutoResolutionResponseGridLayoutGroup: MonoBehaviour
{
    // 날짜 : 2024.03.26
    // 작성자 : JH
    // 추 후 인게임에서 해상도 변경 기능 추가 시, 수정 필요
    // 1. 프로그램 실행 시, SetResolution 필요
    // 2. 인게임에서 해상도 설정 창 생성(지원하지 않는 해상도 확인 || 풀 스크린 모드)
    // 3. 해상도 변경 시, ResolutionResponse 함수 호출
    [Tooltip("기본 해상도")]
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
