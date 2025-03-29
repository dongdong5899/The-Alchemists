using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Tilemap _groundTile;
    [SerializeField] private Tilemap _probTile;
    [SerializeField] private Tilemap _disableTile;

    private readonly float _easingDelay = 0.5f;
    private readonly Color _disabledColor = new Color(1, 1, 1, 0.5f);

    private List<Vector3Int> visit = new List<Vector3Int>();
    private Sequence disableSeq;

    public Tilemap GroundTile => _groundTile;
    public Tilemap ProbTile => _probTile;

    private Vector3Int[] dir = new Vector3Int[4] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };


    public TileBase GetGroundTile(Vector3Int pos) => _groundTile.GetTile(pos);
    public TileBase GetProbTile(Vector3Int pos) => _probTile.GetTile(pos);

    public Vector3Int GetTilePos(Vector2 pos) => _groundTile.WorldToCell(pos);
    public Vector2 GetWorldPos(Vector3Int pos) => _groundTile.CellToWorld(pos);

    public bool DisableMap(Vector3 worldPos)
    {
        Vector3Int cellpos = _disableTile.WorldToCell(worldPos);

        if (_disableTile.GetTile(cellpos) == null) return false;

        if (disableSeq != null && disableSeq.active) disableSeq.Kill();
        disableSeq = DOTween.Sequence();


        visit.Clear();
        Queue<Vector3Int> positions = new Queue<Vector3Int>();
        positions.Enqueue(cellpos);

        while (positions.TryDequeue(out Vector3Int currentPosition))
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3Int nextPostion = currentPosition + dir[i];

                if (_disableTile.GetTile(nextPostion) == null || visit.Contains(nextPostion) == true) continue;

                visit.Add(nextPostion);
                positions.Enqueue(nextPostion);

                Color startColor = Color.white;
                Color endColor = _disabledColor;

                _disableTile.SetTileFlags(nextPostion, TileFlags.None);

                disableSeq.Join(DOTween.To(() => startColor,
                    targetColor => _disableTile.SetColor(nextPostion, targetColor), endColor, _easingDelay));
            }
        }

        return true;

        //for (int i = -exRadius; i <= exRadius; i++)
        //{
        //    for (int j = -exRadius; j <= exRadius; j++)
        //    {
        //Vector3Int targetPos = cellpos + new Vector3Int(j, i);
        //TileBase tile = _disableTile.GetTile(targetPos);

        //if (tile == null) continue;

        //Color lastColor = _disableTile.GetColor(targetPos);

        //bool isInBound = Mathf.Abs(i) + Mathf.Abs(j) < radius;

        //Color startColor = isInBound ? Color.white : _disabledColor;
        //Color endColor = isInBound ? _disabledColor : Color.white;

        //if (lastColor.a > startColor.a && isInBound == false) continue;
        //if (lastColor.a < startColor.a && isInBound == true) continue;

        //_disableTile.SetTileFlags(targetPos, TileFlags.None);
        //DOTween.To(() => startColor,
        //    targetColor => _disableTile.SetColor(targetPos, targetColor), endColor, _easingDelay);
        //    }
        //}
    }

    public void EnableMap()
    {
        if (disableSeq != null && disableSeq.active) disableSeq.Kill();
        disableSeq = DOTween.Sequence();

        for (int i = 0; i < visit.Count; i++)
        {
            Vector3Int position = visit[i]; 

            Color startColor = _disabledColor;
            Color endColor = Color.white;

            _disableTile.SetTileFlags(position, TileFlags.None);

            disableSeq.Join(DOTween.To(() => startColor,
                    targetColor => _disableTile.SetColor(position, targetColor), endColor, _easingDelay));
        }

        //Vector3Int cellpos = _disableTile.WorldToCell(worldPos);
        //int exRadius = Mathf.FloorToInt(expRadius) + 1;

        //for (int i = -exRadius; i <= exRadius; i++)
        //{
        //    for (int j = -exRadius; j <= exRadius; j++)
        //    {
        //        Vector3Int targetPos = cellpos + new Vector3Int(j, i);
        //        TileBase tile = _disableTile.GetTile(targetPos);

        //        if (tile == null) continue;

        //        Color lastColor = _disableTile.GetColor(targetPos);
        //        if (lastColor.a > _disabledColor.a) continue;

        //        Color startColor = _disabledColor;
        //        Color endColor = Color.white;

        //        _disableTile.SetTileFlags(targetPos, TileFlags.None);
        //        DOTween.To(() => startColor,
        //            targetColor => _disableTile.SetColor(targetPos, targetColor), endColor, _easingDelay);
        //    }
        //}
        //Debug.Log("¾ß¹ß");
    }

    private bool IsInTheCircle(Vector2Int anchorPos, Vector2Int targetPos, int radius) =>
        (targetPos.x >= -radius + 1 - anchorPos.y && targetPos.x <= radius + 1
            && targetPos.y >= -radius + 1 && targetPos.y <= radius);
}
