using System;
using System.Collections.Generic;
using BTreeUtility;
using BTreeUtility.Nodes;
using CubeMerge.Runtime.Scripts.Battle;
using UnityEngine;

namespace CubeMerge.Runtime.Scripts
{ 
    public class LoadBattleArea: ActionBase<ProjectContext>
    {
        private bool isLoaded;
        
        protected override void Execute(ProjectContext context)
        {
            if (isLoaded)
                return;

            var viewModel = new BattleAreaViewModel();
            var view = GetSceneView<BattleAreaView>(context);
            view.Init(viewModel);
            view.gameObject.SetActive(true);
            isLoaded = true;
        }

        private static T GetSceneView<T>(ProjectContext context) where T : MonoBehaviour
        {
            foreach (var view in context.SceneViews)
            {
                if (view is not T needView)
                    continue;

                return needView;
            }
            return null;
        }
    }
    
    public class ShowPath: ActionBase<ProjectContext>
    {
        protected override void Execute(ProjectContext context)
        {
            
        }
    }

    public enum NodeId
    { 
        RootSelector, LoadBattleArea, ShowPath
    }

    public class MainNodeMap : INodeMap
    {
        public List<int> Nodes => new List<int>()
        {
           (int) NodeId.RootSelector,
           (int) NodeId.LoadBattleArea,
           (int) NodeId.ShowPath, 
        };

        public Dictionary<int, int> Connections => new Dictionary<int, int>
        {
            {(int) NodeId.RootSelector, (int) NodeId.LoadBattleArea}, 
            {(int) NodeId.LoadBattleArea, (int) NodeId.ShowPath}
        };
    }

    public class MainNodeFactory : NodeFactoryBase
    {
        public MainNodeFactory(INodeMap nodeMap) : base(nodeMap)
        {
        }

        protected override INode CreateNode(int nodeId)
        {
            switch (nodeId)
            {
                case (int) NodeId.RootSelector : return new FirstScoreSelector();
                case (int) NodeId.LoadBattleArea : return new LoadBattleArea();
                case (int) NodeId.ShowPath : return new ShowPath();
                default: throw new Exception($"There is no node with {nodeId} id!");
            }
        }
    }

    public class ProjectContext : IBTContext
    {
        public float DeltaTime { get; set; }
        public MonoBehaviour[] SceneViews { get; set; }
    }

    public class MainClient : BTClient
    {
        public MainClient(ISelector rootSelector, IBTContext context) : base(rootSelector, context)
        {
        }
    }
    
    public class Main
    {
        private ProjectContext _context;
        private Dictionary<int, INode> _nodes;
        private BTClient _mainClient;

        public Main()
        {
            var map = new MainNodeMap();
            var factory = new MainNodeFactory(map);
            _nodes = factory.CreateNodes();
        }

        public void Init(ProjectContext context)
        {
            _context = context;
            _mainClient = new BTClient(_nodes.GetRootSelector(), context);
        }

        public void Execute()
        {
            _mainClient.Execute();
        }
    }
}