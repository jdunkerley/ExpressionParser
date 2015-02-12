using System.Collections.Generic;
using System.Linq;

namespace JDunkerley.Parser.Implementation
{
    /// <summary>
    /// Goal is to take
    /// {5,*,(,3,+,1,)} ==> {5,*,{3,+,1}}
    /// </summary>
    public static class MergeExpressions
    {
        private static void _AddCur(IComponent Cur, ref int ii, List<IComponent> output)
        {
            output.Add(Cur);
            ii++;
        }

        private static IComponent GetExpression(bool Indexer, List<IComponent> Comps, ref int ii)
        {
            bool foundComma = false;
            var tmp = new List<IComponent>();
            int jj = ii + 1;
            while (jj < Comps.Count)
            {
                var cur = Comps[jj];

                switch (cur.Type)
                {
                    case ComponentType.BracketOpen:
                        tmp.Add(GetExpression(false, Comps, ref jj));
                        continue;
                    case ComponentType.BracketClose:
                        if (Indexer) return null;
                        ii = jj + 1;
                        if (foundComma) _MergeCommaExpression(tmp);
                        return new Expression("(", tmp.ToArray());
                    case ComponentType.IndexerOpen:
                        tmp.Add(GetExpression(true, Comps, ref jj));
                        continue;
                    case ComponentType.IndexerClose:
                        if (!Indexer) return null;
                        ii = jj + 1;
                        if (foundComma) _MergeCommaExpression(tmp);
                        return new Expression("[", tmp.ToArray());
                    case ComponentType.Comma:
                        foundComma = true;
                        _MergeCommaExpression(tmp);
                        tmp.Add(cur);
                        jj++;
                        break;
                    default:
                        tmp.Add(cur);
                        jj++;
                        break;
                }
            }

            return null;
        }

        private static void _MergeCommaExpression(List<IComponent> tmp)
        {
            int kk = tmp.Count - 1;
            while (kk >= 0 && tmp[kk].Type != ComponentType.Comma)
                kk--;
            if (kk + 1 < tmp.Count - 1)
            {
                var nComp = new Expression("(", tmp.Skip(kk + 1).ToArray());
                tmp.RemoveRange(kk + 1, tmp.Count - kk - 1);
                tmp.Add(nComp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Comps"></param>
        /// <returns></returns>
        public static List<IComponent> MergeBrackets(this List<IComponent> Comps)
        {
            var output = new List<IComponent>();
            int ii = 0;
            while (ii < Comps.Count)
            {
                var cur = Comps[ii];

                switch (cur.Type)
                {
                    case ComponentType.BracketOpen:
                    case ComponentType.IndexerOpen:
                        cur = GetExpression(cur.Type == ComponentType.IndexerOpen, Comps, ref ii);
                        if (cur == null) return null;
                        output.Add(cur);
                        continue;
                }

                _AddCur(cur, ref ii, output);
            }
            return output;
            
        }

        /// <summary>
        /// Merges Indexers and Functions Together
        /// Indexers map to Function $INDEX(Var,....)
        /// </summary>
        /// <param name="Comps"></param>
        /// <returns></returns>
        public static List<IComponent> MergeFunctions(this List<IComponent> Comps)
        {
            var output = new List<IComponent>();
            int ii = 0;
            while (ii < Comps.Count)
            {
                var cur = Comps[ii];
                var pre = output.Count == 0 ? null : output[output.Count -1];

                if (pre != null && pre.Type == ComponentType.Variable && cur is IExpression && (cur.Type == ComponentType.Bracket || cur.Type == ComponentType.Indexer))
                {
                    var prms = ((IExpression)cur).Params.Where(prm => prm.Type != ComponentType.Comma).ToList();
                    if (cur.Type == ComponentType.Indexer)
                    {
                        prms.Insert(0, pre);
                        cur = new Expression("$INDEX", prms.ToArray());
                    }
                    else
                        cur = new Expression(pre.Text, prms.ToArray());
                    output.RemoveAt(output.Count - 1);
                }

                _AddCur(cur, ref ii, output);
            }

            return output;
        }
    }
}
