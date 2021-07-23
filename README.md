# Byndyusoft.ValidEnumConverter

Custom JsonConverter for enums with value validation.

| | | |
| ------- | ------------ | --------- |
| [**Byndyusoft.ValidEnumConverter**](https://www.nuget.org/packages/Byndyusoft.ValidEnumConverter/) | [![Nuget](https://img.shields.io/nuget/v/Byndyusoft.ValidEnumConverter.svg)](https://www.nuget.org/packages/Byndyusoft.ValidEnumConverter/) | [![Downloads](https://img.shields.io/nuget/dt/Byndyusoft.ValidEnumConverter.svg)](https://www.nuget.org/packages/Byndyusoft.ValidEnumConverter/) |


## Installing

```shell
dotnet add package Byndyusoft.ValidEnumConverter
```

## Usage

Byndyusoft.ValidEnumConverter
designed to properly validate numbers in a string. The System.Text.Json.Serialization library does not correctly validate numbers in a string like "Enum"
For example:
                   We have our own enum, let's call it UserRole:
```csharp
 public enum UserRole
    {
        Administrator = 0,
        Agent = 1
    }
```
And there is a Dto object:
```csharp
         public UserInfoDto {
                   public int Id {get; set; }
                   public UserRole Role {get; set; }
         }
```
When using this Dto object in our controller as an accepted class
On the front side, the following Json can be sent:
```json
{
  "Id": 1,
  "userRole": "589",
}
```
The non-existent value of our UserRole, it is logical to assume that a regular validator should throw an error with a code of 400, but this does not happen.
Therefore, we decided to make our own custom converter. Which validates the numbers in the string correctly

how to use the converter
We go to the Setup.cs of our project:
```csharp
 services.AddControllers ()
                    .AddJsonOptions (options => ...
```
and add following line
```csharp
options.JsonSerializerOptions.Converters.Add(new JsonValidEnumConverterFactory(namingPolicy, allowIntegerValues));
```
where namingPolicy and allowIntegerValues parameters are used in same way as in standard JsonStringEnumConverter.

After that, all Enum types are correctly converted for you.

# Contributing

To contribute, you will need to setup your local environment, see [prerequisites](#prerequisites). For the contribution and workflow guide, see [package development lifecycle](#package-development-lifecycle).

A detailed overview on how to contribute can be found in the [contributing guide](CONTRIBUTING.md).

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET 3.1


## Package development lifecycle

- Implement package logic

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)
