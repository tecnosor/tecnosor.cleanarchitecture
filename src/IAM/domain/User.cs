using tecnosor.cleanarchitecture.common.domain;

namespace iam.domain;

public class User : Aggregate
{
    // Información General
    public UserName UserName { get; }
    public Email Email { get; }
    public PhoneNumber PhoneNumber { get; }
    public bool EmailConfirmed { get; }
    public bool PhoneNumberConfirmed { get; }
    public IList<Role> Roles { get; private set; } = new List<Role>();

    // Agregados
    public Security Security { get; }
    public LockManagement LockManagement { get; }
    public ExternalAuth ExternalAuth { get; }

    public User(UserName userName,
                Email email,
                PhoneNumber phoneNumber,
                bool emailConfirmed,
                bool phoneNumberConfirmed,
                Security security,
                LockManagement lockManagement,
                ExternalAuth externalAuth,
                IList<Role> roles)
    {
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        EmailConfirmed = emailConfirmed;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        Security = security;
        LockManagement = lockManagement;
        ExternalAuth = externalAuth;
    }

    public void AddRole(Role role)
    {
        if (!Roles.Contains(role))
        {
            Roles.Add(role);
        }
    }

    public void RemoveRole(Role role)
    {
        if (!Roles.Contains(role))
        {
            Roles.Add(role);
        }
    }
}