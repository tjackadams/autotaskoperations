Autotask Operations
===================

Status: [![Build status](https://ci.appveyor.com/api/projects/status/24ymfwe6949bx7qp/branch/master?svg=true)](https://ci.appveyor.com/project/ThomasAdams/autotaskoperations/branch/master) [![NuGet](https://img.shields.io/nuget/v/Autotask.svg)](https://www.nuget.org/packages/Autotask/)

Overview
--------

Provides a tool that generates the required xml for Autotask Api queries in a user friendly format.

Installation
------------

Install-Package Autotask

Usage
-----

basic query syntax
```
<Entity> <Property> <Operand> <Value>
```

Example:
All Contracts with an id greater than 0
```
const string query = "Contract id GreaterThan 0";
XDocument result = Query.Generate(query);
```



Links
-----

* Autotask: http://www.autotask.com/



Disclaimer
----------

This project is in no way affiliated with Autotask. 
