using AdventOfCode2024.Day06.Models;
using Xunit.Abstractions;

namespace AdventOfCode2024.Day06;

public class GuardGallivant(ITestOutputHelper output, string[] data) : ChallengeBase<int>
{
    protected override int Part1()
    {
        var guard = CreateGuard();

        var hasGuardLeftMappedArea = false;

        do
        {
            var nextPosition = guard.GetNextPotentialPosition();
            var nextPositionType = data.GetPositionType(nextPosition);

            if (nextPositionType == PositionType.Obstacle)
            {
                guard.ChangeDirection();
            } 
            else if (nextPositionType == PositionType.FreeSpace)
            {
                guard.Move();
            }
            else if (nextPositionType == PositionType.OutsideOfMap)
            {
                hasGuardLeftMappedArea = true;
            }

        } while (hasGuardLeftMappedArea == false);

        return guard.GetNumberOfUniquePositionsVisited();
    }
    
    protected override int Part2()
    {
        var guard = CreateGuard();

        var hasGuardLeftMappedArea = false;
        HashSet<string> positionOfObstructions = new();

        do
        {
            var nextPosition = guard.GetNextPotentialPosition();
            var nextPositionType = data.GetPositionType(nextPosition);
            
            if (nextPositionType == PositionType.Obstacle)
            {
                guard.ChangeDirection();
            } 
            else if (nextPositionType == PositionType.FreeSpace)
            {
                if (WillEnterLoopIfObstructionIsPlacedIntoPosition(guard, nextPosition))
                {
                    positionOfObstructions.Add(nextPosition.ToString());
                }
                
                guard.Move();
            }
            else if (nextPositionType == PositionType.OutsideOfMap)
            {
                hasGuardLeftMappedArea = true;
            }

        } while (hasGuardLeftMappedArea == false);

        return positionOfObstructions.Count;
    }
    
    private Guard CreateGuard()
    {
        const string startingDirection = "^";

        for (var y = 0; y < data.Length; y++)
        {
            var xPositionOfGuard = data[y].IndexOf(startingDirection);
            if (xPositionOfGuard > -1)
            {
                return new Guard(xPositionOfGuard, y, GuardDirection.Up);
            }
        }
        
        throw new Exception("Failed to locate the guard.");
    }
    
    private bool WillEnterLoopIfObstructionIsPlacedIntoPosition(Guard guard, Position obstaclePosition)
    {
        var copyOfGuard = guard.Copy();
        var copyOfGrid = (string[])data.Clone();

        if (obstaclePosition.IsTheSameAs(guard.StartingPosition)) return false;
        
        copyOfGrid.InsertObstacle(obstaclePosition);
        copyOfGuard.ChangeDirection();

        do
        {
            var nextPosition = copyOfGuard.GetNextPotentialPosition();
            var nextPositionType = copyOfGrid.GetPositionType(nextPosition);

            if (copyOfGuard.HasBeenOnPathBefore(nextPosition))
            {
                return true;
            }

            switch (nextPositionType)
            {
                case PositionType.FreeSpace:
                    copyOfGuard.Move();
                    break;
                default:
                    return false;
            }
            
        } while (true);
    }
}
