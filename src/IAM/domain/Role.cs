namespace iam.domain;

public class Role
{
    public string Name { get; }

    public Role(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.");

        var validRoles = new[] { "NormalUser", "PremiumUser", "ITSupport", "BusinessOperator", "Admin" };
        if (!validRoles.Contains(name))
            throw new ArgumentException("Invalid role name.");

        Name = name;
    }

    public override string ToString() => Name;
}