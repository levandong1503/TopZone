# Setup
- change `TopZoneDb` in appsettings.json
- Open Package Manage Console and Run Command Line `Update-Database -Project Infrastructure -StartupProject TopZone`
- Or run on Terminal `dotnet ef database update --project Infrastructure --startup-project TopZone`
- Check DB Migration: `Get-Migration`
- Add Migration `Add-Migration InitialMigraiton -Project Infrastructure -StartupProject TopZone -OutputDir Migrations`
- Remove Migration with Package Manager: `Remove-Migration` Start up project is `TopZone`, Default project is `Infastructure`
# Database
```mermaid
erDiagram
    ApplicationUser {
        GUID UserId PK
        string Email
        string PasswordHash
        string PhoneNumber
        string Address
        string UserType
        bool IsActive
        datetime CreatedAt
        datetime UpdatedAt
    }
    Customer {
        GUID Id PK
        GUID UserId FK
        string FullName
        string AvatarUrl
        datetime DateOfBirth
        string Gender
        int LoyaltyPoints
    }
    Staff {
        GUID Id PK
        GUID UserId FK
        GUID RoleId FK
        string FullName
        string EmployeeCode
        string Department
        datetime HiredAt
        bool IsActive
    }
    RefreshToken {
        GUID Id PK
        GUID ApplicationUserId FK
        string Token UK
        datetime ExpiresAt
        bool IsRevoked
        string DeviceInfo
        datetime CreatedAt
    }
    Role {
        GUID Id PK
        string Name UK
        string Description
        bool IsActive
    }
    Permission {
        GUID Id PK
        string Code UK
        string Name
        string Module
        string Action
    }
    RolePermission {
        GUID RoleId PK
        GUID PermissionId PK
    }
    Product {
        int Id PK
        string ProductName
        string Description
    }
    SubProduct {
        int Id PK
        int ProductId FK
    }
    ImagesProduct {
        int Id PK
        int ProductId FK
        int SubProductId FK
        string Path
        int OrderIndex
    }
    GroupSpecification {
        int Id PK
        string Name
    }
    Specification {
        int Id PK
        int GroupSpecificationId FK
        string Name
    }
    SpecificationSubProduct {
        int Id PK
        int SpecificationId FK
        int SubProductId FK
    }
    Type {
        int Id PK
        string Name
        int ParentTypeId FK
    }
    TypeProduct {
        int Id PK
        int ProductId FK
        int TypeId FK
    }

    ApplicationUser ||--o| Customer : "is a"
    ApplicationUser ||--o| Staff : "is a"
    ApplicationUser ||--o{ RefreshToken : "issues"
    Role ||--o{ Staff : "assigned_to"
    Role ||--o{ RolePermission : "has"
    Permission ||--o{ RolePermission : "in"

    Product ||--o{ SubProduct : "contains"
    Product ||--o{ TypeProduct : "classified_as"
    SubProduct ||--o{ ImagesProduct : "has"
    SubProduct ||--o{ SpecificationSubProduct : "has"
    GroupSpecification ||--o{ Specification : "groups"
    Specification ||--o{ SpecificationSubProduct : "maps_to"
    Type |o--o{ Type : "has_children"
    Type ||--o{ TypeProduct : "categorizes"

```

