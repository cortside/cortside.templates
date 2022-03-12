# cortside.templates

dotnet new --install cortside.templates

```
C:\Work\temp [nonprod] [master ≡ +10 ~0 -0 !]> dotnet new --install cortside.templates
The following template packages will be installed:
   cortside.templates

Success: Cortside.Templates::1.0.11 installed the following templates:
Template Name                     Short Name    Language  Tags
--------------------------------  ------------  --------  -------------------------------------------
ASP.NET Core Template             cortside-api  [C#]      ASP.NET Core/Core/MVC/Template/Web/Cortside
```

dotnet new webapi --name Company.Product --company Company --product Product

```
C:\Work\temp\foo [nonprod] [master ≡ +10 ~0 -0 !]> dotnet new cortside-api --name Company.Product --company Company --product Product
The template "ASP.NET Core Template" was created successfully.
```

The template will be create a new directory using the `--name` value for `Company.Product`.

