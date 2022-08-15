# cortside.templates

dotnet new --install cortside.templates

```
C:\Work\temp [nonprod] [master ≡ +10 ~0 -0 !]> dotnet new --install cortside.templates
The following template packages will be installed:
   cortside.templates

Success: Cortside.Templates::1.0.56 installed the following templates:
Template Name                       Short Name                 Language  Tags
----------------------------------  -------------------------  --------  -------------------------------------------
ASP.NET Core Template               cortside-api               [C#]      ASP.NET Core/Core/MVC/Template/Web/Cortside
ASP.NET Core Template deploy        cortside-api-deploy        [C#]      ASP.NET Core/Core/MVC/Template/Web/Cortside
ASP.NET Core Template editorconfig  cortside-api-editorconfig  [C#]      ASP.NET Core/Core/MVC/Template/Web/Cortside
ASP.NET Core Template Powershell    cortside-api-powershell    [C#]      ASP.NET Core/Core/MVC/Template/Web/Cortside
```

Create new project (created in new Company.Product directory):
```
dotnet new cortside-api --name Company.Product --company Company --product Product
```

Update .editorconfig in already created project: 
```
(from root of repo)
dotnet new cortside-api-editorconfig --force --output ./ --name Acme.ShoppingCart --company Acme --product ShoppingCart
```

Update powershell scripts in already created project: 
```
(from root of repo)
dotnet new cortside-api-powershell --force --output ./ --name Acme.ShoppingCart --company Acme --product ShoppingCart
```

```
C:\Work\temp\foo [nonprod] [master ≡ +10 ~0 -0 !]> dotnet new cortside-api --name Company.Product --company Company --product Product
The template "ASP.NET Core Template" was created successfully.
```

The template will be create a new directory using the `--name` value for `Company.Product`.

FYI: you can add --force to force overwriting existing files
