using RoyalCode.SmartSelector.Generators.Extensions;
using RoyalCode.SmartSelector.Generators.Models.Descriptors;
using System.Text;

namespace RoyalCode.SmartSelector.Generators.Models.Generators.Commands;

internal class SelectLambdaGenerator : ValueNode
{
    private readonly MatchSelection match;

    public SelectLambdaGenerator(MatchSelection match)
    {
        this.match = match;
    }

    public override string GetValue(int ident)
    {
        var sb = new StringBuilder();
        var param = 'a';

        return GetValue(ident, sb, param);
    }

    private string GetValue(int ident, StringBuilder sb, char param)
    {
        // ......... a => new T
        // { 
        sb.Append(param).Append(" => new ").AppendLine(match.OriginType.Name)
            .Ident(ident).Append('{');

        GeneratePropertyCode(ident + 1, sb, param, match.PropertyMatches);

        sb.AppendLine().Ident(ident).Append('}');

        return sb.ToString();
    }

    private static void GeneratePropertyCode(int ident, StringBuilder sb, char param, IReadOnlyList<PropertyMatch> properties)
    {
        foreach (var propMatch in properties)
        {
            sb.AppendLine();

            //     PropertyName = 
            sb.Ident(ident).Append(propMatch.Origin.Name).Append(" = ");

            var assignDescriptor = propMatch.AssignDescriptor!;
            var assignGenerator = GetAssignGenerator(assignDescriptor);

            var assign = new AssignProperties(propMatch.Origin, propMatch.Target!, assignDescriptor.InnerSelection);
            assignGenerator(sb, ident, param, assign);

            // check ToList
            if (assignDescriptor.RequireToList)
            {
                sb.Append('.').Append("ToList()");
            }

            sb.Append(',');
        }
        sb.Length--;
    }

    private static AssignGenerator GetAssignGenerator(AssignDescriptor assignDescriptor)
    {
        return assignDescriptor.AssignType switch
        {
            AssignType.Direct => AssignDirect,
            AssignType.SimpleCast => AssignCast,
            AssignType.NullableTernary => AssignNullableTernary,
            AssignType.NullableTernaryCast => AssignNullableTernaryCast,
            AssignType.NewInstance => AssignNewInstance,
            AssignType.Select => AssignSelect,
            _ => AssignDirect
        };
    }

    private static void AssignDirect(StringBuilder sb, int ident, char param, AssignProperties assign)
    {
        sb.Append(param).Append('.').AppendPropertyPath(assign.Target);
    }

    private static void AssignCast(StringBuilder sb, int ident, char param, AssignProperties assign)
    {
        sb.Append('(').Append(assign.Origin.Type.Name).Append(')');
        AssignDirect(sb, ident, param, assign);
    }

    private static void AssignNullableTernary(StringBuilder sb, int ident, char param, AssignProperties assign)
    {
        sb.Append(param).Append('.').AppendPropertyPath(assign.Target).Append(".HasValue ? ");
        sb.Append(param).Append('.').AppendPropertyPath(assign.Target).Append(".Value : default");
    }

    private static void AssignNullableTernaryCast(StringBuilder sb, int ident, char param, AssignProperties assign)
    {
        sb.Append(param).Append('.').AppendPropertyPath(assign.Target).Append(".HasValue ? ");
        sb.Append('(').Append(assign.Origin.Type.Name).Append(')');
        sb.Append(param).Append('.').AppendPropertyPath(assign.Target).Append(".Value : default");
    }

    private static void AssignNewInstance(StringBuilder sb, int ident, char param, AssignProperties assign)
    {
        var inner = assign.InnerSelection;
        if (inner is null)
            throw new ArgumentException("Inner selection is null.", nameof(inner));

        inner.AddParentProperty(assign.Target);

        sb.Append("new ").AppendLine(assign.Origin.Type.Name)
            .Ident(ident).Append("{");
        
        GeneratePropertyCode(ident + 1, sb, param, inner.PropertyMatches);

        sb.AppendLine().Ident(ident).Append('}');
    }

    private static void AssignSelect(StringBuilder sb, int ident, char param, AssignProperties assign)
    {
        var inner = assign.InnerSelection;
        if (inner is null)
            throw new ArgumentException("Inner selection is null.", nameof(inner));

        sb.Append(param).Append('.').AppendPropertyPath(assign.Target).Append(".Select(");

        var nextParam = (char)(param + 1);

        sb.Append(nextParam).Append(" => new ").AppendLine(assign.Origin.Type.GenericType)
            .Ident(ident).Append('{');

        GeneratePropertyCode(ident + 1, sb, nextParam, inner.PropertyMatches);

        sb.AppendLine().Ident(ident).Append("})");
    }

    private delegate void AssignGenerator(StringBuilder sb, int ident, char param, AssignProperties assign);

    private readonly ref struct AssignProperties(PropertyDescriptor origin, PropertySelection target, MatchSelection? inner)
    {
        /// <summary>
        /// The origin property type descriptor. (DTO property)
        /// </summary>
        public PropertyDescriptor Origin { get; } = origin;

        /// <summary>
        /// The target property selection. (Entity property)
        /// </summary>
        public PropertySelection Target { get; } = target;

        /// <summary>
        /// The inner selection of the target property. (Entity property)
        /// </summary>
        public MatchSelection? InnerSelection { get; } = inner;
    }
}
