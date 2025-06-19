namespace RoyalCode.SmartSelector.Generators.Models.Descriptors;

internal class AssignDescriptor : IEquatable<AssignDescriptor>
{
    public AssignType AssignType { get; set; }

    public bool RequireToList { get; set; }

    public MatchSelection? InnerSelection { get; set; }

    public bool Equals(AssignDescriptor other)
    {
        if (other is null) 
            return false;

        if (ReferenceEquals(this, other)) 
            return true;

        return AssignType == other.AssignType &&
            RequireToList == other.RequireToList &&
            Equals(InnerSelection, other.InnerSelection);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not AssignDescriptor other)
            return false;

        return Equals(other);
    }

    public override int GetHashCode()
    {
        int hashCode = -2066519001;
        hashCode = hashCode * -1521134295 + AssignType.GetHashCode();
        hashCode = hashCode * -1521134295 + RequireToList.GetHashCode();
        hashCode = hashCode * -1521134295 + (InnerSelection?.GetHashCode() ?? 0);
        return hashCode;
    }
}
