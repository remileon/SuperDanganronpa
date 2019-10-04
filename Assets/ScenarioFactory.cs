using System;
using System.Collections.Generic;
using DefaultNamespace;
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
    private readonly BezierCurves bc = new BezierCurves();
    [SerializeField] private GameObject baseEnemy;
    [SerializeField] private GameObject baseEnemy2;
    [SerializeField] private GameObject octoOrangeWeapon;
    [SerializeField] private GameObject octoRedWeapon;
    [SerializeField] private GameObject orangeWeapon;
    [SerializeField] private GameObject randomOrangeWeapon;
    [SerializeField] private GameObject randomRedWeapon;

    [SerializeField] private GameObject redWeapon;
    [SerializeField] private GameObject you;

    public Scenario create()
    {
        var scenario = new Scenario();
        //
        scenario
            .Checkpoint()
            .WaitForSeconds(3.43f)
            .Checkpoint()
            .Spawn(enemy("日向創")
                .activePos(0, 2)
                .cubicPath(bc.line(0, -0.5f))
                .activeTime(4.4f)
                .weapon(octoRedWeapon, 1f)
                .weapon(octoOrangeWeapon, 1f, 0.5f, default, new Vector3(0, 0, 45))
            )
            .Spawn(enemy("演：蛋勒")
                .initPos(-9, 1.1f)
                .activePos(-2, 1.1f)
                .exitPos(9, 1.1f)
                .cubicPath(bc.line(4, 0))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .Spawn(enemy("声：蛋勒")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(-9, 0.5f)
                .cubicPath(bc.line(-4, 0))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("狛枝凪斗")
                .initPos(-9, 1)
                .activePos(-2, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(1, 0))
                .weapon(octoOrangeWeapon, 0.5f)
                .weapon(octoOrangeWeapon, 0.5f, 0.25f, 0f, new Vector3(0, 0, 5))
                .weapon(octoRedWeapon, 2f, 0, 0, new Vector3(0, 0, 22.5f))
            )
            .Spawn(enemy("演：再熊")
                .initPos(9, 1.5f)
                .activePos(2, 1.5f)
                .exitPos(9, 1.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .Spawn(enemy("声：鹿鸣斯特")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(9, 0.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .WaitForAllEnemyDestroyed();
        //
        var nanamiBuilder = enemy("七海千秋")
            .initPos(0)
            .activePos(0, 2)
            .exitPos(0)
            .cubicPath(bc.line(0, -1));
        var nanamiWeaponCount = 16;
        float nanamiWeaponX = 3;
        float nanamiWeaponY = 1;
        for (var i = 0; i < nanamiWeaponCount; ++i)
        {
            var angle = Math.PI * 2 * ((double) i / nanamiWeaponCount);
            var x = Math.Cos(angle) * nanamiWeaponX / 2;
            var y = Math.Sin(angle) * nanamiWeaponY / 2;
            var angle2 = Math.Atan2(x, y);
            var speed = 2;
//            Debug.Log("rotate: " + angle / Math.PI * 180 + ", speed: " + (float) Math.Sqrt(x*x+y*y));
            nanamiBuilder.weapon(orangeWeapon, 2f, default, default,
                new Vector3(0, 0, (float) (angle2 / Math.PI * 180)), default, (float) Math.Sqrt(x * x + y * y) * speed);
        }

        scenario
            .Checkpoint()
            .Spawn(
                nanamiBuilder
            )
            .Spawn(enemy("演：鬼裔")
                .initPos(9, 1.5f)
                .activePos(4, 1.5f)
                .exitPos(9, -0.5f)
                .cubicPath(bc.arc(0, -2, 20))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .Spawn(enemy("声：团子")
                .initPos(-9, 1.5f)
                .activePos(-4, 1.5f)
                .exitPos(-9, -0.5f)
                .cubicPath(bc.arc(0, -2, -20))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("十神白夜")
                .initPos(0)
                .activePos(0, 1.5f)
                .exitPos(0)
                .cubicPath(bc.line(0, -0.1f))
                .weapon(orangeWeapon, 1f, default, 0f, new Vector3(0, 0, 90))
                .weapon(orangeWeapon, 1f, default, 0f, new Vector3(0, 0, 270))
                .weapon(redWeapon, 1f, 0.5f, 0f, new Vector3(0, 0, 90))
                .weapon(redWeapon, 1f, 0.5f, 0f, new Vector3(0, 0, 270))
            )
            .Spawn(enemy("演：兄贵")
                .initPos(9, 1.5f)
                .activePos(2, 1.5f)
                .exitPos(9, 1.5f)
                .cubicPath(bc.line(1, 0))
                .weaponSquare(orangeWeapon, 3, 2, 2f)
            )
            .Spawn(enemy("声：兄贵")
                .initPos(-9, 1.5f)
                .activePos(-2, 1.5f)
                .exitPos(-9, 1.5f)
                .cubicPath(bc.line(-1, 0))
                .weaponSquare(orangeWeapon, 3, 2, 2f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("小泉真昼")
                .initPos(-9, 1)
                .activePos(-2, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(1, 0))
                .weaponSquare(orangeWeapon, 16 * 0.5f, 9 * 0.5f, 3.5f, 0, default, default, default)
                .weaponSquare(orangeWeapon, 16 * 0.5f, 9 * 0.5f, 3.5f, 0.3f, default, default, default)
                .weaponSquare(orangeWeapon, 16 * 0.5f, 9 * 0.5f, 3.5f, 0.6f, default, default, default)
            )
            .Spawn(enemy("演：nina")
                .initPos(9, 1.5f)
                .activePos(2, 1.5f)
                .exitPos(9, 1.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(randomRedWeapon, 3.5f)
                .weapon(randomRedWeapon, 3.5f, 0.3f)
                .weapon(randomRedWeapon, 3.5f, 0.6f)
            )
            .Spawn(enemy("声：腺嘧啶")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(9, 0.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(randomRedWeapon, 3.5f)
                .weapon(randomRedWeapon, 3.5f, 0.3f)
                .weapon(randomRedWeapon, 3.5f, 0.6f)
            )
            .WaitForAllEnemyDestroyed();

        //
        scenario
            .Checkpoint()
            .Spawn(enemy("西園寺日寄子")
                .initPos(-9, 1)
                .activePos(-2, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(1, 0))
                .weaponSector(orangeWeapon)
            )
            .Spawn(enemy("演：月铃猫")
                .initPos(9, 1.5f)
                .activePos(2, 1.5f)
                .exitPos(9, 1.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(randomOrangeWeapon, 0.5f, default, default, new Vector3(0, 0, 45))
                .weapon(randomOrangeWeapon, 0.5f, default, default, new Vector3(0, 0, -45))
            )
            .Spawn(enemy("声：团子")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(9, 0.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(octoRedWeapon, 2f)
            )
            .WaitForAllEnemyDestroyed();

        //
        scenario
            .Checkpoint()
            .Spawn(enemy("弐大猫丸")
                .initPos(9, 1)
                .activePos(2, 1)
                .exitPos(9, 1)
                .cubicPath(bc.line(-1, 0))
                .weapon(orangeWeapon, 0.2f, default, 45f)
                .weapon(orangeWeapon, 0.2f, default, 45f, new Vector3(0, 0, 90))
                .weapon(orangeWeapon, 0.2f, default, 45f, new Vector3(0, 0, 180))
                .weapon(orangeWeapon, 0.2f, default, 45f, new Vector3(0, 0, 270))
            )
            .Spawn(enemy("演：苏联中士")
                .initPos(-9, 1.5f)
                .activePos(-2, 1.5f)
                .exitPos(-9, 1.5f)
                .cubicPath(bc.line(1, 0))
                .weapon(octoRedWeapon, 0.4f)
            )
            .Spawn(enemy("声：尼玛")
                .initPos(-9, 0.5f)
                .activePos(-2, 0.5f)
                .exitPos(-9, 0.5f)
                .cubicPath(bc.line(1, 0))
                .weapon(orangeWeapon, 0.5f, default, -45f)
                .weapon(orangeWeapon, 0.5f, default, -45f, new Vector3(0, 0, 90))
                .weapon(orangeWeapon, 0.5f, default, -45f, new Vector3(0, 0, 180))
                .weapon(orangeWeapon, 0.5f, default, -45f, new Vector3(0, 0, 270))
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("終里赤音")
                .initPos(-9, 1)
                .activePos(-2, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(1, 0))
                .weapon(orangeWeapon, 0.2f, default, 45f)
                .weapon(orangeWeapon, 0.2f, default, 45f, new Vector3(0, 0, 90))
                .weapon(orangeWeapon, 0.2f, default, 45f, new Vector3(0, 0, 180))
                .weapon(orangeWeapon, 0.2f, default, 45f, new Vector3(0, 0, 270))
            )
            .Spawn(enemy("演：柿子")
                .initPos(9, 1.5f)
                .activePos(2, 1.5f)
                .exitPos(9, 1.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(octoRedWeapon, 0.4f)
            )
            .Spawn(enemy("声：莫良")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(9, 0.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(orangeWeapon, 0.5f, default, -45f)
                .weapon(orangeWeapon, 0.5f, default, -45f, new Vector3(0, 0, 90))
                .weapon(orangeWeapon, 0.5f, default, -45f, new Vector3(0, 0, 180))
                .weapon(orangeWeapon, 0.5f, default, -45f, new Vector3(0, 0, 270))
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("九頭竜冬彦")
                .activePos(0, 2)
                .cubicPath(bc.line(0, -0.5f))
                .weapon(octoRedWeapon, 0.6f, default, 20)
                .weapon(octoOrangeWeapon, 0.6f, 0.3f, 20, new Vector3(0, 0, 45))
            )
            .Spawn(enemy("演：大魔王")
                .initPos(-9, 1.1f)
                .activePos(-2, 1.1f)
                .exitPos(9, 1.1f)
                .cubicPath(bc.line(4, 0))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .Spawn(enemy("声：鹿鸣斯特")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(-9, 0.5f)
                .cubicPath(bc.line(-4, 0))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("辺古山ペコ")
                .activePos(0, 2)
                .cubicPath(bc.line(0, -0.5f))
                .weaponLine(orangeWeapon, 1.5f)
            )
            .Spawn(enemy("演：海豚")
                .initPos(-9, 1.1f)
                .activePos(-2, 1.1f)
                .exitPos(9, 1.1f)
                .cubicPath(bc.line(4, 0))
                .weapon(octoRedWeapon, 1.5f)
            )
            .Spawn(enemy("声：莫良")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(-9, 0.5f)
                .cubicPath(bc.line(-4, 0))
                .weapon(octoRedWeapon, 1.5f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("田中眼蛇夢")
                .initPos(9, 1)
                .activePos(-1, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(-5, 0))
                .weapon(octoOrangeWeapon, 100f)
                .weapon(octoOrangeWeapon, 100f, 0.2f)
                .weapon(octoOrangeWeapon, 100f, 0.4f)
                .weapon(octoRedWeapon, 100f, 0.6f)
            )
            .WaitForSeconds(2.5f)
            .Spawn(enemy("演：")
                .initPos(9, 1)
                .activePos(0, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(-5, 0))
                .weapon(octoOrangeWeapon, 100f)
            )
            .WaitForSeconds(0.6f)
            .Spawn(enemy("梦野")
                .initPos(9, 1)
                .activePos(0.5f, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(-5, 0))
                .weapon(octoOrangeWeapon, 100f)
            )
            .WaitForSeconds(0.6f)
            .Spawn(enemy("声：")
                .initPos(9, 1)
                .activePos(1f, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(-5, 0))
                .weapon(octoOrangeWeapon, 100f)
            )
            .WaitForSeconds(0.6f)
            .Spawn(enemy("大佐")
                .initPos(9, 1)
                .activePos(1.5f, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(-5, 0))
                .weapon(octoRedWeapon, 100f)
            )
            .WaitForSeconds(3);
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("ソニア·ネヴァーマインド")
                .initPos(-9, 0)
                .activePos(-0.5f, 0)
                .exitPos(9, 1)
                .cubicPath(bc.line(1, 0))
                .weapon(randomOrangeWeapon, 0.3f)
                .weapon(randomOrangeWeapon, 0.3f, default, default, new Vector3(0, 0, 45))
                .weapon(randomOrangeWeapon, 0.3f, default, default, new Vector3(0, 0, -45))
                .weapon(randomOrangeWeapon, 0.3f, default, default, new Vector3(0, 0, 90))
                .weapon(randomOrangeWeapon, 0.3f, default, default, new Vector3(0, 0, -90))
            )
            .WaitForSeconds(1)
            .Spawn(enemy("演：磷酸钙")
                .initPos(-3)
                .activePos(-3, 2)
                .exitPos(-3)
                .activeTime(4)
                .cubicPath(bc.line(0, -1))
                .weapon(randomRedWeapon, 0.4f)
            )
            .Spawn(enemy("声：谢婉")
                .initPos(3)
                .activePos(3, 2)
                .exitPos(3)
                .activeTime(4)
                .cubicPath(bc.line(0, -1))
                .weapon(randomRedWeapon, 0.4f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("左右田和一")
                .initPos(-9, 1)
                .activePos(2f, 0)
                .exitPos(9, 1)
                .cubicPath(bc.concat(
                    bc.arc(-4, 0, 4),
                    bc.arc(4, 0, 4),
                    bc.arc(-4, 0, 4)
                ))
                .weapon(octoOrangeWeapon, 1.6f)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("演：Sneezer")
                .initPos(-3)
                .activePos(-3, 2)
                .exitPos(-3)
                .activeTime(4)
                .cubicPath(bc.line(0, -1))
                .weapon(octoRedWeapon, 2.5f)
            )
            .Spawn(enemy("声：丁丁")
                .initPos(3)
                .activePos(3, 2)
                .exitPos(3)
                .activeTime(4)
                .cubicPath(bc.line(0, -1))
                .weapon(octoRedWeapon, 2.5f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("罪木蜜柑")
                .activePos(0, 2)
                .cubicPath(bc.line(0, -0.5f))
                .weaponBursts(orangeWeapon)
            )
            .Spawn(enemy("演：腺嘧啶")
                .initPos(-9, 1.1f)
                .activePos(-2, 1.1f)
                .exitPos(9, 1.1f)
                .cubicPath(bc.line(4, 0))
                .weapon(octoRedWeapon, 2f)
            )
            .Spawn(enemy("声：仲间")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(-9, 0.5f)
                .cubicPath(bc.line(-4, 0))
                .weapon(octoRedWeapon, 2f, 1f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("澪田唯吹")
                .initPos(-9, 1)
                .activePos(-2, 1)
                .exitPos(-9, 1)
                .cubicPath(bc.line(1, 0))
                .weaponStar(orangeWeapon)
            )
            .Spawn(enemy("演：亦轩")
                .initPos(9, 1.5f)
                .activePos(2, 1.5f)
                .exitPos(9, 1.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(octoRedWeapon, 2f)
            )
            .Spawn(enemy("声：mori")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(9, 0.5f)
                .cubicPath(bc.line(-1, 0))
                .weapon(octoRedWeapon, 2f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("花村輝々")
                .initPos(9, 1)
                .activePos(2, 1)
                .exitPos(9, 1)
                .cubicPath(bc.line(-1, 0))
                .weapon(octoOrangeWeapon, 1.2f, 0, 0, default)
                .weapon(octoOrangeWeapon, 1.2f, 0, 0, new Vector3(0, 0, 22.5f))
                .weapon(octoOrangeWeapon, 2f, 0.5f, 0, default, default, 5)
                .weapon(octoOrangeWeapon, 2f, 0.5f, 0, new Vector3(0, 0, 22.5f), default, 5)
            )
            .Spawn(enemy("演：墨水")
                .initPos(-9, 1.5f)
                .activePos(-2, 1.5f)
                .exitPos(-9, 1.5f)
                .cubicPath(bc.line(1, 0))
                .weapon(octoRedWeapon, 2f)
            )
            .Spawn(enemy("声：bili")
                .initPos(-9, 0.5f)
                .activePos(-2, 0.5f)
                .exitPos(-9, 0.5f)
                .cubicPath(bc.line(1, 0))
                .weapon(octoRedWeapon, 2f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("モノクマ")
                .activePos(0, 2)
                .cubicPath(bc.line(0, -0.5f))
                .weaponSector(orangeWeapon, 2f, 90f, 5f, 170f, 17, 1)
                .weaponSector(redWeapon, 2f, 90f, 185f, 170f, 17, 1)
            )
            .Spawn(enemy("演：萌汐")
                .initPos(-9, 1.1f)
                .activePos(-2, 1.1f)
                .exitPos(9, 1.1f)
                .cubicPath(bc.line(4, 0))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .Spawn(enemy("声：蛀牙")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(-9, 0.5f)
                .cubicPath(bc.line(-4, 0))
                .weapon(randomOrangeWeapon, 0.5f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("モノミ")
                .activePos(0, 2)
                .cubicPath(bc.line(0, -0.5f))
                .weapon(octoOrangeWeapon, 0.5f, 0f, 45f)
            )
            .Spawn(enemy("演：篮子")
                .initPos(-9, 1.1f)
                .activePos(-2, 1.1f)
                .exitPos(9, 1.1f)
                .cubicPath(bc.line(4, 0))
                .weapon(octoRedWeapon, 2f)
            )
            .Spawn(enemy("声：萌汐")
                .initPos(9, 0.5f)
                .activePos(2, 0.5f)
                .exitPos(-9, 0.5f)
                .cubicPath(bc.line(-4, 0))
                .weapon(octoRedWeapon, 2f, 1f)
            )
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("神座出流")
                .activePos(0, 1)
                .cubicPath(bc.line(0, -0.01f))
                .weapon(octoRedWeapon, 0.2f, 0f, 60f)
                .weapon(octoRedWeapon, 0.2f, 0f, -60f)
                .life(30)
            )
            .Spawn(enemy("演：奇异")
                .activePos(0, 2)
                .cubicPath(bc.line(0, -0.01f))
                .weapon(orangeWeapon, 100f, 100f)
            )
            .WaitForAllEnemyDestroyed();
        // 编剧：奇异 再熊 蛋勒 腺嘧啶 磷酸钙
        scenario
            .Checkpoint()
            .Spawn(enemy("编剧")
                .fontSize(5)
                .initPos(0)
                .activePos(0, 1)
                .exitPos(0)
                .cubicPath(bc.line(0, -1f))
                .weapon(octoRedWeapon, 2f)
                .weapon(octoOrangeWeapon, 0.3f, 0f, 60f)
            );
        string[] scenarists =
        {
            "奇异", "再熊", "蛋勒", "腺嘧啶", "磷酸钙"
        };
        for (var i = 0; i < scenarists.Length; i++)
        {
            var angle = (double) i / (scenarists.Length - 1) * Math.PI;
            scenario
                .WaitForSeconds(0.3f)
                .Spawn(enemy(scenarists[i])
                    .fontSize(3)
                    .initPos((float) (10 * Math.Cos(angle)), (float) (10 * Math.Sin(angle)) + 0.75f)
                    .activePos((float) (5 * Math.Cos(angle)), (float) (2 * Math.Sin(angle)) + 0.75f)
                    .exitPos((float) (10 * Math.Cos(angle)), (float) (10 * Math.Sin(angle)) + 0.75f)
                    .cubicPath(bc.random())
                    .weapon(randomOrangeWeapon, 2f)
                    .life(15)
                );
        }

        scenario.WaitForAllEnemyDestroyed();
        // 剧本协力：莫良 知名不具的非著名社畜 小德 青石木 柿子
        scenario
            .Checkpoint()
            .Spawn(enemy("剧本协力")
                .fontSize(5)
                .initPos(0)
                .activePos(0, 2)
                .exitPos(0)
                .cubicPath(bc.arc(0.1f, 0, 0.4f))
            );
        string[] scenarioHelpers =
        {
            "莫良", "知名不具的非著名社畜", "小德", "青石木", "柿子"
        };
        for (var i = 0; i < scenarioHelpers.Length; i++)
        {
            var angle = (double) i / (scenarioHelpers.Length - 1) * Math.PI;
            scenario
                .WaitForSeconds(0.3f)
                .Spawn(enemy(scenarioHelpers[i])
                    .fontSize(3)
                    .initPos((float) (10 * Math.Cos(angle)), (float) (10 * Math.Sin(angle)) + 0.75f)
                    .activePos((float) (5 * Math.Cos(angle)), (float) (2 * Math.Sin(angle)) + 0.75f)
                    .exitPos((float) (10 * Math.Cos(angle)), (float) (10 * Math.Sin(angle)) + 0.75f)
                    .cubicPath(bc.random())
                    .weapon(randomOrangeWeapon, 2f)
                    .life(15)
                );
        }

        scenario
            .WaitForAllEnemyDestroyed();
        // 音频监督：再熊 奇异
        scenario
            .Checkpoint()
            .Spawn(enemy("音频监督")
                .fontSize(5)
                .initPos(-3, 6f)
                .activePos(-3, 2)
                .exitPos(-3, -4.62f)
                .cubicPath(bc.line(0, -1.5f))
                .weapon(octoOrangeWeapon, 1f, 0, 22.5f)
            )
            .WaitForSeconds(1);
        scenario
            .Spawn(enemy("再熊")
                .fontSize(3)
                .initPos(2, 4.5f)
                .activePos(2, 0.5f)
                .exitPos(2, -4.62f)
                .cubicPath(bc.line(0, -1.5f))
                .weapon(randomOrangeWeapon, 3f, default, default, default, default, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1);
        scenario
            .Spawn(enemy("奇异")
                .fontSize(3)
                .initPos(4, 4.5f)
                .initTime(0.25f)
                .activePos(4, 2.5f)
                .exitPos(4, -4.62f)
                .cubicPath(bc.line(0, -1.5f))
                .weapon(randomOrangeWeapon, 3f, default, default, default, default, 0.8f)
                .life(15)
            )
            .WaitForSeconds(2f);
        // 配音协力：翔子
        scenario
            .Checkpoint()
            .Spawn(enemy("配音协力")
                .fontSize(5)
                .initPos(2.5f, 5.5f)
                .activePos(2.5f, 1.5f)
                .exitPos(2.5f, -4.62f)
                .cubicPath(bc.line(0, -1.5f))
                .weapon(octoOrangeWeapon, 1f, 0, 22.5f)
            )
            .WaitForSeconds(1);
        scenario
            .Spawn(enemy("翔子")
                .fontSize(3)
                .initPos(-0.5f, 5f)
                .activePos(-0.5f, 1)
                .exitPos(-0.5f, -4.62f)
                .cubicPath(bc.line(0, -1.5f))
                .weapon(randomOrangeWeapon, 3f, default, default, default, default, 0.8f)
                .life(15)
            )
            .WaitForSeconds(2f);
        // 视频监督：雷精 再熊
        scenario
            .Checkpoint()
            .Spawn(enemy("视频监督")
                .fontSize(5)
                .initPos(-3.5f, 6f)
                .activePos(-3.5f, 2)
                .exitPos(-3.5f, -4.62f)
                .cubicPath(bc.line(0, -1.5f))
                .weapon(octoOrangeWeapon, 1f, 0, 22.5f)
            )
            .WaitForSeconds(1);
        scenario
            .Spawn(enemy("雷精")
                .fontSize(3)
                .initPos(1f, 5.5f)
                .activePos(1f, 1.5f)
                .exitPos(1f, -4.62f)
                .cubicPath(bc.line(0, -1.5f))
                .weapon(randomOrangeWeapon, 3f, default, default, default, default, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1);
        scenario
            .Spawn(enemy("再熊")
                .fontSize(3)
                .initPos(-1f, 5f)
                .activePos(-1f, 0.5f)
                .exitPos(-1f, -4.62f)
                .cubicPath(bc.line(0, -1.5f))
                .weapon(randomOrangeWeapon, 3f, default, default, default, default, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1.5f);
        //
        scenario
            .WaitForAllEnemyDestroyed();
        //
        scenario
            .Checkpoint()
            .Spawn(enemy("ED「ダンガンロンパ舞台 the [E]nd of SinglE LiFe」")
                .life(60)
                .initPos(0)
                .activePos(0, 1)
                .exitPos(-10, 0)
                .cubicPath(bc.line(0, -1))
            )
            .WaitForSeconds(1f)
            .Spawn(enemy("主人公原画")
                .fontSize(3.5f)
                .initPos(-9, 2.2f)
                .activePos(-3, 2.2f)
                .exitPos(9, 2.2f)
                .cubicPath(bc.line(2, 0))
                .weapon(octoOrangeWeapon, 3f, 0, 22.5f)
                .life(15)
            )
            .Spawn(enemy("程序")
                .fontSize(3.5f)
                .initPos(9, 2.2f)
                .activePos(3, 2.2f)
                .exitPos(-9, 2.2f)
                .cubicPath(bc.line(-2, 0))
                .weapon(octoOrangeWeapon, 3f, 0, 22.5f)
                .life(15)
            )
            .WaitForSeconds(0.5f)
            .Spawn(enemy("篮子")
                .fontSize(3.5f)
                .initPos(-9, 1.5f)
                .activePos(-3, 1.5f)
                .exitPos(9, 1)
                .cubicPath(bc.line(2, 0))
                .weapon(octoOrangeWeapon, 3f, 1.5f, 22.5f)
                .life(15)
            )
            .Spawn(enemy("雷精")
                .fontSize(3.5f)
                .initPos(9, 1.5f)
                .activePos(3, 1.5f)
                .exitPos(-9, 1)
                .cubicPath(bc.line(-2, 0))
                .weapon(octoOrangeWeapon, 3f, 1.5f, 22.5f)
                .life(15)
            )
            .WaitForAllEnemyDestroyed();
        // 舞美监督：腺嘧啶 磷酸钙 蛋勒
        scenario
            .Checkpoint()
            .Spawn(enemy("舞美监督")
                .fontSize(5f)
                .initPos(9, -0.5f)
                .activePos(5, -0.5f)
                .exitPos(-9, -0.5f)
                .cubicPath(bc.line(-3, 0))
                .activeTime(5.5f)
            )
            .Spawn(enemy2("腺嘧啶")
                .fontSize(3)
                .activePos(8.5f, 0)
                .gravity(-0.6f)
                .initSpeed(-4 , 2)
                .k(0.08f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("磷酸钙")
                .fontSize(3)
                .activePos(8.5f, 0)
                .gravity(-0.1f)
                .initSpeed(-6.5f , 0.5f)
                .k(0.06f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("蛋勒")
                .fontSize(3)
                .activePos(8.5f, 0)
                .gravity(-1)
                .initSpeed(-6 , 3)
                .k(0.1f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .WaitForAllEnemyDestroyed();
//        化妆：磷酸钙 萌汐 鬼裔 小月 咩咩 风筝 莫良 罐头 柿子
        scenario
            .Checkpoint()
            .Spawn(enemy("化妆")
                .fontSize(5f)
                .initPos(-9, -0.5f)
                .activePos(-4, -0.5f)
                .exitPos(9, -0.5f)
                .cubicPath(bc.line(3, 0))
                .activeTime(5.5f)
            )
            .Spawn(enemy2("磷酸钙")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(-0.6f)
                .initSpeed(4 , 2)
                .k(0.08f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("萌汐")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(-0.1f)
                .initSpeed(6.5f , 0.5f)
                .k(0.06f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("鬼裔")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(-1)
                .initSpeed(6 , 3)
                .k(0.1f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("小月")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(-0.08f)
                .initSpeed(4, 0.5f)
                .k(0.05f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("咩咩")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(0)
                .initSpeed(6 , 0.2f)
                .k(0.1f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("风筝")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(-0.2f)
                .initSpeed(6 , 0)
                .k(0.15f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("莫良")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(-0.8f)
                .initSpeed(8, 2.5f)
                .k(0.1f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("罐头")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(-0.5f)
                .initSpeed(3 , 1)
                .k(0.1f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .Spawn(enemy2("柿子")
                .fontSize(3)
                .activePos(-8.5f, 0)
                .gravity(-1)
                .initSpeed(3.5f, 3)
                .k(0.1f)
                .weapon(randomOrangeWeapon, 2f)
                .life(15)
            )
            .WaitForAllEnemyDestroyed();
//            灯光：夜宵 雷精 小海 
        scenario.Checkpoint()
            .Spawn(enemy("灯光")
                .fontSize(5)
                .initPos(-9, 2)
                .activePos(-4, 2)
                .exitPos(9, 2)
                .cubicPath(bc.line(8, 0))
                .weapon(octoRedWeapon, 1.5f, 0, 10f)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("夜宵")
                .fontSize(3)
                .initPos(-9, 2)
                .activePos(-4, 2)
                .exitPos(9, 2)
                .cubicPath(bc.line(8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("雷精")
                .fontSize(3)
                .initPos(-9, 2)
                .activePos(-4, 2)
                .exitPos(9, 2)
                .cubicPath(bc.line(8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("小海")
                .fontSize(3)
                .initPos(-9, 2)
                .activePos(-4, 2)
                .exitPos(9, 2)
                .cubicPath(bc.line(8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1);
//            道具：兄贵 奇异 萌汐 绯红
        scenario
            .Spawn(enemy("道具")
                .fontSize(5)
                .initPos(9, 1.2f)
                .activePos(4, 1.2f)
                .exitPos(-9, 1.2f)
                .cubicPath(bc.line(-8, 0))
                .weapon(octoRedWeapon, 1.5f, 0, 10f)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("兄贵")
                .fontSize(3)
                .initPos(9, 1.2f)
                .activePos(4, 1.2f)
                .exitPos(-9, 1.2f)
                .cubicPath(bc.line(-8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("奇异")
                .fontSize(3)
                .initPos(9, 1.2f)
                .activePos(4, 1.2f)
                .exitPos(-9, 1.2f)
                .cubicPath(bc.line(-8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("萌汐")
                .fontSize(3)
                .initPos(9, 1.2f)
                .activePos(4, 1.2f)
                .exitPos(-9, 1.2f)
                .cubicPath(bc.line(-8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("绯红")
                .fontSize(3)
                .initPos(9, 1.2f)
                .activePos(4, 1.2f)
                .exitPos(-9, 1.2f)
                .cubicPath(bc.line(-8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1);
//        舞台后勤：聊无 lickey 木木
        scenario
            .Spawn(enemy("舞台后勤")
                .fontSize(5)
                .initPos(-9, 0.4f)
                .activePos(-4, 0.4f)
                .exitPos(9, 0.4f)
                .cubicPath(bc.line(8, 0))
                .weapon(octoRedWeapon, 1.5f, 0, 10f)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("聊无")
                .fontSize(3)
                .initPos(-9, 0.4f)
                .activePos(-4, 0.4f)
                .exitPos(9, 0.4f)
                .cubicPath(bc.line(8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("lickey")
                .fontSize(3)
                .initPos(-9, 0.4f)
                .activePos(-4, 0.4f)
                .exitPos(9, 0.4f)
                .cubicPath(bc.line(8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("木木")
                .fontSize(3)
                .initPos(-9, 0.4f)
                .activePos(-4, 0.4f)
                .exitPos(9, 0.4f)
                .cubicPath(bc.line(8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            )
            .WaitForSeconds(1);
//            摄像提供：itachi
        scenario
            .Spawn(enemy("摄像提供")
                .fontSize(5)
                .initPos(9, -0.4f)
                .activePos(4, -0.4f)
                .exitPos(-9, -0.4f)
                .cubicPath(bc.line(-8, 0))
                .weapon(octoRedWeapon, 1.5f, 0, 10f)
            )
            .WaitForSeconds(1)
            .Spawn(enemy("itachi")
                .fontSize(3)
                .initPos(9, -0.4f)
                .activePos(4, -0.4f)
                .exitPos(-9, -0.4f)
                .cubicPath(bc.line(-8, 0))
                .weapon(randomOrangeWeapon, 0.8f)
                .life(15)
            );

        scenario.WaitForAllEnemyDestroyed();
        //
        scenario.Checkpoint()
            .Spawn(enemy("特别鸣谢")
                .fontSize(5f)
                .weapon(octoRedWeapon, 1.5f, 0, 10f)
                .weapon(octoOrangeWeapon, 1.5f, 0.75f, 10f)
                .cubicPath(bc.line(0, -0.5f))
                .life(30)
            )
            .WaitForAllEnemyDestroyed()
            .Spawn(enemy("理科一号楼1405")
                .weapon(octoRedWeapon, 1.5f, 0, 10f)
                .weapon(octoOrangeWeapon, 1.5f, 0.75f, 10f)
                .cubicPath(bc.line(0, -0.5f))
                .life(30)
            )
            .WaitForAllEnemyDestroyed()
            .Spawn(enemy("and...")
                .weapon(orangeWeapon, 1000f, 1000f)
                .cubicPath(bc.line(0, -0.5f))
                .life(30)
            )
            .WaitForAllEnemyDestroyed()
            .Spawn(enemyYou()
                .weapon(orangeWeapon, 1000f, 1000f)
                .cubicPath(bc.line(0, -0.5f))
            )
            .WaitForAllEnemyDestroyed();

        scenario.End();
        return scenario;
    }

    private EnemyBuilder enemy(string text)
    {
        return new EnemyBuilder(baseEnemy, text);
    }

    private EnemyBuilder enemyYou()
    {
        return new EnemyBuilder(you, "YOU");
    }

    private EnemyBuilder enemy2(string text)
    {
        return new EnemyBuilder(baseEnemy2, text);
    }

    public class EnemyBuilder
    {
        private readonly GameObject baseEnemy;
        private readonly string text;
        private readonly List<WeaponSlot> weaponSlots = new List<WeaponSlot>();
        private Vector2? _activePos;
        private float? _activeTime;
        [CanBeNull] private List<Vector3[]> _cubicPath;
        private Vector2? _exitPos;
        private float? _fontSize;

        private float? _gravity;
        private Vector2? _initPos;
        public Vector3? _initSpeed;
        private float? _initTime;
        public float? _k;
        private int? _life;
        private Vector3? _speed;

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
            var attachWeapons = enemy.GetComponent<AttachWeapons>();
            if (enemyMove != null)
            {
                if (_initPos.HasValue) enemyMove.initPos = new Vector3(_initPos.Value.x, _initPos.Value.y, 0f);
                if (_initTime.HasValue) enemyMove.initTime = _initTime.Value;
                if (_activePos.HasValue) enemyMove.activePos = new Vector3(_activePos.Value.x, _activePos.Value.y, 0f);
                if (_exitPos.HasValue) enemyMove.exitPos = new Vector3(_exitPos.Value.x, _exitPos.Value.y, 0f);
                if (_cubicPath != null) enemyMove.SetCubicPath(_cubicPath);
                if (_activeTime.HasValue) enemyMove.activeTime = _activeTime.Value;

                attachWeapons.delay = enemyMove.initTime;
            }

            var enemyMove2 = enemy.GetComponent<EnemyMove2>();
            if (enemyMove2 != null)
            {
                if (_activePos.HasValue) enemy.transform.position = new Vector3(_activePos.Value.x, _activePos.Value.y);
                if (_gravity.HasValue) enemyMove2.gravity = _gravity.Value;
                if (_initSpeed.HasValue) enemyMove2.initSpeed = _initSpeed.Value;
                if (_k.HasValue) enemyMove2.k = _k.Value;
            }

            if (_life.HasValue) enemy.GetComponent<DestroyOnCollision>().life = _life.Value;
            if (weaponSlots.Count > 0) attachWeapons.weaponSlots = weaponSlots.ToArray();
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

        public EnemyBuilder initPos(float x, float y = 4.62f)
        {
            _initPos = new Vector2(x, y);
            return this;
        }

        public EnemyBuilder initTime(float initTime)
        {
            _initTime = initTime;
            return this;
        }

        public EnemyBuilder activePos(Vector2 pos)
        {
            _activePos = pos;
            return this;
        }

        public EnemyBuilder activePos(float x, float y)
        {
            _activePos = new Vector2(x, y);
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

        public EnemyBuilder exitPos(float x, float y = 4.62f)
        {
            _exitPos = new Vector2(x, y);
            return this;
        }

        public EnemyBuilder life(int life)
        {
            _life = life;
            return this;
        }

        public EnemyBuilder gravity(float gravity)
        {
            _gravity = gravity;
            return this;
        }

        public EnemyBuilder initSpeed(float x, float y)
        {
            _initSpeed = new Vector3(x, y, 0);
            return this;
        }

        public EnemyBuilder k(float k)
        {
            _k = k;
            return this;
        }

        public EnemyBuilder weapon(GameObject weapon, float interval = 0.1f, float delay = 0f, float rotateSpeed = 0,
            Vector3? rotate = null, Vector3? position = null, float? bulletSpeed = null)
        {
            var weaponSlot = new WeaponSlot();
            weaponSlot.weapon = weapon;
            weaponSlot.position = new Vector3();
            weaponSlot.rotate = rotate.GetValueOrDefault(new Vector3(0, 0, 0));
            weaponSlot.rotateSpeed = rotateSpeed.ToString();
            weaponSlot.delay = delay.ToString();
            weaponSlot.shootInterval = interval.ToString();
            weaponSlot.position = position.GetValueOrDefault(new Vector3(0, 0, 0));
            if (bulletSpeed != null) //                Debug.Log("bulletSpeed:" + bulletSpeed.Value);
                weaponSlot.bulletSpeed = bulletSpeed.Value.ToString();
            weaponSlots.Add(weaponSlot);
            return this;
        }

        public EnemyBuilder weaponSquare(GameObject weapon, float width, float height, float interval = 0.1f,
            float delay = 0f, float rotateSpeed = 0,
            Vector3? rotate = null, Vector3? position = null)
        {
            var xCount = 5;
            var yCount = 3;
            for (var xIdx = 0; xIdx < xCount; ++xIdx)
            for (var yIdx = 0; yIdx < yCount; ++yIdx)
            {
                if (xIdx > 0 && xIdx < xCount - 1 && yIdx > 0 && yIdx < yCount - 1) continue;

                var x = width / (xCount - 1) * xIdx - width / 2;
                var y = height / (yCount - 1) * yIdx - height / 2;
                var angle2 = Math.Atan2(x, y);
                this.weapon(weapon, interval, delay, rotateSpeed, new Vector3(0, 0, (float) (angle2 / Math.PI * 180)),
                    position, (float) Math.Sqrt(x * x + y * y));
            }

            return this;
        }

        public EnemyBuilder weaponSector(GameObject weapon, float interval = 2f, float rotateSpeed = 120f,
            float initRotate = 0f, float angle = 120f, int arcCount = 8, int radiusCount = 5)
        {
            for (var i = 0; i < arcCount; ++i)
                this.weapon(weapon, interval, default, rotateSpeed,
                    new Vector3(0, 0, initRotate + angle / (arcCount - 1) * i), default);

            for (var i = 0; i < radiusCount - 1; ++i)
            {
                var delay = interval * 0.5f / (radiusCount - 1) * (i + 1);
                this.weapon(weapon, interval, delay, rotateSpeed, new Vector3(0, 0, initRotate - rotateSpeed * delay),
                    default);
                this.weapon(weapon, interval, delay, rotateSpeed,
                    new Vector3(0, 0, initRotate + angle - rotateSpeed * delay), default);
            }

            return this;
        }

        public EnemyBuilder weaponLine(GameObject weapon, float interval = 2f, int count = 10, float fromX = -4,
            float fromY = -2, float toX = 4, float toY = -4, float rotateSpeed = 0f)
        {
            for (var i = 0; i < count; ++i)
            {
                var x = fromX + (toX - fromX) * ((float) i / (count - 1));
                var y = fromY + (toY - fromY) * ((float) i / (count - 1));
                var angle = Math.Atan2(x, y);
                this.weapon(weapon, interval, 0f, rotateSpeed, new Vector3(0, 0, (float) (angle / Math.PI * 180)),
                    default, (float) Math.Sqrt(x * x + y * y));
            }

            return this;
        }

        public EnemyBuilder weaponBursts(GameObject weapon)
        {
            var angleInterval = 30f;
            var bulletCount = 4;
            var burstsDelay = 0.16f;
            var bulletDelay = 0.12f;
            var burstCount = (int) (400 / angleInterval);
            var initRotate = -135f;
            for (var i = 0; i < burstCount; ++i)
            for (var j = 0; j < bulletCount; ++j)
                this.weapon(weapon, 100f, i * burstsDelay + j * bulletDelay, 0f,
                    new Vector3(0, 0, initRotate - i * angleInterval), default, 3.6f);

            return this;
        }

        public EnemyBuilder weaponStar(GameObject weapon, float interval = 1.6f, float rotateSpeed = 15)
        {
            var angleInterval = 360f / 5;
            var longDistance = 6f;
            var shortDistance = 3f;
            for (var i = 0; i < 5; ++i)
            {
                var angle1 = angleInterval * i;
                var angle2 = angle1 - angleInterval / 2;
                var angle3 = angle1 + angleInterval / 2;
                var x1 = (float) Math.Cos(angle1 / 180 * Math.PI) * longDistance;
                var y1 = (float) Math.Sin(angle1 / 180 * Math.PI) * longDistance;
                var x2 = (float) Math.Cos(angle2 / 180 * Math.PI) * shortDistance;
                var y2 = (float) Math.Sin(angle2 / 180 * Math.PI) * shortDistance;
                var x3 = (float) Math.Cos(angle3 / 180 * Math.PI) * shortDistance;
                var y3 = (float) Math.Sin(angle3 / 180 * Math.PI) * shortDistance;

                weaponLine(weapon, interval, 5, x1, y1, x2, y2, rotateSpeed);
                weaponLine(weapon, interval, 5, x1, y1, x3, y3, rotateSpeed);
            }

            return this;
        }
    }
}