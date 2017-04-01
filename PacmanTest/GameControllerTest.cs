using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using PacManNamespace.Models;
using PacManNamespace;

[TestClass]
public class GameControllerTest
    {

    //Tests the Init() method to ensure all objecttypes are correct
    [TestMethod]
    public void Init_CharacterTypes_Success()
    {
        GameController ctrl = new GameController();
        ctrl.Init();
        Map map = ctrl.Maps[0];
        Assert.IsTrue(ctrl.Pacman == map.Characters[ObjectType.Pacman]);
        Assert.IsTrue(ctrl.Blinky == map.Characters[ObjectType.Blinky]);
        Assert.IsTrue(ctrl.Pinky == map.Characters[ObjectType.Pinky]);
        Assert.IsTrue(ctrl.Inky == map.Characters[ObjectType.Inky]);
        Assert.IsTrue(ctrl.Clyde == map.Characters[ObjectType.Clyde]);
    }
        
}
