using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ScenarioFactory : MonoBehaviour
{
    private static readonly Vector2 OutPosUp1 = new Vector2(-5f, 4.62f);
    private static readonly Vector2 OutPosUp2 = new Vector2(-4f, 4.62f);
    private static readonly Vector2 OutPosUp3 = new Vector2(-3f, 4.62f);
    private static readonly Vector2 OutPosUp4 = new Vector2(-2f, 4.62f);
    private static readonly Vector2 OutPosUp5 = new Vector2(-1f, 4.62f);
    private static readonly Vector2 OutPosUp6 = new Vector2(0f, 4.62f);
    private static readonly Vector2 OutPosUp7 = new Vector2(1f, 4.62f);
    private static readonly Vector2 OutPosUp8 = new Vector2(2f, 4.62f);
    private static readonly Vector2 OutPosUp9 = new Vector2(3f, 4.62f);
    private static readonly Vector2 OutPosUp10 = new Vector2(4f, 4.62f);
    private static readonly Vector2 OutPosUp11 = new Vector2(5f, 4.62f);

    private static readonly Vector2 InPos1 = new Vector2(0f, 0f);
    [SerializeField] private GameObject baseEnemy;

    public Scenario create()
    {
        var scenario = new Scenario();
        scenario
            .Spawn(enemy("日向創")
                .cubicPath(new List<Vector3[]>
                    {new[] {new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0)}}))
            .Spawn(enemy("演：蛋勒")
                .initPos(OutPosUp3))
            .Spawn(enemy("声：蛋勒")
                .initPos(OutPosUp8))
            .WaitForAllEnemyDestroyed()
            .Checkpoint()
            .Spawn(enemy("狛枝凪斗"))
            .Spawn(enemy("演：再熊"))
            .Spawn(enemy("声：鹿鸣斯特"))
            .WaitForAllEnemyDestroyed()
            .Checkpoint()
            .Spawn(enemy("编剧"))
            .Spawn(enemy("奇异"))
            .Spawn(enemy("再熊"))
            .Spawn(enemy("蛋勒"))
            .Spawn(enemy("腺嘧啶"))
            .Spawn(enemy("磷酸钙"))
            .WaitForAllEnemyDestroyed()
            .Checkpoint()
            .Spawn(enemy("ED「ダンガンロンパ舞台 the [E]nd of SinglE LiFe」"))
            .Spawn(enemy("主人公原画"))
                .Spawn(enemy("篮子"))
            .Spawn(enemy("程序"))
            .Spawn(enemy("雷精"))
            ;
        return scenario;
    }

    private EnemyBuilder enemy(string text)
    {
        return new EnemyBuilder(baseEnemy, text);
    }

    public class EnemyBuilder
    {
        private readonly GameObject baseEnemy;
        private readonly string text;
        private Vector2? _activePos;
        private float? _activeTime;
        [CanBeNull] private List<Vector3[]> _cubicPath;
        private Vector2? _exitPos;
        private float? _fontSize;
        private Vector2? _initPos;

        public EnemyBuilder(GameObject baseEnemy, string text)
        {
            this.baseEnemy = baseEnemy;
            this.text = text;
        }

        public GameObject build()
        {
            var enemy = Instantiate(baseEnemy);
            var tmp = enemy.GetComponent<TextMeshPro>();
            tmp.text = text;
            if (_fontSize.HasValue) tmp.fontSize = _fontSize.Value;
            var enemyMove = enemy.GetComponent<EnemyMove>();
            if (_initPos.HasValue) enemyMove.initPos = new Vector3(_initPos.Value.x, _initPos.Value.y, 0f);
            if (_activePos.HasValue) enemyMove.activePos = new Vector3(_activePos.Value.x, _activePos.Value.y, 0f);
            if (_exitPos.HasValue) enemyMove.exitPos = new Vector3(_exitPos.Value.x, _exitPos.Value.y, 0f);
            if (_cubicPath != null) enemyMove.SetCubicPath(_cubicPath);
            if (_activeTime.HasValue) enemyMove.activeTime = _activeTime.Value;
            return enemy;
        }

        public EnemyBuilder fontSize(float fontSize)
        {
            _fontSize = fontSize;
            return this;
        }

        public EnemyBuilder initPos(Vector2 pos)
        {
            _initPos = pos;
            return this;
        }

        public EnemyBuilder activePos(Vector2 pos)
        {
            _activePos = pos;
            return this;
        }

        public EnemyBuilder activeTime(float activeTime)
        {
            _activeTime = activeTime;
            return this;
        }

        public EnemyBuilder cubicPath(List<Vector3[]> cubicPath)
        {
            _cubicPath = cubicPath;
            return this;
        }

        public EnemyBuilder exitPos(Vector2 pos)
        {
            _exitPos = pos;
            return this;
        }
    }
}