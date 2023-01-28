using BTreeUtility;
using BTreeUtility.Nodes;
using UnityEngine;

namespace Sample
{
    public class SampleClientComponent : BtClientComponent
    {
        public static void CreateSampleClient(ISelector rootSelector, IBTContext context)
        {
            var go = new GameObject($"{nameof(SampleClientComponent)}");
            var client = go.AddComponent<SampleClientComponent>();
            client.Init(rootSelector, context);
        }
    }
}