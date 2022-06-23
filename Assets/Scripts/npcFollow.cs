using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcFollow : Character2D, INpc{

    private Character target;
    private readonly List<Position> queue = new List<Position>();
    
    public NpcFollowCharacter(Character2D target)
    {
        this.target = target;
        this.direction = target.direction;
        queue.Add(target.GetPos());
    }
    
    public override void Move()
    {
        
        if (queue.Count == 0 || queue[queue.Count - 1] != target.GetPos())
        {
            queue.Add(target.GetPos());
        }
        
        var oldPosition = this.GetPos();
        
        this.SetPos(queue[0].GetPos);
        
        if(this.Collision(target))
        {
            this.SetPos(oldPosition);
        }
        else{
            queue.RemoveAt(0);
        }
    }
}