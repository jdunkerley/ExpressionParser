namespace JDunkerley.Parser
{
    /// <summary>
    /// Component Type
    /// </summary>
    public enum ComponentType
    {
        /// <summary>
        /// Is A Comma
        /// </summary>
        Comma,
        /// <summary>
        /// Is An Operator (Only Used During Construction Of Raw Components)
        /// </summary>
        Operator,
        /// <summary>
        /// Binary Operator - A Operator B
        /// </summary>
        Binary,
        /// <summary>
        /// Unary Operator - A Operator
        /// </summary>
        Unary,
        /// <summary>
        /// Backward Unary Operator - Operator A
        /// </summary>
        BackUnary,
        /// <summary>
        /// Function Including Parameters
        /// </summary>
        Function,
        /// <summary>
        /// Indexer Values (Produced After Brackets Merged, Converted To Function When MergeFunction)
        /// </summary>
        Indexer,
        /// <summary>
        /// Indexer Open (Produced As A RawComponent, Removed When MergeBrackets)
        /// </summary>
        IndexerOpen,
        /// <summary>
        /// Indexer Close (Produced As A RawComponent, Removed When MergeBrackets)
        /// </summary>
        IndexerClose,
        /// <summary>
        /// Bracket and Contents (Produced After Brackets Merged)
        /// </summary>
        Bracket,
        /// <summary>
        /// Bracket Open (Produced As A RawComponent, Removed When MergeBrackets)
        /// </summary>
        BracketOpen,
        /// <summary>
        /// Bracket Close (Produced As A RawComponent, Removed When MergeBrackets)
        /// </summary>
        BracketClose,
        /// <summary>
        /// Double Value
        /// </summary>
        Value,
        /// <summary>
        /// Variable (true and false are variables)
        /// </summary>
        Variable
    }
}