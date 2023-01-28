using System;

namespace BTreeUtility.Nodes
{
    public class CompositeQualifierBase<T> : IQualifier where T: class, IBTContext
    {
        protected enum Operator { Or, And }
        
        private IQualifier[] _qualifiers;
        private Operator _operator;
        
        public INode Next { get; set; }

        protected void SetQualifiers(Operator op, params IQualifier[] qualifiers)
        {
            if (qualifiers == null || qualifiers.Length <= 0) 
                throw new ArgumentException("Qualifiers is missing!");

            _operator = op;
            _qualifiers = qualifiers;
            Next = new DefaultNode();
        }
    
        public float Score(IBTContext context)
        {
            var result = 0f;
            for (var i = 0; i < _qualifiers.Length; i++)
            {
                var newScore = _qualifiers[i].Score(context);
                if (newScore <= 0 && _operator == Operator.And)
                {
                    result = 0;
                    break;
                }
                result += _qualifiers[i].Score(context);
            }
            return result;
        }
    }
}