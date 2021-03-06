﻿using System.Collections.Generic;
using game.animalKingdom.installer;
using game.animalKingdom.model.remote;
using game.animalKingdom.model.scene;
using game.core;
using UnityEngine;

namespace game.animalKingdom.view
{
    public abstract class AnimalState : StateBehaviour
    {
        protected readonly AnimalView View;

        protected AnimalState(AnimalView view)
        {
            this.View = view;
        }
    }

    public class AnimalStateIdle : AnimalState
    {
        public AnimalStateIdle(AnimalView view) : base(view)
        {
        }
    }
    
    public class AnimalStateFollow : AnimalState
    {
        public AnimalStateFollow(AnimalView view) : base(view)
        {
        }
    }

    public class AnimalStatePatrol : AnimalState
    {
        private List<Vector3> _positions = new List<Vector3>();
        private int _positionsCount;
        private int _currTargetIndex;
        
        public AnimalStatePatrol(AnimalView view) : base(view)
        {
            _positionsCount = 5;

            for (int i = 0; i < _positionsCount; i++)
            {
                _positions.Add(Utils.RandomFarmLocation);
            }
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            
            MoveToNextTarget();
        }

        private void MoveToNextTarget()
        {
            View.Move(_positions[_currTargetIndex = (++_currTargetIndex % _positionsCount)]);
        }
        
        public override void Tick()
        {
            base.Tick();
            
            float distanceToTarget = Vector3.Distance( View.transform.position, 
                _positions[_currTargetIndex]);
            
            if(distanceToTarget < View.Agent.stoppingDistance)
            {
                MoveToNextTarget();
            }
        }
    }
}