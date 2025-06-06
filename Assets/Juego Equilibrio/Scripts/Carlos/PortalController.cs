using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(DoMove))]
public class PortalController : MonoBehaviour
{
    private DoMove doMove;
    
    private void Awake()
    {
        doMove=GetComponent<DoMove>();
    }
    private void PiecesCmplete()
    {
        doMove.Go();
    }
    private void OnEnable()
    {
        GameManager.OnPiecesCmplete += PiecesCmplete;
    }
    private void OnDisable()
    {
        GameManager.OnPiecesCmplete -= PiecesCmplete;
    }
}
