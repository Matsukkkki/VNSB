using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTargetCustom", story: "move to [target] at [speed]", category: "Action", id: "2e3c9a78f17d07473ff279ad26adf9f4")]
/// 「ターゲットの方向に旋回して移動し続ける」アクションクラス
public partial class MoveToTargetCustomAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Speed;

    // 更新処理
    protected override Status OnUpdate()
    {
        // Target がいない場合は「失敗」を返す
        if (Target?.Value == null)
        {
            return Status.Failure;
        }

        // ターゲットに一定距離まで近づいたら「成功」を返す
        if (Vector3.Distance(GameObject.transform.position, Target.Value.position) < 1.0f)
        {
            return Status.Success;
        }

        // ターゲットに向かって移動する
        GameObject.transform.LookAt(Target.Value.position);
        GameObject.transform.position += GameObject.transform.forward * Speed.Value;
        // 移動中は「動作中」を返す
        return Status.Running;
    }
}