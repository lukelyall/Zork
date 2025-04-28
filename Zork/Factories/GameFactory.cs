namespace ZorkFactory;

public class GameFactory : IZorkFactory
{
    public IArea LoadArea()
    {
        IArea westOfHouse = new WestOfHouse();
        IArea forest = new Forest();
        IArea northOfHouse = new NorthOfHouse();
        IArea southOfHouse = new SouthOfHouse();
        IArea behindHouse = new BehindHouse();
        IArea forestPath = new ForestPath();
        IArea forestNorthEast = new ForestNorthEast();
        IArea clearing = new Clearing();
        IArea forestSouth = new ForestSouth();
        IArea clearingEast = new ClearingEast();
        IArea canyonView = new CanyonView();
        IArea rockyLedge = new RockyLedge();
        IArea canyonBottom = new CanyonBottom();
        IArea endOfRainbow = new EndOfRainbow();
        IArea kitchen = new Kitchen();
        IArea livingRoom = new LivingRoom();
        IArea attic = new Attic();
        IArea upATree = new UpATree();

        westOfHouse.Exits.Add("north", northOfHouse);
        westOfHouse.Exits.Add("west", forest);
        westOfHouse.Exits.Add("south", southOfHouse);

        northOfHouse.Exits.Add("west", westOfHouse);
        northOfHouse.Exits.Add("east", behindHouse);
        northOfHouse.Exits.Add("north", forestPath);

        southOfHouse.Exits.Add("west", westOfHouse);
        southOfHouse.Exits.Add("east", behindHouse);
        southOfHouse.Exits.Add("south", forestSouth);

        behindHouse.Exits.Add("north", northOfHouse);
        behindHouse.Exits.Add("south", southOfHouse);
        behindHouse.Exits.Add("east", clearingEast);
        behindHouse.Exits.Add("west", kitchen);

        forest.Exits.Add("west", forest);
        forest.Exits.Add("east", forestPath);
        forest.Exits.Add("north", clearing);

        forestPath.Exits.Add("south", northOfHouse);
        forestPath.Exits.Add("west", forest);
        forestPath.Exits.Add("east", forestNorthEast);
        forestPath.Exits.Add("north", clearing);
        forestPath.Exits.Add("up", upATree);

        upATree.Exits.Add("down", forestPath);

        forestNorthEast.Exits.Add("west", forestPath);
        forestNorthEast.Exits.Add("east", forestNorthEast);
        forestNorthEast.Exits.Add("south", clearingEast);

        clearing.Exits.Add("south", forestPath);
        clearing.Exits.Add("east", forestNorthEast);
        clearing.Exits.Add("west", forest);

        forestSouth.Exits.Add("west", forest);
        forestSouth.Exits.Add("north", clearingEast);

        clearingEast.Exits.Add("north", forestNorthEast);
        clearingEast.Exits.Add("south", forestSouth);
        clearingEast.Exits.Add("west", behindHouse);
        clearingEast.Exits.Add("east", canyonView);

        canyonView.Exits.Add("west", forestSouth);
        canyonView.Exits.Add("east", rockyLedge);
        canyonView.Exits.Add("down", rockyLedge);

        rockyLedge.Exits.Add("up", canyonView);
        rockyLedge.Exits.Add("down", canyonBottom);

        canyonBottom.Exits.Add("up", rockyLedge);
        canyonBottom.Exits.Add("north", endOfRainbow);

        endOfRainbow.Exits.Add("south", canyonBottom);

        kitchen.Exits.Add("east", behindHouse);
        kitchen.Exits.Add("west", livingRoom);
        kitchen.Exits.Add("up", attic);

        livingRoom.Exits.Add("east", kitchen);

        attic.Exits.Add("down", kitchen);

        return westOfHouse;
    }
}
