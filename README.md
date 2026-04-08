# Setup
- change `TopZoneDb` in appsettings.json
- Open Package Manage Console and Run Command Line `Update-Database -Project Infrastructure -StartupProject TopZone`
- Or run on Terminal `dotnet ef database update --project Infrastructure --startup-project TopZone`
- Check DB

# Database
```mermaid
erDiagram
Product{
    guid IdProduct PK
    nvarchar ProductName
    nvarchar Description

}

SubProduct{
    guid IdSubProduct PK
    guid IdProduct FK
}

TypeProduct{
    guid IdSubProduct FK
    guid IdType FK
}

Specification{
    guid Id PK
    nvarchar Name
    guid GroupId FK
}

SpecificationSubProduct{
    guid IdSubProduct FK
    guid IdSpecification FK
}

GroupSpecification{
    guid Id
    nvarchar Name
    guid GroupId
}

Type{
    guid Id PK
    nvarchar Name 
    guid MainType FK
}


Users{
    uuid Id PK
    string Email
    string PasswordHash
    string PhoneNumber
    string UserType
    bool IsActive
    datetime CreateAt
    datetime UpdateAt
}

Customers{
    uuid Id PK
    uuid UserId FK
    string FullName
    string AvatarUrl
    date DateOfBirth
    string Gender
    int LoyaltyPoints
}

Staffs{
    uuid Id PK
    uuid UserId FK
    string FullName
    string EmployeeCode
    string Department
    uuid RoleId
    datetime HiredAt
    bool IsActive
}

RefreshTokens{
    uuid Id PK
    uuid UserId FK
    string Token UK
    datetime ExpiresAt
    bool IsRevoked
    string DeviceInfo
    datetime CreateAt
}

Roles{
    uuid Id PK
    string Name UK
    string Description
    bool IsActive
}

Permissions{
    uuid Id PK
    string Code UK
    string Name
    string Module
    string Action
}

RolePermissions {
    uuid RoleId PK
    uuid PermissionId FK
}


Product ||--|{ SubProduct : have
SubProduct || -- |{ TypeProduct : have
SubProduct || -- |{ SpecificationSubProduct : have
Specification o| -- |{ SpecificationSubProduct : own
TypeProduct || -- |{ Type : own
Product || -- || Type : own
Type o| -- || Type : own
GroupSpecification || -- |{ Specification : have

Users ||-- |{ RefreshTokens: own
Users ||-- |{ Customers: is-a
Users ||-- |{ Staffs: is-a
Roles ||-- |{ Users: has
Roles || -- |{ RolePermissions: grants
Permissions || -- |{ RolePermissions: belongs-to

```

